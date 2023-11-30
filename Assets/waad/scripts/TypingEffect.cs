using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    //  public GameObject targetObject;

    private TextMeshProUGUI textMeshPro;
    private string fullText;

    private void Start()
    {
        // targetObject.SetActive(false);
        textMeshPro = GetComponent<TextMeshProUGUI>();
        fullText = textMeshPro.text;
        textMeshPro.text = string.Empty;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        while (true)
        {
            textMeshPro.text = string.Empty;

            foreach (char c in fullText)
            {
                textMeshPro.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(3f); // Wait for 3 seconds before clearing and retyping
        }
    }
}