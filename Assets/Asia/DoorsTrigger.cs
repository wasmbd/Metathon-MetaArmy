using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsTrigger : MonoBehaviour
{
    [SerializeField] private Animator entryDoor;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private string closeDoor;
    [SerializeField] private string openDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                entryDoor.Play(openDoor, 0, 0.0f);
                gameObject.SetActive(true);
            }
            else if (closeTrigger)
            {
                entryDoor.Play(closeDoor, 0, 0.0f);
                gameObject.SetActive(true);
            }
        }
    }
}
