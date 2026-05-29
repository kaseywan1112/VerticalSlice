using UnityEngine;

public class MouseHoverHighlight : MonoBehaviour
{
    [Range(0.001f, 0.5f)]
    public float thickness = 0.002f; // 你现在可以在每个物体的面板上单独调整它了！

    private Renderer targetRenderer;
    private int defaultLayer;
    private int outlineLayer;

    void Start()
    {
        // 1. 获取子物体身上的渲染器
        targetRenderer = GetComponentInChildren<Renderer>();

        // 2. 如果找到了渲染器，才去取它的 layer
        if (targetRenderer != null)
        {
            // 这里之前写错了，应该用 targetRenderer.gameObject.layer
            defaultLayer = targetRenderer.gameObject.layer;
            outlineLayer = LayerMask.NameToLayer("Outline");
        }
    }

    void OnMouseEnter()
    {
        if (targetRenderer != null)
        {
            targetRenderer.gameObject.layer = outlineLayer;
            // 鼠标移入时，把这个物体的材质厚度刷成你填写的数值
            targetRenderer.material.SetFloat("_OutlineThickness", thickness);
        }
    }

    void OnMouseExit()
    {
        if (targetRenderer != null)
        {
            targetRenderer.gameObject.layer = defaultLayer;
        }
    }
}