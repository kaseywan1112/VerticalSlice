using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHider : MonoBehaviour
{
    public Transform player;

    public float hideRadius = 3f;

    private Transform cam;
    private Renderer[] treeRenderers;

    void Start()
    {
        cam = Camera.main.transform;

        treeRenderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (player == null || cam == null) return;

        Vector3 camPos = cam.position;
        Vector3 playerPos = player.position;

        foreach (Renderer r in treeRenderers)
        {
            if (r == null) continue;

            Vector3 treePos = r.transform.position;

            float distanceToLine = DistancePointLine(treePos, camPos, playerPos);

            bool isBetween = Vector3.Dot(treePos - camPos, playerPos - camPos) > 0 &&
                             Vector3.Distance(camPos, treePos) < Vector3.Distance(camPos, playerPos);

            if (distanceToLine < hideRadius && isBetween)
            {
                r.enabled = false; 
            }
            else
            {
                r.enabled = true; 
            }
        }
    }
    private float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 lineDir = (lineEnd - lineStart).normalized;
        Vector3 pointDir = point - lineStart;
        float projection = Vector3.Dot(pointDir, lineDir);
        Vector3 closestPoint = lineStart + lineDir * projection;
        return Vector3.Distance(point, closestPoint);
    }
}