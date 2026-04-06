using UnityEngine;

public class QuestNode : MonoBehaviour
{
    int setID = 0;
    bool completed = false;
    QuestSystem qs;

    [Header("Trail Guide")]
    [SerializeField]
    Transform target;

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

    public virtual void OnActivate() { }

    public virtual bool IsOver()
    {
        return completed;
    }

    public virtual void Complete()
    {
        completed = true;
    }

    public virtual void ResetNode()
    {
        completed = false;
    }

    public Vector3 GetTarget()
    {
        return target.position;
    }

    public virtual void UpdateConditions() { }
}
