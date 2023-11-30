using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerIntController : MonoBehaviour
{
  private bool canMove = false;
    private bool canDelete = false;
    private Transform grabbedObject;
    private PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }
    void Update()
    {
        // if (pv.IsMine) // Only run on the local player's instance
        {
            // Check if the local player can interact
            if (canMove && Input.GetKey(KeyCode.M))
            {
                MoveObject();
            }
            else if (canDelete && Input.GetKeyDown(KeyCode.X))
            {
                DeleteObject();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a 3D object that can be interacted with
        // You might want to use tags or layers to identify interactable objects here.
        if (other.CompareTag("Interactable"))
        {
            grabbedObject = other.transform;
            canMove = true;
            canDelete = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset interaction when the player leaves the object's collider
        if (other.CompareTag("Interactable"))
        {
            canMove = false;
            canDelete = false;
        }
    }

   private void MoveObject()
{
    // Make sure an object is grabbed before attempting to move it
    if (grabbedObject != null)
    {
        // Calculate the movement direction based on player input (WASD or arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        // Calculate the movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        
        // Adjust the movement speed based on your preference
        float movementSpeed = 5.0f;
        
        // Translate the grabbed object's position based on the movement vector
        grabbedObject.Translate(movement * movementSpeed * Time.deltaTime);
    }
}

private void DeleteObject()
{
    // Make sure an object is grabbed before attempting to delete it
    if (grabbedObject != null)
    {
        // If you're using Photon's networking, you can call a method to delete the object across the network
        // pv.RPC("DeleteObjectRPC", RpcTarget.AllBuffered, grabbedObject.gameObject.GetPhotonView().ViewID);
        Destroy(grabbedObject.gameObject);
        
        // After deleting, release the reference to the grabbed object
        grabbedObject = null;
    }
}

[PunRPC]
private void DeleteObjectRPC(int viewID)
{
    // Find the object with the given view ID
    PhotonView pvToDelete = PhotonView.Find(viewID);
    
    if (pvToDelete != null)
    {
        // Delete the object locally
        PhotonNetwork.Destroy(pvToDelete.gameObject);
    }
}

}