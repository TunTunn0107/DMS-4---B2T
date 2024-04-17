using UnityEngine;

public class PlayerRaycastDoorlock : MonoBehaviour
{
    public GameObject crosshair; // GameObject to show when the player can interact with the door
    public float interactionDistance; // Distance within which the player can interact
    public LayerMask layers; // LayerMask to filter the raycast

    void Update()
    {
        RaycastHit hit;
        // Perform a raycast from the player's current position forward
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, layers))
        {
            // Check if the hit object has a Doorlock component
            if (hit.collider.gameObject.GetComponent<Doorlock>())
            {
                // If it is a Doorlock, activate the crosshair
                crosshair.SetActive(true);
            }
            else
            {
                // If it is not a Doorlock, deactivate the crosshair
                crosshair.SetActive(false);
            }
        }
        else
        {
            // If nothing is hit, deactivate the crosshair
            crosshair.SetActive(false);
        }
    }
}
