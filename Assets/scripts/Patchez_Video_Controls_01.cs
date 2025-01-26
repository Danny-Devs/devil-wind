using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(VideoPlayer))]
public class Patchez_Video_Controls_01 : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float initialDelay = 3f;
    private Material material;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        material = GetComponent<MeshRenderer>().material;
        SetupMaterial();
        SetAlpha(0);
        StartCoroutine(PlaySequence());
    }

    void SetupMaterial()
    {
        material.SetFloat("_Mode", 2);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(initialDelay);
        yield return FadeCoroutine(0f, 1f);
        videoPlayer.Play();

        while (!videoPlayer.isPlaying)
            yield return null;

        while (videoPlayer.isPlaying || !videoPlayer.isPrepared)
        {
            if (videoPlayer.frame > 0 && videoPlayer.frame >= (long)videoPlayer.frameCount - 2)
                break;
            yield return null;
        }

        yield return FadeCoroutine(1f, 0f);
        SceneManager.LoadScene("Scene03");
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