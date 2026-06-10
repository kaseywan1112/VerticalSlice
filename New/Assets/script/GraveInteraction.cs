using UnityEngine;

public class GraveInteraction : MonoBehaviour
{
    [Header("交互提示设置")]
    // 像刚才相册一样，把你那个 Press F 的 UI 拖进来
    public GameObject manualInteractPrompt;

    [Header("小鬼设置")]
    public GameObject littleGhostObj;
    public DialogueNode ghostDialogue;

    [Header("藏宝图设置")]
    public Sprite treasureMapIcon; // 在 Inspector 里拖入藏宝图在背包里显示的图标

    private bool hasInteracted = false;
    private bool isPlayerNear = false;

    void Start()
    {
        if (littleGhostObj != null) littleGhostObj.SetActive(false);
    }

    // ================= 新增：物理触发检测 (用于按 F 交互) =================
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
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
        // 如果玩家在附近，并且按下了 F 键，且还没互动过
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !hasInteracted)
        {
            ExecuteInteraction();
        }
    }

    // ================= 保留：鼠标点击依然有效 =================
    void OnMouseDown()
    {
        // 确保没有点在UI上，且只触发一次
        if (!hasInteracted && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            ExecuteInteraction();
        }
    }

    // ================= 核心执行逻辑 =================
    void ExecuteInteraction()
    {
        hasInteracted = true;

        // 交互成功后，立刻关掉提示框
        if (manualInteractPrompt != null) manualInteractPrompt.SetActive(false);

        TriggerGraveEvent();
    }

    void TriggerGraveEvent()
    {
        // 1. 小鬼显形
        if (littleGhostObj != null) littleGhostObj.SetActive(true);

        // 2. 触发对话
        if (DialogueManager.Instance != null && ghostDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(ghostDialogue);
        }

        // 3. 将地图送入物品栏
        if (InventoryManager.instance != null && treasureMapIcon != null)
        {
            InventoryManager.instance.AddItem("藏宝图", treasureMapIcon);
            Debug.Log("藏宝图已自动放入背包！");
        }
    }
}