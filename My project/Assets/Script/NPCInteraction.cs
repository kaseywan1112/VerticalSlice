using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{

    public GameObject talkObject;
    public GameObject stopObject;
    public Transform playerTransform;

    public float outerRadius = 3f;
    public float innerRadius = 0.6f;

    public bool canTalk = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) playerTransform = player.transform;
            return;
        }

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < innerRadius)
        {
            ShowStopMode();
        }
        else if (distance < outerRadius)
        {
            ShowTalkMode();
        }
        else
        {
            HideAll();
        }
    }
    void ShowStopMode()
    {
        talkObject.SetActive(false);
        stopObject.SetActive(true);
        canTalk = false;
    }

    void ShowTalkMode()
    {
        talkObject.SetActive(true);
        stopObject.SetActive(false);
        canTalk = true;
    }

    void HideAll()
    {
        talkObject.SetActive(false);
        stopObject.SetActive(false);
        canTalk = false;
    }
}
