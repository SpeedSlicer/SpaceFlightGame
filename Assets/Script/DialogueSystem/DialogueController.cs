using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
public class DialogueController : MonoBehaviour
{
    [SerializeField]
    GameObject dialoguePanel;
    [SerializeField]
    TextMeshProUGUI titleText;
    [SerializeField]
    TextMeshProUGUI spokenText;
    [SerializeField]
    Image image;
    [SerializeField]
    float letterSpeed = 0.3f;
    [SerializeField]
    float waitTime = 2f;

    string[] currentLines;
    Sprite[] currentSprites;
    string[] currentNames;
    bool isOver = true;
    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {

    }

    public bool IsOver()
    {
        return isOver;
    }
    public void Speak(string[] lines, Sprite[] sprites, string[] names, Sprite sprite)
    {
        currentNames = names;
        image.sprite = sprite;
        isOver = false;
        currentLines = lines;
        currentSprites = sprites;
        StartCoroutine(TextLoop());
    }
    
    public IEnumerator TextLoop()
    {
        dialoguePanel.SetActive(true);
        for (int i = 0; i < currentLines.Count(); i++)
        {
            titleText.text = currentNames[i];
            image.sprite = currentSprites[i];
            foreach (char a in currentLines[i])
            {
                spokenText.text += a;
                if (!(a == ' ' || a == '\n'))
                {
                    yield return new WaitForSeconds(letterSpeed);
                }
            } 
            yield return new WaitForSeconds(waitTime);
        }
        isOver = true;
        dialoguePanel.SetActive(false);
    }
}
