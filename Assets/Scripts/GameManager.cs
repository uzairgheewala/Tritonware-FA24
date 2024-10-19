using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    Dialogue,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Dictionary<string, bool> flags = new Dictionary<string, bool>();
    public GameState currentGameState;
    DialogueManager dialogueManager;
    CharacterManager characterManager;

    public void ChangeState(GameState newState)
    {
        currentGameState = newState;
        Logger.Log($"Game state changed to {newState}");
        // Implement state-specific logic
        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
            case GameState.Dialogue:
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                // Handle game over
                break;
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Logger.LogWarning("Duplicate GameManager destroyed.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes if necessary
            Logger.Log("GameManager initialized.");
        }

        InitializeManagers();
    }

    void InitializeManagers()
    {
        if (dialogueManager == null)
            dialogueManager = FindObjectOfType<DialogueManager>();

        if (characterManager == null)
            characterManager = FindObjectOfType<CharacterManager>();

        // Initialize other managers similarly

        // Subscribe to events
        //dialogueManager.OnDialogueEnded += HandleDialogueEnded;
        // Subscribe to other manager events as needed
    }

    // Example event handler
    void HandleDialogueEnded()
    {
        Logger.Log("Dialogue ended. Resuming gameplay.");
        // Implement logic to resume game after dialogue
    }

    // Add methods to control game states, transitions, etc.

    public void SetFlag(string flagName, bool value)
    {
        flags[flagName] = value;
        Logger.Log($"Flag '{flagName}' set to {value}.");
    }

    public bool IsFlagSet(string flagName)
    {
        if (flags.ContainsKey(flagName))
        {
            Logger.Log($"Flag '{flagName}' retrieved with value {flags[flagName]}.");
            return flags[flagName];
        }
        else
        {
            Logger.LogWarning($"Flag '{flagName}' does not exist.");
            return false;
        }
    }

    void OnGUI()
    {
        string flagStatus = "Flags:\n";
        foreach (var flag in flags)
        {
            flagStatus += flag.Key + ": " + flag.Value + "\n";
        }
        GUI.Label(new Rect(10, 10, 300, 500), flagStatus);
    }
}