using Unity.VisualScripting;
using UnityEngine;

public class Ship : Damageable
{
    protected Rigidbody2D rb;
    protected void Start()
    {
        base.Start();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {

    }
}
