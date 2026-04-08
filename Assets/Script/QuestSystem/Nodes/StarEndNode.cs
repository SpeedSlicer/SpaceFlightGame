using System;
using UnityEngine;

public class StarEndNode : QuestNode
{
    [SerializeField]
    SceneTransition sceneManager;

    [SerializeField]
    Timer timer;

    [SerializeField]
    public double goalTime5Star = 50;

    public float[] currencyAmount = new float[5];

    int stars;

    [SerializeField]
    LevelConfig level;

    public override void OnActivate()
    {
        base.OnActivate();
        stars = CalculateStars();
        Debug.Log("Stars earned: " + stars);
        sceneManager.StarScene(
            stars,
            currencyAmount[stars - 1],
            (float)(Time.timeAsDouble - timer.GetStartTime()),
            level.GetLevelID()
        );
    }

    public override void UpdateConditions() { }

    int CalculateStars()
    {
        double elapsedTime = Time.timeAsDouble - timer.GetStartTime();

        if (elapsedTime <= goalTime5Star)
            return 5;
        if (elapsedTime <= goalTime5Star * 1.2)
            return 4;
        if (elapsedTime <= goalTime5Star * 1.5)
            return 3;
        if (elapsedTime <= goalTime5Star * 2.0)
            return 2;

        return 1;
    }
}
