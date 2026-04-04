using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.InputSystem;
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
    float waitTime = 5f;

    string[] currentLines = new string[0];
    Sprite[] currentSprites = new Sprite[0];
    string[] currentNames = new string[0];
    bool isOver = true;

    [SerializeField] 
    RectTransform continueBox;
    [SerializeField]
    InputActionReference continueAction;
    Vector3 inPos, outPos;
    void Start()
    {
        dialoguePanel.SetActive(false);
        outPos = continueBox.anchoredPosition;
        inPos = new Vector3(outPos.x - continueBox.rect.width, outPos.y, outPos.z);
    }

    void Update()
    {

    }

    public bool IsOver()
    {
        return isOver;
    }
    public void Speak(string[] lines, Sprite[] sprites, string[] names)
    {
        currentNames = names;
        isOver = false;
        currentLines = lines;
        currentSprites = sprites;
        StartCoroutine(TextLoop());
    }

    public IEnumerator TextLoop()
    {
        dialoguePanel.SetActive(true);

        for (int i = 0; i < currentLines.Length; i++)
        {
            continueBox.LeanMove(outPos, 0.5f).setEaseInOutSine();
            titleText.text = currentNames[i];
            image.sprite = currentSprites[i];
            spokenText.text = "";

            foreach (char a in currentLines[i])
            {
                spokenText.text += a;

                if (a == '.' || a == ',' || a == '!' || a == '?')
                    yield return new WaitForSeconds(letterSpeed * 2f);
                else if (a != ' ' && a != '\n')
                    yield return new WaitForSeconds(letterSpeed);
            }
            continueBox.LeanMove(inPos, 0.5f).setEaseInOutSine();
            yield return new WaitUntil(() => continueAction.action.triggered);
        }

        isOver = true;
        dialoguePanel.SetActive(false);
    }
}
