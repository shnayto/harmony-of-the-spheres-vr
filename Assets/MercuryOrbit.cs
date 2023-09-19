using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercuryOrbit : MonoBehaviour
{
    public Transform sunTransform;
    public float gravitationalConstant = 1f;
    public float majorAxis = 5f;
    public float eccentricity = 0.5f;
    public float orbitSpeed = 1f;
    public float rotationSpeed = 2f;
    public float angularVelocity = 1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Calculate the initial position on the ellipse
        float angle = Random.Range(0f, 360f);
        Vector3 initialPosition = CalculateEllipsePosition(angle);

        // Set the initial position of the planet
        transform.position = initialPosition;

        // Calculate the distance and direction between the planet and the sun
        Vector3 directionToSun = sunTransform.position - transform.position;
        float distanceToSun = directionToSun.magnitude;

        // Calculate the initial velocity perpendicular to the line connecting the planet and the sun
        Vector3 initialDirection = directionToSun.normalized;
        Vector3 initialVelocity = Vector3.Cross(initialDirection, Vector3.up) * CalculateOrbitalVelocity(distanceToSun);

        // Apply the initial velocity to the planet
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        // Calculate the distance and direction between the planet and the sun
        Vector3 directionToSun = sunTransform.position - transform.position;
        float distanceToSun = directionToSun.magnitude;

        // Calculate the gravitational force based on the position of the planet relative to the sun
        Vector3 gravitationalForce = CalculateGravitationalForce(directionToSun, distanceToSun);

        // Update planet's velocity based on the gravitational force
        rb.velocity += gravitationalForce * Time.deltaTime;

        // Update planet's position based on the velocity
        transform.position += rb.velocity * Time.deltaTime;

        // Rotate the planet around its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Rotate the planet around the sun
        transform.RotateAround(sunTransform.position, sunTransform.up, angularVelocity * Time.deltaTime);
    }

    private Vector3 CalculateGravitationalForce(Vector3 directionToSun, float distanceToSun)
    {
        return gravitationalConstant * sunTransform.GetComponent<Rigidbody>().mass * rb.mass * directionToSun.normalized / Mathf.Pow(distanceToSun, 2);
    }

    private float CalculateOrbitalVelocity(float distanceToSun)
    {
        float semiMajorAxis = majorAxis / 2f;
        return Mathf.Sqrt(gravitationalConstant * sunTransform.GetComponent<Rigidbody>().mass * (2f / distanceToSun - 1f / semiMajorAxis));
    }

    private Vector3 CalculateEllipsePosition(float angle)
    {
        float semiMajorAxis = majorAxis / 2f;
        float semiMinorAxis = semiMajorAxis * Mathf.Sqrt(1f - eccentricity * eccentricity);
        float x = semiMajorAxis * Mathf.Cos(Mathf.Deg2Rad * angle);
        float y = semiMinorAxis * Mathf.Sin(Mathf.Deg2Rad * angle);
        return sunTransform.position + sunTransform.right * x + sunTransform.up * y;
    }
}
