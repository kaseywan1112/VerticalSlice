using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public bool isPlayerNear = false;

    // 复用你之前学的 OnTrigger 逻辑
    void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) isPlayerNear = true; }
    void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) isPlayerNear = false; }

    void Update()
    {
        // 只有在这里我们检测空格按键
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space))
        {
            if (InventoryManager.instance.AddItem(itemName, itemIcon))
            {
                Destroy(gameObject); 
            }
        }
    }
}
