using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControllers : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f; // Adjust this to change camera rotation speed

    private Vector2 rotationInput;

    void Update()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rotationInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

        // Move the camera in the direction of input
        if (moveDirection != Vector3.zero)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(cameraForward);

            Vector3 movement = rotation * moveDirection;
            transform.position += movement * moveSpeed * Time.deltaTime;
        }

        // Rotate the camera with mouse input
        RotateCamera();
    }

    void RotateCamera()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.x -= rotationInput.y * rotationSpeed;
        currentRotation.y += rotationInput.x * rotationSpeed;
        transform.eulerAngles = currentRotation;
    }
}
