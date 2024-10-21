using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCamera : MonoBehaviour
{
    public static PersistentCamera Instance { get; private set; }
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // Optional offset

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
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, -10f);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}