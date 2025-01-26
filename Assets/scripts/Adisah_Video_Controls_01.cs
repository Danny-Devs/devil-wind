using UnityEngine;
using UnityEngine.Video;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(VideoPlayer))]
public class Adisah_Video_Controls_01 : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float initialDelay = 3f;

    private Material material;
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get components
        videoPlayer = GetComponent<VideoPlayer>();
        material = GetComponent<MeshRenderer>().material;

        // Ensure material is set up for transparency
        material.SetFloat("_Mode", 2); // Fade mode
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        // Start invisible
        SetAlpha(0);

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(initialDelay);

        // Fade in
        yield return FadeCoroutine(0f, 1f);

        videoPlayer.Play();

        // Wait for video to start
        while (!videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Wait for video to finish
        while (videoPlayer.isPlaying || !videoPlayer.isPrepared)
        {
            // Check if we're at the end of the video
            if (videoPlayer.frame > 0 && videoPlayer.frame >= (long)videoPlayer.frameCount - 2)
            {
                break;
            }
            yield return null;
        }

        // Fade out
        yield return FadeCoroutine(1f, 0f);
    }

    IEnumerator FadeCoroutine(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            SetAlpha(currentAlpha);
            yield return null;
        }

        SetAlpha(endAlpha);
    }

    void SetAlpha(float alpha)
    {
        Color color = material.color;
        color.a = alpha;
        material.color = color;
    }
}