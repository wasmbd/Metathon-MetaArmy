using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using Photon.Pun;
using IRONHEADGames;
using RootMotion;

public class MultiplayerVRPlayerSynchronization_FinalIK : MonoBehaviourPun, IPunObservable
{
    #region All

    [Tooltip("Root of the VR camera rig")] public GameObject xrRig;
    [Tooltip("The VRIK component.")] public VRIK ik;

    // NetworkTransforms are network snapshots of Transform position, rotation, velocity and angular velocity
    private NetworkTransform rootNetworkT = new NetworkTransform();
    private NetworkTransform headNetworkT = new NetworkTransform();
    private NetworkTransform leftHandNetworkT = new NetworkTransform();
    private NetworkTransform rightHandNetworkT = new NetworkTransform();

    //Main VRPlayer Transform Synch
    [Header("Main VRPlayer Transform Synch")]
    public Transform generalVRPlayerTransform;

    //Position
    private float m_Distance_GeneralVRPlayer;
    private Vector3 m_Direction_GeneralVRPlayer;
    private Vector3 m_NetworkPosition_GeneralVRPlayer;
    private Vector3 m_StoredPosition_GeneralVRPlayer;

    //Rotation
    private Quaternion m_NetworkRotation_GeneralVRPlayer;
    private float m_Angle_GeneralVRPlayer;


    //Main Avatar Transform Synch
    [Header("Main Avatar Transform Synch")]
    public Transform mainAvatarTransform;



    //Position
    private float m_Distance_MainAvatar;
    private Vector3 m_Direction_MainAvatar;
    private Vector3 m_NetworkPosition_MainAvatar;
    private Vector3 m_StoredPosition_MainAvatar;

    //Rotation
    private Quaternion m_NetworkRotation_MainAvatar;
    private float m_Angle_MainAvatar;

    bool m_firstTake = false;


    private void Awake()
    {
        //Main VRPlayer Synch Init
        m_StoredPosition_GeneralVRPlayer = generalVRPlayerTransform.position;
        m_NetworkPosition_GeneralVRPlayer = Vector3.zero;
        m_NetworkRotation_GeneralVRPlayer = Quaternion.identity;

        //Main Avatar Synch Init
        m_StoredPosition_MainAvatar = mainAvatarTransform.localPosition;
        m_NetworkPosition_MainAvatar = Vector3.zero;
        m_NetworkRotation_MainAvatar = Quaternion.identity;
    }

    void OnEnable()
    {
        m_firstTake = true;
    }


    private void Start()
    {
        // Initiation
        if (photonView.IsMine)
        {
            InitiateLocal();
        }
        else
        {
            InitiateRemote();
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            UpdateLocal();
        }
        else
        {

            UpdateRemote();
        }
    }

    // Sync NetworkTransforms
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            #region General VR Player Transform Synch
            //////////////////////////////////////////////////////////////////
            //General VRPlayer Transform Synch
            //Send Main Avatar position data
            this.m_Direction_GeneralVRPlayer = generalVRPlayerTransform.position - this.m_StoredPosition_GeneralVRPlayer;
            this.m_StoredPosition_GeneralVRPlayer = generalVRPlayerTransform.position;

            stream.SendNext(generalVRPlayerTransform.position);
            stream.SendNext(this.m_Direction_GeneralVRPlayer);

            //Send Main Avatar rotation data
            stream.SendNext(generalVRPlayerTransform.rotation);

            #endregion

            #region Main Avatar Transform Synch
            //////////////////////////////////////////////////////////////////
            //Main Avatar Transform Synch
            //Send Main Avatar position data
            this.m_Direction_MainAvatar = mainAvatarTransform.localPosition - this.m_StoredPosition_MainAvatar;
            this.m_StoredPosition_MainAvatar = mainAvatarTransform.localPosition;

            stream.SendNext(mainAvatarTransform.localPosition);
            stream.SendNext(this.m_Direction_MainAvatar);

            //Send Main Avatar rotation data
            stream.SendNext(mainAvatarTransform.localRotation);
            #endregion

            // Send NetworkTransform data
            rootNetworkT.Send(stream);
            headNetworkT.Send(stream);
            leftHandNetworkT.Send(stream);
            rightHandNetworkT.Send(stream);


        }
        else
        {
            #region General VR Player Transform Synch
            ///////////////////////////////////////////////////////////////////
            //Ganeral VR Player Transform Synch

            //Get VR Player position data
            this.m_NetworkPosition_GeneralVRPlayer = (Vector3)stream.ReceiveNext();
            this.m_Direction_GeneralVRPlayer = (Vector3)stream.ReceiveNext();

            if (m_firstTake)
            {
                generalVRPlayerTransform.position = this.m_NetworkPosition_GeneralVRPlayer;
                this.m_Distance_GeneralVRPlayer = 0f;
            }
            else
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                this.m_NetworkPosition_GeneralVRPlayer += this.m_Direction_GeneralVRPlayer * lag;
                this.m_Distance_GeneralVRPlayer = Vector3.Distance(generalVRPlayerTransform.position, this.m_NetworkPosition_GeneralVRPlayer);
            }

            //Get Main Avatar rotation data
            this.m_NetworkRotation_GeneralVRPlayer = (Quaternion)stream.ReceiveNext();
            if (m_firstTake)
            {
                this.m_Angle_GeneralVRPlayer = 0f;
                generalVRPlayerTransform.rotation = this.m_NetworkRotation_GeneralVRPlayer;
            }
            else
            {
                this.m_Angle_GeneralVRPlayer = Quaternion.Angle(generalVRPlayerTransform.rotation, this.m_NetworkRotation_GeneralVRPlayer);
            }
            #endregion

            #region Main Avatar Transform Synch
            ///////////////////////////////////////////////////////////////////
            //Main Avatar Transform Synch

            //Get Main Avatar position data
            this.m_NetworkPosition_MainAvatar = (Vector3)stream.ReceiveNext();
            this.m_Direction_MainAvatar = (Vector3)stream.ReceiveNext();

            if (m_firstTake)
            {
                mainAvatarTransform.localPosition = this.m_NetworkPosition_MainAvatar;
                this.m_Distance_MainAvatar = 0f;
            }
            else
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                this.m_NetworkPosition_MainAvatar += this.m_Direction_MainAvatar * lag;
                this.m_Distance_MainAvatar = Vector3.Distance(mainAvatarTransform.localPosition, this.m_NetworkPosition_MainAvatar);
            }

            //Get Main Avatar rotation data
            this.m_NetworkRotation_MainAvatar = (Quaternion)stream.ReceiveNext();
            if (m_firstTake)
            {
                this.m_Angle_MainAvatar = 0f;
                mainAvatarTransform.rotation = this.m_NetworkRotation_MainAvatar;
            }
            else
            {
                this.m_Angle_MainAvatar = Quaternion.Angle(mainAvatarTransform.rotation, this.m_NetworkRotation_MainAvatar);
            }
            #endregion

            // Receive NetworkTransform data
            rootNetworkT.Receive(stream);
            headNetworkT.Receive(stream);
            leftHandNetworkT.Receive(stream);
            rightHandNetworkT.Receive(stream);

            if (m_firstTake)
            {
                m_firstTake = false;
            }
        }
    }

    #endregion All

    // Code that runs only for local instances of this player
    #region Local

    [LargeHeader("Calibration")]
    [Header("Head")]
    [Tooltip("HMD.")] public Transform centerEyeAnchor;
    [Tooltip("Position offset of the camera from the head bone (root space).")] public Vector3 headAnchorPositionOffset;
    [Tooltip("Rotation offset of the camera from the head bone (root space).")] public Vector3 headAnchorRotationOffset;

    [Header("Hands")]
    [Tooltip("Left Hand Controller")] public Transform leftHandAnchor;
    [Tooltip("Right Hand Controller")] public Transform rightHandAnchor;
    [Tooltip("Position offset of the hand controller from the hand bone (controller space).")] public Vector3 handAnchorPositionOffset;
    [Tooltip("Rotation offset of the hand controller from the hand bone (controller space).")] public Vector3 handAnchorRotationOffset;

    [Header("Scale")]
    [Tooltip("Multiplies the scale of the root.")] public float scaleMlp = 1f;
    public bool fixedScale = true;

    [Header("Data stored by Calibration")]
    public VRIKCalibrator.CalibrationData data = new VRIKCalibrator.CalibrationData();

    private void InitiateLocal()
    {
        xrRig.SetActive(true);

        // Calibrate the character
        data = VRIKCalibrator.Calibrate(ik, centerEyeAnchor, leftHandAnchor, rightHandAnchor, headAnchorPositionOffset, headAnchorRotationOffset, handAnchorPositionOffset, handAnchorRotationOffset, scaleMlp);

        if (fixedScale)
        {
            ik.references.root.localScale = scaleMlp * Vector3.one;
            data.scale = scaleMlp;
        }
    }

    private void UpdateLocal()
    {
        // Update IK target velocities (for interpolation)
        rootNetworkT.ReadVelocitiesLocal(ik.references.root);
        headNetworkT.ReadVelocitiesLocal(ik.solver.spine.headTarget);
        leftHandNetworkT.ReadVelocitiesLocal(ik.solver.leftArm.target);
        rightHandNetworkT.ReadVelocitiesLocal(ik.solver.rightArm.target);

        // Update IK target positions/rotations
        rootNetworkT.ReadTransformLocal(ik.references.root);
        headNetworkT.ReadTransformLocal(ik.solver.spine.headTarget);
        leftHandNetworkT.ReadTransformLocal(ik.solver.leftArm.target);
        rightHandNetworkT.ReadTransformLocal(ik.solver.rightArm.target);
    }

    #endregion Local

    // Code that runs only for remote instances of this player
    #region Remote

    [LargeHeader("Remote")]
    [Tooltip("The speed of interpolating remote IK targets.")] public float proxyInterpolationSpeed = 20f;
    [Tooltip("Max interpolation error square magnitude. IK targets snap to latest synced position if current interpolated position is farther than that.")] public float proxyMaxErrorSqrMag = 4f;
   

    private Transform headIKProxy;
    private Transform leftHandIKProxy;
    private Transform rightHandIKProxy;

    private void InitiateRemote()
    {
        // Remote instance does not have a VR rig, so we use proxies for them. Positions and rotations of proxies are synced via NetworkTransforms
        xrRig.SetActive(false);

        // Ceate IK target proxies
        var proxyRoot = new GameObject("IK Proxies").transform;
        proxyRoot.parent = transform;
        proxyRoot.localPosition = Vector3.zero;
        proxyRoot.localRotation = Quaternion.identity;

        headIKProxy = new GameObject("Head IK Proxy").transform;
        headIKProxy.parent = transform;
        headIKProxy.position = ik.references.head.position;
        headIKProxy.rotation = ik.references.head.rotation;

        leftHandIKProxy = new GameObject("Left Hand IK Proxy").transform;
        leftHandIKProxy.parent = transform;
        leftHandIKProxy.position = ik.references.leftHand.position;
        leftHandIKProxy.rotation = ik.references.leftHand.rotation;

        rightHandIKProxy = new GameObject("Right Hand IK Proxy").transform;
        rightHandIKProxy.parent = transform;
        rightHandIKProxy.position = ik.references.rightHand.position;
        rightHandIKProxy.rotation = ik.references.rightHand.rotation;

        // Assign proxies as IK targets for the remote instance
        ik.solver.spine.headTarget = headIKProxy;
        ik.solver.leftArm.target = leftHandIKProxy;
        ik.solver.rightArm.target = rightHandIKProxy;

        ////Also, assign the head target to VR Animator Controller
        ////Because the XR Rig for remote players are disabled
        ////But VR Animator Controller needs the head speed to run the walk animation.
        //ik.GetComponent<LocomotionSwitcher_FinalIK>().headTarget = headIKProxy;
        
        
    }

    private void UpdateRemote()
    {
        generalVRPlayerTransform.position = Vector3.MoveTowards(generalVRPlayerTransform.position, this.m_NetworkPosition_GeneralVRPlayer, this.m_Distance_GeneralVRPlayer * (1.0f / PhotonNetwork.SerializationRate));
        generalVRPlayerTransform.rotation = Quaternion.RotateTowards(generalVRPlayerTransform.rotation, this.m_NetworkRotation_GeneralVRPlayer, this.m_Angle_GeneralVRPlayer * (1.0f / PhotonNetwork.SerializationRate));

        //mainAvatarTransform.localPosition = Vector3.MoveTowards(mainAvatarTransform.localPosition, this.m_NetworkPosition_MainAvatar, this.m_Distance_MainAvatar * (1.0f / PhotonNetwork.SerializationRate));
        //mainAvatarTransform.localRotation = Quaternion.RotateTowards(mainAvatarTransform.localRotation, this.m_NetworkRotation_MainAvatar, this.m_Angle_MainAvatar * (1.0f / PhotonNetwork.SerializationRate));

        // Apply synced position/rotations to proxies
        if (ik.solver.locomotion.weight <= 0) rootNetworkT.ApplyRemoteInterpolated(ik.references.root, proxyInterpolationSpeed, proxyMaxErrorSqrMag); // Only sync root when using animated locomotion. Procedural locomotion follows head IK proxy anyway
        headNetworkT.ApplyRemoteInterpolated(headIKProxy, proxyInterpolationSpeed, proxyMaxErrorSqrMag);
        leftHandNetworkT.ApplyRemoteInterpolated(leftHandIKProxy, proxyInterpolationSpeed, proxyMaxErrorSqrMag);
        rightHandNetworkT.ApplyRemoteInterpolated(rightHandIKProxy, proxyInterpolationSpeed, proxyMaxErrorSqrMag);
    }

    #endregion Remote


}
