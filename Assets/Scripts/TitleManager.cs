using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button startButton;
    public Button toggleInstructionsButton;
    public GameObject instructionsPanel;
    
    [Header("Scene Settings")]
    public string gameSceneName = "Living Room";
    public string baseSceneName = "BaseScene";
    public string titleSceneName = "Title";

    private bool isInstructionsVisible = false;

    void Start()
    {
        // Initialize Start Button
        if(startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the TitleManager.");
        }

        // Initialize Toggle Instructions Button
        if(toggleInstructionsButton != null)
        {
            toggleInstructionsButton.onClick.AddListener(ToggleInstructions);
        }
        else
        {
            Debug.LogError("Toggle Instructions Button is not assigned in the TitleManager.");
        }

        // Initialize Instructions Panel
        if(instructionsPanel != null)
        {
            instructionsPanel.SetActive(isInstructionsVisible);
        }
        else
        {
            Debug.LogError("Instructions Panel is not assigned in the TitleManager.");
        }
    }

    void OnStartButtonClicked()
    {
        StartCoroutine(LoadGameScenes());
    }
    
    IEnumerator LoadGameScenes()
    {
        // Load BaseScene additively
        AsyncOperation loadBaseScene = SceneManager.LoadSceneAsync(baseSceneName, LoadSceneMode.Additive);
        yield return loadBaseScene;

        if (!loadBaseScene.isDone)
        {
            Debug.LogError($"Failed to load {baseSceneName}.");
            yield break;
        }

        // Set BaseScene as the active scene
        Scene baseScene = SceneManager.GetSceneByName(baseSceneName);
        if (baseScene.IsValid())
        {
            SceneManager.SetActiveScene(baseScene);
            Debug.Log($"{baseSceneName} is now the active scene.");
        }
        else
        {
            Debug.LogError($"Scene {baseSceneName} is not valid.");
            yield break;
        }

        // Load the main game scene additively
        AsyncOperation loadGameScene = SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);
        yield return loadGameScene;

        if (!loadGameScene.isDone)
        {
            Debug.LogError($"Failed to load {gameSceneName}.");
            yield break;
        }

        // Optionally, set the main game scene as the active scene
        Scene gameScene = SceneManager.GetSceneByName(gameSceneName);
        if (gameScene.IsValid())
        {
            SceneManager.SetActiveScene(gameScene);
            Debug.Log($"{gameSceneName} is now the active scene.");
        }
        else
        {
            Debug.LogError($"Scene {gameSceneName} is not valid.");
            yield break;
        }

        // Unload the Title scene
        AsyncOperation unloadTitle = SceneManager.UnloadSceneAsync(titleSceneName);
        yield return unloadTitle;

        if (!unloadTitle.isDone)
        {
            Debug.LogError($"Failed to unload {titleSceneName}.");
            yield break;
        }

        Debug.Log($"{titleSceneName} has been unloaded successfully.");
    }

    void ToggleInstructions()
    {
        isInstructionsVisible = !isInstructionsVisible;
        instructionsPanel.SetActive(isInstructionsVisible);
    }
}