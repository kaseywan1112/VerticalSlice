using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public int maxCapacity = 4;
    public Image[] slotImages;
    public GameObject[] highlightBorders;
    public int selectedIndex = -1;

    public List<string> items = new List<string>();
    public List<Sprite> itemIcons = new List<Sprite>();

    void Awake()
    {
        instance = this;
    }

    public bool AddItem(string itemName, Sprite icon)
    {
        if (items.Count >= maxCapacity) return false;

        items.Add(itemName);
        itemIcons.Add(icon);
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            if (i < items.Count)
            {
                slotImages[i].sprite = itemIcons[i];
                slotImages[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void SelectSlot(int index)
    {
        if (selectedIndex == index)
        {
            selectedIndex = -1;
            Debug.Log("取消了选中状态");
        }
        else
        {
            selectedIndex = index;
            Debug.Log("当前选中了第 " + index + " 个格子");


            if (index < items.Count)
            {
                string selectedItemName = items[index]; 

                if ( selectedItemName == "MagicLamp")
                {
                    Debug.Log("摸到了神灯！触发神灯事件！");
                    if (LampEventHandler.Instance != null)
                    {
                        LampEventHandler.Instance.UseLampFromInventory();
                    }
                }
            }
        }

        for (int i = 0; i < maxCapacity; i++)
        {
            if (highlightBorders[i] != null)
            {
                highlightBorders[i].SetActive(false);
            }
        }

        if (selectedIndex != -1 && highlightBorders[selectedIndex] != null)
        {
            highlightBorders[selectedIndex].SetActive(true);
        }
    }
}