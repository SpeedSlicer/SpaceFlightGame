using UnityEngine;
using UnityEngine.Events;

public class QuestNode : MonoBehaviour
{
    int setID = 0;
    bool completed = false;
    QuestSystem qs;

    [Header("Trail Guide")]
    [SerializeField]
    Transform target;

    [Header("Node Initialize Settings")]
    [SerializeField]
    bool runActionOnActivate = false;

    [SerializeField]
    UnityEvent actionOnActivate;

    [Header("Node Completion Settings")]
    [SerializeField]
    bool runActionOnComplete = false;

    [SerializeField]
    UnityEvent actionOnComplete;

    [Header("Node-Specific Settings")]
    [SerializeField]
    protected bool verbose = false;

    [Header("Task Tracker Settings")]
    [SerializeField]
    QuestPanelManager questPanelManager;

    [SerializeField]
    string taskTitle = "Task Title";

    [SerializeField]
    string taskDescription = "Task Description";

    [SerializeField]
    bool trackTask = true;

    void Update()
    {
        if (IsActive() && !IsOver())
        {
            UpdateConditions();
        }
    }

    public void SetID(int id)
    {
        setID = id;
    }

    public int GetID()
    {
        return setID;
    }

    public void SetQuestSystem(QuestSystem qs)
    {
        this.qs = qs;
    }

    public bool IsActive()
    {
        return qs.GetCurrentNodeID() == setID;
    }

    public virtual void OnActivate()
    {
        if (verbose)
        {
            Debug.Log("Activating node " + setID);
        }
        if (trackTask)
        {
            questPanelManager.SetText(taskTitle, taskDescription);
        }
        if (runActionOnActivate)
        {
            actionOnActivate.Invoke();
        }
    }

    public virtual bool IsOver()
    {
        if (verbose)
        {
            Debug.Log("Checking if node " + setID + " is over: " + completed);
        }
        return completed;
    }

    public virtual void Complete()
    {
        completed = true;
        if (verbose)
        {
            Debug.Log("Node " + setID + " completed.");
        }
        if (runActionOnComplete)
        {
            actionOnComplete.Invoke();
        }
    }

    public virtual void ResetNode()
    {
        if (verbose)
        {
            Debug.Log("Resetting node " + setID);
        }
        completed = false;
    }

    public Vector3 GetTarget()
    {
        return target.position;
    }

    public virtual void UpdateConditions() { }
}
