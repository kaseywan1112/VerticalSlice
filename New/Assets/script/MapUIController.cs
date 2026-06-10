using UnityEngine;

public class MapUIController : MonoBehaviour
{
    // 单例模式，方便其他脚本（比如你的背包系统）直接呼叫它
    public static MapUIController Instance;

    [Header("地图UI面板")]
    public GameObject mapPanel; // 等下在 Unity 里把包含大地图图片的那个全屏 UI 拖给它

    void Awake()
    {
        Instance = this;
        // 游戏一开始，确保地图是关着的
        if (mapPanel != null) mapPanel.SetActive(false);
    }

    // 提供给背包里地图按钮点击调用的方法
    public void OpenMap()
    {
        if (mapPanel != null) mapPanel.SetActive(true);
    }

    // 提供给全屏透明关闭按钮点击调用的方法
    public void CloseMap()
    {
        if (mapPanel != null) mapPanel.SetActive(false);
    }
}