using UnityEngine;

public class EarthInitializer : MonoBehaviour
{
    public GameObject moonPrefab;
    public Transform earthTransform;

    private void Start()
    {
        // Create the first moon for Earth
        CreateMoon();
    }

    private void CreateMoon()
    {
        GameObject newMoon = Instantiate(moonPrefab, earthTransform);
        MoonController moonController = newMoon.GetComponent<MoonController>();
        moonController.earthTransform = earthTransform;

        // Set the initial distance and velocity for the moon
        moonController.moonDistance = 300f;
        moonController.moonVelocity = 30f; // Corresponding velocity for distance 300f

        // Attach the FMOD event instance to the moon's game object
        FMOD.Studio.EventInstance shimmerEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/ShimmerSound");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(shimmerEventInstance, newMoon.transform, newMoon.GetComponent<Rigidbody>());

        // Invert the normalized distance here
        float normalizedDistance = 1f;
        shimmerEventInstance.setParameterByName("Distance", normalizedDistance);

        // Start the shimmer sound
        shimmerEventInstance.start();
        moonController.SetShimmerEventInstance(shimmerEventInstance);
    }
}
