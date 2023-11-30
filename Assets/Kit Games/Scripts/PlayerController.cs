using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cameraTPS;
    public GameObject cameraFPS;
    public Animator animator;
    public GameObject objectToHide;
    bool isTPS = false;

    void Start()
    {
        SwitchCamera();
    }

    void VisiblePlayer(bool flag)
    {
        SkinnedMeshRenderer[] meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].enabled = flag;
        }
    }

    void SetObjectVisibility(bool flag)
    {
        objectToHide.SetActive(flag);
    }

    public void SwitchCamera()
    {
        // Disable visibility of the character when switching cameras
         
        isTPS = !isTPS;
        if (isTPS)
        {
            SetObjectVisibility(true);

            cameraTPS.SetActive(true);
            cameraFPS.SetActive(false);
        }
        else
        {
            cameraTPS.SetActive(false);
            cameraFPS.SetActive(true);
             SetObjectVisibility(false);
        }

        // Enable visibility of the character after switching cameras
       
    }
}
