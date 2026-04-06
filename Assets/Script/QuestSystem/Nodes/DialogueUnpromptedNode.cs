using System;
using System.Collections;
using UnityEngine;

public class DialogueUnpromptedNode : QuestNode
{
    [SerializeField]
    private DialogueController dialogueController;

    [SerializeField]
    private DialogueObject dialogueObject;

    public override void OnActivate()
    {
        base.OnActivate();
        StartCoroutine(RunDialogue());
    }

    private IEnumerator RunDialogue()
    {
        dialogueController.Speak(dialogueObject);
        if (verbose)
        {
            Debug.Log("Started unprompted dialogue");
        }
        while (!dialogueController.IsOver())
        {
            if (verbose)
            {
                Debug.Log("Tick");
            }
            yield return null;
        }

        Complete();
    }
}
