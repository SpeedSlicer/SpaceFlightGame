using Unity.Mathematics.Geometry;
using UnityEngine;

public class ParkingZone : MonoBehaviour
{
    [SerializeField]
    bool playerInZone = false;
    double lastLeaveTime = 0;

    void Start() { }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var zoneBounds = this.gameObject.GetComponent<BoxCollider2D>().bounds;

            playerInZone = zoneBounds.Contains(collision.bounds.center);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInZone = false;
            lastLeaveTime = Time.timeAsDouble;
        }
    }

    public bool LeftNow()
    {
        return Time.timeAsDouble - lastLeaveTime < 0.2f;
    }

    public bool IsPlayerInZone()
    {
        return playerInZone;
    }
}
