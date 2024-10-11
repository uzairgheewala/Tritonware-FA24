using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public TextAsset dialogueJson; 
    public Dialogue dialogue; 


    public void LoadDialogue()
    {

        if (dialogueJson != null)
        {
            dialogue = JsonUtility.FromJson<Dialogue>(dialogueJson.text);
            Debug.Log("Dialogue loaded successfully from JSON for " + gameObject.name);
        }
        else if (dialogue == null) // If no JSON file and no inspector dialogue, log error
        {
            Debug.LogError("No dialogue available. Please assign a JSON file or define dialogue in the Inspector.");
            return; // Exit if no dialogue is found
        }

        // Ensure dialogue has been loaded or defined
        if (dialogue != null)
        {
            // Additional logging for debugging
            Debug.Log("Character: " + dialogue.characterName);
            Debug.Log("First Sentence: " + dialogue.sentences[0].text);
        }
    }

    void Update()
    {
        // Start the dialogue when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && dialogue != null)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
