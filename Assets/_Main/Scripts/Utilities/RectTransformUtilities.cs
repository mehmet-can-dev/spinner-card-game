using System.Collections;
using UnityEngine;

public class RectTransformUtilities
{
    public static AnimationCurve LinearCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public static IEnumerator RectTransformMoveAndScaleAnimation(RectTransform rect,
        Vector2 startAnchoredPosition, Vector2 targetAnchoredPosition, Vector3 startScale, Vector3 targetScale,
        float duration, AnimationCurve curve = null)
    {
        if (curve == null)
            curve = LinearCurve;

        float elapsedTime = 0;
        rect.anchoredPosition = startAnchoredPosition;
        rect.localScale = startScale;

        while (elapsedTime <= duration)
        {
            rect.anchoredPosition = Vector2.Lerp(startAnchoredPosition, targetAnchoredPosition,
                curve.Evaluate(elapsedTime / duration));

            rect.localScale = Vector3.Lerp(startScale, targetScale,
                curve.Evaluate(elapsedTime / duration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator RecTransformMoveAnimation(RectTransform rect,
        Vector2 startAnchoredPosition, Vector2 targetAnchoredPosition,
        float duration, AnimationCurve curve = null)
    {
        if (curve == null)
            curve = LinearCurve;

        float elapsedTime = 0;
        rect.anchoredPosition = startAnchoredPosition;

        while (elapsedTime <= duration)
        {
            rect.anchoredPosition = Vector2.Lerp(startAnchoredPosition, targetAnchoredPosition,
                curve.Evaluate(elapsedTime / duration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}