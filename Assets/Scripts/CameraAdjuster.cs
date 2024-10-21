using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraAdjuster : MonoBehaviour
{
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogError("CameraAdjuster requires a Camera component.");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //AdjustCameraToBackground();
    }

    void AdjustCameraToBackground()
    {
        // Find the background sprite using the tag
        GameObject bgObject = GameObject.FindGameObjectWithTag("Background");

        if (bgObject == null)
        {
            Debug.LogWarning("No GameObject with tag 'Background' found in the scene to adjust the camera.");
            return;
        }

        SpriteRenderer bgRenderer = bgObject.GetComponent<SpriteRenderer>();

        if (bgRenderer == null)
        {
            Debug.LogWarning("Background GameObject found, but it does not have a SpriteRenderer component.");
            return;
        }

        Sprite bgSprite = bgRenderer.sprite;

        if (bgSprite == null)
        {
            Debug.LogWarning("Background SpriteRenderer found, but no sprite is assigned.");
            return;
        }

        // Calculate the height in world units
        float spriteHeight = bgSprite.bounds.size.y;

        // Set the orthographic size to half the sprite's height
        mainCamera.orthographicSize = spriteHeight / 2f;

        // Optionally, adjust camera position to center on the background
        Vector3 bgPosition = bgRenderer.transform.position;
        Vector3 cameraPosition = new Vector3(bgPosition.x, bgPosition.y, mainCamera.transform.position.z);
        mainCamera.transform.position = cameraPosition;

        Debug.Log($"Camera adjusted to background size: {spriteHeight} units height.");
    }
}