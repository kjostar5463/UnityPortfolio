using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject menuUi;
    public GameObject optionUi;

    private KeyCode _escapeKey = KeyCode.Escape;

    private LinkedList<GameObject> _activePopupLList;

    private void Awake()
    {
        _activePopupLList = new LinkedList<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_escapeKey))
        {
            if (_activePopupLList.Count > 0)
            {
                ClosePopup(_activePopupLList.First.Value);
            }
        }
    }

    public void OpenPopup(GameObject popup)
    {
        _activePopupLList.AddFirst(popup);
        popup.gameObject.SetActive(true);
    }

    public void ClosePopup(GameObject popup)
    {
        _activePopupLList.Remove(popup);
        popup.gameObject.SetActive(false);
    }
}
