using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Dialogue System/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    [TextArea(3, 10)]
    public string npcText;
    public DialogueOption[] options;
}

[System.Serializable]
public class DialogueOption
{
    public string replyText;
    public DialogueNode nextNode;
}