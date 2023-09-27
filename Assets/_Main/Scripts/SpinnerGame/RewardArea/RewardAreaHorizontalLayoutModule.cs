using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SpinnerGame.RewardArea
{
    public class RewardAreaHorizontalLayoutModule : MonoBehaviour
    {
        [Header("Child References")] [SerializeField]
        private HorizontalLayoutGroup horizontalLayoutGroup;

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform horizontalLayoutGroupContent;

        [Header("Project References")] [SerializeField]
        private RectTransform rewardAreaRewardUiPrefabRectTransform;

        private Vector2 layoutGroupContentStartSize;

        [Header("AnimationSettings")] [SerializeField]
        private RewardAreaViewScrollAnimation viewScrollAnimation;

        private int fitRewardUiCount;
        private float currentHorizontalValue = 0;
        private float lastHorizontalValue = 0;

        public void Init()
        {
            layoutGroupContentStartSize = GetLayoutGroupContentSize();
            fitRewardUiCount = (int)(horizontalLayoutGroupContent.sizeDelta.x /
                                     (rewardAreaRewardUiPrefabRectTransform.sizeDelta.x +
                                      horizontalLayoutGroup.spacing));

            currentHorizontalValue = scrollRect.horizontalScrollbar.value;
        }

        public bool IsNecessaryToIncreaseLayout(int uniqRewardCount)
        {
            if (uniqRewardCount >= fitRewardUiCount)
                return true;
            return false;
        }

        private bool IsFocussed()
        {
            // Avoid floating point
            if (Math.Abs(scrollRect.horizontalScrollbar.value - currentHorizontalValue) > 0.01f)
            {
                return true;
            }

            return false;
        }

        public void TryFocusIndex(int totalRewardUiCount, int focusRewardUiIndex, Action onComplete)
        {
            currentHorizontalValue = focusRewardUiIndex / (float)totalRewardUiCount;
            if (Math.Abs(currentHorizontalValue - lastHorizontalValue) > 0.01f)
            {
                DOTween.To(() => scrollRect.horizontalScrollbar.value,
                        x => { scrollRect.horizontalScrollbar.value = x; },
                        currentHorizontalValue, viewScrollAnimation.duration).SetEase(viewScrollAnimation.curve)
                    .SetLink(gameObject).OnComplete(() => { onComplete?.Invoke(); });
                lastHorizontalValue = currentHorizontalValue;
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        public void ResetSize()
        {
            SetLayoutGroupContentSize(layoutGroupContentStartSize);
        }

        public void IncreaseLayoutWidth()
        {
            var increaseAmount = rewardAreaRewardUiPrefabRectTransform.sizeDelta.x * 5f;
            var size = GetLayoutGroupContentSize();
            size.x += increaseAmount;
            SetLayoutGroupContentSize(size);
        }

        private void SetLayoutGroupContentSize(Vector2 targetSize)
        {
            var sizeDelta = horizontalLayoutGroupContent.sizeDelta;
            sizeDelta = targetSize;
            horizontalLayoutGroupContent.sizeDelta = sizeDelta;
        }

        private Vector2 GetLayoutGroupContentSize()
        {
            return horizontalLayoutGroupContent.sizeDelta;
        }
    }
}