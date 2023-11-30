using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;

public class AvatarMenu : MonoBehaviour
{
    public GameObject femaleObj;
    public GameObject maleObj;
    public CharacterCreator character;
    public void OnClickAvatar()
    {
        femaleObj.SetActive(false);
        maleObj.SetActive(false);
        Debug.LogError(character.avatar.activeRace.name);
        if(character.avatar.activeRace.name == "HumanMaleDCS")
        {
            maleObj.SetActive(true);
        }
        if(character.avatar.activeRace.name == "HumanFemaleDCS")
        {
            femaleObj.SetActive(true);
        }
    }
}
