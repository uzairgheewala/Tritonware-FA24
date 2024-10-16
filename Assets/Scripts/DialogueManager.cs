using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; // Assign via Inspector
    public TextMeshProUGUI dialogueText; // Assign via Inspector
    public TextMeshProUGUI characterNameText; // Assign via Inspector
    public Button[] choiceButtons; // Assign via Inspector

    public TextAsset dialogueJson; // Assign via Inspector or programmatically
    public Dialogue dialogue; // Dialogue data

    private Queue<Sentence> sentences;
    //private Queue<List<Choice>> choicesQueue;

    void Start()
    {
        sentences = new Queue<Sentence>();
        //choicesQueue = new Queue<List<Choice>>();
        dialoguePanel.SetActive(false);
        Logger.Log("DialogueManager initialized.");
        LoadDialogue();
        StartDialogue();
    }

    public void LoadDialogue(Dialogue dial=null)
    {
        dialogue = dial;
        if (dialogueJson != null)
        {
            dialogue = JsonUtility.FromJson<Dialogue>(dialogueJson.text);
            Logger.Log("Dialogue loaded successfully from JSON." + dialogue.characterName + dialogue.sentences);
        }
        else if (dialogue == null) // If no JSON file and no dialogue assigned, log error
        {
            Logger.LogError("No dialogue available. Please assign a JSON file or define dialogue in the Inspector.");
            return;
        }

        // Check if dialogue has been loaded correctly
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
        if (dialogue == null)
        {
            Logger.LogError("Dialogue is null. Cannot start dialogue.");
            return;
        }

        dialoguePanel.SetActive(true);
        sentences.Clear();
        //choicesQueue.Clear();

        characterNameText.text = dialogue.characterName;
        Logger.Log($"Starting dialogue with {dialogue.characterName}");

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            /*
            if (sentence.choices != null)
            {
                choicesQueue.Enqueue(sentence.choices);
            }
            else
            {
                choicesQueue.Enqueue(new List<Choice>());
            }
            */
        }

        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence currentSentence = sentences.Dequeue();
        dialogueText.text = currentSentence.text;
        Logger.Log($"Displaying sentence: {currentSentence.text}");

        //List<Choice> currentChoices = choicesQueue.Dequeue();
        //DisplayChoices(currentChoices);
        DisplayChoices(currentSentence.choices);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialoguePanel.activeSelf)
        {
            if (choiceButtons.Length > 0 && !choiceButtons[0].gameObject.activeSelf)
            {
                DisplayNextSentence();
            }
            /*
            if (choicesQueue.Count == 0 || choiceButtons[0].gameObject.activeSelf == false)
            {
                DisplayNextSentence();
            }*/
        }
    }

    void DisplayChoices(List<Choice> choices)
    {
        if (choices == null || choices.Count == 0)
        {
            foreach (var button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }
            Logger.Log("No choices to display.");
            return;
        }

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);

                TextMeshProUGUI buttonText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = choices[i].choiceText;
                }
                else
                {
                    Logger.LogError($"TextMeshProUGUI component not found in button {i}");
                }

                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choices[choiceIndex]));
                Logger.Log($"Displaying choice {i}: {choices[i].choiceText}");
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnChoiceSelected(Choice choice)
    {
        Logger.Log($"Choice selected: {choice.choiceText}");

        if (choice.nextDialogue != null)
        {
            LoadDialogue(choice.nextDialogue);
            StartDialogue();
        }
        else
        {
            DisplayNextSentence();
        }
    }
    /*
    void OnChoiceSelected(string nextDialogue)
    {
        Logger.Log($"Choice selected: {nextDialogue}");

        if (!string.IsNullOrEmpty(nextDialogue))
        {
            // Attempt to load the next dialogue JSON
            TextAsset nextDialogueJson = Resources.Load<TextAsset>(nextDialogue);
            if (nextDialogueJson != null)
            {
                dialogueJson = nextDialogueJson;
                LoadDialogue();
                StartDialogue();
            }
            else
            {
                Logger.LogError($"Next dialogue JSON '{nextDialogue}' not found.");
                DisplayNextSentence();
            }
        }
        else
        {
            DisplayNextSentence();
        }
    }*/


    void EndDialogue()
    {
        //dialoguePanel.SetActive(false);
        Logger.Log("Dialogue ended.");
    }
}

[System.Serializable]
public class Dialogue
{
    public string characterName;
    public List<Sentence> sentences;
}

[System.Serializable]
public class Sentence
{
    public string text;
    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    public string choiceText;
    public Dialogue nextDialogue; 
}