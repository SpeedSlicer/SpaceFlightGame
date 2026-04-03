using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIKit : MonoBehaviour
{
    [Header("Connections")]
    public PlayerShip playerShip;
    public Slider healthSlider;
    public Slider gasSlider;
    void Start()
    {
        
    }
    void Update()
    {
        healthSlider.value = playerShip.GetHealth() / playerShip.GetMaxHealth();
        gasSlider.value = playerShip.GetFuel() / playerShip.GetMaxFuel();
    }
}
