using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlanetScaler : MonoBehaviour
{
    public float minScale = 100f;
    public float maxScale = 400f;
    public float sensitivity = 1f;

    private XRBaseInteractor interactor1;
    private XRBaseInteractor interactor2;
    private float initialDistance;
    private Vector3 initialScale;

    private void Update()
    {
        if (interactor1 != null && interactor2 != null)
        {
            float currentDistance = Vector3.Distance(interactor1.transform.position, interactor2.transform.position);
            float scaleFactor = currentDistance / initialDistance;
            Vector3 newScale = initialScale * scaleFactor;

            // Clamp the scale within the specified range
            newScale = Vector3.ClampMagnitude(newScale, maxScale);
            newScale = Vector3.Max(newScale, Vector3.one * minScale);

            transform.localScale = newScale;
        }
    }

    public void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (interactor1 == null)
        {
            interactor1 = interactor;
        }
        else if (interactor2 == null)
        {
            interactor2 = interactor;

            // Calculate the initial distance between the interactors
            initialDistance = Vector3.Distance(interactor1.transform.position, interactor2.transform.position);

            // Store the initial scale of the planet
            initialScale = transform.localScale;
        }
    }

    public void OnSelectExited(XRBaseInteractor interactor)
    {
        if (interactor == interactor1)
        {
            interactor1 = null;
        }
        else if (interactor == interactor2)
        {
            interactor2 = null;
        }
    }
}

