using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    private Renderer characterRenderer;
    private float hideDuration = 5f;
    private float timer = 0f;
    private bool isVisible = true;

    private void Start()
    {
        characterRenderer = GetComponent<Renderer>();

        // Hide the character initially
        SetVisibility(false);
    }

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer has exceeded the hide duration
        if (timer >= hideDuration && !isVisible)
        {
            // Show the character
            SetVisibility(true);
        }
    }

    private void SetVisibility(bool visible)
    {
        characterRenderer.enabled = visible;
        isVisible = visible;
    }
}