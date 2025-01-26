using UnityEngine;
using UnityEngine.SceneManagement;
using Meta.XR;

public class Scene03Controller : MonoBehaviour
{
    private float initialDelay = 5f;
    private float timer = 0f;
    private bool canRestart = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= initialDelay && !canRestart)
        {
            canRestart = true;
            Debug.Log("Press A to restart experience");
        }

        if (canRestart && OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Restarting...");
            Invoke("RestartGame", 2f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Scene01");
    }
}