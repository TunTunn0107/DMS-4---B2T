using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText;
    public string doorOpenAnimName, doorCloseAnimName;
    public AudioClip doorOpen, doorClose;

    private GameObject interactableDoor = null;
    private Animator doorAnim = null;
    private AudioSource doorSound = null;
    private bool isDoorOpen = false;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Check for raycast hit
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Check if the hit object is a door
            if (hit.collider.gameObject.tag == "door")
            {
                if (interactableDoor == null) // New door found
                {
                    interactableDoor = hit.collider.transform.root.gameObject;
                    doorAnim = interactableDoor.GetComponent<Animator>();
                    doorSound = hit.collider.gameObject.GetComponent<AudioSource>();
                    intText.SetActive(true);
                }
            }
        }
        else
        {
            // Clear the interaction if looking away or too far
            interactableDoor = null;
            doorAnim = null;
            doorSound = null;
            intText.SetActive(false);
        }

        // Handle door interaction with 'E' key
        if (Input.GetKeyDown(KeyCode.E) && interactableDoor != null)
        {
            if (isDoorOpen)
            {
                doorSound.clip = doorClose;
                doorSound.Play();
                doorAnim.ResetTrigger("open");
                doorAnim.SetTrigger("close");
                isDoorOpen = false;
            }
            else
            {
                doorSound.clip = doorOpen;
                doorSound.Play();
                doorAnim.ResetTrigger("close");
                doorAnim.SetTrigger("open");
                isDoorOpen = true;
            }
        }
    }
}
