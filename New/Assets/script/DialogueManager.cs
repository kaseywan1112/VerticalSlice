using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcTextUI;
    public TextMeshProUGUI nameTextUI;
    public Image portraitImageUI;

    public GameObject inventoryPanel;


    public GameObject[] optionButtons;
    public GameObject continueButton;


    public GameObject player;

    private DialogueNode currentDataNode;
    private int currentLineIndex = 0;

    public void StartDialogue(DialogueNode startNode)
    {
        dialoguePanel.SetActive(true);
        if (inventoryPanel != null) inventoryPanel.SetActive(false);

        currentDataNode = startNode;
        currentLineIndex = 0;
        DisplayCurrentLine();
    }

    private void DisplayCurrentLine()
    {
        if (currentDataNode == null || currentDataNode.lines == null || currentDataNode.lines.Length == 0)
        {
            EndDialogue();
            return;
        }

        npcTextUI.gameObject.SetActive(true);
        if (nameTextUI != null) nameTextUI.gameObject.SetActive(true);
        HideAllOptionButtons();

        DialogueLine currentLine = currentDataNode.lines[currentLineIndex];

        npcTextUI.text = currentLine.text;
        if (nameTextUI != null) nameTextUI.text = currentLine.speakerName;

        if (portraitImageUI != null)
        {
            if (currentLine.portrait != null)
            {
                portraitImageUI.sprite = currentLine.portrait;
                portraitImageUI.gameObject.SetActive(true);
            }
            else
            {
                portraitImageUI.gameObject.SetActive(false);
            }
        }

        continueButton.SetActive(true);
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();

        if (currentLineIndex < currentDataNode.lines.Length - 1)
        {
            btn.onClick.AddListener(() =>
            {
                currentLineIndex++;
                DisplayCurrentLine();
            });
        }
        else
        {
            if (currentDataNode.options.Length == 0)
            {
                btn.onClick.AddListener(() => EndDialogue());
            }
            else if (currentDataNode.options.Length == 1)
            {
                btn.onClick.AddListener(() =>
                {
                    StartDialogue(currentDataNode.options[0].nextNode);
                });
            }
            else
            {
                btn.onClick.AddListener(() =>
                {
                    continueButton.SetActive(false);
                    ShowOptions(currentDataNode);
                });
            }
        }
    }

    private void ShowOptions(DialogueNode node)
    {
        npcTextUI.gameObject.SetActive(false);

        if (node.options.Length > 0)
        {
            DialogueOption firstOption = node.options[0];

            if (nameTextUI != null)
            {
                if (!string.IsNullOrEmpty(firstOption.speakerName))
                {
                    nameTextUI.gameObject.SetActive(true);
                    nameTextUI.text = firstOption.speakerName;
                }
                else
                {
                    nameTextUI.gameObject.SetActive(false);
                }
            }

            if (portraitImageUI != null)
            {
                if (firstOption.portrait != null)
                {
                    portraitImageUI.sprite = firstOption.portrait;
                    portraitImageUI.gameObject.SetActive(true);
                }
                else
                {
                    portraitImageUI.gameObject.SetActive(false);
                }
            }
        }

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < node.options.Length)
            {
                optionButtons[i].SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = node.options[i].replyText;

                Button btn = optionButtons[i].GetComponent<Button>();
                btn.onClick.RemoveAllListeners();

                DialogueNode nextNode = node.options[i].nextNode;
                btn.onClick.AddListener(() => StartDialogue(nextNode));
            }
            else
            {
                optionButtons[i].SetActive(false);
            }
        }
    }

    private void HideAllOptionButtons()
    {
        foreach (var btn in optionButtons)
        {
            if (btn != null) btn.SetActive(false);
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        if (portraitImageUI != null)
        {
            portraitImageUI.gameObject.SetActive(false);
        }

        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(true);
        }

        if (player != null)
        {
            Variables.Object(player).Set("isDialogueOpen", false);
        }
    }
}