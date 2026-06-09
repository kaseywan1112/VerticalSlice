using UnityEngine;
using UnityEngine.Playables;

public class HouseTrigger : MonoBehaviour
{
    public PlayableDirector houseTimeline;
    public DialogueNode arrivalDialogue;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            // 播放你配置好 Activation 和 Animation 的 Timeline
            if (houseTimeline != null) houseTimeline.Play();

            // 动画结束后的对话
            Invoke("TriggerDialogue", (float)houseTimeline.duration);
        }
    }

    void TriggerDialogue()
    {
        if (DialogueManager.Instance != null && arrivalDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(arrivalDialogue);
        }
    }
}