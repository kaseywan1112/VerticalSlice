using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class LampEventHandler : MonoBehaviour
{
    public static LampEventHandler Instance;

    public DialogueNode lampForcedDialogue;

    public PlayableDirector summonTimeline;
    void Awake()
    {
        Instance = this;
    }

    public void UseLampFromInventory()
    {
        Debug.Log("玩家在背包里点击了神灯！准备强迫他搓一搓...");


        if (DialogueManager.Instance != null && lampForcedDialogue != null)
        {
            DialogueManager.Instance.StartConversation(lampForcedDialogue, StartSummonAnimation);
        }
    }

    private void StartSummonAnimation()
    {
        Debug.Log("逻辑闭环：对话结束，牛精灵出场！");
        if (summonTimeline != null)
        {
            summonTimeline.Play();
        }
    }
}
