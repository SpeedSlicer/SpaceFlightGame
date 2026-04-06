using UnityEngine;

public class GoalPlanet : MonoBehaviour
{
    [SerializeField]
    ParkingZone parkingZone;

    public bool IsPlayerInZone()
    {
        return parkingZone.IsPlayerInZone();
    }
}
