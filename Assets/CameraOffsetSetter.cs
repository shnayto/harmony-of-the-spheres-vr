using UnityEngine;

public class CameraOffsetSetter : MonoBehaviour
{
    public float yOffset = 1100f;
    public float delay = 0.2f;

    private GameObject cameraOffsetObject;
    private Transform cameraOffsetTransform;
    private bool isFirstTeleport = true;

    private void Start()
    {
        cameraOffsetObject = GameObject.Find("Camera Offset");
        if (cameraOffsetObject != null)
        {
            cameraOffsetTransform = cameraOffsetObject.transform;
        }

        Invoke(nameof(ChangeCameraOffset), delay);
    }

    private void ChangeCameraOffset()
    {
        if (cameraOffsetTransform != null)
        {
            if (isFirstTeleport)
            {
                // Set the initial y offset.
                Vector3 newPosition = cameraOffsetTransform.position;
                newPosition.y = yOffset;
                cameraOffsetTransform.position = newPosition;
            }
        }
    }

    private void Update()
    {
        if (HasTeleported())
        {
            if (isFirstTeleport)
            {
                // Reset the y offset to 0 after the first teleportation.
                Vector3 newPosition = cameraOffsetTransform.position;
                newPosition.y = 0f;
                cameraOffsetTransform.position = newPosition;

                isFirstTeleport = false; // Update the flag to indicate the first teleportation has occurred.
                Debug.Log("First teleportation has occurred. isFirstTeleport is now false.");
            }
        }
    }

    private bool HasTeleported()
    {
        // Check if a teleportation has occurred by comparing the current position
        // with the previous frame's position.
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, transform.position);
        return distance > 0.1f; // You can adjust this threshold as needed.
    }
}
