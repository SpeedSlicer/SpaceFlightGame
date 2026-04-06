using UnityEngine;

public class NPCNode : QuestNode
{
    [SerializeField]
    NPC connectedNPC;

    [SerializeField]
    DialogueObject dialogueObject;

    public NPC GetConnectedNPC()
    {
        return connectedNPC;
    }

    public DialogueObject GetDialogueObject()
    {
        return dialogueObject;
    }
}
