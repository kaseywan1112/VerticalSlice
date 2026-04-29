using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;

    private bool isPlayerNear = false;
    private static GameObject sharedPrompt;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (sharedPrompt == null)
            {
                sharedPrompt = other.transform.Find("InteractionPrompt")?.gameObject;
            }

            if (sharedPrompt != null) sharedPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (sharedPrompt != null) sharedPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space))
        {
            if (InventoryManager.instance.AddItem(itemName, itemIcon))
            {
                if (sharedPrompt != null)
                {
                    sharedPrompt.SetActive(false);
                }
                Destroy(gameObject);
            }
        }
    }
}