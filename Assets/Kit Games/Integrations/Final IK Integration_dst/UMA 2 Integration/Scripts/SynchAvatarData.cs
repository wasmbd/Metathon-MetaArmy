using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA.CharacterSystem;
using System.IO;
using Photon.Pun;
using CompressString;
using UMA;

public class SynchAvatarData : MonoBehaviour
{
    [SerializeField]
    private DynamicCharacterAvatar avatar;

    [SerializeField]
    GameObject UMAGLIB_Gameobject;

    [SerializeField]
    PhotonView _photonView;
    #region Unity Methods

    private void Awake()
    {
        if (_photonView.IsMine)
        {
            if (UMAGLIB_Gameobject != null)
            {
                //UMAGLIB gameobject is necessary to have UMA Avatars rendered and created in any Unity scene.
                //That is why we instantiated it locally.
                Instantiate(UMAGLIB_Gameobject);
            }

            if (_photonView == null)
            {
                _photonView = GetComponent<PhotonView>();
            }

            if (_photonView.IsMine)
            {
                VirtualWorldManager.Instance.OnNewPlayerEntered.AddListener(OnNewPlayerEntered);

            }

        }
    }
   
    private void OnEnable()
    {
        avatar.CharacterCreated.AddListener(OnCharacterCreated);

    }

    private void OnDisable()
    {
        avatar.CharacterCreated.RemoveListener(OnCharacterCreated);
    }
    #endregion
    void OnNewPlayerEntered()
    {
        Debug.Log("New Player came bro!");
        Invoke("LoadAvatarRemotely", 3); 

    }


    private void OnCharacterCreated(UMAData data)
    {
        Debug.Log("Character Created");
        //First, we read and load the UMA Avatar DNA Data locally.
        //We do this with OnCharacterCreated event and a small delay to ensure that UMA Avatar is successfully created.
        Invoke("LoadAvatarLocally", 3);

    }
    

    void LoadAvatarRemotely()
    {
        if (_photonView.IsMine)
        {
            if (File.Exists(Application.persistentDataPath + MultiplayerVRConstants.AVATAR_SAVE_FILE_NAME))
            {
                string myRecipe = File.ReadAllText(Application.persistentDataPath + MultiplayerVRConstants.AVATAR_SAVE_FILE_NAME);
                string decompressedString = StringCompressor.DecompressString(myRecipe);
                Debug.Log(myRecipe);
                _photonView.RPC("RemotelyLoadUMAAvatarDNAData", RpcTarget.All, decompressedString);

            }
        }
        
    }
    void LoadAvatarLocally()
    {
        if (_photonView.IsMine)
        {
            if (File.Exists(Application.persistentDataPath + MultiplayerVRConstants.AVATAR_SAVE_FILE_NAME))
            {
                string myRecipe = File.ReadAllText(Application.persistentDataPath + MultiplayerVRConstants.AVATAR_SAVE_FILE_NAME);
                string decompressedString = StringCompressor.DecompressString(myRecipe);
                //Debug.Log(myRecipe);
                avatar.ClearSlots();
                avatar.LoadFromRecipeString(decompressedString);

                //After locally loading UMA Avatar DNA Data,
                //Let's check if there are any players other than us.
                if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
                {
                    //If the player count is more than 1, then, there are other players in the room.
                    //This means Photon will instantiate our player gameobject in every player's local game.
                    //So, we need to send our own UMA Avatar DNA Data to our remote player gameobjects.
                    _photonView.RPC("RemotelyLoadUMAAvatarDNAData", RpcTarget.All, decompressedString);

                }
                else
                {
                    Debug.Log("No need to send the UMA Avatar DNA Data over network since we are alone in this room. ");
                }
            }

           
        }
    }

    [PunRPC]
    public void RemotelyLoadUMAAvatarDNAData(string recipeString)
    {
        if (!_photonView.IsMine)
        {
            avatar.ClearSlots();
            avatar.LoadFromRecipeString(recipeString);
        }
    }
   

}
