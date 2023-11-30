using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using Photon.Pun;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class LocomotionSwitcher_FinalIK : MonoBehaviour
{

    [SerializeField]
    GameObject MainAvatarGameobject;

    [SerializeField]
    PhotonView _photonView;


    [SerializeField]
    VRAnimatorController_FinalIK vrAnimatorController;

    [SerializeField]
    VRIK vRIK;

    [SerializeField]
    Animator animator;
   
    bool finalIKEnabled = false;
    private float maxMoveSpeed = 1f;


    private Vector3 previousPos;
    public Transform headTarget;

    public float speedTreshold = 0.1f;
    [Range(0, 1)]
    public float smoothing = 1;

    private Vector3 animationSpeedVector = Vector3.zero;
    #region Unity Methods


    private void Start()
    {



        if (!CheckIfConnectedAndInRoom())
        {
            Debug.Log("We are not in a Photon room.");

        }

        previousPos = headTarget.position;
        EnableFinalIKLocomotion();
        finalIKEnabled = true;
    }



    private void Update()
    {
        //Calculating movement speed
        Vector3 headsetSpeed = (headTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;
       

        //Debug.Log(currentMoveSpeed);
        if (headsetSpeed.magnitude < 0.25 * maxMoveSpeed && !finalIKEnabled)
        {
            //If we are slowly moving, we use the FinalIK's locomotion which is simply calculating the legs movement with Inverse Kinematics.
            if (CheckIfConnectedAndInRoom())
            {
                //If we are in a Photon room, we synchronize this operation.
                _photonView.RPC("EnableFinalIKLocomotion_RPC", RpcTarget.All);
            }
            else
            {
                //If we are not in Photon room, we just locally Enable Final IK Locomotion.
                EnableFinalIKLocomotion();
            }
            finalIKEnabled = true;
        }
        else if (headsetSpeed.magnitude >= 0.25 * maxMoveSpeed)
        {

            //If the movement magnitude is big enough, it means we are moving with Joystick.
            //And if we are  moving with Joystick, 
            //Disable Final IK's default locomotion and enable VR Animator Controller to animate the legs.
            //But first, we just check that if we are in Photon room.
            //According to that, we synch this operation across the network or not.
            if (CheckIfConnectedAndInRoom())
            {
                //If we are in a Photon room, we synchronize this operation.
                _photonView.RPC("DisableFinalIKLocomotion_RPC", RpcTarget.All);
            }
            else
            {
                //If we are not in Photon room, we just locally Disable Final IK Locomotion.
                DisableFinalIKLocomotion();
            }

            //Local speed
            Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
            animationSpeedVector = headsetLocalSpeed;
            previousPos = headTarget.position;
            finalIKEnabled = false;

        }
    }
    #endregion

    #region Public Methods
    public void DisableFinalIKLocomotion()
    {
        vRIK.solver.Reset();
        vRIK.solver.locomotion.weight = 0;
        vrAnimatorController.enabled = true;
        vrAnimatorController.ProcessAnimation(animationSpeedVector);
       
    }

    public void EnableFinalIKLocomotion()
    {
        vRIK.solver.Reset();
        animator.SetBool("isMoving", false);
        animator.SetFloat("DirectionX", 0f);
        animator.SetFloat("DirectionY", 0f);
        vRIK.solver.locomotion.weight = 1;
        vrAnimatorController.enabled = false;
        


    }
    #endregion

    #region Private Methods
    /// <summary>
    /// This method is for checking if we are in a Photon room or not
    /// We do this so that we can run this script both locally and remotely
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


    #region RPC Calls
    //One feature that sets PUN aside from other Photon packages is the support for "Remote Procedure Calls" (RPCs).
    //Remote Procedure Calls are exactly what the name implies: method-calls on remote clients in the same room.
    //For more info on RPC calls, check: https://doc.photonengine.com/en-us/pun/current/gameplay/rpcsandraiseevent

    [PunRPC]
    public void EnableFinalIKLocomotion_RPC()
    {
        EnableFinalIKLocomotion();
    }

    [PunRPC]
    public void DisableFinalIKLocomotion_RPC()
    {
        DisableFinalIKLocomotion();
    }
    #endregion
}
