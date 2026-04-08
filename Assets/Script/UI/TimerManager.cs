using System;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    double nextTimeCountdown = 0;
    bool disabled = true;

    [SerializeField]
    TextMeshProUGUI textObj;

    [SerializeField]
    AlertManager alertManager;
    bool hasTriggerWarning1,
        hasTriggeredWarning2;

    void Start()
    {
        hasTriggeredWarning2 = false;
        hasTriggerWarning1 = false;
    }

    void Update()
    {
        if (!disabled)
        {
            textObj.text = string.Format(
                "{0}:{1:00}",
                (int)(nextTimeCountdown - Time.time),
                (int)((nextTimeCountdown - Time.time - (int)(nextTimeCountdown - Time.time)) * 60)
            );
            if ((nextTimeCountdown / Time.time) < 0.2 && !hasTriggerWarning1)
            {
                hasTriggerWarning1 = true;

                alertManager.SendAlert(
                    1f,
                    "Speed Up!",
                    "Your time is running out!",
                    AlertManager.AlertType.Info
                );
            }
            if ((nextTimeCountdown / Time.time) < 0 && !hasTriggeredWarning2)
            {
                hasTriggeredWarning2 = true;
                alertManager.SendAlert(
                    1f,
                    "TIME!!",
                    "Your time has run out!",
                    AlertManager.AlertType.Warning
                );
            }
        }
        else
        {
            hasTriggeredWarning2 = false;
            hasTriggerWarning1 = false;
            textObj.text = "No Timer Active";
        }
    }

    public void SetTimer(double time)
    {
        nextTimeCountdown = time;
        disabled = false;
    }

    public bool IsOver()
    {
        return !disabled && Time.time >= nextTimeCountdown;
    }

    public void DisableTimer()
    {
        disabled = true;
    }
}
