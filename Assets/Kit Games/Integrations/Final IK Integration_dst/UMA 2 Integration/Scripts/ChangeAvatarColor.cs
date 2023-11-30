using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using System;

[Serializable]
public class ChangeAvatarColor
{

    [SerializeField] protected List<Material> colorList = new List<Material>();

    [SerializeField] protected int currentColorIndex;

    public void ChangeColor(bool isNext, DynamicCharacterAvatar avatar, string wardrobeSlot)
    {
       
        if (isNext)
        {
            currentColorIndex++;
        }
        else
        {
            currentColorIndex--;
        }
        currentColorIndex = Mathf.Clamp(currentColorIndex, 0, colorList.Count - 1);
        avatar.SetColor("Shirt", colorList[currentColorIndex].color);
        avatar.UpdateColors();



    }


}
