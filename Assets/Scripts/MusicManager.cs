using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    private static MusicManager instance;
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}