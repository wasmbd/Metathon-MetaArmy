using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using StarterAssets;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPositions;
    public GameObject spawnedPlayer;
    public GameObject normalCharacter;

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the player at the default spawn point (index 0) initially
        SpawnPlayer(PlayerPrefs.GetInt("spawnindex", 0));
    }

    #endregion

    #region Public Methods

    // Public method to set the spawn position based on the user's selection
    public void SetSpawnPosition(int spawnIndex)
    {
        if (spawnIndex >= 0 && spawnIndex < spawnPositions.Length)
        {
            MovePlayerToSpawnPoint(spawnIndex);
        }
        else
        {
            Debug.LogError("Invalid spawn position index!");
        }
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
        }
    }

    #endregion

    #region Private Methods

    public GameObject playerPrefab;
    private void SpawnPlayer(int spawnIndex)
{
    Debug.Log("ERRROOOOOOOOOOOOOOOO" );
    Vector3 instantiatePosition = spawnPositions[spawnIndex].position;
    string loadedDeviceName = XRSettings.loadedDeviceName;

   if (loadedDeviceName.Contains("Oculus") || (SystemInfo.deviceType == DeviceType.Handheld && SystemInfo.operatingSystem.Contains("Android")))
    {
    //     if (MultiplayerVRConstants.USE_FINALIK)
    //     {
    //         spawnedPlayer = PhotonNetwork.Instantiate("NetworkedVRPlayerPrefab_FinalIK", instantiatePosition, Quaternion.identity, 0);
    //     }
    //     else if (MultiplayerVRConstants.USE_FINALIK_UMA2)
    //     {
            
    //        spawnedPlayer = PhotonNetwork.Instantiate("NetworkedVRPlayerPrefab_FinalIK_UMA2", instantiatePosition, Quaternion.identity, 0);
//            spawnedPlayer = Instantiate(playerPrefab);
//            spawnedPlayer.transform.position = instantiatePosition; 
    //     }
    //     else
    //     {
    //         spawnedPlayer = PhotonNetwork.Instantiate("NetworkedVRPlayerPrefab", instantiatePosition, Quaternion.identity, 0);
    //     }
        // spawnedPlayer = playerPrefab.transform.GetChild(2).gameObject;
         MovePlayerToSpawnPoint(spawnIndex);
    }
    else
    {
        spawnedPlayer = normalCharacter.transform.GetChild(2).gameObject;
         MovePlayerToSpawnPoint(spawnIndex);
     }
}


    private void MovePlayerToSpawnPoint(int spawnIndex)
    {
        if (spawnedPlayer != null)
        {
            Vector3 newPosition = spawnPositions[spawnIndex].position;
            if(spawnedPlayer.GetComponent<ThirdPersonController>() != null)
                spawnedPlayer.GetComponent<ThirdPersonController>().enabled = false;
            spawnedPlayer.SetActive(false);
            spawnedPlayer.transform.position = newPosition;
            if(spawnedPlayer.GetComponent<ThirdPersonController>() != null)
                spawnedPlayer.GetComponent<ThirdPersonController>().enabled = true;
            spawnedPlayer.gameObject.SetActive(true);
            Debug.LogError(newPosition);
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    [PunRPC]
    private void ChangePlayerPosition(Vector3 newPosition)
    {
    }

    #endregion
}