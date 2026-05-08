using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCInteraction : MonoBehaviour
{
    public DialogueNode startingNode;

    public Transform playerTransform;
    public float interactRadius = 3f;
    private bool isPlayerInRange = false;

    public GameObject interactPrompt;

    public UnityEvent onDialogueComplete;

    void Start()
    {
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        isPlayerInRange = Vector3.Distance(transform.position, playerTransform.position) <= interactRadius;

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

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space) && !isDialogueOpen)
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        if (startingNode != null)
        {
            DialogueManager.Instance.StartConversation(startingNode, FinishDialogue);
        }
    }

    public void FinishDialogue()
    {
        onDialogueComplete.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}