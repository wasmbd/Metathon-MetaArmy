using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    /* public AudioSource clip;*/
    public Slider slider;
    public float sliderValue = 10;
    // Start is called before the first frame update
    public void Start()
    {
        /*  clip = GetComponent<AudioSource>();*/
        StartCoroutine(Countinng());
    }

    // Update is called once per frame
    public void Update()
    {
        /*        StartCoroutine(Countinng());*/
        slider.value = sliderValue;
        /*if (slider != null) { }*/
    }
    public IEnumerator Countinng()
    {
        /*yield return new WaitForSeconds(10);
        clip.Play();
        Debug.Log("timeOut");*/
        while (true)
        {
            yield return new WaitForSeconds(1);
            sliderValue++;
            //yield return new WaitForSeconds(1);
        }

    }
}
