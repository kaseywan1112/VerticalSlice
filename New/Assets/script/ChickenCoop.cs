using UnityEngine;

public class ChickenCoop : MonoBehaviour
{
    public DialogueNode knockDialogue;
    public Transform playerTransform; // 记得在 Inspector 里把 Player 拖进来
    public float interactDistance = 3f;

    public GameObject interactPrompt;

    private bool isPlayerInRange = false;
    private bool hasKnocked = false;

    void Start()
    {
        // 关键修复：如果 Inspector 里忘了拖 Player，运行时自动找，防止 Build 后报错
        if (playerTransform == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) playerTransform = p.transform;
        }

        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    void Update()
    {
        // 只有没敲过门时才检测距离
        if (playerTransform == null) return;

        isPlayerInRange = Vector3.Distance(transform.position, playerTransform.position) <= interactDistance;
        bool isDialogueOpen = DialogueManager.Instance != null && DialogueManager.Instance.dialoguePanel.activeSelf;

        // 如果已经敲过门，强制隐藏 UI 并退出逻辑
        if (hasKnocked)
        {
            if (interactPrompt != null && interactPrompt.activeSelf)
                interactPrompt.SetActive(false);
            return;
        }

        // 正常的显示隐藏逻辑
        if (interactPrompt != null)
        {
            if (isPlayerInRange && !isDialogueOpen)
            {
                interactPrompt.SetActive(true);
            }
            else
            {
                interactPrompt.SetActive(false);
            }
        }

        // 交互逻辑
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isDialogueOpen)
        {
            PerformKnock();
        }
    }

    private void PerformKnock()
    {
        if (interactPrompt != null) interactPrompt.SetActive(false);

        if (DialogueManager.Instance != null)
        {
            // 开始对话，传入 ChickenManager 的回调
            DialogueManager.Instance.StartConversation(knockDialogue, OnDialogueComplete);
        }

        hasKnocked = true;
    }

    // 对话结束后的清理回调
    private void OnDialogueComplete()
    {
        // 触发鸡舍逻辑
        if (ChickenManager.Instance != null)
        {
            ChickenManager.Instance.StartChickenExit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}