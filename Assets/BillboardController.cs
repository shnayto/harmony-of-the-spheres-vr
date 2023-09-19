using UnityEngine;

public class BillboardController : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found in the scene. Make sure you have a Camera tagged as 'MainCamera'.");
        }
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            // Get the forward direction of the main camera
            Vector3 cameraForward = mainCamera.transform.forward;

            // Ignore the camera's Y component to avoid flipping
            cameraForward.y = 0f;

            // Set the quad's rotation to look in the same direction as the camera
            if (cameraForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(cameraForward);
            }
        }
    }
}

