using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public class ScreenFader : MonoBehaviour
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private static ScreenFader instance;

    public static ScreenFader Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ScreenFader>();
            return instance;
        }
    }

    void OnEnable()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 0.5f;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign camera after scene load
        if (canvas != null)
            canvas.worldCamera = Camera.main;
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

    private IEnumerator FadeAndLoadSceneSequence(string sceneName, float fadeOutDuration, float fadedWait, float fadeInDuration)
    {
        yield return Fade(canvasGroup.alpha, 1f, fadeOutDuration); // Fade to black
        yield return new WaitForSeconds(fadedWait);
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.1f); // Short wait for camera to initialize
        yield return Fade(1f, 0f, fadeInDuration); // Fade back in
    }

    public void FadeAndLoadScene(string sceneName, float fadeOut = 1f, float wait = 0.2f, float fadeIn = 1f)
    {
        StartCoroutine(FadeAndLoadSceneSequence(sceneName, fadeOut, wait, fadeIn));
    }

    // 🔥 This method shows up in the Unity UI Button's OnClick() dropdown
    public void LoadSceneFromButton(string sceneName)
    {
        FadeAndLoadScene(sceneName); // Uses default fade durations
    }
}
