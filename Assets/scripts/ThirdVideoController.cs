using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class ThirdVideoController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private float triggerDistance = 2f;
    [SerializeField] private float sceneChangeDelay = 2f;
    private bool isTriggered = false;

    void Start()
    {
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
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(sceneChangeDelay);
        SceneManager.LoadScene("Scene02");
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}