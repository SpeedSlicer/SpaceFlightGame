using UnityEngine;

public class Timer : MonoBehaviour
{
    double startTimer = 0;

    void Start()
    {
        startTimer = Time.timeAsDouble;
    }

    public double GetStartTime()
    {
        Debug.Log(Time.timeAsDouble - startTimer);
        return startTimer;
    }
}
