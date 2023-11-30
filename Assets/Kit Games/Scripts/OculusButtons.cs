using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusButtons : MonoBehaviour
{

    public GameObject buttonToHide;
    public GameObject buttonToHide2;
    public GameObject buttonToHide3;
    public GameObject buttonToHide4;
    public GameObject buttonToHide5;
    public GameObject buttonToHide6;
    // Start is called before the first frame update

     public GameObject buttonToShow;
    public GameObject buttonToShow2;
    public GameObject buttonToShow3;
    public GameObject buttonToShow4;
    public GameObject buttonToShow5;
    public GameObject buttonToShow6;
    void Start()
    {
        string loadedDeviceName = XRSettings.loadedDeviceName;
         Debug.Log("Loaded Device: " + loadedDeviceName);
        

        if (loadedDeviceName.Contains("Oculus") || Application.platform == RuntimePlatform.Android)
        {
            // Oculus VR device detected or Android platform, show the button
            buttonToHide.SetActive(true);
             buttonToHide2.SetActive(true);
              buttonToHide3.SetActive(true);
               buttonToHide4.SetActive(true);
                buttonToHide5.SetActive(true);
                 buttonToHide6.SetActive(true);

                 //to show 
                 buttonToShow.SetActive(false);
             buttonToShow2.SetActive(false);
              buttonToShow3.SetActive(false);
               buttonToShow4.SetActive(false);
                buttonToShow5.SetActive(false);
                 buttonToShow6.SetActive(false);
        }
        else
        {
            // Neither Oculus VR device nor Android platform, hide the button
            buttonToHide.SetActive(false);
             buttonToHide2.SetActive(false);
              buttonToHide3.SetActive(false);
               buttonToHide4.SetActive(false);
                buttonToHide5.SetActive(false);
                 buttonToHide6.SetActive(false);

                 //toshow

                  buttonToShow.SetActive(true);
             buttonToShow2.SetActive(true);
              buttonToShow3.SetActive(true);
               buttonToShow4.SetActive(true);
                buttonToShow5.SetActive(true);
                 buttonToShow6.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
