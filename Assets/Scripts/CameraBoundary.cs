using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraBoundary : MonoBehaviour
{
    private Transform background; 
    private Camera mainCamera;

    private float minX, maxX, minY, maxY;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogError("CameraBoundary requires a Camera component.");
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
        AssignBackground();
    }

    void AssignBackground()
    {
        background = GameObject.FindGameObjectWithTag("Background")?.transform;

        if (background == null)
        {
            Debug.LogWarning("No Background GameObject found with tag 'Background'. Camera boundaries not set.");
            return;
        }

        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
        if (bgRenderer == null)
        {
            Debug.LogWarning("Background GameObject does not have a SpriteRenderer component.");
            return;
        }

        Sprite bgSprite = bgRenderer.sprite;
        if (bgSprite == null)
        {
            Debug.LogWarning("Background SpriteRenderer found, but no sprite is assigned.");
            return;
        }

        float spriteWidth = bgSprite.bounds.size.x;
        float spriteHeight = bgSprite.bounds.size.y;

        Vector3 bgPosition = background.position;

        // Calculate camera boundaries
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        minX = bgPosition.x - (spriteWidth / 2f) + (cameraWidth / 2f);
        maxX = bgPosition.x + (spriteWidth / 2f) - (cameraWidth / 2f);
        minY = bgPosition.y - (spriteHeight / 2f) + (cameraHeight / 2f);
        maxY = bgPosition.y + (spriteHeight / 2f) - (cameraHeight / 2f);

        Debug.Log($"Camera Boundaries - MinX: {minX}, MaxX: {maxX}, MinY: {minY}, MaxY: {maxY}");
    }

    void LateUpdate()
    {
        if (background == null)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("No Player GameObject found with tag 'Player'.");
            return;
        }

        Vector3 playerPosition = player.transform.position;

        float clampedX = Mathf.Clamp(playerPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(playerPosition.y, minY, maxY);

        Vector3 desiredPosition = new Vector3(clampedX, clampedY, mainCamera.transform.position.z);

        // Only update camera position if it's within boundaries
        mainCamera.transform.position = desiredPosition;
    }
}