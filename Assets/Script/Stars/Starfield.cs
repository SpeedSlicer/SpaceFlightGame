using UnityEngine;

public class StarField : MonoBehaviour
{
    public GameObject starPrefab;
    public int starCount = 100;
    public float width = 20f;
    public float height = 20f;
    public float minScale = 0.05f, maxScale = 0.1f;
    private GameObject[] stars;

    void Start()
    {
        stars = new GameObject[starCount];

        for (int i = 0; i < starCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-width, width),
                Random.Range(-height, height),
                0
            );

            GameObject star = Instantiate(starPrefab, pos, Quaternion.identity, transform);
            float scale = Random.Range(minScale, maxScale);
            star.transform.localScale = Vector3.one * scale;
            stars[i] = star;
        }
    }
}