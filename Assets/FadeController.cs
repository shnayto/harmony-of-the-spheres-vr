using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public GameObject fadePanel;
    public float fadeDuration = 1f;
    public string sceneName;

    private CanvasGroup fadeCanvasGroup;
    private string targetSceneName;

    private void Start()
    {
        fadeCanvasGroup = fadePanel.GetComponent<CanvasGroup>();
        FadeIn();
    }

    public void FadeOut(string sceneName)
    {
        targetSceneName = sceneName;
        fadePanel.SetActive(true);
        StartCoroutine(FadeCanvasGroup(fadeCanvasGroup, 1f, fadeDuration));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(fadeCanvasGroup, 0f, fadeDuration));
        Invoke(nameof(DisableFadePanel), fadeDuration);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startTime = Time.time;
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    private void DisableFadePanel()
    {
        fadePanel.SetActive(false);
    }

    public void LoadSceneWithFade(string sceneName)
    {
        FadeOut(sceneName);
        Invoke(nameof(LoadScene), fadeDuration);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
