using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndDisappear : MonoBehaviour
{
    public GameObject objectToShow;
    public float displayDuration = 3f;

    private float timer = 0f;
    private bool isDisplaying = false;

    // Call this function to trigger the object display
    public void ShowObject()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
            isDisplaying = true;
            timer = 0f;
        }
    }

    private void Update()
    {
        if (isDisplaying)
        {
            timer += Time.deltaTime;
            if (timer >= displayDuration)
            {
                // Time's up, hide the object
                objectToShow.SetActive(false);
                isDisplaying = false;
            }
        }
    }
}
