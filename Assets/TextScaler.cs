using UnityEngine;
using TMPro;

public class TextScaler : MonoBehaviour
{
    public Transform earthTransform; // Reference to the planet's transform
    public float maxDistanceFromPlanet = 10f; // The maximum distance from the planet to scale the text
    public float scaleFactor = 2f; // The scale factor to apply when the camera is far enough

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (IsCameraNearPlanet())
        {
            transform.localScale = initialScale;
        }
        else
        {
            transform.localScale = initialScale * scaleFactor;
        }
    }

    private bool IsCameraNearPlanet()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null || earthTransform == null)
            return false;

        float distanceToPlanet = Vector3.Distance(mainCamera.transform.position, earthTransform.position);
        return distanceToPlanet < maxDistanceFromPlanet;
    }
}

