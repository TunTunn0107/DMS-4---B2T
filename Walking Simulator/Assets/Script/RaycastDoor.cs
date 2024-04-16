using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDoor : MonoBehaviour
{
    public GameObject crosshair;
    public float interactionDistance;
    public LayerMask layers;
    public string interactableComponentName = "door"; // Component name to interact with, changeable in Inspector

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, layers))
        {
            Component interactableComponent = hit.collider.gameObject.GetComponent(interactableComponentName);

            if (interactableComponent != null)
            {
                crosshair.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Use reflection to invoke the method
                    interactableComponent.GetType().GetMethod("openClose")?.Invoke(interactableComponent, null);
                }
            }
            else
            {
                crosshair.SetActive(false);
            }
        }
        else
        {
            crosshair.SetActive(false);
        }
    }
}
