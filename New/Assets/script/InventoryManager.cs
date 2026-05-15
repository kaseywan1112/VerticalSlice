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

    public void OnSlotPointerDown(int index)
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            if (highlightBorders[i] != null)
            {
                highlightBorders[i].SetActive(false);
            }
        }

        if (index < items.Count && highlightBorders[index] != null)
        {
            highlightBorders[index].SetActive(true);
            selectedIndex = index;
        }
    }

    public void OnSlotPointerUp(int index)
    {
        if (index >= 0 && index < maxCapacity && highlightBorders[index] != null)
        {
            highlightBorders[index].SetActive(false);
        }

        if (index < items.Count)
        {
            string selectedItemName = items[index];

            if (selectedItemName == "MagicLamp" || selectedItemName == "神灯")
            {
                if (LampEventHandler.Instance != null)
                {
                    LampEventHandler.Instance.UseLampFromInventory();
                }
            }
        }

        selectedIndex = -1;
    }
}