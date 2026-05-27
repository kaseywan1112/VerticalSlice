using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using Cinemachine;

public class ChickenManager : MonoBehaviour
{
    public static ChickenManager Instance;

    public PlayableDirector exitTimeline;
    public GameObject chickenNpc;
    public GameObject playerObject;

    public CameraFollow cameraFollowScript;
    public CinemachineBrain cineBrain;
    public GameObject chickenCamera; 

    private bool hasExited = false;

    void Awake() { Instance = this; }

    public void StartChickenExit()
    {
        if (hasExited) return;

        if (exitTimeline != null)
        {
            if (chickenNpc != null) chickenNpc.SetActive(true);
            if (playerObject != null) playerObject.SetActive(false);

            if (cameraFollowScript != null) cameraFollowScript.enabled = false;
            if (cineBrain != null) cineBrain.enabled = true;

            if (chickenCamera != null) chickenCamera.SetActive(true);

            exitTimeline.Play();
            hasExited = true;

            StartCoroutine(WaitAndRestore((float)exitTimeline.duration));
        }
    }

    private IEnumerator WaitAndRestore(float duration)
    {
        yield return new WaitForSeconds(duration + 0.1f);

        if (playerObject != null) playerObject.SetActive(true);
        if (chickenNpc != null) chickenNpc.SetActive(true);
        if (chickenCamera != null) chickenCamera.SetActive(false);
        if (cineBrain != null) cineBrain.enabled = false;
        if (cameraFollowScript != null) cameraFollowScript.enabled = true;
    }
}