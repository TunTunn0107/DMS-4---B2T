using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public float speed = 5.0f;
    public float runMultiplier = 2.0f; // The multiplier for running speed
    public float jumpForce = 5.0f;
    public float sensitivity = 2.0f; // Mouse sensitivity for looking around
    public float maxYAngle = 80.0f; // Maximum vertical angle to look up and down
    public AudioClip jumpSound;
    public AudioClip walkingSound;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private float rotationX = 0; // Current rotation around the x-axis (pitch)
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to the center of the screen
        Cursor.visible = false; // Hide cursor

        // Add AudioSource component and configure
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f; // 3D sound
        audioSource.volume = 0.5f; // Adjust volume if needed
    }

    void Update()
    {
        // Prepare movement direction
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontal, 0.0f, vertical).normalized;

        // Determine if running
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? speed * runMultiplier : speed;

        // Walking and running animation
        bool isMoving = movementDirection.magnitude > 0;
        animator.SetBool("Walk", isMoving);
        // Optionally, add a separate running animation if available
        // animator.SetBool("Run", isMoving && isRunning);

        // Play walking sound
        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.clip = walkingSound;
            audioSource.Play();
        }
        else if (!isMoving)
        {
            audioSource.Stop();
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            isGrounded = false;
            // Play jump sound
            if (jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        // Punch and Jab
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Punch");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("Jab");
        }

        // Rotate character with mouse movement
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * sensitivity);

        // Rotate camera vertically
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    void FixedUpdate()
    {
        // Movement using Rigidbody for collision detection
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontal, 0.0f, vertical).normalized;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? speed * runMultiplier : speed;

        if (movementDirection.magnitude > 0)
        {
            Vector3 movement = transform.TransformDirection(movementDirection) * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    // Check if the character is grounded
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
