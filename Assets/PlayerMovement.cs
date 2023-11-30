using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    public XRController leftController;
    public XRController rightController;
    public float movementSpeed = 3f;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 leftThumbstick = leftController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftStickValue) ? leftStickValue : Vector2.zero;
        Vector2 rightThumbstick = rightController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightStickValue) ? rightStickValue : Vector2.zero;

        Vector3 movement = (transform.forward * leftThumbstick.y + transform.right * leftThumbstick.x) * movementSpeed * Time.deltaTime;
        characterController.Move(movement);

        float rotation = rightThumbstick.x * Time.deltaTime * 100f;
        transform.Rotate(Vector3.up, rotation);
    }
}