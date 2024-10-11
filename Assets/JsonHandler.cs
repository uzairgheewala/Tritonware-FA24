using UnityEngine;

public class JsonDialogueLoader : MonoBehaviour
{
    public Dialogue dialogue; 

    void Start()
    {
        LoadDialogue();
        
    }

    void LoadDialogue()
    {
        
        TextAsset json = Resources.Load<TextAsset>("dialogue"); 
        if (json != null)
        {
           
            dialogue = JsonUtility.FromJson<Dialogue>(json.text);
            Debug.Log("Dialogue loaded successfully!");
            Debug.Log("Character: " + dialogue.characterName);
            Debug.Log("First Sentence: " + dialogue.sentences[0].text);
        }
        else
        {
            Debug.LogError("Failed to load dialogue.json. Ensure it's in the Resources folder.");
        }
    }
}
