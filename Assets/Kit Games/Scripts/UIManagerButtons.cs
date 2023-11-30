using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerButtons : MonoBehaviour
{
    public GameObject femaleButton;
    public GameObject maleButton;
    public GameObject nextButton;
    public GameObject uiOne;
    public GameObject uiTwo;

    void Start()
    {
        // Deactivate UI canvases and the "Next" button initially
        nextButton.SetActive(false);
        uiOne.SetActive(false);
        uiTwo.SetActive(false);
    }

    public void OnFemaleButtonClick()
    {
        // Activate the "Next" button and show UIOne
        nextButton.SetActive(true);
        uiOne.SetActive(true);

        // Deactivate the "Female" and "Male" buttons
        femaleButton.SetActive(false);
        maleButton.SetActive(false);
    }

    public void OnMaleButtonClick()
    {
        // Activate the "Next" button and show UITwo
        nextButton.SetActive(true);
        uiTwo.SetActive(true);

        // Deactivate the "Female" and "Male" buttons
        femaleButton.SetActive(false);
        maleButton.SetActive(false);
    }

    public void OnNextButtonClick()
    {
        // Deactivate the "Next" button and hide both UIOne and UITwo
        nextButton.SetActive(false);
        uiOne.SetActive(false);
        uiTwo.SetActive(false);

        // Reactivate the "Female" and "Male" buttons
        femaleButton.SetActive(true);
        maleButton.SetActive(true);
    }
}