using System;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    double nextTimeCountdown = 0;
    bool disabled = true;

    [SerializeField]
    TextMeshProUGUI textObj;

    void Update()
    {
        if (!disabled)
        {
            textObj.text = string.Format(
                "{0}:{1:00}",
                (int)(nextTimeCountdown - Time.time),
                (int)((nextTimeCountdown - Time.time - (int)(nextTimeCountdown - Time.time)) * 60)
            );
        }
        else
        {
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
