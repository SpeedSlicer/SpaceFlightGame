using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrailSelector : MonoBehaviour
{
    public static bool[] ownedTrails;
    GameObject[] selectedGameObjects;
    GameObject[] coverBuy;
    public TrailObject[] tos = new TrailObject[0];
    public Button[] buttons = new Button[0];

    [SerializeField]
    ShipCustomizer shipCustomizer;
    int lastIndex = 0;

    // todo im rushing so these are going to be shit
    void Start()
    {
        if (TrailMatStorage.mat == null)
        {
            TrailMatStorage.mat = tos[0].mat;
        }
        if (ownedTrails == null)
        {
            ownedTrails = new bool[buttons.Length];
        }
        if (selectedGameObjects == null)
        {
            selectedGameObjects = new GameObject[buttons.Length];
        }
        coverBuy = new GameObject[buttons.Length];
        ownedTrails[0] = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            coverBuy[i] = buttons[i].transform.Find("CoverBuy").gameObject;
            selectedGameObjects[i] = buttons[i].transform.Find("Selected").gameObject;
        }
        foreach (var sel in selectedGameObjects)
        {
            sel.SetActive(false);
        }
        for (int i = 0; i < ownedTrails.Length; i++)
        {
            if (ownedTrails[i])
            {
                coverBuy[i].SetActive(false);
                buttons[i].interactable = true;
            }
            else
            {
                coverBuy[i].SetActive(true);
                buttons[i].interactable = false;
            }
            coverBuy[i]
                .GetComponentsInChildren<Transform>(true)
                .FirstOrDefault(t => t.name == "Cost")
                .gameObject.GetComponent<TextMeshProUGUI>()
                .text = $"${tos[i].cost}";
        }
        selectedGameObjects[TrailMatStorage.selectedIndex].SetActive(true);
    }

    void Update()
    {
        if (TrailMatStorage.selectedIndex != lastIndex)
        {
            selectedGameObjects[lastIndex].SetActive(false);
            selectedGameObjects[TrailMatStorage.selectedIndex].SetActive(true);
            lastIndex = TrailMatStorage.selectedIndex;
        }
    }

    public void Buy(int to)
    {
        if (GameManager.GetCoins() >= tos[to].cost)
        {
            GameManager.RemoveCoins(tos[to].cost);
            ownedTrails[to] = true;
            coverBuy[to].SetActive(false);
            buttons[to].interactable = true;
        }
    }

    public void SelectTrail(int select)
    {
        TrailMatStorage.selectedIndex = select;
        TrailMatStorage.mat = tos[select].mat;
        shipCustomizer.UpdateSprite();
    }
}
