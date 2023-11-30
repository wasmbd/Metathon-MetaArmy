using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using System;

[Serializable]
public class ChangeAvatarItem
{
    public enum WardrobeSlots { Hair, Chest, Legs, Feet, Beard, Body, Helmet }
    [SerializeField] protected WardrobeSlots wardrobeSlot;

    [SerializeField] protected List<string> modelsMale = new List<string>();
    [SerializeField] protected List<string> modelsFemale = new List<string>();

    [SerializeField] protected int currentIndexMale;
    [SerializeField] protected int currentIndexFemale;
    public void ChangeItem(int index, DynamicCharacterAvatar avatar)
    {
        if (index == -1)
        {
            avatar.ClearSlot(Enum.GetName(typeof(WardrobeSlots), wardrobeSlot));
        }
        else
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentIndexFemale = index;
                currentIndexMale = index;
                currentIndexMale = Mathf.Clamp(currentIndexMale, 0, modelsMale.Count - 1);
                if (currentIndexMale != -1 && modelsMale.Count != 0)
                {
                    if (modelsMale[currentIndexMale] == "None")
                    {

                        avatar.ClearSlot(Enum.GetName(typeof(WardrobeSlots), wardrobeSlot));
                    }
                    else

                    {

                        avatar.SetSlot((Enum.GetName(typeof(WardrobeSlots), wardrobeSlot)), modelsMale[currentIndexMale]);
                    }
                }

            }
            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentIndexFemale = index;
                currentIndexMale = index;
                currentIndexFemale = Mathf.Clamp(currentIndexFemale, 0, modelsFemale.Count - 1);

                if (currentIndexFemale != -1 && modelsFemale.Count != 0)
                {
                    if (modelsFemale[currentIndexFemale] == "None")
                    {

                        avatar.ClearSlot(Enum.GetName(typeof(WardrobeSlots), wardrobeSlot));
                    }
                    else
                    {
                        avatar.SetSlot((Enum.GetName(typeof(WardrobeSlots), wardrobeSlot)), modelsFemale[currentIndexFemale]);
                    }
                }
            }
        }

        if (wardrobeSlot == WardrobeSlots.Helmet)
        {
            avatar.ClearSlot(Enum.GetName(typeof(WardrobeSlots), WardrobeSlots.Hair));
        }
        if (wardrobeSlot == WardrobeSlots.Hair)
        {
            avatar.ClearSlot(Enum.GetName(typeof(WardrobeSlots), WardrobeSlots.Helmet));
        }
        avatar.BuildCharacter();
    }
}
