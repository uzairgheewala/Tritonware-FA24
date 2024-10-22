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

    public GameObject nameInputPanel; // Assign the name input panel in the Inspector

    public void ChangeState(GameState newState)
    {
        currentGameState = newState;
        Logger.Log($"Game state changed to {newState}");
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
            DontDestroyOnLoad(gameObject);
            Logger.Log("GameManager initialized.");
        }

        InitializeManagers();
    }

    void Start()
    {
        ShowNameInput(); // Show the name input panel when the game starts
    }

    void InitializeManagers()
    {
        if (dialogueManager == null)
            dialogueManager = FindObjectOfType<DialogueManager>();

        if (characterManager == null)
            characterManager = FindObjectOfType<CharacterManager>();

        // Initialize other managers similarly
    }

    private void ShowNameInput()
    {
        if (nameInputPanel != null)
        {
            nameInputPanel.SetActive(true); // Show the name input panel
        }
        else
        {
            Logger.LogWarning("Name input panel is not assigned in GameManager.");
        }
    }

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
        //GUI.Label(new Rect(10, 10, 300, 500), flagStatus);
    }
}
