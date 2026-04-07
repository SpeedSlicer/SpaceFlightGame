using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceButtonTween
    : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler
{
    public float hoverScale = 1.08f;
    public float pressScale = 0.92f;

    public float hoverTime = 0.2f;
    public float pressTime = 0.08f;

    private Vector3 originalScale;
    private Image img;

    public Color normalColor = Color.white;
    public Color hoverColor = new Color(0.7f, 0.85f, 1f);

    private bool locked = false; // 👈 NEW

    void Start()
    {
        originalScale = transform.localScale;
        img = GetComponent<Image>();
    }

    // 👉 Call this before your reward animation
    public void LockTween()
    {
        locked = true;
        LeanTween.cancel(gameObject);
    }

    // 👉 Optional unlock if needed later
    public void UnlockTween()
    {
        locked = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (locked)
            return;

        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, originalScale * hoverScale, hoverTime).setEaseOutQuad();

        LeanTween
            .value(gameObject, img.color, hoverColor, hoverTime)
            .setOnUpdate((Color c) => img.color = c);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (locked)
            return;

        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, originalScale, hoverTime).setEaseInOutQuad();

        LeanTween
            .value(gameObject, img.color, normalColor, hoverTime)
            .setOnUpdate((Color c) => img.color = c);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (locked)
            return;

        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, originalScale * pressScale, pressTime).setEaseInQuad();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (locked)
            return;

        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, originalScale * hoverScale, 0.25f).setEaseOutBack();
    }
}
