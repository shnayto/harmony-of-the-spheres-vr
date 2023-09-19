using System.Collections;
using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    public Material blackMaterial; // Reference to the 'Black' material
    public float fadeDuration = 5f; // Duration of the fade-out effect in seconds

    private Renderer quadRenderer;
    private float targetAlpha = 0f;
    private float startAlpha = 1f;

    private void Start()
    {
        if (blackMaterial == null)
        {
            Debug.LogError("Please assign the 'Black' material to the script.");
            enabled = false;
            return;
        }

        // Get the renderer component of the quad
        quadRenderer = GetComponent<Renderer>();
        if (quadRenderer == null)
        {
            Debug.LogError("Renderer component not found on the quad.");
            enabled = false;
            return;
        }

        // Set the initial alpha value of the material to 1 (fully opaque)
        Color color = blackMaterial.color;
        color.a = 1f;
        blackMaterial.color = color;

        // Start the fade-out coroutine
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the fade-out

        float elapsedTime = 0f;

        // Gradually decrease the alpha value of the material to 0 (fully transparent)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);

            // Update the material's alpha value
            Color color = blackMaterial.color;
            color.a = currentAlpha;
            blackMaterial.color = color;

            yield return null; // Wait for the next frame
        }

        // Ensure the final alpha value is set to the target value
        Color finalColor = blackMaterial.color;
        finalColor.a = targetAlpha;
        blackMaterial.color = finalColor;
        Debug.LogError("Done");
    }
}
