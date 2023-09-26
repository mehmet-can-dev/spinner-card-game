using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerGameModuleNavigator : MonoBehaviour
{
    public void Init()
    {
    }

    public void NavigateRewards(RewardArea rewardArea, RewardItemData rwData, SpinnerContentUi spinnerContentUi,
        Action onComplete)
    {
        if (!rewardArea.IsRewardedCreated(rwData.itemId))
            rewardArea.AddItem(rwData);
        StartCoroutine(StartRewardedAnimation(rewardArea, spinnerContentUi, rwData, onComplete));
    }

    private IEnumerator StartRewardedAnimation(RewardArea rewardArea, SpinnerContentUi spinnerContentUi,
        RewardItemData rwData, Action onComplete)
    {
        //Todo avoid horizontal group position latency
        yield return new WaitForEndOfFrame();
        var targetPos = rewardArea.GetRewardUiPosition(rwData.itemId);

        CurrencyCreateData currencyCreateData = new CurrencyCreateData()
        {
            spawnPos = spinnerContentUi.transform.position,
            spawnCount = Mathf.Min(rwData.rewardAmount, CurrencyParticleController.MAXCOUNT),
            targetPos = targetPos,
            sprite = rwData.itemSprite
        };

        void PerItemCollected()
        {
            var perAmount = rwData.rewardAmount / (float)currencyCreateData.spawnCount;
            rewardArea.UpdateItem(rwData.itemId, (int)perAmount);
        }

        void LastItemCollected()
        {
            var remainingAmount = rwData.rewardAmount % currencyCreateData.spawnCount;
            if (remainingAmount != 0)
            {
                rewardArea.UpdateItem(rwData.itemId, remainingAmount);
            }

            onComplete?.Invoke();
        }

        CurrencyParticleController.Instance.Create(currencyCreateData, PerItemCollected, LastItemCollected);
    }
}