using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public TextAsset dialogueJson; 
    public Dialogue dialogue; 
    public string playerName;
    public static DialogueLoader Instance { get; private set; }


    void Start()
    {
        LoadDialogue();
    }

    public void LoadDialogue()
    {
        if (dialogueJson != null)
        {
            dialogue = JsonUtility.FromJson<Dialogue>(dialogueJson.text);
            Logger.Log("Dialogue loaded successfully from JSON for " + gameObject.name);
        }
        else if (dialogue == null) // If no JSON file and no inspector dialogue, log error
        {
            Logger.LogError("No dialogue available. Please assign a JSON file or define dialogue in the Inspector.");
            return; // Exit if no dialogue is found
        }

        // Ensure dialogue has been loaded or defined
        if (dialogue != null)
        {
            Logger.Log($"Character: {dialogue.characterName}");
            if (dialogue.sentences != null && dialogue.sentences.Count > 0)
            {
                Logger.Log($"First Sentence: {dialogue.sentences[0].text}");
            }
            else
            {
                Logger.LogWarning("Dialogue has no sentences.");
            }
        }
    }

    void Update()
    {
        // Start the dialogue when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && dialogue != null)
        {
            //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            Logger.Log("Dialogue started via spacebar.");
        }
    }
}
