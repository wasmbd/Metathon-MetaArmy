using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorItem : MonoBehaviour
{
    Animator animator;
    bool isOpen = false;
    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Player"))
        {
            isOpen = !isOpen;
            animator.SetBool("isOpen", isOpen);
        }
    }
}
