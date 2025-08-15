using UnityEngine;
using UnityEngine.InputSystem;

public class SlingshotArea : MonoBehaviour
{
    [SerializeField] private LayerMask _slingshotareamask;
    public bool IsWithinSlingShotArea()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(InputManager.MousePosition);
        if (Physics2D.OverlapPoint(worldPosition, _slingshotareamask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
