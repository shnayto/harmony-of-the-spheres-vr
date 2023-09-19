using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public InputActionReference menuAction;
    public GameObject menuCanvas;

    private Vector3 originalPosition;
    private bool isTeleported = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnEnable()
    {
        menuAction.action.Enable();
        menuAction.action.performed += ShowMenu;
    }

    private void OnDisable()
    {
        menuAction.action.Disable();
        menuAction.action.performed -= ShowMenu;
    }

    private void ShowMenu(InputAction.CallbackContext context)
    {
        if (menuCanvas != null)
        {
            if (!isTeleported)
            {
                TeleportPlayer(new Vector3(0f, 3000f, 0f));
                menuCanvas.SetActive(true);
            }
            else
            {
                if (transform.position == originalPosition) // Check if the player is at the starting position
                {
                    TeleportPlayer(new Vector3(0f, 3000f, 0f)); // Teleport back to Y = 3000
                    menuCanvas.SetActive(true); // Enable the canvas
                }
                else
                {
                    TeleportPlayer(originalPosition); // Teleport back to the starting position
                    menuCanvas.SetActive(false); // Disable the canvas
                }
            }
            isTeleported = !isTeleported; // Toggle isTeleported after each teleportation
        }
    }

    private void Update()
    {
        if (isTeleported && transform.position.y < 2000f)
        {
            menuCanvas.SetActive(false); // Disable the canvas when the player goes below y = 2000
            isTeleported = false; // Reset isTeleported when the player manually goes below y = 2000
        }
    }

    private void TeleportPlayer(Vector3 targetPosition)
    {
        // Perform any additional logic here if required before teleporting

        // Teleport the player to the target position
        transform.position = targetPosition;
    }
}

