using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public float speed = 5.0f;
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
        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Walking animation
        bool isWalking = horizontal != 0 || vertical != 0;
        animator.SetBool("Walk", isWalking);

        // Play walking sound
        if (isWalking && !audioSource.isPlaying)
        {
            audioSource.clip = walkingSound;
            audioSource.Play();
        }
        else if (!isWalking)
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

        // Rotate character with A and D keys
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * sensitivity);

        // Rotate camera vertically
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
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
