using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public TextAsset dialogueJson; // For loading dialogue from JSON
    public Dialogue dialogue; // The dialogue data
    public Sprite characterSprite; // Character sprite to be shown during dialogue
    public AudioClip typingSound; // Assign the typing sound effect here
    public string playerName;
    public static DialogueLoader Instance { get; private set; }

    void Awake()
    {
        // Implementing Singleton pattern to prevent duplicates
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
        }
    }

    void Start()
    {
        LoadDialogue();
    }

    public void LoadDialogue()
    {
        if (dialogueJson == null)
        {
            Debug.LogError("No JSON file assigned. Please assign a JSON file for dialogue.");
            return;
        }

        dialogue = JsonUtility.FromJson<Dialogue>(dialogueJson.text);
        if (dialogue == null)
        {
            Debug.LogError("Failed to load dialogue. Make sure your JSON format matches the Dialogue class.");
            return; 
        }

        if (dialogue.sentences != null && dialogue.sentences.Count > 0)
        {
            // Replace [char] placeholder with player's name
            for (int i = 0; i < dialogue.sentences.Count; i++)
            {
                dialogue.sentences[i].text = dialogue.sentences[i].text.Replace("[char]", playerName);
            }
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
