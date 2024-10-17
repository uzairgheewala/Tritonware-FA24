using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public TextAsset dialogueJson; // For loading dialogue from JSON
    public Dialogue dialogue; // The dialogue data
    public Sprite characterSprite; // Character sprite to be shown during dialogue
    public AudioClip typingSound; // Assign the typing sound effect here

    void Start()
    {
        LoadDialogue();
    }

    public void LoadDialogue()
    {
        if (dialogueJson != null)
        {
            dialogue = JsonUtility.FromJson<Dialogue>(dialogueJson.text);
            Debug.Log("Dialogue loaded successfully from JSON for " + gameObject.name);
        }
        else if (dialogue == null)
        {
            Debug.LogError("No dialogue available. Please assign a JSON file or define dialogue in the Inspector.");
            return; 
        }

        if (dialogue != null && dialogue.sentences != null && dialogue.sentences.Count > 0)
        {
            Debug.Log($"Character: {dialogue.characterName}, First Sentence: {dialogue.sentences[0].text}");
        }
        else
        {
            Debug.LogWarning("Dialogue has no sentences.");
        }
    }

    public void StartDialogue()
    {
        if (dialogue != null)
        {
            // Show the character sprite and start the dialogue
            DialogueManager.Instance.characterSprite = characterSprite; // Set the sprite for the current dialogue
            DialogueManager.Instance.typingSound = typingSound; // Set the typing sound for the DialogueManager
            DialogueManager.Instance.StartDialogue(dialogue); // Start the dialogue
            Debug.Log("Dialogue started.");
        }
        else
        {
            Debug.LogError("Dialogue is null. Cannot start dialogue.");
        }
    }

    public void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue(); // Call the end dialogue method
    }
}
