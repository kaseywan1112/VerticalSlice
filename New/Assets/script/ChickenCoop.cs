using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCoop : MonoBehaviour
{
    public DialogueNode knockDialogue;
    public Transform playerTransform;
    public float interactDistance = 3f;

    public GameObject interactPrompt; 

    private bool isPlayerInRange = false;
    private bool hasKnocked = false; 

    void Start()
    {

        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    void Update()
    {

        if (hasKnocked || playerTransform == null) return;

        isPlayerInRange = Vector3.Distance(transform.position, playerTransform.position) <= interactDistance;

        bool isDialogueOpen = DialogueManager.Instance != null && DialogueManager.Instance.dialoguePanel.activeSelf;

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

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isDialogueOpen)
        {
            if (interactPrompt != null) interactPrompt.SetActive(false);

            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartConversation(knockDialogue, ChickenManager.Instance.StartChickenExit);
            }

            hasKnocked = true; 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}