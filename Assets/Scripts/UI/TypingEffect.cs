using UnityEngine;
using TMPro;
using System.Collections;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.05f;

    private string fullText;
    private string currentText = "";

    void Start()
    {
        if (textMeshPro != null)
        {
            fullText = textMeshPro.text;
            textMeshPro.text = "";
        }
    }

    public void StartTypingEffect()
    {
        if (textMeshPro != null)
        {
            StartCoroutine(TypeText());
        }
    }

    private IEnumerator TypeText()
    {
        fullText = textMeshPro.text;
        textMeshPro.text = "";

        currentText = "";

        foreach (char letter in fullText.ToCharArray())
        {
            currentText += letter;
            textMeshPro.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
