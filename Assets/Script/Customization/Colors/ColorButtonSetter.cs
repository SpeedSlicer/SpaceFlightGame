using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorButtonSetter : MonoBehaviour
{
    [SerializeField]
    Button[] buttons;

    [SerializeField]
    GameObject[] selectedObject;

    [SerializeField]
    Color selectedColor;

    [SerializeField]
    Color normalColor;

    [SerializeField]
    ShipCustomizer ship;

    void Start()
    {
        foreach (var e in selectedObject)
        {
            e.SetActive(false);
        }
    }

    public void Update()
    {
        int index = ShipCustomizerStorage.setIndex;

        for (int i = 0; i < buttons.Length; i++)
        {
            Image img = buttons[i].image;

            if (i == index)
            {
                selectedObject[i].SetActive(true);
            }
            else
            {
                selectedObject[i].SetActive(false);
            }
        }
        ship.UpdateSprite();
    }
}
