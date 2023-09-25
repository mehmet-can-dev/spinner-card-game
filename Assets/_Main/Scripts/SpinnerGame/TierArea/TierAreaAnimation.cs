using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierAreaAnimation : MonoBehaviour
{
    public void Init()
    {
    }

    public IEnumerator SlidingAnimation(RectTransform sliderRect, TierAreaZoneUi leftTierAreaZoneUi,
        TierAreaZoneUi rightTierAreaZoneUi)
    {
        StartCoroutine(RightToMiddleAnimation(rightTierAreaZoneUi,0));
        StartCoroutine(MiddleToLeftAnimation(leftTierAreaZoneUi));
        yield return SliderAnimation(sliderRect);
    }

    public IEnumerator RightToMiddleAnimation(TierAreaZoneUi tierAreaZoneUi, float delay)
    {
        var zoneRect = tierAreaZoneUi.ZoneRectTransform;
        var tierAreaZoneUiWidthHalf = zoneRect.rect.width * 0.5f;
        var zoneRectStartRightAnchoredPosition = -Vector2.left * tierAreaZoneUiWidthHalf;
        var duration = 1f;
        var startScale = Vector3.Scale(Vector3.one, Vector3.up + Vector3.forward);
        yield return RectTransformMoveAndScaleAnimation(zoneRect, zoneRectStartRightAnchoredPosition,
            Vector2.zero, startScale, Vector3.one, duration, delay);
    }

    public IEnumerator MiddleToLeftAnimation(TierAreaZoneUi tierAreaZoneUi)
    {
        var zoneRect = tierAreaZoneUi.ZoneRectTransform;

        var tierAreaZoneUiWidthHalf = zoneRect.rect.width * 0.5f;
        var zoneRectTargetLeftAnchoredPosition = Vector2.left * tierAreaZoneUiWidthHalf;
        var duration = 1f;
        var startScale = Vector3.Scale(Vector3.one, Vector3.up + Vector3.forward);
        yield return RectTransformMoveAndScaleAnimation(zoneRect, Vector2.zero,
            zoneRectTargetLeftAnchoredPosition, Vector3.one, startScale, duration);
    }

    public IEnumerator SliderAnimation(RectTransform sliderRect)
    {
        var duration = 1f;
        yield return RecTransformMoveAnimation(sliderRect, Vector2.zero, Vector2.left * 120, duration);
    }

    private static IEnumerator RectTransformMoveAndScaleAnimation(RectTransform rect,
        Vector2 startAnchoredPosition, Vector2 targetAnchoredPosition, Vector3 startScale, Vector3 targetScale,
        float duration, float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0;
        rect.anchoredPosition = startAnchoredPosition;
        rect.localScale = startScale;

        while (elapsedTime <= duration)
        {
            rect.anchoredPosition = Vector2.Lerp(startAnchoredPosition, targetAnchoredPosition,
                elapsedTime / duration);

            rect.localScale = Vector3.Lerp(startScale, targetScale,
                elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private static IEnumerator RecTransformMoveAnimation(RectTransform rect,
        Vector2 startAnchoredPosition, Vector2 targetAnchoredPosition,
        float duration, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        float elapsedTime = 0;
        rect.anchoredPosition = startAnchoredPosition;

        while (elapsedTime <= duration)
        {
            rect.anchoredPosition = Vector2.Lerp(startAnchoredPosition, targetAnchoredPosition,
                elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}