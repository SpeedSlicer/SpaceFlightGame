using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public string[] damageableTags = new string[0];
    private float health = 0;
    [Header("Health Settings")]
    public float initialhealth = 50;
    protected void Start()
    {
        health = initialhealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetHealth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return initialhealth;
    }

    public virtual bool Damage(float amount, string source)
    {
        if (damageableTags.Contains(source))
        {
            health -= math.abs(amount);
            return true;
        }
        return false;
    }
}
