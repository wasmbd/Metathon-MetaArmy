using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AvatarZoomManager : MonoBehaviour
{
    [SerializeField]
    Vector3 zoomIn = new Vector3(70, -90, -40);
    [SerializeField]
    Vector3 zoomIn2 = new Vector3(50, -120, -20);
    [SerializeField]
    Vector3 zoomOut;

    public bool isZoom = false;
    float zoom = 1f;

    private void Start()
    {
        string loadedDeviceName = XRSettings.loadedDeviceName;
        Debug.Log("AvatarZoomManager Start");

        zoomOut = transform.localPosition;
    }

    void Update()
    {
        string loadedDeviceName = XRSettings.loadedDeviceName;
        if (loadedDeviceName.Contains("Oculus") || Application.platform == RuntimePlatform.Android)
        {

            if (isZoom)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, zoomIn, 0.01f);
        }
        else
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, zoomOut, 0.01f);            
        }
        }else
        {
        }

            // Get the scroll wheel input
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        zoom = Mathf.Clamp(zoom - scrollInput, 0, 1);

        Vector3 targetZoom = isZoom ? zoomIn2 : zoomIn; // Choose the appropriate zoom level

        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, Vector3.Lerp(targetZoom, zoomOut, zoom), 0.01f);
    }

    public void ToggleZoom()
{
    isZoom = !isZoom;
}

    // This function will be called when the button is clicked to toggle the zoom level
    public void ToggleZoomOne()
    {
       // isZoom = !isZoom;
        if (isZoom = isZoom)
        {
            isZoom = !isZoom;
            
        }
        else
        {
            isZoom = isZoom;
        }
    }

    public void ToggleZoomTow()
    {
       
         // isZoom = isZoom;
        if (isZoom = !isZoom)
        {
            isZoom = isZoom;
        }else
        {
            isZoom = !isZoom;
        }

    }

}
