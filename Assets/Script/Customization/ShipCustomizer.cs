using UnityEngine;

public class ShipCustomizer : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer shipRenderer;

    [SerializeField]
    GameObject lPS;

    [SerializeField]
    GameObject rPS;

    void Start()
    {
        shipRenderer = GetComponent<SpriteRenderer>();
        if (
            ColorUtility.TryParseHtmlString(
                ShipCustomizerStorage.storage[ShipCustomizerStorage.setIndex],
                out Color col
            )
        )
        {
            shipRenderer.color = col;
        }
        if (TrailMatStorage.mat != null)
        {
            lPS.GetComponent<ParticleSystemRenderer>().material = TrailMatStorage.mat;
            rPS.GetComponent<ParticleSystemRenderer>().material = TrailMatStorage.mat;
        }
    }

    public void UpdateSprite()
    {
        shipRenderer = GetComponent<SpriteRenderer>();
        if (
            ColorUtility.TryParseHtmlString(
                ShipCustomizerStorage.storage[ShipCustomizerStorage.setIndex],
                out Color col
            )
        )
        {
            shipRenderer.color = col;
        }
        lPS.GetComponent<ParticleSystemRenderer>().material = TrailMatStorage.mat;
        rPS.GetComponent<ParticleSystemRenderer>().material = TrailMatStorage.mat;
    }
}
