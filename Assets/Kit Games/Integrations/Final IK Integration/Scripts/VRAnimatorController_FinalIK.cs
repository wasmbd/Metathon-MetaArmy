using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using IRONHEADGames;
using Photon.Pun;

public class VRAnimatorController_FinalIK : MonoBehaviour
{
   

    public float speedTreshold = 0.1f;
    [Range(0, 1)]
    public float smoothing = 1;

    

    [SerializeField]
    private Animator animator;


    bool finalIKEnabled = false;
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();

        }
        // headTarget = MainAvatarGameobject.GetComponent<VRIK>().solver.spine.headTarget;
      
        
    }

    

    // Update is called once per frame
    void Update()
    {
        //if (_photonView.IsMine)
        //{
            //Compute speed
            //Vector3 headsetSpeed = (headTarget.position - previousPos) / Time.deltaTime;
            //headsetSpeed.y = 0;

           

            ////Local speed
            //Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
            //previousPos = headTarget.position;

            //Set Animator Values
            //float previousDirectionX = animator.GetFloat("DirectionX");
            //float previousDirectionY = animator.GetFloat("DirectionY");

            //animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedTreshold);
            //animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
            //animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));

        //}

    }

    public void ProcessAnimation(Vector3 animationSpeedVector)
    {
        //Set Animator Values
        float previousDirectionX = animator.GetFloat("DirectionX");
        float previousDirectionY = animator.GetFloat("DirectionY");

        animator.SetBool("isMoving", animationSpeedVector.magnitude > speedTreshold);
        animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(animationSpeedVector.x, -1, 1), smoothing));
        animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(animationSpeedVector.z, -1, 1), smoothing));
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
}
