using System.Collections;
using TMPro;
using UnityEngine;
public class TypingEffectReception : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    public GameObject targetObject;

    private TextMeshProUGUI textMeshPro;
    private string fullText;

    private void Start()
    {
        targetObject.SetActive(false);
        textMeshPro = GetComponent<TextMeshProUGUI>();
        fullText = textMeshPro.text;
        textMeshPro.text = string.Empty;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(typingSpeed + 0.2f);
        targetObject.SetActive(true);
    }
}