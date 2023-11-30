using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    int currentStep = 0;
    public Image[] menuLines;
    public Image[] numCircles;
    public Text[] menuTitles;
    public Text[] menuNums;
    public GameObject[] ticks;
    public GameObject[] panels;
    public GameObject backButton;
    public Transform[] customizeToggles;
    public Transform[] customizeFemaleToggles;
    public AvatarZoomManager avatarZoom;

    public AvatarZoomOculus avatarZoomOculus;
    void Start()
    {
        UpdateUI();
    }
    public void NextStep()
    {
        currentStep++;
        UpdateUI();
    }
    public void PrevStep()
    {
        currentStep--;
        UpdateUI();
    }

    public void UpdateUI()
    {
        int i;
        if (backButton != null)
        {
            if (currentStep >= 1)
            {
                backButton.gameObject.SetActive(true);
                SetNextToggle(true);
            }
            else
            {
                backButton.gameObject.SetActive(false);
                SetNextToggle(false);

            }
        }
        else return;

        for (i = 0; i <= currentStep; i++)
        {
            if(i < currentStep)
            {
                ticks[i].gameObject.SetActive(true);
              //  backButton.gameObject.SetActive(true);
                Debug.Log(currentStep);
            
            }
          
            menuNums[i].color = new Color(1, 1, 1, 1f);
            menuTitles[i].color = new Color(1, 1, 1, 1f);
            numCircles[i].color = new Color(1, 1, 1, 1f);
            if(i > 0)
                menuLines[i - 1].color = new Color(1, 1, 1, 1f);
        }
        for(; i < menuNums.Length; i++)
        {
            menuNums[i].color = new Color(1, 1, 1, .4f);
            menuTitles[i].color = new Color(1, 1, 1, .4f);
            numCircles[i].color = new Color(1, 1, 1, .4f);
            if(i > 0)
                menuLines[i - 1].color = new Color(1, 1, 1, .4f);
        }
        for(i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[currentStep].SetActive(true);
    }
    public void OnClickToggle(Transform selected)
    {
        int index = selected.GetSiblingIndex() - 2;
        if(index > 0)
        {
            for(int i = 0; i < index; i++)
            {
                SetPreviousToggle(false);
            }
        }
        if(index < 0)
        {
            for(int i = 0; i < -index; i++)
            {
                SetNextToggle(false);
            }
        }
       
            if (customizeToggles[0].parent.GetChild(2).name == customizeToggles[1].name || customizeToggles[0].parent.GetChild(2).name == customizeToggles[2].name
            || customizeToggles[0].parent.GetChild(2).name == customizeToggles[4].name)
            {
                avatarZoom.isZoom = true;
            }
            else
            {
                avatarZoom.isZoom = false;
            }
       
    }    
    public void SetPreviousToggle(bool isSelect)
    {
        for(int i = 0; i < customizeToggles.Length; i++)
        {
            customizeToggles[i].gameObject.SetActive(true);
        }
        customizeToggles[0].parent.GetChild(0).SetSiblingIndex(4);
        customizeToggles[0].parent.GetChild(5).gameObject.SetActive(false);
        if(isSelect)
        {
            customizeToggles[0].parent.GetChild(2).GetComponent<Toggle>().isOn = true;
        }
    }
    public void SetNextToggle(bool isSelect)
    {
        for(int i = 0; i < customizeToggles.Length; i++)
        {
            customizeToggles[i].gameObject.SetActive(true);
        }
        customizeToggles[0].parent.GetChild(5).SetSiblingIndex(0);
        customizeToggles[0].parent.GetChild(5).gameObject.SetActive(false);
        if(isSelect)
        {
            customizeToggles[0].parent.GetChild(2).GetComponent<Toggle>().isOn = true;
        }
    }
    
    
    public void OnClickFemaleToggle(Transform selected)
    {
        int index = selected.GetSiblingIndex() - 2;
        if(index > 0)
        {
            for(int i = 0; i < index; i++)
            {
                SetPreviousFemaleToggle(false);
            }
        }
        if(index < 0)
        {
            for(int i = 0; i < -index; i++)
            {
                SetNextFemaleToggle(false);
            }
        }
        if(avatarZoom != null)
        {
            if(customizeFemaleToggles[0].parent.GetChild(2).name == customizeFemaleToggles[1].name || customizeFemaleToggles[0].parent.GetChild(2).name == customizeFemaleToggles[2].name
            || customizeFemaleToggles[0].parent.GetChild(2).name == customizeFemaleToggles[4].name)
            {
                avatarZoom.isZoom = true;
            }
            else
            {
                avatarZoom.isZoom = false;
            }
        }
    }    
    public void SetPreviousFemaleToggle(bool isSelect)
    {
        customizeFemaleToggles[0].parent.GetChild(0).SetSiblingIndex(4);
        if(isSelect)
        {
            customizeFemaleToggles[0].parent.GetChild(2).GetComponent<Toggle>().isOn = true;
        }
    }
    public void SetNextFemaleToggle(bool isSelect)
    {
        customizeFemaleToggles[0].parent.GetChild(4).SetSiblingIndex(0);
        if(isSelect)
        {
            customizeFemaleToggles[0].parent.GetChild(2).GetComponent<Toggle>().isOn = true;
        }
    }
}
