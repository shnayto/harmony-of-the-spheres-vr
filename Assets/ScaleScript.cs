using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleScript : MonoBehaviour
{
    public InputActionReference scaleReference = null;
    private Transform objectTransform = null;
    public bool isScalingEnabled = true;
    public float minScale = 100f; // Minimum scale value
    public float maxScale = 500f; // Maximum scale value
    public AudioSource audioSource = null;
    public float minVolume = 0f; // Minimum volume value
    public float maxVolume = 1f; // Maximum volume value

    private void Awake()
    {
        objectTransform = transform;
    }

    private void Update()
    {
        if (!isScalingEnabled)
            return;

        float value = scaleReference.action.ReadValue<float>();

        float scaledScale = Mathf.Lerp(minScale, maxScale, value); // Map the input value to the scale range
        UpdateScale(scaledScale);

        float scaledVolume = Mathf.Lerp(minVolume, maxVolume, value); // Map the input value to the volume range
        UpdateVolume(scaledVolume);
    }

    private void UpdateScale(float value)
    {
        Vector3 newScale = Vector3.one * value;
        objectTransform.localScale = newScale;
    }

    private void UpdateVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
        }
    }

    public void EnableScaling()
    {
        isScalingEnabled = true;
    }

    public void DisableScaling()
    {
        isScalingEnabled = false;
    }

    public bool IsScalingEnabled()
    {
        return isScalingEnabled;
    }
}


