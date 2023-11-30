using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSelect : MonoBehaviour
{
    public RectTransform selectedObj;
    void Start()
    {
        selectedObj.gameObject.SetActive(false);
    }
    public void SelectObj(RectTransform obj)
    {
        selectedObj.anchoredPosition = obj.anchoredPosition;
        selectedObj.gameObject.SetActive(true);
    }
}
