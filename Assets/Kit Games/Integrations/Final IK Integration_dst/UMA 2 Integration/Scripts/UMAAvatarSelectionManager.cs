using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using System;
using UnityEngine.Experimental.AI;
using System.IO;
using CompressString;
public class UMAAvatarSelectionManager : MonoBehaviour
{
   
    public string myRecipe;

    [SerializeField]
    private DynamicCharacterAvatar avatar;

    /// <summary>
    /// Singleton Implementation
    /// </summary>
    public static UMAAvatarSelectionManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        LoadRecipe();
    }


    public void SaveRecipe()
    {
        myRecipe = avatar.GetCurrentRecipe();
        string compressedString = CompressString.StringCompressor.CompressString(myRecipe);
        File.WriteAllText(Application.persistentDataPath + MultiplayerVRConstants.AVATAR_SAVE_FILE_NAME, compressedString);
        if (DebugUIManager.instance !=null)
        {
            DebugUIManager.instance.ShowDebugUIMessage("Saved");
        }
    }

    public void LoadRecipe()
    {
        if (File.Exists(Application.persistentDataPath + MultiplayerVRConstants.AVATAR_SAVE_FILE_NAME))
        {
            myRecipe = File.ReadAllText(Application.persistentDataPath + MultiplayerVRConstants.AVATAR_SAVE_FILE_NAME);
            string decompressedString = CompressString.StringCompressor.DecompressString(myRecipe);
            avatar.ClearSlots();
            avatar.LoadFromRecipeString(decompressedString);
        }
        else
        {
            Debug.Log("No saved recipe found!");
        }
    }    
}
