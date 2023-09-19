using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero; // The position to face (0, 0, 0) in world space
    public float rotationOffset = 180f; // The rotation offset in degrees

    private void Update()
    {
        // Calculate the direction from the current position to the target position
        Vector3 directionToTarget = targetPosition - transform.position;

        // If the direction is not zero, rotate the object to face the target position with the offset
        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

            // Apply the rotation offset
            targetRotation *= Quaternion.Euler(0f, rotationOffset, 0f);

            transform.rotation = targetRotation;
        }
    }
}
