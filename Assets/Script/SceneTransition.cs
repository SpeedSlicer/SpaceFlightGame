using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    FadeController fadeController;

    public void NextScene(string sceneName)
    {
        fadeController.SetFadeActive(true);
        StartCoroutine(loadSceneSeconds(fadeController.GetTimeSeconds(), sceneName));
    }

    public void NextScene(string sceneName, float seconds)
    {
        fadeController.SetFadeActive(true);
        StartCoroutine(loadSceneSeconds(seconds, sceneName));
    }

    public void StarScene(int starAmount, float rewardAmount)
    {
        NextScene("Stars");
        GameManager.rewardAmount = rewardAmount;
        GameManager.starAmount = starAmount;
    }

    IEnumerator loadSceneSeconds(float seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }
}
