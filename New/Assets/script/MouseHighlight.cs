using UnityEngine;
using System.Collections.Generic;

public class ThickOutlineAuto : MonoBehaviour
{
    public Material baseOutlineMaterial; // 拖入你那个做好的描边材质
    public int layerCount = 20;          // 想要多厚，这里改数字就行

    private Renderer rend;
    private Material[] originalMaterials;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null) return;
        originalMaterials = rend.materials;
    }

    void OnMouseEnter()
    {
        List<Material> newMats = new List<Material>(originalMaterials);

        // 自动克隆 20 层，每层比前一层宽 0.05
        for (int i = 1; i <= layerCount; i++)
        {
            Material m = new Material(baseOutlineMaterial);
            m.SetFloat("_Outline_Thickness", i * 0.005f);
            newMats.Add(m);
        }
        rend.materials = newMats.ToArray();
    }

    void OnMouseExit()
    {
        rend.materials = originalMaterials;
    }
}