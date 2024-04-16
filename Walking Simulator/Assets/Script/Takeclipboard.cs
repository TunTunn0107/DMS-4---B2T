using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeclipboard : MonoBehaviour
{
    public GameObject Items;
    public Transform ItemsParent;
    public GameObject Text;
    public GameObject TextDrop;
    public GameObject ImageTaken; // Reference to the image GameObject that should be activated
    public Camera playerCamera; // Camera from which to cast rays
    public float rayDistance = 5.0f; // Maximum distance for raycast
    private bool isHoldingItem = false; // Track if the item is currently held

    private Vector3 originalPosition; // To store the original position
    private Quaternion originalRotation; // To store the original rotation

    // Start is called before the first frame update
    void Start()
    {
        Text.SetActive(false);
        TextDrop.SetActive(false);
        ImageTaken.SetActive(false); // Ensure the image is also initially hidden
        Items.GetComponent<Rigidbody>().isKinematic = true;
        
        // Store the initial position and rotation
        originalPosition = Items.transform.position;
        originalRotation = Items.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHoldingItem) // Only perform raycasting if not holding an item
        {
            HandleRaycasting();
        }
        HandleInput();
    }

    private void HandleRaycasting()
    {
        RaycastHit hit;
        // Cast a ray from the camera forward
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, rayDistance))
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

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Q))  // Handle 'Drop'
        {   
            Drop();
            Text.SetActive(false);
            TextDrop.SetActive(false);
            ImageTaken.SetActive(false); // Hide the image when item is dropped
        }

        if (Input.GetKey(KeyCode.R) && Text.activeSelf)  // Handle 'Take' only if raycast detects the item
        {   
            Take();
            Text.SetActive(false);
            TextDrop.SetActive(true);
            ImageTaken.SetActive(true); // Show the image indicating the item has been taken
        }
    }

    void Drop()
    {
        ItemsParent.DetachChildren();
        Items.transform.position = originalPosition; // Reset position to original
        Items.transform.rotation = originalRotation; // Reset rotation to original
        Items.GetComponent<Rigidbody>().isKinematic = false;
        Items.GetComponent<MeshCollider>().enabled = true;
        isHoldingItem = false; // Item is no longer held
    }

    void Take()
    {   
        Items.GetComponent<Rigidbody>().isKinematic = true;
        Items.transform.position = ItemsParent.transform.position;
        Items.transform.rotation = ItemsParent.transform.rotation;
        Items.GetComponent<MeshCollider>().enabled = false;
        Items.transform.SetParent(ItemsParent);
        isHoldingItem = true; // Item is now held
    }
}
