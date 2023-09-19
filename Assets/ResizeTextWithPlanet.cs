using UnityEngine;

public class ResizeTextWithPlanet : MonoBehaviour
{
    public Transform planet; // Reference to the planet GameObject (assigned in the Inspector).

    // Adjust this factor to control the text size relative to the planet size.
    public float textScaleFactor = 0.05f;

    void Update()
    {
        if (planet != null)
        {
            // Calculate the desired scale for the text based on the planet's scale.
            float desiredScale = planet.localScale.x * textScaleFactor;

            // Apply the desired scale to the text GameObject.
            transform.localScale = new Vector3(desiredScale, desiredScale, desiredScale);
        }
    }
}

