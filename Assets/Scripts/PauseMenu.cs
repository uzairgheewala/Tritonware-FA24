using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public Button resumeButton;
    public Button quitButton;

    private bool isPaused = false;

    void Start()
    {
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(OnResumeButtonClicked);
        }
        else
        {
            Debug.LogError("Resume Button is not assigned in PauseMenu.");
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
        else
        {
            Debug.LogError("Quit Button is not assigned in PauseMenu.");
        }

        // Initially hide the Pause Menu
        pauseCanvas.SetActive(false);
    }

    void Update()
    {
        // Toggle pause menu with the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    
        if (Input.GetKeyDown(KeyCode.I))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Show the pause menu and pause the game
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
            GameManager.Instance.ChangeState(GameState.Paused);
        }
        else
        {
            // Hide the pause menu and resume the game
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
            GameManager.Instance.ChangeState(GameState.Playing);
        }
    }

    void OnResumeButtonClicked()
    {
        // Resumes the game when clicking the Resume button
        TogglePause();
    }

    void OnQuitButtonClicked()
    {
        // Load the Title Scene when clicking the Quit button
        Time.timeScale = 1f; // Ensure the game is unpaused
        SceneManager.LoadScene("TitleScene");
    }
}