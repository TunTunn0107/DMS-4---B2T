using UnityEngine;

public class ItemRaycaster : MonoBehaviour
{
    public GameObject Items;         // The target item object
    public GameObject Text;          // UI Text to show/hide based on raycast
    public Camera playerCamera;      // Camera from which to cast rays
    public float rayDistance = 5.0f; // Maximum distance for raycast
    public LayerMask layers;

    // Update is called once per frame
    void Update()
    {
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        RaycastHit hit;
        // Cast a ray from the camera forward
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, rayDistance,layers))
        {
            // Check if the hit object is the Items object
            if (hit.collider.gameObject == Items)
            {
                Text.SetActive(true);
            }
            else
            {
                Text.SetActive(false);
            }
        }
        else
        {
            Text.SetActive(false);
        }
    }
}
