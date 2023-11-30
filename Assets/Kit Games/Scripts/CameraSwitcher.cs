using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera alternateCamera;
    public float rotationSpeed = 10f;

    private Camera activeCamera;
    private bool isRotating = false;
    private Vector3 mouseOrigin;

    private void Start()
    {
        activeCamera = mainCamera;
        alternateCamera.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isRotating && Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            activeCamera.transform.Rotate(Vector3.up, mouseX * rotationSpeed);
        }
    }

    public void SwitchCamera()
    {
        mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeSelf);
        alternateCamera.gameObject.SetActive(!alternateCamera.gameObject.activeSelf);

        activeCamera = mainCamera.gameObject.activeSelf ? mainCamera : alternateCamera;

        // Reset rotation when switching cameras
        activeCamera.transform.rotation = Quaternion.identity;

        // Enable rotation for the newly activated camera
        isRotating = true;
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }
}
