using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{
    public string targetRoomName; // Name of the room to load
    public Vector3Int targetEntrancePosition; // Player's position in the target room

    private CustomSceneManager sceneManager;

    void Start()
    {
        sceneManager = FindObjectOfType<CustomSceneManager>();
        if (sceneManager == null)
        {
            Logger.LogError("SceneManager not found in the scene.");
        }

        if (string.IsNullOrEmpty(targetRoomName))
        {
            Logger.LogError("TargetRoomName not set on Doorway.");
        }

        if (targetEntrancePosition == Vector3Int.zero)
        {
            Logger.LogWarning("TargetEntrancePosition not set on Doorway. Using default.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !string.IsNullOrEmpty(targetRoomName))
        {
            Logger.Log($"Player triggered doorway to {targetRoomName}");
            SceneManager.LoadScene(targetRoomName);
        }
    }
}