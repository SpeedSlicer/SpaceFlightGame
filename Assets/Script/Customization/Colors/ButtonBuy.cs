using UnityEngine;
using UnityEngine.UI;

public class ButtonBuy : MonoBehaviour
{
    [SerializeField]
    int index = 0;

    [SerializeField]
    float cost = 0;

    [SerializeField]
    Button button;

    void Start()
    {
        button = GetComponentInParent<Button>();
    }

    void Update()
    {
        if (ShipCustomizerStorage.owned[index])
        {
            button.interactable = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            button.interactable = false;
        }
    }

    public void Buy()
    {
        if (GameManager.GetCoins() >= cost)
        {
            GameManager.RemoveCoins(cost);
            ShipCustomizerStorage.owned[index] = true;
        }
    }
}
