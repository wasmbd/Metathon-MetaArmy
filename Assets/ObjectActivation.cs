using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivation : MonoBehaviour
{
    public float delayInSeconds = 3f;
    public GameObject targetObject;

    private void Start()
    {
        targetObject.SetActive(false);
        Invoke("EnableTargetObject", delayInSeconds);
    }

    private void EnableTargetObject()
    {
        targetObject.SetActive(true);
    }
}
