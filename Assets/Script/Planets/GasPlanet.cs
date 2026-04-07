using System;
using UnityEngine;

public class GasPlanet : MonoBehaviour
{
    public ParkingZone parkingZone;
    PlayerShip playerShip;

    [SerializeField]
    float refuelPerSecond = 5f;
    AlertManager alertManager;
    bool hasAlertedFull = false;

    [SerializeField]
    DialogueController dialogueController;

    [SerializeField]
    DialogueObject dialogueObject;

    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        alertManager = GameObject
            .FindGameObjectWithTag("AlertManager")
            .GetComponent<AlertManager>();
    }

    void FixedUpdate()
    {
        if (parkingZone.IsPlayerInZone() && !hasAlertedFull)
        {
            playerShip.Refuel(refuelPerSecond * 0.02f);
            dialogueController.Speak(dialogueObject);
            if (playerShip.GetFuel() >= playerShip.GetMaxFuel())
            {
                alertManager.SendAlert(
                    2f,
                    "Fully Refueled",
                    "Your ship is now fully refueled.",
                    AlertManager.AlertType.Correct
                );
                hasAlertedFull = true;
            }
            playerShip.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
        else if (!parkingZone.IsPlayerInZone())
        {
            hasAlertedFull = false;
        }
    }

    public bool HasBeenUsed()
    {
        return hasAlertedFull;
    }
}
