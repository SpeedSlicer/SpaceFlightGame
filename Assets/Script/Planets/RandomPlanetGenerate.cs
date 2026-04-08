using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomPlanetGenerate : MonoBehaviour
{
    [SerializeField]
    Sprite[] planetSprites = new Sprite[0];

    [SerializeField]
    string[] names = new string[0];

    [SerializeField]
    string[] normalLines = new string[0];

    [SerializeField]
    string[] angryLines = new string[0];

    [SerializeField]
    Sprite[] alienSprites = new Sprite[0];

    [SerializeField]
    ShippingNode shippingNode;
    Transform targetPos;

    [SerializeField]
    ParkingZone parkingZone;

    [SerializeField]
    SpriteRenderer img;

    void Start()
    {
        targetPos = this.transform;
        string name = names[Random.Range(0, names.Count() - 1)];
        Sprite sprite = alienSprites[Random.Range(0, alienSprites.Count() - 1)];

        shippingNode.angryDialogueObject = new DialogueObject(
            new string[] { angryLines[Random.Range(0, angryLines.Count() - 1)] },
            new Sprite[] { sprite },
            new string[] { name }
        );
        shippingNode.dialogueObject = new DialogueObject(
            new string[] { normalLines[Random.Range(0, angryLines.Count() - 1)] },
            new Sprite[] { sprite },
            new string[] { name }
        );
        shippingNode.SetTarget(targetPos);
        shippingNode.parkingZone = parkingZone;
        img.sprite = planetSprites[Random.Range(0, planetSprites.Count() - 1)];
    }

    void Update() { }
}
