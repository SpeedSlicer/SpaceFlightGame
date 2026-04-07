using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField]
    QuestNode[] questNodes = new QuestNode[0];

    [SerializeField]
    DialogueController dialogueController;

    [SerializeField]
    int index = 0;

    [SerializeField]
    LineTargetFollower lineTargetFollower;

    [SerializeField]
    bool started = false;

    [SerializeField]
    TextMeshProUGUI taskLeftText;

    void Start()
    {
        for (int j = 0; j < questNodes.Length; j++)
        {
            questNodes[j].SetID(j);
            questNodes[j].SetQuestSystem(this);
        }
        lineTargetFollower.SetActive(true);
        questNodes[index].OnActivate();
    }

    // TODO replace spagetti
    void Update()
    {
        if (index < questNodes.Length)
        {
            lineTargetFollower.SetPosition(questNodes[index].GetTarget());
        }
        else
        {
            lineTargetFollower.SetActive(false);
        }
        if (questNodes.Length > 0 && index < questNodes.Length && started)
        {
            if (questNodes[index].IsOver())
            {
                index++;
                if (index >= questNodes.Length)
                {
                    QuestOver();
                }
                else
                {
                    lineTargetFollower.SetActive(true);
                    questNodes[index].OnActivate();
                }
            }
        }
        if (taskLeftText != null)
        {
            taskLeftText.text = $"{index + 1} / {questNodes.Length}";
        }
    }

    public int GetCurrentNodeID()
    {
        return index;
    }

    public async Task<bool> IsNPCNodeActive(int npcID)
    {
        if (questNodes.Length > 0 && index < questNodes.Length)
        {
            if (questNodes[index] is NPCNode npcNode)
            {
                if (npcNode.GetConnectedNPC().GetNPCID() == npcID)
                {
                    dialogueController.Speak(npcNode.GetDialogueObject());

                    while (!dialogueController.IsOver())
                    {
                        await Task.Yield();
                    }
                    questNodes[index].Complete();
                    return true;
                }
            }
        }
        return false;
    }

    public virtual void QuestOver()
    {
        lineTargetFollower.SetActive(false);
    }

    public void StartQuest()
    {
        started = true;
        lineTargetFollower.SetActive(true);
    }
}
