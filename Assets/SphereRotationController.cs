using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotationController : MonoBehaviour
{
    public bool rotateEnabled = true; // Toggle to control rotation
    public float rotationSpeed = 200f; // Rotation speed in degrees per second
    private float originalRotationSpeed; // Original rotation speed value

    private void Start()
    {
        originalRotationSpeed = rotationSpeed;
    }

    private void FixedUpdate()
    {
        if (rotateEnabled)
        {
            // Rotate the sphere around the Y-axis
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void IncreaseRotationSpeedOverTime()
    {
        StartCoroutine(LerpRotationSpeed(originalRotationSpeed * 2f, 1f));
    }

    public void ResetRotationSpeedOverTime()
    {
        StartCoroutine(LerpRotationSpeed(originalRotationSpeed, 1f));
    }

    private IEnumerator LerpRotationSpeed(float targetSpeed, float duration)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;
        float startSpeed = rotationSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            rotationSpeed = Mathf.Lerp(startSpeed, targetSpeed, t);
            yield return null;
        }

        rotationSpeed = targetSpeed;
    }
}
