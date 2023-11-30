using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationWithMouse : MonoBehaviour
{
    private bool isRotating = false;
    private Vector3 previousMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse click is on the object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                isRotating = true;
                previousMousePosition = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // Calculate the rotation amount based on the mouse movement
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - previousMousePosition;

            // Adjust the rotation speed for a smoother rotation
            float rotationSpeed = 1f;

            // Rotate the object around its Y-axis based on the mouse movement
            transform.Rotate(Vector3.up, -mouseDelta.x * rotationSpeed, Space.World);

            previousMousePosition = currentMousePosition;
        }
    }
}