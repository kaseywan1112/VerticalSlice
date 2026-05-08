using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("0. 核心：追踪目标")]
    public Transform target;            // 依然需要把幽灵拖进来作为画面的绝对核心

    [Header("1. 基础视角设定 (机械迷城感)")]
    public float cameraHeight = 12f;
    public float cameraDistance = 10f;
    public float basePitch = 45f;

    [Header("2. 鼠标视差平移 (你自己把控数值)")]
    [Tooltip("【关键！】别设太大，建议 2 到 5 左右。如果太大，鼠标一晃主角就出屏幕了。")]
    public float maxPanDistance = 3f;   // 回归一个小一点的安全数值

    [Tooltip("整体跟随的平滑度，0.05 左右有很好的橡皮筋手感")]
    public float smoothSpeed = 0.05f;

    void LateUpdate()
    {
        if (target == null) return;

        // --- 1. 基础位置：永远死死盯住玩家 ---
        Vector3 basePosition = target.position - new Vector3(0, 0, cameraDistance);
        basePosition.y = cameraHeight; // 锁死高度

        // --- 2. 鼠标偏移：只在 X 轴上做文章 ---
        float mouseXRatio = Mathf.Clamp01(Input.mousePosition.x / Screen.width);
        float panMultiplier = (mouseXRatio - 0.5f) * 2f; // 把鼠标位置转换成 -1 到 1
        Vector3 mouseOffset = new Vector3(panMultiplier * maxPanDistance, 0f, 0f);

        // --- 3. 最终目标：玩家位置 + 鼠标偏移 ---
        Vector3 targetFinalPosition = basePosition + mouseOffset;

        // --- 4. 丝滑执行 ---
        // 位置：慢悠悠地插值移动过去
        transform.position = Vector3.Lerp(transform.position, targetFinalPosition, smoothSpeed);

        // 角度：死死锁住 45 度，绝对不抬头低头
        transform.rotation = Quaternion.Euler(basePitch, 0, 0);
    }
}