using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UMAEyesColorController : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(OnChangeEyesColor());

    }

    // Update is called once per frame
    IEnumerator OnChangeEyesColor()
    {
        while (true)
        {
            yield return null;
            string colorStr = PlayerPrefs.GetString("eyescolor", "");
            if (colorStr != "")
            {
                Color color;
                ColorUtility.TryParseHtmlString("#" + colorStr, out color);
                SkinnedMeshRenderer[] avatarMesh = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
                for (int i = 0; i < avatarMesh.Length; i++)
                {
                    for (int j = 0; j < avatarMesh[i].materials.Length; j++)
                    {
                        // Debug.LogError(avatarMesh[i].materials[j].name + " : " + color.ToString());
                        if (avatarMesh[i].materials[j].name.Contains("ManEyesOriginal1") || avatarMesh[i].materials[j].name.Contains("EyesOriginalWoman"))
                        {
                            avatarMesh[i].materials[j].color = color;
                        }
                    }
                }
            }
        }
    }
}
