using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for IEnumerator

public class MyAutoSceneTransition : MonoBehaviour
{
    public float delay = 9f;

    void Start()
    {
        // Ensure OVRManager is initialized
        if (OVRManager.instance != null)
        {
            // Disable passthrough at the start
            OVRManager.instance.isInsightPassthroughEnabled = false;
        }
        else
        {
            Debug.LogError("OVRManager instance is missing in the scene!");
        }
        StartCoroutine(TransitionPostDelay());
    }

    private IEnumerator TransitionPostDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Scene01");
    }
}
