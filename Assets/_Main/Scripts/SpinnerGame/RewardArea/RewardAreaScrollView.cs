using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SpinnerGame.RewardArea
{
    public class RewardAreaScrollView : MonoBehaviour
    {
        private int fitRewardUiCount;
        private float currentHorizontalValue = 0;
        private float lastHorizontalValue = 0;

        [Header("Child References")] [SerializeField]
        private HorizontalLayoutGroup horizontalLayoutGroup;

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform horizontalLayoutGroupContent;

        [Header("Project References")] [SerializeField]
        private RectTransform rewardAreaRewardUiPrefabRectTransform;

        private Vector2 layoutGroupContentStartSize;

        [Header("AnimationSettings")] [SerializeField]
        private RewardAreaViewScrollAnimation viewScrollAnimation;

        public void Init()
        {
            layoutGroupContentStartSize = GetLayoutGroupContentSize();
            fitRewardUiCount = (int)(horizontalLayoutGroupContent.sizeDelta.x /
                                     (rewardAreaRewardUiPrefabRectTransform.sizeDelta.x +
                                      horizontalLayoutGroup.spacing));

            currentHorizontalValue = scrollRect.horizontalScrollbar.value;
        }

        public void TryFocusIndex(int totalRewardUiCount, int focusRewardUiIndex)
        {
            currentHorizontalValue = focusRewardUiIndex / (float)totalRewardUiCount;

            // Avoid floating point
            if (Math.Abs(currentHorizontalValue - lastHorizontalValue) > 0.01f)
            {
                DOTween.To(() => scrollRect.horizontalScrollbar.value,
                        x => { scrollRect.horizontalScrollbar.value = x; },
                        currentHorizontalValue, viewScrollAnimation.duration).SetEase(viewScrollAnimation.curve)
                    .SetLink(gameObject);
                lastHorizontalValue = currentHorizontalValue;
            }
        }

        public void IncreaseLayoutWidth()
        {
            var increaseAmount = rewardAreaRewardUiPrefabRectTransform.sizeDelta.x + horizontalLayoutGroup.spacing;
            var size = GetLayoutGroupContentSize();
            size.x += increaseAmount;
            SetLayoutGroupContentSize(size);
            fitRewardUiCount++;
        }

        public bool IsNecessaryToIncreaseLayout(int uniqRewardCount)
        {
            if (uniqRewardCount >= fitRewardUiCount)
                return true;
            return false;
        }

        public void ResetSize()
        {
            SetLayoutGroupContentSize(layoutGroupContentStartSize);
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