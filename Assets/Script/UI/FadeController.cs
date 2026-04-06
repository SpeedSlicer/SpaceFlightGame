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
    }

    private void setColorCallback(Color c)
    {
        img.color = c;
    }
}
