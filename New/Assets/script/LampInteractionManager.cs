using UnityEngine;

public class SimplePrompt : MonoBehaviour
{
    public GameObject promptUI; // 在 Unity 里拖入你的 InteractionPrompt
    public float radius = 3f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (promptUI != null) promptUI.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // 核心：简单直接的显示/隐藏，不依赖任何第三方脚本
        if (dist <= radius)
        {
            if (promptUI != null && !promptUI.activeSelf) promptUI.SetActive(true);
        }
        else
        {
            if (promptUI != null && promptUI.activeSelf) promptUI.SetActive(false);
        }
    }
    public void ForceHide()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
            // 这里可以加一个布尔值锁住它，防止 OnTriggerExit 把 UI 再弄乱
            this.enabled = false;
        }
    }
}