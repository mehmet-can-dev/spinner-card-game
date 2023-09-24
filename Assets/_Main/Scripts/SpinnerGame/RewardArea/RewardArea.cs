using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardArea : MonoBehaviour
{
    [SerializeField] private RewardAreaSpawner rewardAreaSpawner;

    private Dictionary<string, int> rewardContainer;

    public void Init()
    {
    }

    public void AdjustItem(RewardItemData rewardItemData)
    {
        if (rewardContainer == null)
            rewardContainer = new Dictionary<string, int>();

        if (rewardContainer.ContainsKey(rewardItemData.itemId))
        {
            rewardContainer[rewardItemData.itemId] += rewardItemData.rewardAmount;
            rewardItemData.rewardAmount = rewardContainer[rewardItemData.itemId];
            rewardAreaSpawner.UpdateContent(rewardItemData);
        }
        else
        {
            rewardContainer.Add(rewardItemData.itemId, rewardItemData.rewardAmount);
            rewardAreaSpawner.SpawnContent(rewardItemData);
        }
    }
}