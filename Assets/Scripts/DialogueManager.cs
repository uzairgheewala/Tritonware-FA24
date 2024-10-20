using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
<<<<<<< HEAD

public class DialogueManager : MonoBehaviour
{
=======
using System.Collections; // Make sure to include this for IEnumerator

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; } // Singleton instance

>>>>>>> Uzair
    public GameObject dialoguePanel; // Assign via Inspector
    public TextMeshProUGUI dialogueText; // Assign via Inspector
    public TextMeshProUGUI characterNameText; // Assign via Inspector
    public Button[] choiceButtons; // Assign via Inspector
    public Image characterImage; // UI Image for the character's PNG sprite
    public Canvas dialogueCanvas; // Persistent canvas for dialogue

    public TextAsset dialogueJson; // Assign via Inspector or programmatically
    public Dialogue dialogue; // Dialogue data
    public Sprite characterSprite; // Character sprite for current dialogue

    private Queue<Sentence> sentences;
    private AudioSource audioSource; // AudioSource for playing sounds
    public AudioClip typingSound; // Assign via Inspector
    public static DialogueManager Instance { get; private set; }

    void Awake()
    {
        // Ensure only one instance of DialogueManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not present
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
    }

    void Start()
    {
        sentences = new Queue<Sentence>();
        dialoguePanel.SetActive(false);
        characterImage.enabled = false; // Hide character image by default
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
            // Replace [char] with the player's name in each sentence
            for (int i = 0; i < dialogue.sentences.Count; i++)
            {
                dialogue.sentences[i].text = dialogue.sentences[i].text.Replace("[char]", DialogueLoader.Instance.playerName);
            }

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
        characterImage.enabled = true; // Show the character image when dialogue starts
        characterImage.sprite = characterSprite; // Set the sprite for the current character
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
        StartCoroutine(TypeSentence(currentSentence.text)); // Start typing the sentence
        DisplayChoices(currentSentence.choices);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; // Clear the dialogue text

        for (int i = 0; i < sentence.Length; i++)
        {
            dialogueText.text += sentence[i]; // Add one letter at a time

            // Play typing sound for every other character
            if (typingSound != null && i % 2 == 0) // Play sound if the index is even
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(0.05f); // Wait for a short time before adding the next letter
        }
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
<<<<<<< HEAD
            Logger.Log("No choices to display.");
=======
>>>>>>> Uzair
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

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        characterImage.enabled = false; // Hide the character image when dialogue ends
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
