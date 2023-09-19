using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Calculate the direction from the "Glow Hover" to the camera
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;

            // Calculate the rotation to face the camera along the X-axis
            Quaternion targetRotation = Quaternion.LookRotation(-directionToCamera, Vector3.up);

            // Apply the rotation to the text
            transform.rotation = targetRotation;
        }
    }
}

