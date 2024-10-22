using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public Button startButton;
    public string gameSceneName = "Living Room";

    void Start()
    {
        if(startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the TitleManager.");
        }
    }

    void OnStartButtonClicked()
    {
        SceneManager.LoadScene("BaseScene");
        SceneManager.LoadScene(gameSceneName);

        // Optionally, set player position after scene load
        // This can also be handled in CustomSceneManager's Start method
    }
}