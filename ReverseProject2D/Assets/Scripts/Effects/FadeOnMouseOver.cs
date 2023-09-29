using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FadeOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float fadeDuration = 0.5f;
    public float fadeAlpha = 0.5f;

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCoroutine(fadeAlpha));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCoroutine(1f));
    }

    private System.Collections.IEnumerator FadeCoroutine(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
