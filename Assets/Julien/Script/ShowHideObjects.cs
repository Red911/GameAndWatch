using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideObjects : MonoBehaviour
{

    [SerializeField] private List<GameObject> _objects;

    public void ShowObject(int objectToShow)
    {
        if (objectToShow > 0 && objectToShow <= _objects.Count)
            _objects[objectToShow-1].SetActive(true);
    }

    public void HideObject(int objectToHide)
    {
        _objects[objectToHide].SetActive(false);
    }
}
