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

    void Start()
    {
        sentences = new Queue<Sentence>();
        dialoguePanel.SetActive(false);
        // LoadDialogue();
        // StartDialogue();
    }

    public void LoadDialogue(Dialogue dial = null)
    {
        dialogue = dial;
        if (dialogueJson != null)
        {
            dialogue = JsonUtility.FromJson<Dialogue>(dialogueJson.text);
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

    public void StartDialogue(Dialogue newDialogue = null)
    {

        if (newDialogue != null)
        {
            dialogue = newDialogue;
        }
        if (dialogue == null)
        {
            Debug.LogError("Dialogue is null. Cannot start dialogue.");
            return;
        }

        dialoguePanel.SetActive(true);
        sentences.Clear();

        characterNameText.text = dialogue.characterName;

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
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
            return;
        }

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);

                TextMeshProUGUI buttonText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = choices[i].choiceText;

                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choices[choiceIndex]));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnChoiceSelected(Choice choice)
    {
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

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended.");
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
