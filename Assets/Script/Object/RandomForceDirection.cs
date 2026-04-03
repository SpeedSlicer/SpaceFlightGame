using System;
using System.Collections;
using UnityEngine;

public class RandomAsteroidDirection : MonoBehaviour
{
    Rigidbody2D rb;
    public float randomMinX = -5, randomMaxX = 5;
    public float randomMinY = -5, randomMaxY = 5;
    public float destroyTime = 10;
    public bool doesDestroy = true;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if (doesDestroy)
        {
            Destroy(this.gameObject, destroyTime);
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(new Vector2(UnityEngine.Random.Range(randomMinX, randomMaxX), UnityEngine.Random.Range(randomMinY, randomMaxY)));
    }
}
