using System.Collections;
using UnityEngine;
using UnityEngine.Playables; // 引入 Timeline 命名空间

public class AlbumEndingSequence : MonoBehaviour
{
    [Header("1. 基础配置")]
    public string itemName = "相册";
    public Sprite itemIcon;
    public GameObject dialogueUI;
    public GameObject manualInteractPrompt;
    public Transform playerTransform;

    [Header("2. 对话流程节点")]
    public DialogueNode noteDialogue;       // 纸条：充满悔恨的独白
    public DialogueNode bullEndingDialogue; // 牛：索要愿望
    public DialogueNode chickenEndingDialogue; // 鸡：抢走愿望

    [Header("3. 场景角色（必须在场景中预先隐藏）")]
    public GameObject cowEndingNPC;
    public GameObject chickenEndingNPC;

    [Header("4. 演出 Timeline 一条龙")]
    // 【全新加入】牛登场的专门 Timeline
    public PlayableDirector cowArrivalTimeline;
    // 鸡从天而降的专门 Timeline
    public PlayableDirector chickenArrivalTimeline;
    // 最后的大结局（披萨拉远）Timeline
    public PlayableDirector endingTimeline;

    private bool isPlayerNear = false;
    private bool hasPickedUp = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPickedUp)
        {
            isPlayerNear = true;
            if (manualInteractPrompt != null) manualInteractPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (manualInteractPrompt != null) manualInteractPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !hasPickedUp)
        {
            hasPickedUp = true;
            if (manualInteractPrompt != null) manualInteractPrompt.SetActive(false);

            if (InventoryManager.instance != null)
                InventoryManager.instance.AddItem(itemName, itemIcon);

            if (GetComponent<MeshRenderer>() != null) GetComponent<MeshRenderer>().enabled = false;
            if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = false;

            // 开启纯线性的电影级大结局流程
            StartCoroutine(PlayEndingSequence());
        }
    }

    IEnumerator PlayEndingSequence()
    {
        // ================= 阶段一：读纸条 =================
        if (DialogueManager.Instance != null && noteDialogue != null)
            DialogueManager.Instance.StartDialogue(noteDialogue);

        yield return new WaitForSeconds(0.5f);
        // 等待纸条对话框关闭
        while (dialogueUI != null && dialogueUI.activeInHierarchy) yield return null;


        // ================= 阶段二：牛出现演出 (Timeline) =================
        // 【防坑关键】在播放动画前，强行激活牛并拉到玩家面前，唤醒 Timeline 组件
        if (cowEndingNPC != null && playerTransform != null)
        {
            cowEndingNPC.SetActive(true);
        }

        if (cowArrivalTimeline != null)
        {
            cowArrivalTimeline.Play();
            // 精确等待牛的登场动画播完
            yield return new WaitForSeconds((float)cowArrivalTimeline.duration);
        }

        // ================= 阶段三：牛的索要愿望对话 =================
        if (DialogueManager.Instance != null && bullEndingDialogue != null)
            DialogueManager.Instance.StartDialogue(bullEndingDialogue);

        yield return new WaitForSeconds(0.5f);
        // 等待牛的对话框关闭
        while (dialogueUI != null && dialogueUI.activeInHierarchy) yield return null;


        // ================= 阶段四：鸡出现演出 (Timeline) =================
        // 【防坑关键】在播放动画前，强行激活鸡！
        if (chickenEndingNPC != null)
        {
            chickenEndingNPC.SetActive(true);
        }

        if (chickenArrivalTimeline != null)
        {
            chickenArrivalTimeline.Play();
            // 精确等待鸡的降落动画播完
            yield return new WaitForSeconds((float)chickenArrivalTimeline.duration);
        }

        // ================= 阶段五：鸡索要愿望对话 =================
        if (DialogueManager.Instance != null && chickenEndingDialogue != null)
            DialogueManager.Instance.StartDialogue(chickenEndingDialogue);

        yield return new WaitForSeconds(0.5f);
        // 等待鸡的对话框关闭
        while (dialogueUI != null && dialogueUI.activeInHierarchy) yield return null;


        // ================= 阶段六：播放大结局 (披萨 Timeline) =================
        if (endingTimeline != null)
        {
            // 【终极防坑】强制激活挂载了该 Timeline 的游戏物体！
            endingTimeline.gameObject.SetActive(true);
            endingTimeline.Play();
        }

    }
}