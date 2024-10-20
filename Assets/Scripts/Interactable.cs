using UnityEngine;

public class Interactable : MonoBehaviour
{
    private DialogueLoader dialogueLoader; 
    private bool isPlayerInRange = false; 

    void Start()
    {
        dialogueLoader = GetComponent<DialogueLoader>(); // Get the DialogueLoader component
    }

    void Update()
    {
        // Check for interaction input
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Changed to E
        {
            dialogueLoader?.LoadDialogue(); // Safely call LoadDialogue
            dialogueLoader?.StartDialogue(); // Safely call StartDialogue
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; 
        }
    }
}
