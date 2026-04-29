using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 5f;
    public Vector3 offset;

    public bool enableZLimit = true;
    public float minZ = -10f;
    public float maxZ = 10f; 

    public bool enableXLimit = false;
    public float minX = -10f;
    public float maxX = 10f;

    void Start()
    {
        if (offset == Vector3.zero && target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        if (enableZLimit)
        {
            smoothedPosition.z = Mathf.Clamp(smoothedPosition.z, minZ, maxZ);
        }

        if (enableXLimit)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        }
        transform.position = smoothedPosition;
    }
}
