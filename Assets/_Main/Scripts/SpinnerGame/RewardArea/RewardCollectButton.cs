using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpinnerGame.RewardArea
{
    public class RewardCollectButton : MonoBehaviour, IPointerClickHandler
    {
        private Action onClicked;
        private bool isActive = true;

        [Header("Module References")] [SerializeField]
        private RewardCollectButtonModuleAnimation rewardCollectButtonModuleAnimation;

        public void Init(Action onClicked)
        {
            this.onClicked = onClicked;
            rewardCollectButtonModuleAnimation.Init();
        }

        public void SetActive(bool active, bool useAnim)
        {
            isActive = active;
            if (active)
            {
                rewardCollectButtonModuleAnimation.Open(useAnim);
            }
            else
            {
                rewardCollectButtonModuleAnimation.Close(useAnim);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isActive)
                onClicked?.Invoke();
        }
    }
}