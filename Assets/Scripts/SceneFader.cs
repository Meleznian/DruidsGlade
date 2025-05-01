using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void StartGame(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeIn()
    {
        float t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            fadeGroup.alpha = t / fadeDuration;
            yield return null;
        }
        fadeGroup.alpha = 0;
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = t / fadeDuration;
            yield return null;
        }
        fadeGroup.alpha = 1;
        SceneManager.LoadScene(sceneName);
    }
}
