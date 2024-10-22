using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections.Generic;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineCameraManager : MonoBehaviour
{
    public static CinemachineCameraManager Instance { get; private set; }
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineConfiner2D confiner;

    [Tooltip("Map each scene name to its corresponding Confiner Shape name.")]
    public List<SceneConfiner> sceneConfinerMap = new List<SceneConfiner>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Logger.LogWarning("Duplicate PersistentCamera instance destroyed.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            Logger.Log("PersistentCamera instance created.");
        }

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineCameraManager requires a CinemachineVirtualCamera component.");
        }

        confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
        if (confiner == null)
        {
            Debug.LogError("CinemachineConfiner2D extension not found on the virtual camera.");
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
        //if (scene.name == "Kitchen")
        //{
        AssignConfinerShape(scene.name);
        //}
    }

    void AssignConfinerShape(string sceneName)
    {
        // Find the confiner shape name for the current scene
        string confinerShapeName = null;
        foreach (SceneConfiner sc in sceneConfinerMap)
        {
            if (sc.sceneName.Equals(sceneName, System.StringComparison.OrdinalIgnoreCase))
            {
                confinerShapeName = sc.confinerShapeName;
                break;
            }
        }

        if (string.IsNullOrEmpty(confinerShapeName))
        {
            Debug.LogWarning($"No confiner shape defined for scene '{sceneName}'. Disabling Cinemachine Confiner.");
            confiner.m_BoundingShape2D = null;
            return;
        }

        // Find the confiner shape by name
        GameObject confinerShape = GameObject.Find(confinerShapeName);

        if (confinerShape == null)
        {
            Debug.LogWarning($"Confiner shape '{confinerShapeName}' not found in scene '{sceneName}'. Disabling Cinemachine Confiner.");
            confiner.m_BoundingShape2D = null;
            return;
        }

        PolygonCollider2D polyCollider = confinerShape.GetComponent<PolygonCollider2D>();
        if (polyCollider == null)
        {
            Debug.LogWarning($"ConfinerShape '{confinerShapeName}' does not have a PolygonCollider2D component. Disabling Cinemachine Confiner.");
            confiner.m_BoundingShape2D = null;
            return;
        }

        confiner.m_BoundingShape2D = polyCollider;
        Debug.Log($"Cinemachine Confiner assigned to: {confinerShape.name}");
    }
}

[System.Serializable]
public class SceneConfiner
{
    public string sceneName;          // Name of the scene (e.g., "Kitchen")
    public string confinerShapeName;  // Name of the confiner shape GameObject (e.g., "Confiner_K")
}