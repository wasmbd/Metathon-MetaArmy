using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using System.IO;
using Photon.Pun;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{
    public DynamicCharacterAvatar avatar;

    public ChangeAvatarItem ChangeAvatarItemForHair;
    public ChangeAvatarItem ChangeAvatarItemForChest;
    public ChangeAvatarItem ChangeAvatarItemForFeet;
    public ChangeAvatarColor ChangeAvatarSkinColor;
    public ChangeAvatarColor ChangeAvatarChestColor;
    public ChangeAvatarItem ChangeAvatarItemForEyes;
    public ChangeAvatarItem ChangeAvatarItemForExpression;
    public ChangeAvatarItem ChangeAvatarItemForBeard; // New variable for beard
    public ChangeAvatarItem ChangeAvatarItemForBody; // New variable for body
    public ChangeAvatarItem ChangeAvatarItemForHelmet; // New variable for helmet

    public UMAWardrobeRecipe[] male;
    public UMAWardrobeRecipe[] male1;
    public UMAWardrobeRecipe[] female;
    public UMAWardrobeRecipe[] female1;

    public enum WardrobeSlots { Hair, Chest, Legs, Feet, Beard, Body, Helmet }

    public void SwitchGender(int index)
    {
        if ((index == 1 || index == 2))
        {
            avatar.ChangeRace("HumanMaleDCS");
            if (index == 1)
            {
                // // Change eye slot for male character
                // ChangeAvatarItemForEyes.ChangeItem(false, avatar, true);
                // // Change expression slot for male character
                // ChangeAvatarItemForExpression.ChangeItem(false, avatar, true);
                // // Change beard slot for male character
                // ChangeAvatarItemForBeard.ChangeItem(false, avatar, true);
                avatar.ClearSlots();
                foreach (UMAWardrobeRecipe recipe in male1)
                {
                    avatar.SetSlot(recipe);
                }
                avatar.BuildCharacter();
            }
            else
            {
                avatar.ClearSlots();
                foreach (UMAWardrobeRecipe recipe in male)
                {
                    avatar.SetSlot(recipe);
                }
                avatar.BuildCharacter();
            }
        }

        if ((index == 3 || index == 4))
        {
            avatar.ChangeRace("HumanFemaleDCS");
            if (index == 3)
            {
                // // Change eye slot for female character
                // ChangeAvatarItemForEyes.ChangeItem(false, avatar, true);
                // // Change expression slot for female character
                // ChangeAvatarItemForExpression.ChangeItem(false, avatar, true);
                // // Change beard slot for female character
                // ChangeAvatarItemForBeard.ChangeItem(false, avatar, true);
                avatar.ClearSlots();
                foreach (UMAWardrobeRecipe recipe in female1)
                {
                    avatar.SetSlot(recipe);
                }
                avatar.BuildCharacter();
            }
            else
            {
                avatar.ClearSlots();
                foreach (UMAWardrobeRecipe recipe in female)
                {
                    avatar.SetSlot(recipe);
                }
                avatar.BuildCharacter();
            }
        }
    }

    public void ChangeHair(int index)
    {
        ChangeAvatarItemForHair.ChangeItem(index, avatar);
    }

    public void ChangeChest(int index)
    {
        ChangeAvatarItemForChest.ChangeItem(index, avatar);
    }

    public void ChangeFeet(int index)
    {
        ChangeAvatarItemForFeet.ChangeItem(index, avatar);
    }

    public void ChangeSkinColor(Image colorImg)
    {
        // ChangeAvatarSkinColor.ChangeColor(index,avatar);
        avatar.characterColors.SetColor("Skin", colorImg.color);
        avatar.BuildCharacter();

        PlayerPrefs.SetString("skincolor", ColorUtility.ToHtmlStringRGBA(colorImg.color));
    }

    public void ChangeShirtColor(bool isNext)
    {
        ChangeAvatarSkinColor.ChangeColor(isNext, avatar, "Shirt");
    }

    public void ChangeEyes(Image colorImg)
    {
        // ChangeAvatarItemForEyes.ChangeItem(index, avatar);
        avatar.characterColors.SetColor("Eyes", colorImg.color);
        avatar.BuildCharacter();

        PlayerPrefs.SetString("eyescolor", ColorUtility.ToHtmlStringRGBA(colorImg.color));
    }

    public void ChangeExpression(int index)
    {
        ChangeAvatarItemForExpression.ChangeItem(index, avatar);
    }

    public void ChangeBeard(int index)
    {
        ChangeAvatarItemForBeard.ChangeItem(index, avatar);
    }

    public void ChangeBody(int index)
    {
        ChangeAvatarItemForBody.ChangeItem(index, avatar);
    }

    public void ChangeHelmet(int index)
    {
        ChangeAvatarItemForHelmet.ChangeItem(index, avatar);
    }
}
