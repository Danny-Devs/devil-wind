using UnityEngine;

public class RedDoorController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float triggerDistance = 3f;

    private bool hasPlayed = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (!hasPlayed && playerCamera != null)
        {
            float distance = Vector3.Distance(transform.position, playerCamera.position);
            if (distance <= triggerDistance)
            {
                audioSource.Play();
                hasPlayed = true;
                Debug.Log($"Playing audio. Distance: {distance}");
            }
        }
    }
}