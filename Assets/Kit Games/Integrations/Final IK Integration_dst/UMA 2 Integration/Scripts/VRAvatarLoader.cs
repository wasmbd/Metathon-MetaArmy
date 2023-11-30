using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using System;
using UnityEngine.Experimental.AI;
using System.IO;
public class VRAvatarLoader : MonoBehaviour
{
    public string myRecipe;

    [SerializeField]
    private DynamicCharacterAvatar avatar;

    [SerializeField]
    private DynamicCharacterAvatar avatarToBeEdited;

    // Start is called before the first frame update
    void Start()
    {
        LoadRecipe();
    }
    public void LoadRecipe()
    {

        if (File.Exists(Application.persistentDataPath + MultiplayerVRConstants.AVATAR_SAVE_FILE_NAME))
        {
            myRecipe = File.ReadAllText(Application.persistentDataPath + MultiplayerVRConstants.AVATAR_SAVE_FILE_NAME);

            avatar.ClearSlots();
            avatar.LoadFromRecipeString(myRecipe);

        }

    }

    private void OnEnable()
    {
        //avatarToBeEdited.CharacterUpdated.AddListener(OnCharacterUpdated);

    }

    private void OnDisable()
    {
        //avatarToBeEdited.CharacterUpdated.RemoveListener(OnCharacterUpdated);


    }
    void OnCharacterUpdated(UMAData data)
    {

        //avatar.ClearSlots();
        //avatar.LoadFromRecipeString(avatarToBeEdited.GetCurrentRecipe());

    }
}
