using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int maxnumberofshots = 3;
    [SerializeField] private float _secondstowaitbeforeDeathcheck = 3f;
    [SerializeField] private GameObject _restartScreenObjet;
    [SerializeField] private SlingshotHandler _slingshothandler;
    private int _usednumberofshots;
    private Iconhandler _iconhandler;
    private List<Baddie> _baddies = new List<Baddie>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _iconhandler = FindObjectOfType<Iconhandler>();
        Baddie[] baddies = FindObjectsOfType<Baddie>();
        for (int i = 0; i < baddies.Length; i++)
        {
            _baddies.Add(baddies[i]);
        }
    }
    public void useshot()
    {
        _usednumberofshots++;
        _iconhandler.useshot(_usednumberofshots);
        Checkforlastshot();
    }
    public bool HasEnoughShot()
    {
        if (_usednumberofshots < maxnumberofshots)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Checkforlastshot()
    {
        if (_usednumberofshots == maxnumberofshots)
        {
            StartCoroutine(checkafterwaittime());

        }
    }
    private IEnumerator checkafterwaittime()
    {
        yield return new WaitForSeconds(_secondstowaitbeforeDeathcheck);

        if (_baddies.Count == 0)
        {
            wingame();
        }
        else
        {
            Restartgame();
        }
    }


    public void RemoveBadddie(Baddie baddie)
    {
        _baddies.Remove(baddie);
        CheckforAllDeadbaddies();
    }
    private void CheckforAllDeadbaddies()
    {
        if (_baddies.Count == 0)
        {
            wingame();
        }
    }
    #region win/lose

    private void wingame()
    {
        _restartScreenObjet.SetActive(true);
        _slingshothandler.enabled = false;
    }

    public void Restartgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        
    }




    #endregion
}
