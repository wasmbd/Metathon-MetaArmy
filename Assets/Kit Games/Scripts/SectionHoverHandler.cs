using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionHoverHandler : MonoBehaviour
{
    // Reference to the buttons GameObject
    public GameObject buttonsObject;

    // Ensure the buttons are hidden when the game starts
    void Start()
    {
        if (buttonsObject != null)
            buttonsObject.SetActive(false);
    }

    // Called when the mouse enters the section
    void OnMouseEnter()
    {
        if (buttonsObject != null)
            buttonsObject.SetActive(true);
    }

    // Called when the mouse exits the section
    void OnMouseExit()
    {
        if (buttonsObject != null)
            buttonsObject.SetActive(false);
    }
}
