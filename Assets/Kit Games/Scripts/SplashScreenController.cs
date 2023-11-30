using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenController : MonoBehaviour
{
    public GameObject splashCanvas;
    public GameObject splashImage;
    public float displayTime = 5f;

    private void Start()
    {
        // Disable the splash canvas after the specified display time
        Invoke("HideSplashCanvas", displayTime);
    }

    private void HideSplashCanvas()
    {
        splashImage.SetActive(true);
        splashCanvas.SetActive(false);
    }
}
