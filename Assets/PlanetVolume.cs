using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlanetVolume : MonoBehaviour
{
    public Transform planetTransform;
    public string parameterName = "Earth Volume"; // Default parameter name
    public float minScale = 140f;
    public float maxScale = 340f;

    private void Start()
    {
        UpdateParameter();
    }

    private void Update()
    {
        UpdateParameter();
    }

    private void UpdateParameter()
    {
        // Get the current scale of the planet object
        float planetScale = planetTransform.localScale.x;

        // Map the planet scale to the parameter range (e.g., 0.1 to 1)
        float normalisedScale = Mathf.InverseLerp(minScale, maxScale, planetScale);

        // Set the global parameter value based on the normalized planet scale
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameterName, normalisedScale);
    }
}


