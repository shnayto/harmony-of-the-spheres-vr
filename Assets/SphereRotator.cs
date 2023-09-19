using UnityEngine;
using UnityEngine.InputSystem;

public class SphereRotator : MonoBehaviour
{
    public float rotationSpeedMultiplier = 5f;
    public InputActionReference gripAction;
    public InputActionReference controllerVelocityAction;
    public float rotationSpeed = 0f; // Public variable for rotation speed
    public bool isGripEnabled = true; // Public variable to enable/disable grip control
    public SphereCollider sphereCollider; // Reference to the SphereCollider component
    public float maxDistanceFromPlanet = 200f;
    public Transform earthTransform;

    private bool isGripPressed = false;
    private Vector3 lastControllerVelocity = Vector3.zero;
    public float radiusLeeway = 3f;
    private float originalColliderRadius; // Store the original radius of the sphere collider
    private float enlargedColliderRadius;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        originalColliderRadius = sphereCollider.radius;
        enlargedColliderRadius = originalColliderRadius * 8f; // Store the original size of the sphere collider
    }

    private void OnEnable()
    {
        // Enable the grip and controller velocity actions
        gripAction.action.Enable();
        controllerVelocityAction.action.Enable();

        // Subscribe to grip press and release events
        gripAction.action.started += OnGripPress;
        gripAction.action.canceled += OnGripRelease;
    }

    private void OnDisable()
    {
        // Disable the grip and controller velocity actions
        gripAction.action.Disable();
        controllerVelocityAction.action.Disable();

        // Unsubscribe from grip press and release events
        gripAction.action.started -= OnGripPress;
        gripAction.action.canceled -= OnGripRelease;
    }

   private void Update()
{
    // Check if both grip is pressed and velocity tracking is enabled
    if (isGripPressed && isGripEnabled && controllerVelocityAction.action.enabled)
    {
        // Get the controller's velocity from the action
        Vector3 controllerVelocity = controllerVelocityAction.action.ReadValue<Vector3>();

        // Calculate the rotation speed based on the magnitude of the velocity
        rotationSpeed = controllerVelocity.magnitude * rotationSpeedMultiplier;

        // Rotate the sphere around its Y-axis at the calculated speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Check if the Main Camera is near the planet and resize the sphere collider accordingly
        if (IsCameraNearPlanet())
        {
            sphereCollider.radius = enlargedColliderRadius;
        }
        else
        {
            sphereCollider.radius = originalColliderRadius;
        }

        // Store the current velocity as the last velocity
        lastControllerVelocity = controllerVelocity;
    }
    else if (lastControllerVelocity != Vector3.zero)
    {
        // If grip is released and there's a stored last velocity, continue spinning using the last velocity
        rotationSpeed = lastControllerVelocity.magnitude * rotationSpeedMultiplier;
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
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


    private void OnGripPress(InputAction.CallbackContext context)
    {
        isGripPressed = true;
    }

    private void OnGripRelease(InputAction.CallbackContext context)
    {
        isGripPressed = false;
    }

    public void EnableGripControl()
    {
        isGripEnabled = true;
    }

    public void DisableGripControl()
    {
        isGripEnabled = false;
        sphereCollider.radius = originalColliderRadius; // Restore the sphere collider radius when grip control is disabled
    }

    // Call this function to enable the hover effect
    public void OnHover()
    {
        if (IsCameraNearPlanet())
        {
            sphereCollider.radius = originalColliderRadius*radiusLeeway;
        }
    }

    // Call this function to disable the hover effect and restore the original radius
    public void OffHover()
    {
        if (IsCameraNearPlanet())
        {
         sphereCollider.radius = originalColliderRadius;
        }
    }
}