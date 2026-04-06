using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIKit : MonoBehaviour
{
    [Header("Connections")]
    public PlayerShip playerShip;
    public Slider healthSlider;
    public Slider gasSlider;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;

    void Start() { }

    void Update()
    {
        healthSlider.value = playerShip.GetHealth() / playerShip.GetMaxHealth();
        healthText.text = $"{Math.Round(playerShip.GetHealth())}";
        gasSlider.value = playerShip.GetFuel() / playerShip.GetMaxFuel();
        energyText.text = $"{Math.Round(playerShip.GetFuel())}";
    }
}
