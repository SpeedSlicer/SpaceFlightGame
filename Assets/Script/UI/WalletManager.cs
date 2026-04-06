using TMPro;
using UnityEngine;

public class WalletManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI walletCurrencyText;

    void Start() { }

    void Update()
    {
        walletCurrencyText.text = $"${GameManager.GetCoins()}";
    }
}
