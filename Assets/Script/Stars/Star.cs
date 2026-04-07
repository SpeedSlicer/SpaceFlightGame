using UnityEngine;

public class Star : MonoBehaviour
{
    public float depth = 1f;
    public float bounds = 25f;
    private Transform player;
    private Rigidbody2D playerRb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 velocity = playerRb.linearVelocity;
        transform.position += (Vector3)(-velocity * depth * Time.deltaTime);

        WrapAround();
    }

    void WrapAround()
    {
        Vector3 offset = transform.position - player.position;

        if (offset.x > bounds)
            offset.x -= bounds * 2;
        if (offset.x < -bounds)
            offset.x += bounds * 2;
        if (offset.y > bounds)
            offset.y -= bounds * 2;
        if (offset.y < -bounds)
            offset.y += bounds * 2;

        transform.position = player.position + offset;
    }
}
