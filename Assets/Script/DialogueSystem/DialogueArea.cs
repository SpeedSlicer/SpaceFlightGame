using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueArea : MonoBehaviour
{
    [SerializeField]
    DialogueController controller;
    [SerializeField]
    GameManager gm;
    [SerializeField]
    string[] currentLines = new string[0];
    [SerializeField]
    Sprite[] currentSprites = new Sprite[0];
    [SerializeField]
    string[] currentNames = new string[0];

    bool canInteract = false;
    [SerializeField]
    InputActionReference interactInputAction;
    bool hasTriggered = false;

    void OnEnable()
    {
        interactInputAction.action.Enable();
    }
    void OnDisable()
    {
        interactInputAction.action.Disable();
    }
    void Start()
    {

    }
    void Update()
    {
        if (canInteract &&
     !hasTriggered &&
     interactInputAction.action.triggered &&
     controller.IsOver())
        {
            controller.Speak(currentLines, currentSprites, currentNames);
            hasTriggered = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.SetInteractActive(true);
            canInteract = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            gm.SetInteractActive(false);
        }
    }
}
