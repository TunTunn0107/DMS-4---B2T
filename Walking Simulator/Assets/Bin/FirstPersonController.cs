using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float movementSpeed = 5.0f; // Speed of the character movement
    public float mouseSensitivity = 100.0f; // Sensitivity of the mouse movement
    public Transform playerCamera; // Reference to the camera transform
    public float cameraPitchLimit = 90.0f; // Limit to how far up or down the camera can look

    // Camera bobbing variables
    public float bobbingFrequency = 2f; // How fast the bobbing occurs
    public float bobbingAmount = 0.05f; // The amount of bobbing up and down
    private float bobbingTimer = 0.0f;

    private CharacterController controller;
    private float cameraPitch = 0.0f; // Current pitch of the camera
    private Vector3 originalCameraPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Make the cursor invisible
        originalCameraPosition = playerCamera.localPosition; // Store the original local position of the camera
    }

    void Update()
    {
        UpdateMovement(); // Update the character's movement
        UpdateLook(); // Update the camera's look direction
        UpdateCameraBobbing(); // Update the camera bobbing effect continuously
    }

    void UpdateMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Get input from the horizontal axis (A/D or Left/Right arrow keys)
        float vertical = Input.GetAxis("Vertical"); // Get input from the vertical axis (W/S or Up/Down arrow keys)

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical; // Calculate the movement direction
        controller.Move(moveDirection * movementSpeed * Time.deltaTime); // Move the character controller
    }

    void UpdateLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Get mouse movement on the X-axis
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Get mouse movement on the Y-axis

        cameraPitch -= mouseY; // Update the camera pitch based on mouse Y-axis movement
        cameraPitch = Mathf.Clamp(cameraPitch, -cameraPitchLimit, cameraPitchLimit); // Clamp the camera pitch to the specified limit

        playerCamera.localEulerAngles = Vector3.right * cameraPitch; // Apply the pitch rotation to the camera
        transform.Rotate(Vector3.up * mouseX); // Rotate the character based on mouse X-axis movement
    }

    void UpdateCameraBobbing()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f) // Check if there's significant movement input
        {
            bobbingTimer += Time.deltaTime * bobbingFrequency;
        }
        else
        {
            // Reset the timer if the player is not moving to smooth out the transition
            bobbingTimer = 0;
        }

        float bobbingOffset = Mathf.Sin(bobbingTimer) * bobbingAmount;
        playerCamera.localPosition = originalCameraPosition + new Vector3(0, bobbingOffset, 0); // Apply the bobbing effect to the camera's local position
    }
}
