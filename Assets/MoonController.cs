using UnityEngine;
using UnityEngine.Events;

public class MoonController : MonoBehaviour
{
    public float moonVelocity = 20f;
    public float moonDistance = 200f;
    public Transform earthTransform;

    private Vector3 moonAxis;
    private float currentMoonAngle = 0f;
    private float previousMoonAngle = 0f;
    private float fullOrbitAngle = 360f;
    private int orbitCount = 0;
    private FMOD.Studio.EventInstance shimmerEventInstance;

    public UnityEvent OnFullOrbitCompleted; // Event delegate for full orbit completion

    private void Start()
    {
        moonAxis = (transform.position - earthTransform.position).normalized;
    }

    private void FixedUpdate()
    {
        RotateAroundEarth();
        CheckFullOrbitCompletion();
    }

    private void RotateAroundEarth()
    {
        currentMoonAngle += moonVelocity * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, currentMoonAngle, 0f);
        Vector3 desiredPosition = earthTransform.position + rotation * (moonAxis * moonDistance);
        transform.position = desiredPosition;
    }

    private void CheckFullOrbitCompletion()
    {
        currentMoonAngle %= fullOrbitAngle; // Keep currentMoonAngle within the range of 0 to 360

        if (currentMoonAngle < previousMoonAngle)
        {
            orbitCount++;
            OnFullOrbitCompleted.Invoke();
        }

        previousMoonAngle = currentMoonAngle;
    }

    public bool HasCompletedFullOrbit()
    {
        // Add any additional conditions to check if the full orbit has been completed
        return orbitCount > 0;
    }

    public void SetShimmerEventInstance(FMOD.Studio.EventInstance eventInstance)
    {
        shimmerEventInstance = eventInstance;
    }
}



