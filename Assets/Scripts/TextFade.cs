using System.Collections;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro; // Reference to the TextMeshPro component
    [SerializeField] private float fadeDuration = 1.0f; // Duration of the fade-out effect
    [SerializeField] private float displayDuration = 2.0f; // Duration to display the text before fading
    private Color color = new Color(0,0.0378871f,0.3433962f);

    public void StartShowAndFade()
    {
        textMeshPro.gameObject.SetActive(true);
        textMeshPro.color = color;
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        // Wait for the specified display duration
        yield return new WaitForSeconds(displayDuration);
        
        Color originalColor = textMeshPro.color; // Store the original color
        float startAlpha = originalColor.a; // Get the starting alpha value
        
        // Fade out loop
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(startAlpha, 0, normalizedTime); // Interpolate alpha value
            textMeshPro.color = newColor; // Apply new color to the text
            yield return null; // Wait for the next frame
        }
        
        // Ensure the text is fully transparent at the end
        Color finalColor = originalColor;
        finalColor.a = 0;
        textMeshPro.color = finalColor;
        textMeshPro.gameObject.SetActive(false);
    }
}