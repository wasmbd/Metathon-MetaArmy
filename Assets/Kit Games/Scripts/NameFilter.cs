using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameFilter : MonoBehaviour
{
    public TMP_InputField inputField;
    public List<string> allNames;
    public Transform nameListContainer;
    public GameObject namePrefab;

    private List<TMP_Text> nameTexts = new List<TMP_Text>();

    private void Start()
    {
        inputField.onValueChanged.AddListener(FilterNames);
    }

    public void FilterNames(string filterText)
    {
        // Clear previous names
        foreach (TMP_Text nameText in nameTexts)
        {
            Destroy(nameText.gameObject);
        }
        nameTexts.Clear();

        // Filter names and create name objects
        foreach (string name in allNames)
        {
            if (name.ToLower().Contains(filterText.ToLower()))
            {
                GameObject nameObject = Instantiate(namePrefab, nameListContainer);
                TMP_Text nameText = nameObject.GetComponent<TMP_Text>();
                nameText.text = name;

                nameTexts.Add(nameText);
            }
        }
    }
}
