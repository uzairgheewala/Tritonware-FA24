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

    private Queue<string> sentences;
    private Queue<List<Choice>> choicesQueue;

    void Start()
    {
        sentences = new Queue<string>();
        choicesQueue = new Queue<List<Choice>>();
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);
        sentences.Clear();
        choicesQueue.Clear();

        // Set character name in the UI
        characterNameText.text = dialogue.characterName;

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence.text);
            if (sentence.choices != null)
            {
                choicesQueue.Enqueue(sentence.choices);
            }
            else
            {
                choicesQueue.Enqueue(new List<Choice>()); // Enqueue an empty list if null
            }
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

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        List<Choice> currentChoices = choicesQueue.Dequeue();
        DisplayChoices(currentChoices);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialoguePanel.activeSelf)
        {
            if (choicesQueue.Count == 0 || choiceButtons[0].gameObject.activeSelf == false)
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
                if (buttonText != null)
                {
                    buttonText.text = choices[i].choiceText; // Use TextMeshPro for setting text
                }
                else
                {
                    Debug.LogError($"TextMeshProUGUI component not found in button {i}");
                }

                int choiceIndex = i; 
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choices[choiceIndex].nextDialogue));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnChoiceSelected(string nextDialogue)
    {
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
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
    public string nextDialogue; 
}