using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;

public class LampEventHandler : MonoBehaviour
{
    public static LampEventHandler Instance;

    public DialogueNode lampForcedDialogue;
    public DialogueNode shortSummonDialogue;
    public DialogueNode lampRecallDialogue;
    public DialogueNode pickupDialogue;

    public PlayableDirector summonTimeline;
    public Transform playerTransform;
    public GameObject bullNpc;
    public GameObject pressSpacePrompt;

    public DialogueNode chickenForbiddenDialogue;
    public float chickenForbiddenRadius = 5f;
    public GameObject chickenObject;

    private bool isGenieActive = false;
    private bool hasMetGenie = false;

    void Awake()
    {
        Instance = this;
    }

    public void TriggerPickupDialogue()
    {
        if (DialogueManager.Instance != null && pickupDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(pickupDialogue);
        }
        else
        {
            Debug.LogWarning("LampEventHandler: 对话管理器或 PickupDialogue 未配置！");
        }
    }

    public void UseLampFromInventory()
    {
        if (DialogueManager.Instance == null) return;

        // 【新增】只要玩家使用了神灯，立刻播放擦神灯(Window Wipe)的声音！
        if (AudioManager.Instance != null) AudioManager.Instance.PlayRubLamp();

        if (chickenObject != null && chickenObject.activeInHierarchy)
        {
            float distToChicken = Vector3.Distance(playerTransform.position, chickenObject.transform.position);
            if (distToChicken < chickenForbiddenRadius)
            {
                if (chickenForbiddenDialogue != null)
                {
                    DialogueManager.Instance.StartDialogue(chickenForbiddenDialogue);
                }
                return;
            }
        }

        if (!isGenieActive)
        {
            if (!hasMetGenie)
            {
                if (lampForcedDialogue != null)
                    DialogueManager.Instance.StartConversation(lampForcedDialogue, StartSummonAnimation);
            }
            else
            {
                if (shortSummonDialogue != null)
                    DialogueManager.Instance.StartConversation(shortSummonDialogue, StartSummonAnimation);
                else
                    StartSummonAnimation();
            }
        }
        else
        {
            if (lampRecallDialogue != null)
                DialogueManager.Instance.StartConversation(lampRecallDialogue, RecallGenie);
        }
    }

    private void StartSummonAnimation()
    {
        hasMetGenie = true;

        // 【新增】进入召唤流程，马上要放 Timeline 了，播放牛 Pop Up 的音效！
        if (AudioManager.Instance != null) AudioManager.Instance.PlayPopUp();

        if (summonTimeline != null && playerTransform != null)
        {
            Transform stage = summonTimeline.transform;
            Vector3 idealPosition = playerTransform.position + playerTransform.forward * 2f;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(idealPosition, out hit, 3.0f, NavMesh.AllAreas))
                stage.position = hit.position;
            else
                stage.position = playerTransform.position;

            stage.rotation = Quaternion.LookRotation(playerTransform.position - stage.position);

            if (pressSpacePrompt != null) pressSpacePrompt.SetActive(false);

            summonTimeline.Play();
            isGenieActive = true;
        }
    }

    private void RecallGenie()
    {
        if (bullNpc != null)
        {
            bullNpc.SetActive(false);
            isGenieActive = false;
        }
    }
}