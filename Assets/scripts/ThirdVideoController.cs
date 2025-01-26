using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;

public class ThirdVideoController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private float triggerDistance = 2f;
    [SerializeField] private PlayableDirector timelineDirector; // Reference to the PlayableDirector
    private bool isTriggered = false;

    void Start()
    {
        // Subscribe to the loopPointReached event
        videoPlayer.loopPointReached += OnVideoFinished;

    }

    void Update()
    {
        if (!isTriggered)
        {
            float distance = Vector3.Distance(transform.position, playerCamera.position);
            if (distance <= triggerDistance)
            {
                videoPlayer.Play();
                isTriggered = true;
                Debug.Log($"Video triggered. Distance: {distance}");
            }
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished playing.");
        // Start the timeline when the video finishes
        if (timelineDirector != null)
        {
            timelineDirector.Play();
        }
        else
        {
            Debug.LogWarning("PlayableDirector is not assigned.");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
