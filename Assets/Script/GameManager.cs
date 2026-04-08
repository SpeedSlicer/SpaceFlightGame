using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject interactGO;

    [SerializeField]
    TextMeshProUGUI interactText;
    static int npcID = 0;

    [SerializeField]
    QuestSystem currentQuestSystem;

    private static float coins = 0;
    public static int starAmount = 0;
    public static float rewardAmount = 0;
    public static float time = 0;
    public static int level = 0;
    public static float[] maxLevelTimes = new float[]
    {
        float.PositiveInfinity,
        float.PositiveInfinity,
        float.PositiveInfinity,
        float.PositiveInfinity,
    };

    void Start()
    {
        SetInteractActive(false);
        currentQuestSystem.StartQuest();
        interactText.text = "Press E to Interact";
    }

    void Update() { }

    public void SetInteractActive(bool interact)
    {
        if (interactGO != null)
        {
            interactGO.transform.LeanScale(
                interact ? new Vector3(0.35f, 0.35f, 0.35f) : new Vector3(0, 0, 0),
                0.2f
            );
        }
    }

    public static int GetNewNPCID()
    {
        npcID++;
        return npcID;
    }

    public static float GetCoins()
    {
        return coins;
    }

    public static void AddCoins(float amount)
    {
        coins += amount;
    }

    public static void RemoveCoins(float amount)
    {
        coins -= amount;
    }
}
