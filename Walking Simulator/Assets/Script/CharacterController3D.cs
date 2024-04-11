using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private bool rotateLeft;
    private bool rotateRight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotateLeft = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rotateRight = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            rotateLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rotateRight = false;
        }

        if (rotateLeft)
        {
            transform.Rotate(Vector3.up, -50 * Time.deltaTime);
        }

        if (rotateRight)
        {
            transform.Rotate(Vector3.up, 50 * Time.deltaTime);
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
