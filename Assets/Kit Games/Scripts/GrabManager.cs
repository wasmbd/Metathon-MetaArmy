using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRUiKits.Utils;
using Photon.Pun;
using MiniJSON;
using System.Linq;
using UnityEngine.XR;
public class GrabManager : MonoBehaviour
{
    public static GrabManager instance;
    public GameObject[] items;
    public Transform mainCameraTransform; // Reference to the main camera

     public Transform mainCameraTransformVR; //FOR VR
    public Transform canvasTransform;

    private GameObject grabbedObject; // Reference to the currently grabbed object
    public List<Dictionary<string, object>> grabbedObjList = new List<Dictionary<string, object>>();
    public CardListManager cardListManager;

    public CardListManager cardListManagerVR;

    void Start()
    {
        instance = this;
        Invoke("LoadData", 1f);
    }
    public void ChangeColor()
    {
        for(int j = 0; j < grabbedObjList.Count; j++)
        {
            GameObject obj = grabbedObjList[j]["obj"] as GameObject;
            Image[] imgs = obj.GetComponentsInChildren<Image>();
            Text[] texts = obj.GetComponentsInChildren<Text>();
            for(int i = 0; i < imgs.Length; i++)
            {
                if(!imgs[i].transform.name.Contains("emoji"))
                {
                    imgs[i].color = ColorChangeScript.Instance.GetImageColor();
                }
            }
            for(int i = 0; i < texts.Length; i++)
            {
                texts[i].color = ColorChangeScript.Instance.GetTextColor();
            }
        }
    }
    public Vector3 ConvertStringToVector3(string vector3String)
    {
        // Remove any unnecessary characters and split the string into components
        vector3String = vector3String.Replace("(", "").Replace(")", "");
        string[] components = vector3String.Split(',');

        // Ensure we have exactly three components
        if (components.Length == 3 && float.TryParse(components[0], out float x) && float.TryParse(components[1], out float y) && float.TryParse(components[2], out float z))
        {
            return new Vector3(x, y, z);
        }

        // Return Vector3.zero if the conversion fails
        return Vector3.zero;
    }
    void LoadData()
    {
        List<object> listData = Json.Deserialize(PlayerPrefs.GetString("school", "[]")) as List<object>;
        for(int i = 0; i < listData.Count; i++)
        {
            Dictionary<string, object> dicData = listData[i] as Dictionary<string, object>;
            GameObject obj = new GameObject();
            
            if(dicData["type"].ToString() == "3D")
                obj = Instantiate(cardListManager.cardList[int.Parse(dicData["index"].ToString())].prefab);
            else
            {
                obj = Instantiate(cardListManager.uiCardList[int.Parse(dicData["index"].ToString())].prefab, canvasTransform);
            }
            Dictionary<string, object> objData = new Dictionary<string, object>();
            objData["position"] = obj.transform.position;
            objData["obj"] = obj;
            objData["type"] = dicData["type"];
            objData["index"] = dicData["index"];
            grabbedObjList.Add(objData);
        }
    }
    void SaveData()
    {
        List<object> listData = new List<object>();
        for (int i = 0; i < grabbedObjList.Count; i++)
        {
            Dictionary<string, object> dicData = grabbedObjList[i] as Dictionary<string, object>;
            Dictionary<string, object> objData = new Dictionary<string, object>();
            objData["position"] = (dicData["obj"] as GameObject).transform.position;
            objData["type"] = dicData["type"];
            objData["index"] = dicData["index"];
            listData.Add(objData);
        }
        PlayerPrefs.SetString("school", Json.Serialize(listData));
    }

    public void CreateItem(Card card)
    {
        GameObject obj = null;
        string loadedDeviceName = XRSettings.loadedDeviceName;

         if (loadedDeviceName.Contains("Oculus") || Application.platform == RuntimePlatform.Android)
        {
if(card.type == "3D")
        {
            Debug.LogError(mainCameraTransformVR.name);
            obj = Instantiate(card.prefab);
            obj.transform.position = mainCameraTransformVR.position + mainCameraTransformVR.forward * 2f;
        }
        else
        {
            //Un-Comment if you want into the ui canvas
                       // obj = Instantiate(card.prefab, canvasTransform);

            //USE THE SAME METHOD 

             Debug.LogError(mainCameraTransformVR.name);
            obj = Instantiate(card.prefab);
            obj.transform.position = mainCameraTransformVR.position + mainCameraTransformVR.forward * 2f;
        }
        Dictionary<string, object> dicData = new Dictionary<string, object>();
        // Image[] imgs = obj.GetComponentsInChildren<Image>();
        // Text[] texts = obj.GetComponentsInChildren<Text>();
        // for(int i = 0; i < imgs.Length; i++)
        // {
        //     imgs[i].color = ColorChangeScript.Instance.GetImageColor();
        // }
        // for(int i = 0; i < texts.Length; i++)
        // {
        //     texts[i].color = ColorChangeScript.Instance.GetTextColor();
        // }
        
        dicData["type"] = card.type;
        dicData["index"] = card.index;
        dicData["obj"] = obj;
        grabbedObjList.Add(dicData);
        SaveData();
        }else{
        if(card.type == "3D")
        {
            Debug.LogError(mainCameraTransform.name);
            obj = Instantiate(card.prefab);
            obj.transform.position = mainCameraTransform.position + mainCameraTransform.forward * 2f;
        }
        else
        {
            //Un-Comment if you want into the ui canvas
                       // obj = Instantiate(card.prefab, canvasTransform);

            //USE THE SAME METHOD 

             Debug.LogError(mainCameraTransform.name);
            obj = Instantiate(card.prefab);
            obj.transform.position = mainCameraTransform.position + mainCameraTransform.forward * 2f;
        }
        Dictionary<string, object> dicData = new Dictionary<string, object>();
        // Image[] imgs = obj.GetComponentsInChildren<Image>();
        // Text[] texts = obj.GetComponentsInChildren<Text>();
        // for(int i = 0; i < imgs.Length; i++)
        // {
        //     imgs[i].color = ColorChangeScript.Instance.GetImageColor();
        // }
        // for(int i = 0; i < texts.Length; i++)
        // {
        //     texts[i].color = ColorChangeScript.Instance.GetTextColor();
        // }
        
        dicData["type"] = card.type;
        dicData["index"] = card.index;
        dicData["obj"] = obj;
        grabbedObjList.Add(dicData);
        SaveData();
        }
        
    }

    // Method to detect if the player wants to grab an object (you can call this from input events, e.g., button press, etc.)
    public void GrabObject()
    {
        if (grabbedObject == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out hit))
            {
                grabbedObject = hit.collider.gameObject;
            }
        }
    }

    // Method to delete the currently grabbed object
    public void DeleteGrabbedObject()
    {
        if (grabbedObject != null)
        {
            Destroy(grabbedObject); // Destroy the object on all clients in a networked environment
            grabbedObject = null;
        }
    }
}
