using UnityEngine;

public class RandomDirection : MonoBehaviour
{
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }
}
