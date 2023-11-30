using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameSceneManager : MonoBehaviour
{
    public GameObject normalObj;
    void Start()
    {
        string loadedDeviceName = XRSettings.loadedDeviceName;

        if (loadedDeviceName.Contains("Oculus"))
        {
            // Oculus VR device detected, hide the button
            normalObj.SetActive(false);
        }
        else
        {
            // Oculus VR device not detected, show the button
            normalObj.SetActive(true);
        }
    }
}
