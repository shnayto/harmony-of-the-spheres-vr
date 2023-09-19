using UnityEngine;

public class HoverSphereCollider : MonoBehaviour
{
    public float hoverRadiusIncrease = 3f; // Amount to increase the radius when hovering on
    public float maxDistanceFromPlanet = 600f;
    public Transform earthTransform;

    private SphereCollider sphereCollider;
    private float originalRadius;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        originalRadius = sphereCollider.radius;
    }

    // Call this function to enable the hover effect
    public void OnHover()
    {
        if (IsCameraNearPlanet())
        {
            sphereCollider.radius = originalRadius + hoverRadiusIncrease;
        }
    }

    // Call this function to disable the hover effect and restore the original radius
    public void OffHover()
    {
        if (IsCameraNearPlanet())
        {
         sphereCollider.radius = originalRadius;
        }
    }

    private bool IsCameraNearPlanet()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
            return false;

        float distanceToPlanet = Vector3.Distance(mainCamera.transform.position, earthTransform.position);
        return distanceToPlanet > maxDistanceFromPlanet;
    }
}
