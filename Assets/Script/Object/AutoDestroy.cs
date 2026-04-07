using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 8f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
