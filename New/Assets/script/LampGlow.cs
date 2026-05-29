using UnityEngine;

public class SimpleGlow : MonoBehaviour
{
    public Material glowMaterial; // 把 Mat_Glow 拖到这
    private MeshRenderer meshRenderer;
    private Material[] originalMaterials;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalMaterials = meshRenderer.materials;
    }

    void OnMouseEnter()
    {
        // 增加一个材质槽，把发光材质加进去
        Material[] newMaterials = new Material[originalMaterials.Length + 1];
        for (int i = 0; i < originalMaterials.Length; i++)
            newMaterials[i] = originalMaterials[i];

        newMaterials[newMaterials.Length - 1] = glowMaterial;
        meshRenderer.materials = newMaterials;
    }

    void OnMouseExit()
    {
        // 恢复原状
        meshRenderer.materials = originalMaterials;
    }
}