using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.IO;
using System;
using Photon.Realtime;
using ExitGames.Client.Photon;
using RootMotion.FinalIK;

public class PlayerNetworkSetup_FinalIK : MonoBehaviour
{
    public GameObject LocalXRRigGameobject;
    public GameObject MainAvatarGameobject;
    public GameObject LeftHandTarget;
    public GameObject RightHandTarget;

    public TextMeshProUGUI PlayerName_Text;
    private PhotonView _photonView;

    public SkinnedMeshRenderer AvatarSkinnedMeshRenderer;


    [SerializeField]
    GameObject nonNetworkedGameobjects;


    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        AvatarSkinnedMeshRenderer.enabled = false;

    }


    // Start is called before the first frame update
    void Start()
    {
        //Setup the player
       // if (_photonView.IsMine)
       // {
            InitiateLocal();

      //  }
       // else
      //  {
      //      InitiateRemote();
      //  }

      //  name = "FinalIK_PUN_VR_Player " + (_photonView.IsMine ? "(Local)" : "(Remote)");

        if (PlayerName_Text != null)
        {
            PlayerName_Text.text = _photonView.Owner.NickName;
        }
    }


    private void InitiateLocal()
    {
        //The player is local, it means we should enable its movement controller and activate the XR Rig.
        LocalXRRigGameobject.SetActive(true);
       

      
        //Adding Audio Listener for voice chat setup
        MainAvatarGameobject.AddComponent<AudioListener>();

        //Instantiating objects in Unity can effect performance if the object has complicated mesh such as Human avatar
        //So, we delay the rendering of the human avatar to gin performance.
        Invoke("EnableRenderingForAvatar", 2.0f);

        //Adjusting Left and Right hand target of Final IK Fullbody VR system.
        //This is a workaround for the bug that is resetting the hand anchor positions, which breaks the fullbody presence.
        LeftHandTarget.transform.localPosition = new Vector3(-0.04f,0f,-0.1f);
        RightHandTarget.transform.localPosition = new Vector3(0.04f, 0f, -0.1f);

    }

    private void InitiateRemote()
    {
        //The player is remote. 
        //Remote instance does not have a VR rig
        LocalXRRigGameobject.SetActive(false);

        //De-activating non-networked gameobjects such as Voice Debug UI and UI Menu.
        //These gameobjects will be activated only locally so there is no need them to be active in remote players.
        nonNetworkedGameobjects.SetActive(false);

    }

    private void EnableRenderingForAvatar()
    {
        _photonView.RPC("EnableRenderingForAvatar_RPC",RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void EnableRenderingForAvatar_RPC()
    {
        AvatarSkinnedMeshRenderer.enabled = true;
    }
}
