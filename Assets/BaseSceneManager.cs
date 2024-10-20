using UnityEngine;

public class BaseSceneManager : MonoBehaviour
{
    private static BaseSceneManager instance;

    void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents destruction on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate if it already exists
        }
    }
}
