using System;
using UnityEngine;

public class DialogueObject : MonoBehaviour
{
    [SerializeField]
    string[] currentLines = new string[0];

    [SerializeField]
    Sprite[] currentSprites = new Sprite[0];

    [SerializeField]
    string[] currentNames = new string[0];

    [SerializeField]
    float speed = 0.1f;

    public DialogueObject(string[] currentLines, Sprite[] currentSprites, string[] currentNames)
    {
        
        this.currentLines = currentLines;
        this.currentSprites = currentSprites;
        this.currentNames = currentNames;
    }
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

    public float GetSpeed()
    {
        return speed;
    }
}
