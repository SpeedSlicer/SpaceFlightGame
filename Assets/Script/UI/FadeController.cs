using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    Color fullColor;

    [SerializeField]
    Color emptyColor;

    public GameObject fadeObject;
    Image img;

    [SerializeField]
    float timeSeconds = 0.5f;


    void Start()
    {
        img = fadeObject.GetComponent<Image>();
        SetFadeActive(false);
    }

    void Update() { }

    public float GetTimeSeconds()
    {
        return timeSeconds;
    }

    public void SetFadeActive(bool isActive)
    {
        if (isActive)
        {
            img.color = emptyColor;
            LeanTween.value(img.gameObject, setColorCallback, emptyColor, fullColor, timeSeconds);
        }
        else if (!isActive)
        {
            img.color = fullColor;
            LeanTween.value(img.gameObject, setColorCallback, fullColor, emptyColor, timeSeconds);
        }
        StartCoroutine(SetStateAfterSeconds(isActive));
    }

    public void SetFadeActive(bool isActive, float seconds)
    {
        if (isActive)
        {
            img.color = emptyColor;
            LeanTween.value(img.gameObject, setColorCallback, emptyColor, fullColor, timeSeconds);
        }
        else if (!isActive)
        {
            img.color = fullColor;
            LeanTween.value(img.gameObject, setColorCallback, fullColor, emptyColor, timeSeconds);
        }
        StartCoroutine(SetStateAfterSeconds(isActive, seconds));
    }

    IEnumerator SetStateAfterSeconds(bool active)
    {
        if (active)
        {
            fadeObject.SetActive(active);
        }
        yield return new WaitForSeconds(timeSeconds);
        if (!active)
        {
            fadeObject.SetActive(active);
        }
    }

    IEnumerator SetStateAfterSeconds(bool active, float seconds)
    {
        if (active)
        {
            fadeObject.SetActive(active);
        }
        yield return new WaitForSeconds(seconds);
        if (!active)
        {
            fadeObject.SetActive(active);
        }
    }

    private void setColorCallback(Color c)
    {
        img.color = c;
    }
}
