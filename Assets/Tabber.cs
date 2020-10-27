using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tabber : MonoBehaviour
{
    [SerializeField] private EventSystem _eSystem;

    [SerializeField] private TabSection[] _sections;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
            foreach(TabSection t in _sections)
                //if (t._elements.Select(o => o.gameObject).Contains(_eSystem.currentSelectedGameObject))
                    for (int i = 0; i < t._elements.Count(); i++)
                        if(t._elements[i].gameObject == _eSystem.currentSelectedGameObject)
                            StartCoroutine(SelectNewSelectable(t._elements[i + 1 == t._elements.Count() ? 0 : i + 1].gameObject));
    }

    private IEnumerator SelectNewSelectable(GameObject newSelect)
    {
        _eSystem.SetSelectedGameObject(null);
        yield return null;
        _eSystem.SetSelectedGameObject(newSelect);
    }

    [Serializable]
    public struct TabSection
    {
        public string _name;
        public List<Selectable> _elements;
    }
}