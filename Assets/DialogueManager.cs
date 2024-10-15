using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; // Assign via Inspector
    public Text dialogueText; // Assign via Inspector
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

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence.text);
            choicesQueue.Enqueue(sentence.choices);
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

    void DisplayChoices(List<Choice> choices)
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = choices[i].choiceText;
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

public class Dialogue
{
    public string characterName;
    public List<Sentence> sentences;
}

public class Sentence
{
    public string text;
    public List<Choice> choices;
}

public class Choice
{
    public string choiceText;
    public string nextDialogue; 
}