using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float cameraHeight = 12f;
    public float cameraDistance = 10f;
    public float basePitch = 45f;

    public float maxPanDistance = 4f;
    public float panSmoothTime = 0.5f;
    public float maxPanSpeed = 10f;

    [Range(0.01f, 0.4f)]
    public float edgeThreshold = 0.1f;

    public bool isInDialogue = false;

    private Vector3 currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 basePosition = target.position - new Vector3(0, 0, cameraDistance);
        basePosition.y = cameraHeight;

        Vector3 mouseOffset = Vector3.zero;
        if (!isInDialogue)
        {
            float mouseXRatio = Mathf.Clamp01(Input.mousePosition.x / Screen.width);
            float panMultiplier = 0f;

            if (mouseXRatio < edgeThreshold)
            {
                panMultiplier = -(1f - (mouseXRatio / edgeThreshold));
            }
            else if (mouseXRatio > 1f - edgeThreshold)
            {
                panMultiplier = (mouseXRatio - (1f - edgeThreshold)) / edgeThreshold;
            }

            mouseOffset = new Vector3(panMultiplier * maxPanDistance, 0f, 0f);
        }

        Vector3 targetFinalPosition = basePosition + mouseOffset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetFinalPosition,
            ref currentVelocity,
            panSmoothTime,
            maxPanSpeed,
            Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(basePitch, 0, 0);
    }
}