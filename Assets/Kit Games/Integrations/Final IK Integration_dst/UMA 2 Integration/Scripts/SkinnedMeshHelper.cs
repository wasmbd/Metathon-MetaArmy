using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;

/// <summary>
//Sometimes, Character arms and hands are not rendered in certain angles.
//This becomes an issue with VR due to the freedom in looking at different angles.
//This script helps with the issue by setting updateWhenOffscreen bool of the SkinnedMeshRenderer to true.
/// </summary>
public class SkinnedMeshHelper : MonoBehaviour
{
    [SerializeField]
    GameObject UMAAvatar_Gameobject;
    private void OnEnable()
    {
        UMAAvatar_Gameobject.GetComponent<DynamicCharacterAvatar>().CharacterCreated.AddListener(OnCharacterCreated);
    }

    private void OnDisable()
    {
        UMAAvatar_Gameobject.GetComponent<DynamicCharacterAvatar>().CharacterCreated.RemoveListener(OnCharacterCreated);
    }

    //This callback method is called just after the UMA Character is created.
    //And it enables updateWhenOffscreen bool.
    public void OnCharacterCreated(UMAData data)
    {
        Component[] skinnedMeshRenderers;
        skinnedMeshRenderers = GetComponentsInChildren(typeof(SkinnedMeshRenderer));
        if (skinnedMeshRenderers.Length > 0)
        {
            foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
                skinnedMeshRenderer.updateWhenOffscreen = true;
        }
    }
}
