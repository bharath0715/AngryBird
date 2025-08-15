using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Iconhandler : MonoBehaviour
{
    [SerializeField] private Image[] _icons;
    [SerializeField] private Color _usedcolor;
    public void useshot(int shotnumber)
    {
     for (int i = 0; i < _icons.Length; i++)
     {
            if (shotnumber == i + 1)
            {
                _icons[i].color = _usedcolor;
                return;
        }
     }
    }
}

