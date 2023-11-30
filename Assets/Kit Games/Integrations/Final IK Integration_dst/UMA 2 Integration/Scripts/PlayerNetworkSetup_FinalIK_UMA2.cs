using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UMA;
using UMA.CharacterSystem;


public class PlayerNetworkSetup_FinalIK_UMA2 : MonoBehaviour
{
    public GameObject LocalXRRigGameobject;
    public GameObject MainAvatarGameobject;
    public GameObject LeftHandTargetGameobject;
    public GameObject RightHandTargetGameobject;

    public TextMeshProUGUI PlayerName_Text;

    [SerializeField]
    private DynamicCharacterAvatar avatar;

    [SerializeField]
    GameObject nonNetworkedGameobjects;

    private PhotonView _photonView;

    public enum RaiseEventCodes
    {
        PlayerSpawnEventCode = 0,
        AvatarBuildCode = 1
    }

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        avatar.CharacterCreated.AddListener(OnCharacterCreated);

    }

    private void OnDisable()
    {
        avatar.CharacterCreated.RemoveListener(OnCharacterCreated);
    }

    // Start is called before the first frame update
    void Start()
    {

        //Setup the player
       // if (_photonView.IsMine)
      //  {
            InitiateLocal();
       // }
       // else
       // {
       //     InitiateRemote();
      //  }

       // name = "FinalIK_UMA2_PUN2_VR_Player " + (_photonView.IsMine ? "(Local)" : "(Remote)");


        if (PlayerName_Text != null)
        {
            PlayerName_Text.text = _photonView.Owner.NickName;
        }
    }



    private void OnCharacterCreated(UMAData data)
    {
        Debug.Log("Character Created");
        //_photonView.RPC("InitializeAnimation", RpcTarget.All);
    }

    private void InitiateLocal()
    {
        //The player is local, it means we should activate the XR Rig for the local VR player.
        LocalXRRigGameobject.SetActive(true);
        //gameObject.GetComponent<MovementController_FinalIK>().enabled = true;

       

        //Adding Audio Listener for voice chat setup
        MainAvatarGameobject.AddComponent<AudioListener>();

        //Adjusting Left and Right hand target of Final IK Fullbody VR system.
        //This is a workaround for the bug that is resetting the hand anchor positions, which breaks the fullbody presence.
        LeftHandTargetGameobject.transform.localPosition = new Vector3(-0.04f, 0f, -0.1f);
        RightHandTargetGameobject.transform.localPosition = new Vector3(0.04f, 0f, -0.1f);
    }

    private void InitiateRemote()
    {
        //The player is remote. 
        //Remote instance does not have a VR rig
        LocalXRRigGameobject.SetActive(false);
        //gameObject.GetComponent<MovementController_FinalIK>().enabled = false;

        //We do not need this script for Remote players.
        //Becaue probably, the arms and hand will be always rendered.
        MainAvatarGameobject.GetComponent<SkinnedMeshHelper>().enabled = false;

        //DCARendererManager helps us NOT to render Head for local players.
        //But for remote VR Players, we need to render the head mesh.
        //So, we do not need DCARendererManager for remote VR Players.
        MainAvatarGameobject.GetComponent<DCARendererManager>().enabled = false;

        //De-activating non-networked gameobjects such as Voice Debug UI and UI Menu.
        //These gameobjects will be activated only locally so there is no need them to be active in remote players.
        nonNetworkedGameobjects.SetActive(false);
    }

    [PunRPC]
    public void InitializeAnimation()
    {
        avatar.gameObject.GetComponent<Animator>().enabled = true;
        //avatar.gameObject.GetComponent<VRAnimatorController>().enabled = true;
    }
}
