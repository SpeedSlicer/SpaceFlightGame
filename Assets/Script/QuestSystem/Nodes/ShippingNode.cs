using System.Threading;
using UnityEngine;

public class ShippingNode : QuestNode
{
    [SerializeField]
    public ParkingZone parkingZone;

    [SerializeField]
    AlertManager alertManager;

    [SerializeField]
    DialogueController dialogueController;

    [SerializeField]
    public DialogueObject dialogueObject;

    [SerializeField]
    public DialogueObject angryDialogueObject;

    [SerializeField]
    bool thanks = true;

    [SerializeField]
    bool timed = true;

    [SerializeField]
    public float timeLimit = 10f;
    bool begin = false;

    [SerializeField]
    TimerManager timerManager;

    // TODO use setters
    [SerializeField]
    bool rewardEnabled = true;

    [SerializeField]
    public float moneyReward = 10;

    [SerializeField]
    public float angryMoneyReward = 5;

    GameObject playerObject;

    public override void OnActivate()
    {
        base.OnActivate();
        playerObject = GameObject.FindGameObjectWithTag("Player");
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
                            playerObject.GetComponent<AudioSource>().Play();
                        }
                    }
                    else
                    {
                        dialogueController.Speak(dialogueObject);
                        if (rewardEnabled)
                        {
                            GameManager.AddCoins(moneyReward);
                            playerObject.GetComponent<AudioSource>().Play();
                        }
                    }
                }
                else
                {
                    dialogueController.Speak(dialogueObject);
                    if (rewardEnabled)
                    {
                        GameManager.AddCoins(moneyReward);
                        playerObject.GetComponent<AudioSource>().Play();
                    }
                }
            }
            else
            {
                if (rewardEnabled)
                {
                    GameManager.AddCoins(moneyReward);
                    playerObject.GetComponent<AudioSource>().Play();
                }
            }
            Complete();
            timerManager.DisableTimer();
        }
    }
}
