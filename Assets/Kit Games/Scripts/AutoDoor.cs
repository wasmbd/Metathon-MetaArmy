using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public float openDistance = 3f; // Distance at which the door opens automatically
    public float closeDistance = 4f; // Distance at which the door closes automatically
    public float doorSpeed = 2f; // Speed at which the door opens/closes

    private Vector3 initialPosition;
    private bool isOpen = false;
    private bool isPlayerNear = false;
    private Transform player;

    private void Start()
    {
          initialPosition = transform.position;
    GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
    if (playerObject != null)
    {
        player = playerObject.transform;
    }
    else
    {
        Debug.LogError("Player object not found in the scene. Make sure the player object is tagged as 'Player'.");
    }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isOpen && distanceToPlayer <= openDistance)
        {
            // Open the door
            OpenDoor();
        }
        else if (isOpen && distanceToPlayer > closeDistance)
        {
            // Close the door
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        transform.position = Vector3.Lerp(transform.position, initialPosition + transform.right * 2f, Time.deltaTime * doorSpeed);
        isOpen = true;
    }

    private void CloseDoor()
    {
        transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * doorSpeed);
        isOpen = false;
    }
}
