using System.Collections;
using TMPro;
using UnityEngine;

public class QuestPanelManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI titleText;

    [SerializeField]
    TextMeshProUGUI descriptionText;

    [Header("Effect Settings")]
    float typingSpeed = 0.05f;
    float deleteSpeed = 0.05f;

    void Start()
    {
        SetText("No Task Active:", "Awaiting a new task...");
    }

    void Update() { }

    public void SetText(string title, string description)
    {
        titleText.text = "";
        descriptionText.text = "";
        StopAllCoroutines();
        StartCoroutine(TypeText(title, description));
    }

    IEnumerator TypeText(string title, string description)
    {
        foreach (char c in titleText.text)
        {
            titleText.text = titleText.text.Substring(0, titleText.text.Length - 1);
            yield return new WaitForSeconds(typingSpeed);
        }

        foreach (char c in title)
        {
            titleText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        foreach (char c in descriptionText.text)
        {
            descriptionText.text = descriptionText.text.Substring(
                0,
                descriptionText.text.Length - 1
            );
            yield return new WaitForSeconds(typingSpeed);
        }
        foreach (char c in description)
        {
            descriptionText.text += c;
            yield return new WaitForSeconds(deleteSpeed);
        }
    }
}
