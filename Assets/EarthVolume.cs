using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class EarthVolume : MonoBehaviour
{
    public Transform earthTransform;

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
        // Get the current scale of the Earth object
        float earthScale = earthTransform.localScale.x;

        // Map the Earth scale to the Earth Volume parameter range (e.g., 0.1 to 1)
        float normalizedScale = Mathf.InverseLerp(140f, 340f, earthScale); // Assuming the Earth scale can vary between 140 to 340

        // Set the global parameter value based on the normalized Earth scale
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Earth Volume", normalizedScale);
    }
}
