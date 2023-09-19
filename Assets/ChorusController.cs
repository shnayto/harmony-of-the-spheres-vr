using UnityEngine;
using UnityEngine.Audio;

public class ChorusController : MonoBehaviour
{
    public Transform sphereTransform;
    public AudioChorusFilter chorusFilter;
    public float minRotationSpeed = 0f;
    public float maxRotationSpeed = 1000f;
    public float minChorusDepth = 0f;
    public float maxChorusDepth = 1f;

    private void FixedUpdate()
    {
        // Get the rotation speed from the sphere's rotation
        float rotationSpeed = sphereTransform.rotation.eulerAngles.magnitude;

        // Map the rotation speed to the chorus depth range
        float chorusDepth = Mathf.Lerp(minChorusDepth, maxChorusDepth, Mathf.InverseLerp(minRotationSpeed, maxRotationSpeed, rotationSpeed));

        // Set the chorus depth property of the Audio Chorus Filter
        chorusFilter.depth = chorusDepth;
    }
}




