using UnityEngine;
using UnityEngine.Playables;

public class GraveTrigger : MonoBehaviour
{
    [Header("Timeline 设置")]
    public PlayableDirector graveTimeline;

    [Header("相机引用 (双重保险)")]
    public GameObject mainCameraObj;
    public GameObject graveCameraObj;

    [Header("对话设置")]
    public DialogueNode graveDialogue;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            // 播放 Timeline
            if (graveTimeline != null)
            {
                graveTimeline.Play();
                // 等待 Timeline 播放完毕后调用 OnCutsceneEnd
                Invoke("OnCutsceneEnd", (float)graveTimeline.duration);
            }
            else
            {
                OnCutsceneEnd();
            }
        }
    }

    void OnCutsceneEnd()
    {
        // 1. 相机状态强制归位
        if (mainCameraObj != null) mainCameraObj.SetActive(true);
        if (graveCameraObj != null) graveCameraObj.SetActive(false);

        // 2. 完美复刻 HouseTrigger 的对话触发逻辑
        if (DialogueManager.Instance != null && graveDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(graveDialogue);
        }
    }
}