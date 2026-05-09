using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class TriggerTimeline : MonoBehaviour
{
    public PlayableDirector timelineToPlay;

    public Camera mainCamera;


    public CameraFollow cameraFollowScript;

    private bool hasPlayed = false;
    private CinemachineBrain cineBrain;

    void Start()
    {
        if (mainCamera != null)
        {
            cineBrain = mainCamera.GetComponent<CinemachineBrain>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (timelineToPlay != null)
            {
                if (cameraFollowScript != null) cameraFollowScript.enabled = false;
                if (cineBrain != null) cineBrain.enabled = true;

                timelineToPlay.Play();
                hasPlayed = true;

                timelineToPlay.stopped += OnTimelineFinished;
            }
        }
    }

    void OnTimelineFinished(PlayableDirector director)
    {
        if (cineBrain != null) cineBrain.enabled = false;
        if (cameraFollowScript != null) cameraFollowScript.enabled = true;

        timelineToPlay.stopped -= OnTimelineFinished;
    }
}