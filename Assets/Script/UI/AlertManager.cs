using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour
{
    [Header("Connections")]
    [SerializeField]
    Image image;

    [SerializeField]
    TextMeshProUGUI title;

    [SerializeField]
    TextMeshProUGUI description;

    [SerializeField]
    GameObject innerPanel;

    [SerializeField]
    GameObject outerPanel;

    [Header("Animation Timing")]
    [SerializeField]
    float expandDuration = 0.55f; // Panel bursts open

    [SerializeField]
    float contractDuration = 0.45f; // Panel collapses away

    [SerializeField]
    float contentFadeIn = 0.35f; // Text/icon fades in after panel

    [SerializeField]
    float contentFadeOut = 0.2f; // Text/icon fades before panel

    [SerializeField]
    float contentFadeDelay = 0.25f; // Wait before fading content in

    [Header("Scale Punches")]
    [SerializeField]
    float overshootScale = 1.08f; // How much it "pops" past final size

    [SerializeField]
    float collapseScale = 0.0f; // Collapses to nothing on dismiss

    [Header("Glow / Shimmer")]
    [SerializeField]
    float glowPunchScale = 1.12f; // Extra punch on the inner panel

    [SerializeField]
    float glowDuration = 0.18f; // Duration of that micro-punch

    [Header("Sprite Connections")]
    [SerializeField]
    Sprite warning;

    [SerializeField]
    Sprite correct;

    [SerializeField]
    Sprite info;

    public enum AlertType
    {
        Warning,
        Correct,
        Info,
    }

    CanvasGroup _outerGroup;
    CanvasGroup _innerGroup;
    RectTransform _outerRect;
    RectTransform _innerRect;
    Coroutine _dismissCoroutine;

    [SerializeField]
    InputActionReference testInput;

    void Awake()
    {
        _outerRect = outerPanel.GetComponent<RectTransform>();
        _innerRect = innerPanel.GetComponent<RectTransform>();

        _outerGroup = GetOrAdd<CanvasGroup>(outerPanel);
        _innerGroup = GetOrAdd<CanvasGroup>(innerPanel);

        SetHiddenState();
    }

    public void SendAlert(
        float displayTime,
        string alertTitle,
        string alertDescription,
        Sprite alertSprite
    )
    {
        if (_dismissCoroutine != null)
            StopCoroutine(_dismissCoroutine);

        LeanTween.cancel(outerPanel);
        LeanTween.cancel(innerPanel);

        title.text = alertTitle;
        description.text = alertDescription;
        image.sprite = alertSprite;
        image.enabled = alertSprite != null;

        outerPanel.SetActive(true);
        _dismissCoroutine = StartCoroutine(AlertRoutine(displayTime));
    }

    public void SendAlert(
        float displayTime,
        string alertTitle,
        string alertDescription,
        AlertType alertType
    )
    {
        SendAlert(displayTime, alertTitle, alertDescription, GetAlertSprite(alertType));
    }

    Sprite GetAlertSprite(AlertType type)
    {
        switch (type)
        {
            case AlertType.Warning:
                return warning;
            case AlertType.Correct:
                return correct;
            case AlertType.Info:
                return info;
            default:
                return correct;
        }
    }

    IEnumerator AlertRoutine(float displayTime)
    {
        AnimateIn();
        yield return new WaitForSeconds(displayTime);
        AnimateOut(() => SetHiddenState());
    }

    void AnimateIn()
    {
        _outerRect.localScale = Vector3.zero;
        _innerRect.localScale = Vector3.one;
        _outerGroup.alpha = 0f;
        _innerGroup.alpha = 0f;

        LeanTween
            .scale(outerPanel, Vector3.one * overshootScale, expandDuration * 0.7f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() =>
            {
                LeanTween
                    .scale(outerPanel, Vector3.one, expandDuration * 0.35f)
                    .setEase(LeanTweenType.easeInOutSine);
            });

        LeanTween
            .alphaCanvas(_outerGroup, 1f, expandDuration * 0.55f)
            .setEase(LeanTweenType.easeOutQuad);

        LeanTween
            .alphaCanvas(_innerGroup, 1f, contentFadeIn)
            .setDelay(contentFadeDelay)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(GlowPunch);
    }

    void AnimateOut(System.Action onDone)
    {
        LeanTween.alphaCanvas(_innerGroup, 0f, contentFadeOut).setEase(LeanTweenType.easeInQuad);

        LeanTween
            .scale(outerPanel, Vector3.one * 1.04f, contractDuration * 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setOnComplete(() =>
            {
                LeanTween
                    .scale(outerPanel, Vector3.one * collapseScale, contractDuration * 0.75f)
                    .setEase(LeanTweenType.easeInExpo)
                    .setOnComplete(() => onDone?.Invoke());
            });

        LeanTween
            .alphaCanvas(_outerGroup, 0f, contractDuration * 0.65f)
            .setDelay(contractDuration * 0.2f)
            .setEase(LeanTweenType.easeInQuad);
    }

    void GlowPunch()
    {
        LeanTween
            .scale(innerPanel, Vector3.one * glowPunchScale, glowDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                LeanTween
                    .scale(innerPanel, Vector3.one, glowDuration)
                    .setEase(LeanTweenType.easeInOutSine);
            });
    }

    void SetHiddenState()
    {
        outerPanel.SetActive(false);
        _outerRect.localScale = Vector3.zero;
        _innerRect.localScale = Vector3.one;
        _outerGroup.alpha = 0f;
        _innerGroup.alpha = 0f;
    }

    static T GetOrAdd<T>(GameObject go)
        where T : Component
    {
        T comp = go.GetComponent<T>();
        return comp != null ? comp : go.AddComponent<T>();
    }
}
