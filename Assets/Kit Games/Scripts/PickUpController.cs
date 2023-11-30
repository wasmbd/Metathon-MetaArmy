using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Transform holdArea;
    GameObject heldObj;
    Rigidbody heldObjRB;
    [SerializeField] float pickupRange = 5.0f;
    [SerializeField] float pickupForce = 10;
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(heldObj == null)
            {
            // Cast a ray from the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits;

                // Detect all colliders intersecting the ray
                hits = Physics.RaycastAll(ray);

                // Loop through the hits and do something with each collider
                foreach (RaycastHit hit in hits)
                {
                    // You can access hit.collider for the collider that was hit
                    // Here, we're just printing the name of the game object with the collider
                    if(hit.collider.gameObject.tag == "Interactable")
                    {
                        PickupObject(hit.collider.gameObject);
                        break;
                    }
                }
            }
            else
            {
                DropObject();
            }
        }
        if(heldObj != null)
        {
            MoveObject();
        }
        
    }
    
    void PickupObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
            
            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }
    
    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 8.0f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
        else
        {
            heldObjRB.velocity = Vector3.zero;
        }
    }
    
    void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        
        heldObjRB.transform.parent = null;
        heldObj = null;
    }
}
