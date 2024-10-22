using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}