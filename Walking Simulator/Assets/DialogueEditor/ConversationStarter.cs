using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] public Camera playerCamera; // Camera from which to cast rays
    [SerializeField] public GameObject interactionText; // Text to display when the player can initiate conversation

    [SerializeField] private NPCConversation myConversation;
   
    public float rayDistance = 5.0f; // Maximum distance for raycast

    private bool canTalk = false; // Flag to check if the player is in range to start conversation

    private void Start()
    {
        // Initially, make sure the interaction text is not visible
        interactionText.SetActive(false);
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
            // Check if the hit object is the NPC object
            if (hit.collider.gameObject == gameObject)
            {
                interactionText.SetActive(true);
                canTalk = true;
            }
            else
            {
                interactionText.SetActive(false);
                canTalk = false;
            }
        }
        else
        {
            interactionText.SetActive(false);
            canTalk = false;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.T) && canTalk)
        {
            // Ensure that the ConversationManager and the conversation data are available
            if (ConversationManager.Instance != null && myConversation != null)
            {
                ConversationManager.Instance.StartConversation(myConversation);
            }
            else
            {
                Debug.LogWarning("ConversationManager instance or conversation data is missing.");
            }
        }
    }
}
