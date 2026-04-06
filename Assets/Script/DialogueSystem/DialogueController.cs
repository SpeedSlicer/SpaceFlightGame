using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    float defaultSpeed = 0.3f;

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

    [SerializeField]
    PlayerShip playerShip;
    Vector3 inPos,
        outPos;
    float letterSpeed = 0.1f;

    void Start()
    {
        dialoguePanel.SetActive(false);
        outPos = continueBox.anchoredPosition;
        inPos = new Vector3(outPos.x - continueBox.rect.width, outPos.y, outPos.z);
        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        letterSpeed = defaultSpeed;
    }

    void Update() { }

    public bool IsOver()
    {
        return isOver;
    }

    public void Speak(string[] lines, Sprite[] sprites, string[] names)
    {
        if (!isOver)
            return;
        currentNames = names;
        isOver = false;
        currentLines = lines;
        currentSprites = sprites;
        StartCoroutine(TextLoop());
        SetSpeed(defaultSpeed);
    }

    public void Speak(DialogueObject dialogueObject)
    {
        Speak(dialogueObject.GetLines(), dialogueObject.GetSprites(), dialogueObject.GetNames());
        SetSpeed(dialogueObject.GetSpeed());
    }

    public void CancelSpeech()
    {
        StopAllCoroutines();
        playerShip.SetFreezePlayer(false);
        isOver = true;
        dialoguePanel.SetActive(false);
        continueBox.LeanMove(outPos, 0.5f).setEaseInOutSine();
    }

    public IEnumerator TextLoop()
    {
        dialoguePanel.SetActive(true);
        playerShip.SetFreezePlayer(true);
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

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => continueAction.action.triggered);
        }
        playerShip.SetFreezePlayer(false);
        isOver = true;
        dialoguePanel.SetActive(false);
        continueBox.LeanMove(outPos, 0.5f).setEaseInOutSine();
    }

    public void SetSpeed(float newSpeed)
    {
        letterSpeed = newSpeed;
    }
}
