using UnityEngine;
using TMPro; // Ensure you have this namespace
using UnityEngine.UI;

public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField nameInputField; // Change to TMP_InputField
    public Text feedbackText; // Keep using Unity's Text
    public Button submitButton; // Reference for the submit button

    private void Start()
    {
        // Optionally clear any previous input or messages
        nameInputField.text = string.Empty;
        feedbackText.text = string.Empty;
    }

    public void OnSubmitName()
    {
        if (nameInputField == null)
        {
            Debug.LogError("nameInputField is not assigned.");
            return;
        }

        string playerName = nameInputField.text; // Get the text from the TMP input field
        if (!string.IsNullOrEmpty(playerName))
        {
            if (DialogueLoader.Instance == null)
            {
                Debug.LogError("DialogueLoader instance is null. Make sure it's in the scene and initialized.");
                return;
            }

            DialogueLoader.Instance.playerName = playerName; // Set the player's name in DialogueLoader
            feedbackText.text = "Name accepted!"; // Update feedback text
            Debug.Log("Player Name Set: " + playerName); // Log to console

            DialogueLoader.Instance.LoadDialogue(); // Load the dialogue after name input

            // Disable the input field and feedback text to hide them
            nameInputField.gameObject.SetActive(false);
            feedbackText.gameObject.SetActive(false);
            
            // Disable the submit button after submission
            if (submitButton != null) // Check if the button reference is assigned
            {
                submitButton.interactable = false; // Disable the button after submission
            }
            else
            {
                Debug.LogError("Submit button is not assigned.");
            }
        }
        else
        {
            feedbackText.text = "Please enter a name."; // Update feedback if input is empty
        }
    }
}
