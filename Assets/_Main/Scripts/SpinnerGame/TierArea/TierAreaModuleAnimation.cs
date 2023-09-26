using System;
using System.Collections;
using UnityEngine;

namespace SpinnerGame.TierArea
{
    public class TierAreaModuleAnimation : MonoBehaviour
    {
        [Header("Project Reference")] [SerializeField]
        private TierAreaSlidingAnimationSettingsSO tierAreaSlidingAnimationSettingsSo;

        private const float SLIDERRECTSLIDEAMOUNT = 120;

        public void Init()
        {
        }

        public void StartSlidingAnimation(RectTransform sliderRect, TierAreaZoneUi leftTierAreaZoneUi,
            TierAreaZoneUi rightTierAreaZoneUi, Action onComplete)
        {
            StartCoroutine(SlidingAnimation(sliderRect, leftTierAreaZoneUi, rightTierAreaZoneUi,
                onComplete));
        }

        private IEnumerator SlidingAnimation(RectTransform sliderRect, TierAreaZoneUi leftTierAreaZoneUi,
            TierAreaZoneUi rightTierAreaZoneUi, Action onComplete)
        {
            StartCoroutine(RightToMiddleAnimation(rightTierAreaZoneUi));
            StartCoroutine(MiddleToLeftAnimation(leftTierAreaZoneUi));
            yield return SliderAnimation(sliderRect);
            onComplete?.Invoke();
        }

        private IEnumerator RightToMiddleAnimation(TierAreaZoneUi tierAreaZoneUi)
        {
            var zoneRect = tierAreaZoneUi.ZoneRectTransform;
            var tierAreaZoneUiWidthHalf = zoneRect.rect.width * 0.5f;
            var zoneRectStartRightAnchoredPosition = -Vector2.left * tierAreaZoneUiWidthHalf;
            var duration = tierAreaSlidingAnimationSettingsSo.duration;
            var curve = tierAreaSlidingAnimationSettingsSo.curve;

            var startScale = Vector3.Scale(Vector3.one, Vector3.up + Vector3.forward);
            yield return RectTransformUtilities.RectTransformMoveAndScaleAnimation(zoneRect,
                zoneRectStartRightAnchoredPosition,
                Vector2.zero, startScale, Vector3.one, duration, curve);
        }

        private IEnumerator MiddleToLeftAnimation(TierAreaZoneUi tierAreaZoneUi)
        {
            var zoneRect = tierAreaZoneUi.ZoneRectTransform;

            var tierAreaZoneUiWidthHalf = zoneRect.rect.width * 0.5f;
            var zoneRectTargetLeftAnchoredPosition = Vector2.left * tierAreaZoneUiWidthHalf;
            var duration = tierAreaSlidingAnimationSettingsSo.duration;
            var startScale = Vector3.Scale(Vector3.one, Vector3.up + Vector3.forward);
            var curve = tierAreaSlidingAnimationSettingsSo.curve;

            yield return RectTransformUtilities.RectTransformMoveAndScaleAnimation(zoneRect, Vector2.zero,
                zoneRectTargetLeftAnchoredPosition, Vector3.one, startScale, duration, curve);
        }

        private IEnumerator SliderAnimation(RectTransform sliderRect)
        {
            var duration = tierAreaSlidingAnimationSettingsSo.duration;
            var curve = tierAreaSlidingAnimationSettingsSo.curve;
            yield return RectTransformUtilities.RecTransformMoveAnimation(sliderRect, Vector2.zero,
                Vector2.left * SLIDERRECTSLIDEAMOUNT, duration, curve);
        }
    }
}