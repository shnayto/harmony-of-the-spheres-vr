using UnityEngine;

public class ConstantTextScale : MonoBehaviour
{
    public Transform planet; // Reference to the planet GameObject (assigned in the Inspector).
    public float textScaleFactor = 0.05f; // Adjust this factor to control the initial text size.

    private Vector3 initialScale; // Store the initial scale of the text.

    void Start()
    {
        // Store the initial scale of the text.
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (planet != null)
        {
            // Calculate the inverse scale factor based on the planet's scale.
            float inverseScaleFactor = textScaleFactor / planet.localScale.x;

            // Apply the inverse scale factor to the text GameObject to maintain constant size.
            transform.localScale = initialScale * inverseScaleFactor;
        }
    }
}
