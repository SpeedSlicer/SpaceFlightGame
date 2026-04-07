using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    int npcID;

    bool canInteract = false;

    [SerializeField]
    bool disableInteractDuringDialogue = true;

    [SerializeField]
    GameManager gm;

    [SerializeField]
    DialogueObject defaultDialogueObject;

    [SerializeField]
    QuestSystem questSystem;

    [SerializeField]
    DialogueController dialogueController;

    [SerializeField]
    InputActionReference inputActions;

    void OnEnable()
    {
        inputActions.action.Enable();
    }

    void OnDisable()
    {
        inputActions.action.Disable();
    }

    void Start()
    {
        npcID = GameManager.GetNewNPCID();
    }

    void Update()
    {
        if (canInteract && inputActions.action.WasPressedThisFrame())
        {
            HandleInteraction();
        }
    }

    private async void HandleInteraction()
    {
        if (questSystem != null)
        {
            gm.SetInteractActive(false);
            canInteract = false;
            bool wasQuestActive = await questSystem.IsNPCNodeActive(npcID);
            if (!wasQuestActive && defaultDialogueObject != null)
            {
                dialogueController.Speak(defaultDialogueObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dialogueController.IsOver())
        {
            if (gm != null)
            {
                canInteract = true;
                gm.SetInteractActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gm != null)
            {
                gm.SetInteractActive(false);
                canInteract = false;
            }
        }
    }

    public int GetNPCID()
    {
        return npcID;
    }
}
