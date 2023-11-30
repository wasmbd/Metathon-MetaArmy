using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusKeyboard : MonoBehaviour
{
    public GameObject buttonToHide;
   public GameObject buttonToHide2;
   public GameObject buttonToHide3;
  
       void Start()
    {
        string loadedDeviceName = XRSettings.loadedDeviceName;
         Debug.Log("Loaded Device: " + loadedDeviceName);
        

        if (loadedDeviceName.Contains("Oculus") || Application.platform == RuntimePlatform.Android)
        {
            // Oculus VR device detected or Android platform, show the button
            buttonToHide.SetActive(true);
            buttonToHide2.SetActive(false);
            buttonToHide3.SetActive(false);
        }
        else
        {
            // Neither Oculus VR device nor Android platform, hide the button
            buttonToHide.SetActive(false);
          
            buttonToHide2.SetActive(true);
            buttonToHide3.SetActive(true);
                    }
    }
}
