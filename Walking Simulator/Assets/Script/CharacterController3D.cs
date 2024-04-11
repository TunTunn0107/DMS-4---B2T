using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public float sensitivity = 2.0f; // Mouse sensitivity for looking around
    public float maxYAngle = 80.0f; // Maximum vertical angle to look up and down

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private float rotationX = 0; // Current rotation around the x-axis (pitch)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to the center of the screen
        Cursor.visible = false; // Hide cursor
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

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            isGrounded = false;
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
