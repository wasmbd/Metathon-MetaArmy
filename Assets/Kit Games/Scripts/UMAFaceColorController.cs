using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UMAFaceColorController : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(OnChangeSkinColor());

    }
    IEnumerator OnChangeSkinColor()
    {
        while(true)
        {
            yield return null;
            string colorStr = PlayerPrefs.GetString("skincolor", "");
            if(colorStr != "")
            {
                Color color;
                ColorUtility.TryParseHtmlString("#" + colorStr, out color);
                SkinnedMeshRenderer[] avatarMesh = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
                for(int i = 0; i < avatarMesh.Length; i++)
                {
                    for(int j = 0; j < avatarMesh[i].materials.Length; j++)
                    {
                        // Debug.LogError(avatarMesh[i].materials[j].name + " : " + color.ToString());
                        if(avatarMesh[i].materials[j].name.Contains("ManOriginalFace") || avatarMesh[i].materials[j].name.Contains("HeadOriginalWoman"))
                        {
                            avatarMesh[i].materials[j].color = color;
                        }
                    }
                }
            }
        }
    }
}
