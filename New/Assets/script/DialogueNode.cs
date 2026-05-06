using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public Sprite portrait;
    [TextArea(3, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue System/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    public DialogueLine[] lines;

    public DialogueOption[] options;
}

[System.Serializable]
public class DialogueOption
{
    public string speakerName;    
    public Sprite portrait;     
    public string replyText;
    public DialogueNode nextNode;
}