using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Needed for manipulating UI Text

public class ConversationTrigger : MonoBehaviour
{   
    public Flowchart Flowchart; // Reference to the Flowchart component
    public Text dialoguePrompt; // UI Text to display the "Press T to talk" message
    public float maxDistance = 5.0f; // Maximum distance for the raycast

    private bool playerInRange = false;

    private void Start()
    {
        dialoguePrompt.gameObject.SetActive(false); // Hide the text at start
    }

    private void Update()
    {
        RaycastForPlayer();
        if (playerInRange && Input.GetKeyDown(KeyCode.T))
        {
            Flowchart.ExecuteBlock("Sarah Talk");
            dialoguePrompt.gameObject.SetActive(false); // Optionally hide the prompt after starting the conversation
        }
    }

    private void RaycastForPlayer()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, maxDistance))
        {
            if (hit.collider.gameObject.CompareTag("Player")) // Make sure to tag your player GameObject with the tag "Player"
            {
                dialoguePrompt.gameObject.SetActive(true);
                playerInRange = true;
                return;
            }
        }

        dialoguePrompt.gameObject.SetActive(false);
        playerInRange = false;
    }
}
