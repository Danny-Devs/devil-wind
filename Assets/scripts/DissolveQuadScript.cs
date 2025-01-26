using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveQuadScript : MonoBehaviour
{
    private Material quadMaterial; // Reference to the quad's material

    private void Start()
    {
        // Get the material from the Renderer
        quadMaterial = GetComponent<Renderer>().material;

        if (quadMaterial == null)
        {
            Debug.LogError("No material found on the quad!");
            return;
        }

        // Start fading out
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float duration = 1f; // Duration of the fade
        float elapsedTime = 0f;

        Color color = quadMaterial.color; // Get the current color of the material

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            quadMaterial.color = new Color(color.r, color.g, color.b, alpha); // Update the alpha
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the alpha is exactly 0 at the end
        quadMaterial.color = new Color(color.r, color.g, color.b, 0f);
    }
}
