using UnityEngine;
using FMODUnity;

public class PlanetVolumeCompensation : MonoBehaviour
{
    public string parameterName = "YourGlobalParameterName"; // Set this to your FMOD global parameter name
    public float maxDistanceFromPlanet = 600f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found in the scene.");
            return;
        }
    }

    private void Update()
    {
        if (mainCamera == null)
            return;

        float distanceToPlanet = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Set the FMOD parameter value to 1 if the distance is beyond the threshold, 0 otherwise
        float parameterValue = distanceToPlanet > maxDistanceFromPlanet ? 1f : 0f;

        // Set the FMOD parameter value
        RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
    }
}

