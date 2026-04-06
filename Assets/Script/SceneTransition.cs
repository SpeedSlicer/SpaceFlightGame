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

    IEnumerator loadSceneSeconds(float seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }
}
