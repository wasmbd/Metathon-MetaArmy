using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
public class MovementController_FinalIK : MonoBehaviour
{
    public float speed = 2.0f;
    public float speedThresholdForActivatingVRWalkingAnim = 1.5f;

    public List<XRController> controllers;

    public GameObject head = null;

    [SerializeField]
    LocomotionSwitcher_FinalIK locomotionSwitcher_FinalIK;

    PhotonView _photonView;


    private Vector3 lastPos;
    bool finalIKEnabled = false;

    #region Unity Methods
    private void Awake()
    {
        if (CheckIfConnectedAndInRoom())
        {
            _photonView = GetComponent<PhotonView>();
        }
        else
        {
            Debug.Log("We are not in a Photon room.");
        }
    }

    private void Start()
    {
        lastPos = transform.position;
        finalIKEnabled = true;
    }

    private void Update()
    {
        int movesSpeed = Mathf.RoundToInt(Vector3.Distance(transform.position, lastPos) / Time.fixedDeltaTime);
        lastPos = transform.position;
        //Debug.Log(movesSpeed);
        if (movesSpeed < 0.25 && !finalIKEnabled)
        {
            if (CheckIfConnectedAndInRoom())
            {
                //If we are in a Photon room, we synchronize this operation.
                _photonView.RPC("EnableFinalIKLocomotion_RPC", RpcTarget.All);
            }
            else
            {
                //If we are not in Photon room, we just locally Enable Final IK Locomotion.
                locomotionSwitcher_FinalIK.EnableFinalIKLocomotion();
            }
            finalIKEnabled = true;
        }
        else if (movesSpeed > 1.5)
        {
            finalIKEnabled = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (XRController xRController in controllers)
        {
            if (xRController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 positionVector))
            {
                if (positionVector.magnitude > 0.20f)
                {

                    Move(positionVector);
                }
               
               
            }
            else
            {
                //If we are not moving with joystick, 
                //Then, we should procedurally move with Final IK's default locomotion
                locomotionSwitcher_FinalIK.EnableFinalIKLocomotion();
            }
        }
    }
    #endregion


    #region Private Methods
    private void Move(Vector2 positionVector)
    {
        // Apply the touch position to the head's forward Vector
        Vector3 direction = new Vector3(positionVector.x, 0, positionVector.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        // Rotate the input direction by the horizontal head rotation
        direction = Quaternion.Euler(headRotation) * direction;

        // Apply speed and move
        Vector3 movement = direction * speed;

        //We should check if the movement is big enough to switch to walking animation
        if (movement.magnitude > speedThresholdForActivatingVRWalkingAnim)
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
                locomotionSwitcher_FinalIK.DisableFinalIKLocomotion();
            }
        }
        else
        {
            if (CheckIfConnectedAndInRoom())
            {
                //If we are in a Photon room, we synchronize this operation.
                _photonView.RPC("EnableFinalIKLocomotion_RPC", RpcTarget.All);
            }
            else
            {
                //If we are not in Photon room, we just locally Enable Final IK Locomotion.
                locomotionSwitcher_FinalIK.EnableFinalIKLocomotion();
            }
        }

        //This is the main code line that moves the main VR player.
        transform.position += (Vector3.ProjectOnPlane(Time.deltaTime * movement, Vector3.up));
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