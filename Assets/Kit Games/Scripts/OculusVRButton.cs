using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HideObjectsBasedOnDevice : MonoBehaviour
{
    public GameObject[] objectsToHide;

    void Start()
    {
        if (objectsToHide != null && objectsToHide.Length > 0)
        {
            string loadedDeviceName = XRSettings.loadedDeviceName;
            bool isOculusDevice = loadedDeviceName.Contains("Oculus");
            bool isAndroidDevice = SystemInfo.deviceType == DeviceType.Handheld;

            bool hideObjects = isOculusDevice || isAndroidDevice;

            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(!hideObjects);
                }
                else
                {
                    Debug.LogWarning("An object to hide is not assigned in the inspector.");
                }
            }
        }
        else
        {
            Debug.LogWarning("No objects to hide are assigned in the inspector.");
        }
    }
}
