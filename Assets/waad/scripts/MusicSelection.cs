using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelection : MonoBehaviour
{
 /*   public GameObject music1Obj;
    public GameObject music2Obj;
    public GameObject music3Obj;
    public GameObject music4Obj;*/
    public AudioSource music1;
    public AudioSource music2;
    public AudioSource music3;
    public AudioSource music4;
    public AudioSource music5;
    public AudioSource music6;

    // Start is called before the first frame update
    void Start()
    {
       
       // music1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropDownFunctioning(int value)
    {
     
        if (value == 0)
        {
         /*   music1Obj.SetActive(true);
            music2Obj.SetActive(false);
            music3Obj.SetActive(false);
            music4Obj.SetActive(false);*/
            music2.Stop();
            music3.Stop();
            music4.Stop();
            music5.Stop();
            music6.Stop();
        }
        if (value == 1)
        {
          /*  music1Obj.SetActive(true);*/
            music1.Play();
            music2.Stop();
            music3.Stop();
            music4.Stop();
            music5.Stop();
            music6.Stop();
        }
        if (value == 2)
        {
         /*   music2Obj.SetActive(true);*/
            music1.Stop();
            music3.Stop();
            music4.Stop();
            music5.Stop();
            music6.Stop();
            music2.Play();
            
        }
        if (value == 3)
        {
          /*  music3Obj.SetActive(true);*/
            music1.Stop();
            music2.Stop();
            music4.Stop();
            music5.Stop();
            music6.Stop();
            music3.Play();
        }
        if (value == 4)
        {
           /* music4Obj.SetActive(true);*/
            music1.Stop();
            music2.Stop();
            music3.Stop();
            music5.Stop();
            music6.Stop();
            music4.Play();
        }
        if (value == 5)
        {
            /* music4Obj.SetActive(true);*/
            music1.Stop();
            music2.Stop();
            music3.Stop();
            music4.Stop();
            music6.Stop();
            music5.Play();
        }
        if (value == 6)
        {
            /* music4Obj.SetActive(true);*/
            music1.Stop();
            music2.Stop();
            music3.Stop();
            music4.Stop();
            music5.Stop();
            music6.Play();
        }

    }
}
