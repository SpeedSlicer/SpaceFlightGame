using System;
using UnityEngine;

public class FuelNode : QuestNode
{
    [SerializeField]
    GasPlanet gasPlanet;

    void Start() { }

    public override void UpdateConditions()
    {
        if (gasPlanet.HasBeenUsed() && IsActive() && !IsOver())
        {
            Complete();
        }
    }
}
