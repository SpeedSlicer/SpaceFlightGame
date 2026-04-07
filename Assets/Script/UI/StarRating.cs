using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarRewardUI : MonoBehaviour
{
    [Header("Stars")]
    public Image[] stars;
    public Sprite filledStar;
    public Sprite emptyStar;

    int currentRating = 0;
    bool lockedIn = false;

    [Header("Audio")]
    public AudioClip thudSound;
    public float volume = 1f;

    [Header("Reward Text")]
    public TextMeshProUGUI rewardText;
    public string fullText = "Reward Earned!";
    public float typingSpeed = 0.03f;

    [Header("Continue Button")]
    public GameObject continueButton;

    void Start()
    {
        UpdateVisuals();
        SetRating(GameManager.starAmount);
        fullText = $"+ {GameManager.rewardAmount} Coins";
        GameManager.AddCoins(GameManager.rewardAmount);
        continueButton.transform.localScale = Vector2.zero;
        ConfirmRating();
    }

    public void SetRating(int rating)
    {
        if (lockedIn)
            return;

        currentRating = rating;
        UpdateVisuals();
    }

    public void RemoveRating()
    {
        if (lockedIn)
            return;

        currentRating = 0;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = (i < currentRating) ? filledStar : emptyStar;
        }
    }

    public void ConfirmRating()
    {
        if (lockedIn || currentRating == 0)
            return;

        lockedIn = true;

        PlaySlamEffect();
        StartCoroutine(RewardSequence());
    }

    void PlaySlamEffect()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i >= currentRating)
                continue;

            GameObject star = stars[i].gameObject;
            RectTransform rt = star.GetComponent<RectTransform>();
            Image img = stars[i];

            float delay = i * 0.07f;
            bool isLast = (i == currentRating - 1);
            float scaleMultiplier = isLast ? 1.9f : 1.6f;

            rt.localScale = Vector3.zero;
            img.color = new Color(1f, 1f, 1f, 0f);

            LeanTween.alpha(rt, 1f, 0.05f).setDelay(delay);

            LeanTween
                .scale(rt, Vector3.one * 0.65f, 0.08f)
                .setDelay(delay)
                .setEase(LeanTweenType.easeInQuad);

            LeanTween
                .scale(rt, Vector3.one * scaleMultiplier, 0.18f)
                .setDelay(delay + 0.08f)
                .setEase(LeanTweenType.easeOutBack)
                .setOnComplete(() =>
                {
                    AudioSource.PlayClipAtPoint(thudSound, Camera.main.transform.position, volume);

                    img.color = Color.white * 2f;

                    LeanTween
                        .value(star, Color.white * 2f, Color.white, 0.2f)
                        .setOnUpdate((Color c) => img.color = c);
                });

            LeanTween
                .scale(rt, Vector3.one, 0.12f)
                .setDelay(delay + 0.26f)
                .setEase(LeanTweenType.easeInOutQuad);

            rt.localRotation = Quaternion.Euler(0, 0, Random.Range(-15f, 15f));
            LeanTween
                .rotateZ(star, 0f, 0.25f)
                .setDelay(delay + 0.08f)
                .setEase(LeanTweenType.easeOutBack);
        }

        LeanTween.moveLocalX(gameObject, 8f, 0.05f).setEaseShake().setLoopPingPong(2);
    }

    IEnumerator RewardSequence()
    {
        yield return new WaitForSeconds(0.6f + currentRating * 0.07f);

        yield return StartCoroutine(TypeText(fullText));

        yield return new WaitForSeconds(0.2f);

        ShowButton();
    }

    IEnumerator TypeText(string text)
    {
        rewardText.text = "";

        foreach (char c in text)
        {
            rewardText.text += c;

            rewardText.transform.localScale = Vector3.one * 1.05f;
            LeanTween.scale(rewardText.rectTransform, Vector3.one, 0.1f);

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void ShowButton()
    {
        RectTransform rt = continueButton.GetComponent<RectTransform>();
        CanvasGroup cg = continueButton.GetComponent<CanvasGroup>();

        if (cg == null)
            cg = continueButton.AddComponent<CanvasGroup>();

        rt.localScale = Vector3.zero;
        cg.alpha = 0f;

        LeanTween.alphaCanvas(cg, 1f, 0.2f);

        LeanTween
            .scale(rt, Vector3.one * 1.2f, 0.25f)
            .setEase(LeanTweenType.easeOutBack)
            .setOnComplete(() =>
            {
                LeanTween.scale(rt, Vector3.one, 0.1f).setEase(LeanTweenType.easeInOutQuad);

                SpaceButtonTween tween = continueButton.GetComponent<SpaceButtonTween>();
                if (tween != null)
                {
                    tween.UnlockTween();
                }
            });
    }
}
