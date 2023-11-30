using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarZoomOculus : MonoBehaviour
{
    [SerializeField]
    Vector3 zoomIn = new Vector3(70, -90, -40);
    [SerializeField]
    Vector3 zoomOut;
    public bool isZoom = false;
    private void Start()
    {
        zoomOut = transform.localPosition;
    }
    void Update()
    {
        if(isZoom)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, zoomIn, 0.01f);
        }
        else
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, zoomOut, 0.01f);            
        }
    }

    public void SuperZoom(){
        
    }
}
