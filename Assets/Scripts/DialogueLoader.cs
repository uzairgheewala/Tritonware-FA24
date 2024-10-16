using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public TextAsset dialogueJson; 
    public Dialogue dialogue; 

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
        else if (dialogue == null)
        {
            Logger.LogError("No dialogue available. Please assign a JSON file or define dialogue in the Inspector.");
            return; 
        }

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

    public void StartDialogue()
    {
        if (dialogue != null)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue); // Start the dialogue
            Logger.Log("Dialogue started.");
        }
    }
}
