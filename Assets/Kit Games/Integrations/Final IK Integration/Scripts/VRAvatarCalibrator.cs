using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEditor;
using Photon.Pun;

public class VRAvatarCalibrator : MonoBehaviour
{
    public VRIK ik;
    public float scaleMlp = 1f;

    [SerializeField]
    PhotonView _photonView;

    private void Start()
    {
        //Invoke("InstantiateCalibration", 2f);
    }

    #region Custom Methods
    public void InstantiateCalibration()
    {
        if (CheckIfConnectedAndInRoom())
        {
            //If we are in a Photon room, we synchronize this operation.
            //This will ensure that, VR avatar will be calibrated across the network.
            _photonView.RPC("CalibrateAvatar_RPC", RpcTarget.All);
        }
        else
        {
            //If we are not in Photon room, we just locally calibrate the avatar.
            CalibrateAvatar_Locally();
        }
    }


    [PunRPC]
    public void CalibrateAvatar_RPC()
    {
        CalibrateAvatar_Locally();
    }

    private void CalibrateAvatar_Locally()
    {
        //Compare the height of the head target to the height of the head bone, multiply scale by that value.
        float sizeF = (ik.solver.spine.headTarget.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
        ik.references.root.localScale *= sizeF * scaleMlp;
    }

    /// <summary>
    /// This method is for checking if we are in a Photon room or not
    /// We do this so that we can run this Movement Controller_FinalIK script both locally and remotely
    /// </summary>
    /// <returns></returns>
    bool CheckIfConnectedAndInRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom)
        {
            return true;
        }
        return false;
    }
    #endregion
}
