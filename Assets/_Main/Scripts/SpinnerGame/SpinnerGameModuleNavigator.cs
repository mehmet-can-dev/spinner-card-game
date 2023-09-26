using System;
using System.Collections;
using UnityEngine;

public class SpinnerGameModuleNavigator : MonoBehaviour
{
    public void Init()
    {
    }

    public void NavigateRewards(RewardAreaBase rewardAreaBase, RewardItemData rwData, SpinnerContentUi spinnerContentUi,
        Action onComplete)
    {
        if (!rewardAreaBase.IsRewardedCreated(rwData.itemId))
            rewardAreaBase.AddItem(rwData);
        StartCoroutine(StartRewardedAnimation(rewardAreaBase, spinnerContentUi, rwData, onComplete));
    }

    private IEnumerator StartRewardedAnimation(RewardAreaBase rewardAreaBase, SpinnerContentUi spinnerContentUi,
        RewardItemData rwData, Action onComplete)
    {
        //Todo avoid horizontal group position latency
        yield return new WaitForEndOfFrame();
        var targetPos = rewardAreaBase.GetRewardUiPosition(rwData.itemId);

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