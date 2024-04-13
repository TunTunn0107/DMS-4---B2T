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

    // Start is called before the first frame update
    void Start()
    {
        Text.SetActive(false);
        Items.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRaycasting();
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
    if (Input.GetKey(KeyCode.T))  // Corrected from Getkey to GetKey
    {
        Drop();
        Text.SetActive(false);
        TextDrop.SetActive(false);  // Added missing semicolon here
    }

    if (Input.GetKey(KeyCode.R))  // Handle 'Take' separately
    {
        Take();
        TextDrop.SetActive(true);
    }
}


    void Drop()
    {
        ItemsParent.DetachChildren();
        Items.transform.eulerAngles = new Vector3(0, 0, 0);  // Reset rotation
        Items.GetComponent<Rigidbody>().isKinematic = false;
        Items.GetComponent<MeshCollider>().enabled = true;
    }

    void Take()
    {   
        if (Text.activeSelf) // Only take if Text is active, indicating raycast hit
        {
            Items.GetComponent<Rigidbody>().isKinematic = true;
            Items.transform.position = ItemsParent.transform.position;
            Items.transform.rotation = ItemsParent.transform.rotation;
            Items.GetComponent<MeshCollider>().enabled = false;
            Items.transform.SetParent(ItemsParent);
        }
    }
}