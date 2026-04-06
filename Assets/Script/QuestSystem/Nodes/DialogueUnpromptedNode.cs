using System.Threading.Tasks;
using UnityEngine;

public class DialogueUnpromptedNode : QuestNode
{
    [SerializeField]
    DialogueController dialogueController;

    [SerializeField]
    DialogueObject dialogueObject;

    public override void OnActivate()
    {
        base.OnActivate();
        Task.Run(async () => await TaskAsync());
        Complete();
    }

    public async Task<bool> TaskAsync()
    {
        dialogueController.Speak(dialogueObject);

        while (!dialogueController.IsOver())
        {
            await Task.Yield();
        }
        return true;
    }

    void Start() { }

    void Update() { }
}
