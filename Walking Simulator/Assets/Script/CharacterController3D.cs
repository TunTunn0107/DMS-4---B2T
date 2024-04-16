using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public float speed = 5.0f; // Walking speed
    public float sensitivity = 2.0f; // Mouse sensitivity for looking around
    public float maxYAngle = 80.0f; // Maximum vertical angle to look up and down
    public AudioClip walkingSound;

    private Rigidbody rb;
    private Animator animator;
    private AudioSource audioSource;
    public Transform cameraTransform; // Assign this in Unity's inspector

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f;
        audioSource.volume = 0.5f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void FixedUpdate()
    {
        PerformMovement();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontal, 0.0f, vertical).normalized;
        float currentSpeed = speed;
        bool isMoving = movementDirection.magnitude > 0;
        animator.SetBool("Walk", isMoving); // Ensure this line is uncommented

        // Handle walking sound
        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.clip = walkingSound;
            audioSource.Play();
        }
        else if (!isMoving)
        {
            audioSource.Stop();
        }
    }

    private void PerformMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontal, 0.0f, vertical).normalized;
        float currentSpeed = speed;

        if (movementDirection.magnitude > 0)
        {
            Vector3 movement = transform.TransformDirection(movementDirection) * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Yaw rotation (left and right)
        transform.Rotate(Vector3.up * mouseX);

        // Pitch rotation (up and down)
        cameraTransform.localEulerAngles += new Vector3(-mouseY, 0, 0);
        float rotationX = cameraTransform.localEulerAngles.x;
        // Clamping the X rotation to stay within maxYAngle
        if (rotationX > 180) rotationX -= 360; // Adjust rotation values going over 180 degrees
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
