using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Button startGameButton = GameObject.Find("StartButton").GetComponent<Button>();
    Button resetButton = GameObject.Find("ResetButton").GetComponent<Button>();
    Button exitGameButton = GameObject.Find("ExitButton").GetComponent<Button>();

    public void StartGame()
    {
        if (startGameButton != null)
        {

        }
    }

    public void Reset()
    {
        if (resetButton != null)
        {

        }
    }

    public void ExitGame()
    {
        if (exitGameButton != null)
        {

        }
    }
}
