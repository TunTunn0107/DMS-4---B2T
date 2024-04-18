using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    public Transform cameraTransform;  // Drag your main camera here via the Inspector
    public float rayLength = 100.0f;   // Maximum distance of raycast
    public LayerMask targetLayer;      // Layer mask to specify which layer the raycast should hit
    public Image reticle;              // Drag your UI reticle image here

    private void Update()
    {
        UpdateReticle();
    }

    private void UpdateReticle()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, rayLength, targetLayer))
        {
            reticle.color = Color.red;  // Change reticle color to red if hitting a target
        }
        else
        {
            reticle.color = Color.white;  // Change back to white if not
        }
    }
}
