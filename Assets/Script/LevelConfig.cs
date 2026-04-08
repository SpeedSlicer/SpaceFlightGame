using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    [SerializeField]
    int levelID = 0;

    public int GetLevelID()
    {
        return levelID;
    }
}
