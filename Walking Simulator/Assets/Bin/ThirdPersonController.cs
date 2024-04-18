using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public float jumpForce = 5.0f;
    public Camera playerCamera;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private float rotationY = 0.0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
        Jump();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None; // Release the cursor on pressing Escape
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        rb.MovePosition(transform.position + movement);
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -60f, 60f); // Clamp the vertical rotation

        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.localEulerAngles = new Vector3(rotationY, 0f, 0f);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayer, QueryTriggerInteraction.Ignore);
        
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
