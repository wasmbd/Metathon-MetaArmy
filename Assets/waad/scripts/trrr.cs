using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trrr : MonoBehaviour
{
   // public AudioSource shoutingSound;
    public GameObject canvasObject; // Reference to the canvas object
   // public GameObject musicObject;

    private bool canvasActive=false; // Flag to track if the canvas is active

    void Start()
    {
      //  canvasActive = // Assign AudioSource component to shoutingSound
        canvasObject.SetActive(false); // Initially deactivate the canvas
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Check if the object is an enemy and the canvas is not already active
        {
           // shoutingSound.Play();
            canvasObject.SetActive(true); // Activate the canvas
            canvasActive = true; // Set the flag to indicate that the canvas is active
        }
    }
   /* private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && canvasActive) // Check if the object is an enemy and the canvas is not already active
        {
            // shoutingSound.Play();
            canvasObject.SetActive(false); // Activate the canvas
            canvasActive = false; // Set the flag to indicate that the canvas is active
        }
    }*/
  
}