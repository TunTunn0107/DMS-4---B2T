using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    public Flowchart flowchart; // Reference to the Flowchart component for dialogues
    public GameObject textGameObject; // Reference to the GameObject that contains the text component
    public float maxDistance = 5.0f; // Maximum distance for interaction, adjustable from the Unity Inspector
    public string dialogueBlockName = "DefaultTalk"; // Name of the dialogue block to execute, settable in the Inspector

    private bool playerInRange = false; // Flag to check if the player is in range to talk

    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * maxDistance;

        // Debug raycast in the scene view
        Debug.DrawRay(transform.position, forward, Color.green);

        // Check for raycast hits
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            // Check if the hit object is the player
            if (hit.collider.CompareTag("Player"))
            {
                if (!playerInRange)
                {
                    playerInRange = true;
                    textGameObject.SetActive(true); // Show interactive text
                }

                // Check for the key press and execute the specified dialogue block
                if (Input.GetKeyDown(KeyCode.T))
                {
                    ExecuteDialogueBlock();
                }
            }
        }
        else if (playerInRange)
        {
            playerInRange = false;
            textGameObject.SetActive(false); // Hide the text when the player is out of range
        }
    }

    // Function to execute the dialogue block
    public void ExecuteDialogueBlock()
    {   
        textGameObject.SetActive(false);
        if (flowchart != null && !string.IsNullOrEmpty(dialogueBlockName))
        {
            flowchart.ExecuteBlock(dialogueBlockName);
        }
    }
}
