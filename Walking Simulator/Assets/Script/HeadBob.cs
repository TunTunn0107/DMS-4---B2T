using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    public Vector3 midpoint = new Vector3(0.0f, 2.0f, 0.0f);

    private float timer = 0.0f;
    private Transform cameraTransform;
    private Vector3 originalLocalPosition;

    void Awake()
    {
        cameraTransform = GetComponent<Transform>();
        originalLocalPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 localPosition = cameraTransform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer += bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer -= (Mathf.PI * 2);
            }
        }

        if (waveslice != 0)
        {
            Vector3 translateChange = new Vector3(0, waveslice * bobbingAmount, 0);
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange *= totalAxes;

            localPosition.y = midpoint.y + translateChange.y;
        }
        else
        {
            localPosition.y = midpoint.y;
        }

        cameraTransform.localPosition = new Vector3(midpoint.x, localPosition.y, midpoint.z);
    }
}