using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;
using System.Collections;

public class DoorTriggerController : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float openDistance = 4f;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float animationDuration = 1.0f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = door.transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(Vector3.forward * openAngle);
    }

    void Update()
    {
        if (!isOpen)
        {
            float distance = Vector3.Distance(transform.position, playerCamera.position);
            if (distance <= openDistance)
            {
                StartCoroutine(DoorSequence());
                isOpen = true;
                Debug.Log($"Opening door. Distance: {distance}");
            }
        }
    }

    IEnumerator DoorSequence()
    {
        yield return StartCoroutine(AnimateDoor(closedRotation, openRotation));
    }

    IEnumerator AnimateDoor(Quaternion startRotation, Quaternion endRotation)
    {
        float elapsed = 0;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;
            door.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        door.transform.rotation = endRotation;
    }
}