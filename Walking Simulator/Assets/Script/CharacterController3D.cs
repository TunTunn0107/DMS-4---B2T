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
    public AudioClip runningSound; // Added running sound

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private float rotationX = 0; // Current rotation around the x-axis (pitch)
    private AudioSource audioSource;
    public float collisionCheckDistance = 0.5f; // Distance to check for collision ahead

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
        HandleJump();
        HandleCombat();
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

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? speed * runMultiplier : speed;

        bool isMoving = movementDirection.magnitude > 0;
        animator.SetBool("Walk", isMoving && !isRunning);
        animator.SetBool("Run", isRunning && isMoving); // Optional: add running animation

        // Handle walking and running sounds
        if (isMoving && !audioSource.isPlaying)
        {
            if (isRunning)
            {
                audioSource.clip = runningSound;
            }
            else
            {
                audioSource.clip = walkingSound;
            }
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
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? speed * runMultiplier : speed;

        if (movementDirection.magnitude > 0)
        {
            Vector3 movement = transform.TransformDirection(movementDirection) * currentSpeed * Time.fixedDeltaTime;
            // Check for collision before moving
            if (!Physics.Raycast(transform.position, movement, collisionCheckDistance))
            {
                rb.MovePosition(rb.position + movement);
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            isGrounded = false;
            if (jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    private void HandleCombat()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Punch");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("Jab");
        }
    }

    private void HandleRotation()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * sensitivity);
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

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
