using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItems : MonoBehaviour
{
    public GameObject Items;
    public Transform ItemsParent;
    public GameObject Text;
    public GameObject TextDrop;
    public Camera playerCamera; // Camera from which to cast rays
    public float rayDistance = 5.0f; // Maximum distance for raycast
    private bool isHoldingItem = false; // Track if the item is currently held

    // Start is called before the first frame update
    void Start()
    {
        Text.SetActive(false);
        Items.GetComponent<Rigidbody>().isKinematic = true;
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
        }

        if (Input.GetKey(KeyCode.R) && Text.activeSelf)  // Handle 'Take' only if raycast detects the item
        {   
            Take();
            Text.SetActive(false);
            TextDrop.SetActive(true);
        }
    }

    void Drop()
    {
        ItemsParent.DetachChildren();
        Items.transform.eulerAngles = new Vector3(0, 0, 0);  // Reset rotation
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
