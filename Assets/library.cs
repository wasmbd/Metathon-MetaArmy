using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class library : MonoBehaviour
{/* public GameObject music1Obj;
    public GameObject music2Obj;
    public GameObject music3Obj;
    public GameObject music4Obj;*/
    /* public AudioSource music1;
     public AudioSource music2;
     public AudioSource music3;
     public AudioSource music4;
     public AudioSource music5;
     public AudioSource basicBG;*/

    //public GameObject objectPos;//dragcube
    public Rigidbody objectBody;//dragplayer

    // Start is called before the first frame update
    void Start()
    {
       // objectPos.GetComponent<Transform>();
        objectBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropDownFunctioning(int value)
    {


        Vector3 movement = new Vector3(0f, 0f, 0f);
        movement.Normalize();
        objectBody.MovePosition(movement);
        if (value == 0)
        {
            /*            SceneManager.LoadScene(4);
             *            
            */
           // objectPos.transform.AddComponent<Transform.>();
        }
        if (value == 1)
        {
            SceneManager.LoadScene(4);
            objectBody.MovePosition(movement);
        }
        if (value == 2)
        {
            SceneManager.LoadScene(4);
            objectBody.MovePosition(movement);
        }
        if (value == 3)
        {
            SceneManager.LoadScene(4);
            objectBody.MovePosition(movement);

        }
        if (value == 4)
        {
            SceneManager.LoadScene(4);
            objectBody.MovePosition(movement);

        }
       

    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(4);
        }
    }*/
}
