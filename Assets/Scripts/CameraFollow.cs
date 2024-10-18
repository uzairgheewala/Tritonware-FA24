using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player's Transform
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // Optional offset

    void Update()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, -10f);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
