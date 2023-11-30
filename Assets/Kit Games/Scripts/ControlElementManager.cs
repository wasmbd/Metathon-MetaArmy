using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlElementManager : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject[] uiPanels;
    GameObject selectedObj;

    public void OnRotate(bool isPlus)
    {
        selectedObj.transform.Rotate(!isPlus ? Vector3.down : Vector3.up, 10);
    }
    public void OnRotateZ(bool isClockwise)
    {
        if (selectedObj != null)
        {
            selectedObj.transform.Rotate(Vector3.forward, isClockwise ? -90 : 90);
        }
    }

     public void DeleteSelected()
    {
        if (selectedObj != null)
        {
            // Optionally, you can add any cleanup or specific deletion logic here
            // before destroying the object.
            
            // Destroy the selected object.
            Destroy(selectedObj);

            // Clear the selected object reference.
            selectedObj = null;

            // Hide the control panel since there's no selected object.
            controlPanel.SetActive(false);
        }
    }

    public void OnScale(bool isPlus)
    {
        selectedObj.transform.localScale = selectedObj.transform.localScale * (isPlus ? 1.1f : 0.9f);
    }

    public void OnChangeColor(Image colorImg)
    {
        ChangeColor(selectedObj.transform, colorImg.color);
    }

    private void ChangeColor(Transform parent, Color color)
    {
        // Check if the current transform has a MeshRenderer
        MeshRenderer meshRenderer = parent.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // Do something with the MeshRenderer, for example, enable or disable it
            meshRenderer.material.color = color;
        }

        // Recursively check the children of the current transform
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            // Recursively call the function on the child
            ChangeColor(child, color);
        }
    }

    void Update()
    {
        controlPanel.SetActive(selectedObj != null);
        if (Input.GetMouseButtonDown(0) && selectedObj == null)
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits;

            // Detect all colliders intersecting the ray
            hits = Physics.RaycastAll(ray);

            // Loop through the hits and do something with each collider
            foreach (RaycastHit hit in hits)
            {
                // You can access hit.collider for the collider that was hit
                // Here, we're just printing the name of the game object with the collider
                if (hit.collider.gameObject.tag == "Interactable")
                {
                    selectedObj = hit.collider.gameObject;
                    SetActiveOutline(selectedObj.transform, true);
                    for (int i = 0; i < uiPanels.Length; i++)
                    {
                        uiPanels[i].SetActive(false);
                    }
                    break;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedObj != null)
                SetActiveOutline(selectedObj.transform, false);
            selectedObj = null;
        }
    }

    private void SetActiveOutline(Transform parent, bool isActive)
    {
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            Outline2 outline = child.GetComponent<Outline2>();

            if (outline != null)
            {
                outline.OutlineMode = isActive ? Outline2.Mode.OutlineAll : Outline2.Mode.OutlineHidden;
                outline.enabled = isActive;
            }

            // Recursively collect colliders from the children of this child
            SetActiveOutline(child, isActive);
        }
    }
}
