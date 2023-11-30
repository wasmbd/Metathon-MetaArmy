using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // Add this line to include the System.Linq namespace
using TMPro;
using VRUiKits.Utils;

public class ColorChangeScript : MonoBehaviour
{
    public static ColorChangeScript Instance;
    public Image[] whiteImages; // Assign the white images to this array in the inspector
    public TMP_Text[] textElements; // Assign the TextMeshPro components to this array in the inspector
    public Text[] normalTextElements; // Assign the regular Text components to this array in the inspector

    public bool isColorChanged = false;
    public Color originalImageColor = Color.white;
    public Color targetImageColor = Color.black;
    public Color originalTextColor = Color.black;
    public Color targetTextColor = Color.white;
    public Color originalNormalTextColor = Color.black; // Add this line
    public Color targetNormalTextColor = Color.white; // Add this line
    
    void Start()
    {
        Instance = this;
    }
    
    public Color GetImageColor()
    {
        if(isColorChanged)
            return targetImageColor;
        else
            return originalImageColor;
    }
    
    public Color GetTextColor()
    {
        if(isColorChanged)
            return targetTextColor;
        else
            return originalTextColor;
    }

    public void OnButtonClick()
    {
        isColorChanged = !isColorChanged;
        GrabManager.instance.ChangeColor();
        
        // Debugging statements
    if (CardListManager.instance != null)
    {
        for (int i = 0; i < CardListManager.instance.cardItems.Count; i++)
        {
            CardListManager.instance.cardItems[i].title.GetComponent<Text>().color = targetTextColor;
        }
    }

        // Change the color of the images
        for (int i = 0; i < whiteImages.Length; i++)
        {
            if (isColorChanged)
            {
                whiteImages[i].color = targetImageColor;
            }
            else
            {
                whiteImages[i].color = originalImageColor;
            }
        }

        // Change the color of the TextMeshPro elements
        for (int i = 0; i < textElements.Length; i++)
        {
            if (isColorChanged)
            {
                textElements[i].color = targetTextColor;
            }
            else
            {
                textElements[i].color = originalTextColor;
            }
        }

        // Change the color of the normal Text elements
        for (int i = 0; i < normalTextElements.Length; i++)
        {
            if (isColorChanged)
            {
                normalTextElements[i].color = targetNormalTextColor;
            }
            else
            {
                normalTextElements[i].color = originalNormalTextColor;
            }
        }
    }
}
