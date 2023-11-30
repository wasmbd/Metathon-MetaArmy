using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusHideVR : MonoBehaviour
{
    public List<GameObject> buttonsToHide = new List<GameObject>();

    void Start()
    {
        string loadedDeviceName = XRSettings.loadedDeviceName;

        if (loadedDeviceName.Contains("Oculus") || Application.platform == RuntimePlatform.Android)
        {
            // Oculus VR device detected or Android platform, show the buttons
            foreach (GameObject button in buttonsToHide)
            {
                button.SetActive(true);
            }
        }
        else
        {
            // Neither Oculus VR device nor Android platform, hide the buttons
            foreach (GameObject button in buttonsToHide)
            {
                button.SetActive(false);
            }
        }
    }
}