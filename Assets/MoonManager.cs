using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class MoonManager : MonoBehaviour
{
    public GameObject moonPrefab;
    public Transform earthTransform;
    public Transform teleportTransform;
    public InputActionReference buttonAAction;
    public InputActionReference buttonBAction;
    public int maxMoonCount = 10;
    public float maxDistanceFromPlanet = 200f;
    public float distanceScale = 1.0f;
    public float sizeScale = 1.0f;
    public string shimmerSoundEvent = "event:/ShimmerSound"; // Default event path

    private List<GameObject> moons = new List<GameObject>();
    private List<FMOD.Studio.EventInstance> shimmerEventInstances = new List<FMOD.Studio.EventInstance>();

    private void OnEnable()
    {
        buttonAAction.action.Enable();
        buttonBAction.action.Enable();
        buttonAAction.action.started += OnButtonAPressed;
        buttonBAction.action.started += OnButtonBPressed;
    }

    private void OnDisable()
    {
        buttonAAction.action.Disable();
        buttonBAction.action.Disable();
        buttonAAction.action.started -= OnButtonAPressed;
        buttonBAction.action.started -= OnButtonBPressed;
    }

    private void OnButtonAPressed(InputAction.CallbackContext context)
    {
        if (moons.Count < maxMoonCount && IsCameraNearPlanet())
        {
            CreateMoon();
        }
    }

    private void OnButtonBPressed(InputAction.CallbackContext context)
    {
        if (moons.Count > 0 && IsCameraNearPlanet())
        {
            DeleteOldestMoon();
        }
    }

    private bool IsCameraNearPlanet()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
            return false;

        float distanceToPlanet = Vector3.Distance(mainCamera.transform.position, teleportTransform.position);
        return distanceToPlanet < maxDistanceFromPlanet;
    }

    //  private void Update()
    // {
    //     if (Keyboard.current.upArrowKey.wasPressedThisFrame)
    //     {
    //         if (moons.Count < maxMoonCount)
    //         {
    //             CreateMoon();
    //         }
    //     }

    //     if (Keyboard.current.downArrowKey.wasPressedThisFrame)
    //     {
    //         if (moons.Count > 0)
    //         {
    //             DeleteOldestMoon();
    //         }
    //     }
    // }


    private void CreateMoon()
{
    GameObject newMoon = Instantiate(moonPrefab);
    MoonController moonController = newMoon.GetComponent<MoonController>();
    moonController.earthTransform = earthTransform;

    // Set specific distances for the first 4 moons
    if (moons.Count < 4)
    {
        if (moons.Count == 0)
        {
            // First moon, between 2 and 2.6 times the sizeScale
            float randomizedSizeScale = Random.Range(2f, 2.6f) * sizeScale;
            moonController.transform.localScale = Vector3.one * randomizedSizeScale;
            moonController.moonDistance = (500f - (60f * moons.Count)) * distanceScale;
        }
        else if (moons.Count == 1)
        {
            // Second moon, between 1.7 and 2.3 times the sizeScale
            float randomizedSizeScale = Random.Range(1.3f, 2f) * sizeScale;
            moonController.transform.localScale = Vector3.one * randomizedSizeScale;
            moonController.moonDistance = (500f - (60f * moons.Count)) * distanceScale;
        }
        else
        {
            // Third and Fourth moons, with standard distance logic and sizeScale
            float randomizedSizeScale = Random.Range(0.7f, 1.3f) * sizeScale;
            moonController.transform.localScale = Vector3.one * randomizedSizeScale;
            moonController.moonDistance = (500f - (60f * moons.Count)) * distanceScale;
        }
    }
    else
    {
        // Other moons, with random distance between 100 and 300 and standard sizeScale
        float randomizedSizeScale = Random.Range(0.7f, 1.3f) * sizeScale;
        moonController.transform.localScale = Vector3.one * randomizedSizeScale;
        moonController.moonDistance = Random.Range(250f, 400f) * distanceScale;
    }

    // Linearly interpolate velocity based on distance
    float minDistance = 250f * distanceScale;
    float maxDistance = 500f * distanceScale;
    float minVelocity = 70f;
    float maxVelocity = 30f;
    float normalizedDistance = Mathf.InverseLerp(minDistance, maxDistance, moonController.moonDistance);
    
    moonController.moonVelocity = Mathf.Lerp(minVelocity, maxVelocity, normalizedDistance);
    moons.Add(newMoon);

    FMOD.Studio.EventInstance shimmerEventInstance = FMODUnity.RuntimeManager.CreateInstance(shimmerSoundEvent); // Use the variable here
    shimmerEventInstances.Add(shimmerEventInstance);

    // Invert the normalized distance here
    normalizedDistance = 1f - normalizedDistance;
    Debug.Log(normalizedDistance);
    shimmerEventInstance.setParameterByName("Distance", normalizedDistance);

    // Attach the FMOD event instance to the moon's game object
    FMODUnity.RuntimeManager.AttachInstanceToGameObject(shimmerEventInstance, newMoon.transform, newMoon.GetComponent<Rigidbody>());

    shimmerEventInstance.start();
    moonController.SetShimmerEventInstance(shimmerEventInstance);
}


    private void DeleteOldestMoon()
    {
        if (moons.Count > 0)
        {
            int lastIndex = moons.Count - 1;
            GameObject newestMoon = moons[lastIndex];
            FMOD.Studio.EventInstance newestShimmerEventInstance = shimmerEventInstances[lastIndex];

            moons.RemoveAt(lastIndex);
            shimmerEventInstances.RemoveAt(lastIndex);

            Destroy(newestMoon);
            newestShimmerEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            newestShimmerEventInstance.release();
        }
    }

    private void OnDestroy()
    {
        // Stop and release all FMOD event instances when the object is destroyed or the scene changes
        foreach (var shimmerEventInstance in shimmerEventInstances)
        {
            if (shimmerEventInstance.isValid())
            {
                shimmerEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                shimmerEventInstance.release();
            }
        }
    }

}

