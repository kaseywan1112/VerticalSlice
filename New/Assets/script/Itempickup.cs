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
                // 关键点：不管 InteractionPrompt 藏在 Player 的哪一层子物体里，通通找出来！
                sharedPrompt = other.transform.GetComponentInChildren<Transform>(true)
                                    .Find("InteractionPrompt")?.gameObject;

                // 如果上面还是不行，干脆用这句最暴力的：
                if (sharedPrompt == null)
                {
                    Transform[] allChildren = other.GetComponentsInChildren<Transform>(true);
                    foreach (Transform child in allChildren)
                    {
                        if (child.name == "InteractionPrompt")
                        {
                            sharedPrompt = child.gameObject;
                            break;
                        }
                    }
                }
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
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            if (InventoryManager.instance.AddItem(itemName, itemIcon))
            {
                SimplePrompt prompt = GetComponent<SimplePrompt>();
                if (prompt != null)
                {
                    prompt.ForceHide();
                }
                Destroy(gameObject);
            }
        }

    }
}