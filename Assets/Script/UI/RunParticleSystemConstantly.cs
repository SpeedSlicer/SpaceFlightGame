using UnityEngine;

public class ParticleFadeIn : MonoBehaviour
{
    public ParticleSystem ps;
    public float fadeDuration = 2.0f;
    public float targetRate = 50f;

    private void Start()
    {
        var main = ps.main;
        main.prewarm = true; 
        
        StartCoroutine(FadeInEmission());
    }

    System.Collections.IEnumerator FadeInEmission()
    {
        var emission = ps.emission;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            emission.rateOverTime = Mathf.Lerp(0, targetRate, elapsed / fadeDuration);
            yield return null;
        }
        emission.rateOverTime = targetRate;
    }
}
