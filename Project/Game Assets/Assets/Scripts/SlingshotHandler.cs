using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class SlingshotHandler : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;
    [Header("Transform References")]
    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private Transform _idlePosition;
    [Header("Slingshot stat")]
    [SerializeField] private float _maxDistance = 3.5f;
    [SerializeField] private float _shotforce = 5f;
    [SerializeField] private float _timebetweenbirdrespawn = 2f;
    [Header("Scripts")]
    [SerializeField] private SlingshotArea _slingshotarea;
    [Header("Bird")]
    [SerializeField] private Angrybird _angrybirdprefab;
    [SerializeField] private float _birdpositionoffset = 2f;
    private Vector2 _slingshotlinesposition;
    private Vector2 _direction;
    private Vector2 _directionnormalized;

    private bool _clickedwithinarea;
    private bool _birdonslingshot;
    private Angrybird _spawnedbird;
    private void Awake()
    {
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;
        Spawnbird();
    }
    private void Update()
    {
        if (InputManager.WasLeftMousebuttonpressed && _slingshotarea.IsWithinSlingShotArea())
        {
            _clickedwithinarea = true;
        }

        if (InputManager.IsLeftmouspressed && _clickedwithinarea && _birdonslingshot)
        {
            DrawSlingShot();
            PositionandRotateAngrybird();

        }
        if (InputManager.WasLeftMousebuttonreleased && _birdonslingshot && _clickedwithinarea)
        {
            if(GameManager.instance.HasEnoughShot())
            {
                _clickedwithinarea = false;
                _spawnedbird.LaunchBird(_direction, _shotforce);
                GameManager.instance.useshot();
                _birdonslingshot = false;
                SetLines(_centerPosition.position);
                
                if (GameManager.instance.HasEnoughShot())
                {
                    StartCoroutine(Spawnbirdaftertime());
                }
            }
        }

    }
    #region SlingShotMethods

    private void DrawSlingShot()
    {

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(InputManager.MousePosition);
        _slingshotlinesposition = _centerPosition.position + Vector3.ClampMagnitude(touchPosition - _centerPosition.position, _maxDistance);
        SetLines(_slingshotlinesposition);
        _direction = (Vector2)_centerPosition.position - _slingshotlinesposition;
        _directionnormalized = _direction.normalized;

    }
    private void SetLines(Vector2 position)
    {
        if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
        {
            _leftLineRenderer.enabled = true;
            _rightLineRenderer.enabled = true;
        }
        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _leftStartPosition.position);
        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);

    }
    #endregion
    #region Angrybirdmethods

    private void Spawnbird()
    {
        SetLines(_idlePosition.position);
        Vector2 dir = (_centerPosition.position - _idlePosition.position).normalized;
        Vector2 spawnPosition = (Vector2)_idlePosition.position + dir * _birdpositionoffset;

        _spawnedbird = Instantiate(_angrybirdprefab, spawnPosition, Quaternion.identity);
        _spawnedbird.transform.right = dir;
        _birdonslingshot = true;
    }
    private void PositionandRotateAngrybird()
    {
        _spawnedbird.transform.position = _slingshotlinesposition + _directionnormalized * _birdpositionoffset;
        _spawnedbird.transform.right = _directionnormalized;
    }
    private IEnumerator Spawnbirdaftertime()
    {
        yield return new WaitForSeconds(_timebetweenbirdrespawn);
        Spawnbird();

    }


    #endregion
}
