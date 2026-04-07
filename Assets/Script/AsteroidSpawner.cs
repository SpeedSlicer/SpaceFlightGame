using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;

    [Header("Asteroid Settings")]
    public float launchForce = 8f;
    bool enabled = false;
    List<GameObject> asteroids = new List<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        if (enabled)
        {
            if (player == null || asteroidPrefab == null)
                return;

            Vector2 randomDir = Random.insideUnitCircle.normalized;

            Vector2 spawnPos = (Vector2)player.position + randomDir * spawnRadius;

            GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

            Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = ((Vector2)player.position - spawnPos).normalized;
                rb.linearVelocity = direction * launchForce;
            }
            asteroids.Add(asteroid);
        }
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
        asteroids.ForEach((a) => Destroy(a));
    }
}
