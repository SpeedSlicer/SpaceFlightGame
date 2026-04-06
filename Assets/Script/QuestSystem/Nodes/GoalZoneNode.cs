using UnityEngine;

public class GoalZoneNode : QuestNode
{
    [SerializeField]
    GoalPlanet goalPlanet;

    public override void UpdateConditions()
    {
        base.UpdateConditions();
        if (goalPlanet.IsPlayerInZone())
        {
            Complete();
        }
    }
}
