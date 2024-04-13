using UnityEngine;

public class CombinedRaycaster : MonoBehaviour
{
    public GameObject[] interactableObjects; // Array of items and doors, all must have an IInteractable interface
    public GameObject interactionText; // UI Text or crosshair to show/hide based on raycast
    public Camera playerCamera;      // Camera from which to cast rays
    public float rayDistance = 5.0f; // Maximum distance for raycast
    public LayerMask layers;         // Layer mask to filter raycast hits

    // Update is called once per frame
    void Update()
    {
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        RaycastHit hit;
        // Cast a ray from the camera forward
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, rayDistance, layers))
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactionText.SetActive(true); // Show the UI text or crosshair
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(); // Call interact on the hit object
                }
            }
            else
            {
                interactionText.SetActive(false);
            }
        }
        else
        {
            interactionText.SetActive(false);
        }
    }
}

public interface IInteractable
{
    void Interact();
}


