using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<string> items = new List<string>();
    public int maxCapacity = 4;

    public Image[] slotImages;
    public GameObject[] highlightBorders;

    public int selectedIndex = -1;

    void Awake() { instance = this; }

    public bool AddItem(string itemName, Sprite icon)
    {
        if (items.Count >= maxCapacity) return false;

        items.Add(itemName);
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            if (i < items.Count)
            {
                slotImages[i].sprite = null;
                slotImages[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                slotImages[i].color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void SelectSlot(int index)
    {
        selectedIndex = index;
        Debug.Log("当前选中了第 " + index + " 个格子");
    }
}