using UnityEngine;
using FMODUnity;

public class PlanetHoverVolume : MonoBehaviour
{
    public string parameterName = "Earth Hover"; // Default parameter name
    public float rampDuration = 0.2f; // Duration for ramping up and down (in seconds)
    public float hoverDuration = 0.5f; // Duration for hovering at max volume (in seconds)
    public float maxDistanceFromPlanet = 600f;
    public Transform earthTransform;

    private float currentVolume = 0f;

    private void Start()
    {
        UpdateParameter();
    }

    private void Update()
    {
        UpdateParameter();
    }

    // Call this function to start the 'Hover On' effect
    public void HoverOn()
    {
        if (IsCameraNearPlanet())
        {
            StartCoroutine(RampVolume(1f, rampDuration));
            Invoke("StartHoverOff", rampDuration + hoverDuration);
        }
    }

    private void StartHoverOff()
    {
        StartCoroutine(RampVolume(0f, rampDuration*4));
    }

    private void UpdateParameter()
    {
        // Do something with the currentVolume value if needed
    }

    private System.Collections.IEnumerator RampVolume(float targetVolume, float duration)
    {
        float startVolume = currentVolume;
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            currentVolume = Mathf.Lerp(startVolume, targetVolume, t);
            RuntimeManager.StudioSystem.setParameterByName(parameterName, currentVolume);
            yield return null;
        }

        currentVolume = targetVolume;
        RuntimeManager.StudioSystem.setParameterByName(parameterName, currentVolume);
    }

    private bool IsCameraNearPlanet()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
            return false;

        float distanceToPlanet = Vector3.Distance(mainCamera.transform.position, earthTransform.position);
        return distanceToPlanet > maxDistanceFromPlanet;
    }
}


