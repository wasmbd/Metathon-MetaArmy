using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 2.0f;
    public float minZoom = 1.0f;
    public float maxZoom = 10.0f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 position = transform.localPosition;

        position.y -= scroll * zoomSpeed;

        position.y = Mathf.Clamp(position.y, minZoom, maxZoom);

        transform.localPosition = position;
    }
}