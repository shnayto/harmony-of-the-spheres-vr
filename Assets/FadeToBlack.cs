using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    private void Start()
    {
        StartCoroutine(FadeToBlackScreen());
    }

    private IEnumerator FadeToBlackScreen()
    {
        fadeImage.color = Color.clear;

        // Fade to black
        float startTime = Time.time;
        while (Time.time - startTime < fadeDuration)
        {
            float alpha = (Time.time - startTime) / fadeDuration;
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        fadeImage.color = Color.black;

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}