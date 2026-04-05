using UnityEngine;

public class DialogueObject : MonoBehaviour
{
    [SerializeField]
    string[] currentLines = new string[0];

    [SerializeField]
    Sprite[] currentSprites = new Sprite[0];

    [SerializeField]
    string[] currentNames = new string[0];

    public string[] GetLines()
    {
        return currentLines;
    }
    public Sprite[] GetSprites()
    {
        return currentSprites;
    }
    public string[] GetNames()
    {
        return currentNames;
    }

}
