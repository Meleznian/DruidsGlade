using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenFader : MonoBehaviour
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private static ScreenFader instance;
    public static ScreenFader Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindFirstObjectByType<ScreenFader>();
            return instance;
        }
    }
    void OnEnable()
    {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 0.5f;
            canvas = GetComponent<Canvas>();
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }
    }
    public static void FadeOut(float duration = 1f)
    {
        if (Instance != null)
            Instance.StartCoroutine(Instance.Fade(Instance.canvasGroup.alpha, 0f, duration));
    }
    public static void FadeIn(float duration = 1f)
    {
        if (Instance != null)
            Instance.StartCoroutine(Instance.Fade(Instance.canvasGroup.alpha, 1f, duration));
    }
    public static void FadeOutAndIn(Action action = null, float fadeOutTime = 1f, float fadedTime = 1f, float fadeInTime = 1f)
    {
        if (Instance != null)
            Instance.StartCoroutine(Instance.FadeSequence(action, fadeOutTime, fadedTime, fadeInTime));
    }
    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }
    private IEnumerator FadeSequence(Action action, float fadeOutDuration, float fadedDuration, float fadeInDuration)
    {
        yield return Fade(canvasGroup.alpha, 1f, fadeInDuration);
        yield return new WaitForSeconds(fadedDuration / 2.0f);
        action?.Invoke();
        yield return new WaitForSeconds(fadedDuration / 2.0f);
        yield return Fade(canvasGroup.alpha, 0f, fadeOutDuration);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
