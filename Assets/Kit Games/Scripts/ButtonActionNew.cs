using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActionNew : MonoBehaviour
{ 
    public Button firstButton;
    public Button secondButton;

    private bool isFirstButtonClickable = false;

    private void Start()
    {
        // Disable the first button's interaction initially
        firstButton.interactable = false;

        // Add a listener to the second button's click event
        secondButton.onClick.AddListener(OnSecondButtonClick);
    }

    private void OnSecondButtonClick()
    {
        isFirstButtonClickable = true;
        firstButton.interactable = true;

        // Add a listener to the first button's click event when the second button is clicked
        firstButton.onClick.AddListener(OnFirstButtonClick);
    }

     private void OnFirstButtonClick()
    {
        // Open google.com in the browser
        Application.OpenURL("https://www.google.com");
    }
}