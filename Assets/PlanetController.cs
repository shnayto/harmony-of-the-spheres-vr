using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public float velocity = 14f;
    public float distance = 1800f;
    public Transform cameraOffset;

    public string sunTag = "Sun"; // Tag assigned to the sun object

    private Transform sunTransform;
    private Vector3 orbitAxis;
    private Vector3 initialPosition;
    private Vector3 lastDesiredPosition; // Store the last desired position
    private bool isOrbiting = true; // Flag to control orbital motion
    private bool isVelocitySuspended = false; // Flag to track velocity suspension
    private float savedVelocity; // Saved velocity value when suspended
    private float currentAngle = 0f; // Current angle of rotation

    private void Awake()
    {
        sunTransform = GameObject.FindGameObjectWithTag(sunTag).transform;
        initialPosition = transform.position;
        orbitAxis = (transform.position - sunTransform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (isOrbiting)
        {
            if (isVelocitySuspended)
            {
                PauseOrbit();
            }
            else
            {
                ResumeOrbit();
            }
        }
    }

    private void PauseOrbit()
    {
        // Set the position to the last desired position
        transform.position = lastDesiredPosition;
    }

    private void ResumeOrbit()
    {
        currentAngle += velocity * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f,currentAngle,0f);
        Vector3 desiredPosition = rotation * (orbitAxis * distance);
        transform.position = desiredPosition;
        // Store the last desired position
        lastDesiredPosition = desiredPosition;
    }

    public void PauseMotion()
    {
        isVelocitySuspended = true;
    }

    public void ResumeMotion()
    {
        if (cameraOffset.position.y >= 1000f)
        {
            isVelocitySuspended = false;
        }
    }
}
