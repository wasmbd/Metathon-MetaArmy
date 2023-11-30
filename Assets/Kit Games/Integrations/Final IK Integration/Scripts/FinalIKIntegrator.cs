using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalIKIntegrator : MonoBehaviour
{

    [SerializeField]
    FullbodyIKSolutionHolder fullbodyIKSolutionHolder;

    private void OnEnable()
    {
        MultiplayerVRConstants.USE_FINALIK = fullbodyIKSolutionHolder.useFinalIK;
        MultiplayerVRConstants.USE_FINALIK_UMA2 = fullbodyIKSolutionHolder.useFinalIK_UMA2;
    }


}
