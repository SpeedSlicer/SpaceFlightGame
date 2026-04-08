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

    public void StarScene(int starAmount, float rewardAmount, float time, int level)
    {
        NextScene("Stars");
        GameManager.rewardAmount = rewardAmount;
        GameManager.starAmount = starAmount;
        GameManager.time = time;
        if (float.IsPositiveInfinity(GameManager.maxLevelTimes[level]))
        {
            GameManager.maxLevelTimes[level] = time;
        }
        else if (GameManager.maxLevelTimes[level] > time)
        {
            GameManager.maxLevelTimes[level] = time;
        }
        GameManager.level = level;
    }

    IEnumerator loadSceneSeconds(float seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }
}
