using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPanelManager : MonoBehaviour
{
    public GameObject[] buttons;
    int selectedIdx;
    public UIManager uiManager;
    public void SelectRoom(int index)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        buttons[index].SetActive(true);
        selectedIdx = index;
    }
    public void JoinRoom()
    {
        uiManager.RoomManagerScriptRef.JoinOrCreateRoom(selectedIdx);
    }
}
