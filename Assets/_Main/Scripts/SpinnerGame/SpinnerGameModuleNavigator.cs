using System;
using System.Collections;
using CurrencyParticle;
using SpinnerGame.RewardArea;
using SpinnerGame.Spinner;
using UnityEngine;

namespace SpinnerGame
{
    public class SpinnerGameModuleNavigator : MonoBehaviour
    {
        public void Init()
        {
        }

        public void NavigateRewards(RewardAreaBase rewardAreaBase, RewardItemData rwData,
            SpinnerContentUi spinnerContentUi,
            Action onComplete)
        {
            if (!rewardAreaBase.IsRewardedCreated(rwData.itemId))
                rewardAreaBase.AddItem(rwData);
            rewardAreaBase.UpdateView(rwData.itemId);
            StartRewardedNavigation(rewardAreaBase, spinnerContentUi, rwData, onComplete);
        }

        private void StartRewardedNavigation(RewardAreaBase rewardAreaBase, SpinnerContentUi spinnerContentUi,
            RewardItemData rwData, Action onComplete)
        {
            var targetTransform = rewardAreaBase.GetRewardUiTransform(rwData.itemId);

            CurrencyCreateData currencyCreateData = new CurrencyCreateData()
            {
                spawnPos = spinnerContentUi.transform.position,
                spawnCount = Mathf.Min(rwData.rewardAmount, CurrencyParticleController.MAXCOUNT),
                targetTransform = targetTransform,
                sprite = rwData.itemSprite
            };

            void PerItemCollected()
            {
                var perAmount = rwData.rewardAmount / (float)currencyCreateData.spawnCount;
                rewardAreaBase.UpdateItem(rwData.itemId, (int)perAmount);
            }

            void LastItemCollected()
            {
                var remainingAmount = rwData.rewardAmount % currencyCreateData.spawnCount;
                if (remainingAmount != 0)
                {
                    rewardAreaBase.UpdateItem(rwData.itemId, remainingAmount);
                }

                onComplete?.Invoke();
            }

            CurrencyParticleController.Instance.Create(currencyCreateData, PerItemCollected, LastItemCollected);
        }
    }
}