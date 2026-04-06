using Unity.GraphToolkit.Editor;
using UnityEngine;

public class AlertNode : QuestNode
{
    [SerializeField]
    AlertManager alertManager;

    [SerializeField]
    float alertDuration = 2f;

    [SerializeField]
    string alertTitle = "Alert Node Activated";

    [SerializeField]
    string alertDescription = "This is an alert from an Alert Node.";

    [SerializeField]
    AlertManager.AlertType alertType = AlertManager.AlertType.Correct;

    public override void OnActivate()
    {
        base.OnActivate();
        alertManager.SendAlert(alertDuration, alertTitle, alertDescription, alertType);
        Complete();
    }
}
