using System.Threading;
using UnityEngine;

public class ShippingNode : QuestNode
{
    [SerializeField]
    ParkingZone parkingZone;

    [SerializeField]
    AlertManager alertManager;

    [SerializeField]
    DialogueController dialogueController;

    [SerializeField]
    DialogueObject dialogueObject;

    [SerializeField]
    DialogueObject angryDialogueObject;

    [SerializeField]
    bool thanks = false;

    [SerializeField]
    bool timed = false;

    [SerializeField]
    float timeLimit = 10f;
    bool begin = false;

    [SerializeField]
    TimerManager timerManager;

    [SerializeField]
    bool rewardEnabled = true;

    [SerializeField]
    float moneyReward = 10;

    [SerializeField]
    float angryMoneyReward = 5;

    public override void OnActivate()
    {
        base.OnActivate();
        if (timed)
        {
            timerManager.SetTimer(timeLimit + Time.timeAsDouble);
        }
    }

    public override void UpdateConditions()
    {
        if (!begin)
        {
            begin = true;
        }
        // TODO rewrite spagetti
        if (parkingZone.IsPlayerInZone() && IsActive() && !IsOver())
        {
            alertManager.SendAlert(
                2f,
                "Parked & Delivered!",
                "You have successfully delivered the package.",
                AlertManager.AlertType.Correct
            );
            if (thanks)
            {
                if (timed)
                {
                    if (timerManager.IsOver())
                    {
                        dialogueController.Speak(angryDialogueObject);
                        if (rewardEnabled)
                        {
                            GameManager.AddCoins(angryMoneyReward);
                        }
                    }
                    else
                    {
                        dialogueController.Speak(dialogueObject);
                        if (rewardEnabled)
                        {
                            GameManager.AddCoins(moneyReward);
                        }
                    }
                }
                else
                {
                    dialogueController.Speak(dialogueObject);
                    if (rewardEnabled)
                    {
                        GameManager.AddCoins(moneyReward);
                    }
                }
            }
            else
            {
                if (rewardEnabled)
                {
                    GameManager.AddCoins(moneyReward);
                }
            }
            Complete();
            timerManager.DisableTimer();
        }
    }
}
