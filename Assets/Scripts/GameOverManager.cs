using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Button retryButton;
    public string gameSceneName = "LivingRoom";
    public string titleSceneName = "TitleScene";

    void Start()
    {
        if(retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryButtonClicked);
        }
        else
        {
            Debug.LogError("Retry Button is not assigned in the GameOverManager.");
        }
    }

    void OnRetryButtonClicked()
    {
        // Load the game scene
        SceneManager.LoadScene(gameSceneName);
    }

    // Optionally, add a method to return to the title screen
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}