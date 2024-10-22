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
        Debug.Log($"Scene loaded: {scene.name}");
        AdjustCameraToBackground();
    }

    void AdjustCameraToBackground()
    {
        // Find the background sprite using the tag
        GameObject bgObject = GameObject.FindGameObjectWithTag("Background");

        if (bgObject == null)
        {
            Debug.LogWarning("No GameObject with tag 'Background' found in the scene to adjust the camera.");
            mainCamera.orthographicSize = 5f; // Default size
            return;
        }

        SpriteRenderer bgRenderer = bgObject.GetComponent<SpriteRenderer>();

        if (bgRenderer == null)
        {
            Debug.LogWarning("Background GameObject found, but it does not have a SpriteRenderer component.");
            mainCamera.orthographicSize = 5f; // Default size
            return;
        }

        Sprite bgSprite = bgRenderer.sprite;

        if (bgSprite == null)
        {
            Debug.LogWarning("Background SpriteRenderer found, but no sprite is assigned.");
            mainCamera.orthographicSize = 5f; // Default size
            return;
        }

        // Calculate the height and width in world units
        float spriteHeight = bgSprite.bounds.size.y;
        float spriteWidth = bgSprite.bounds.size.x;
        Debug.Log($"Background Sprite Size - Width: {spriteWidth}, Height: {spriteHeight}");

        // Calculate required orthographic size based on height and aspect ratio
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float targetOrthographicSize = spriteHeight / 2f;

        // Adjust orthographic size to ensure the entire background fits
        float targetOrthographicSizeWidth = (spriteWidth / screenAspect) / 2f;
        targetOrthographicSize = Mathf.Max(targetOrthographicSize, targetOrthographicSizeWidth);

        mainCamera.orthographicSize = targetOrthographicSize;
        Debug.Log($"Camera orthographic size set to: {mainCamera.orthographicSize}");

        // Optionally, adjust camera position to center on the background
        Vector3 bgPosition = bgRenderer.transform.position;
        Vector3 cameraPosition = new Vector3(bgPosition.x, bgPosition.y, mainCamera.transform.position.z);
        transform.position = cameraPosition;
        Debug.Log($"Camera position set to: {cameraPosition}");
    }
}