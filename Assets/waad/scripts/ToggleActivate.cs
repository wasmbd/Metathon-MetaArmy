using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleActivate : MonoBehaviour
{
    public Toggle toggle;
    public GameObject objectToActivate;

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
       if (isOn)
        {
            Invoke("activateObject()", 5f);
        }
        
    }
    private void activateObject()
    {
        objectToActivate.SetActive(true);
    }
}
