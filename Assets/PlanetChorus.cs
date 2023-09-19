using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlanetChorus : MonoBehaviour
{
    public string parameterName = "Earth Chorus"; // Parameter name for the Earth Chorus
    public float minRotationSpeed = 1f;
    public float maxRotationSpeed = 500f;

    private SphereRotator sphereRotator; // Reference to the SphereRotator script

    private void Awake()
    {
        sphereRotator = GetComponent<SphereRotator>(); // Get the SphereRotator component attached to the same GameObject
    }

    private void Update()
    {
        // Get the rotation speed from the SphereRotator script
        float rotationSpeed = sphereRotator.rotationSpeed;

        // Map the rotation speed to the parameter range (e.g., 0 to 1)
        float normalisedRotationSpeed = Mathf.InverseLerp(minRotationSpeed, maxRotationSpeed, rotationSpeed);

        // Set the global parameter value based on the normalized rotation speed
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameterName, normalisedRotationSpeed);
    }
}


