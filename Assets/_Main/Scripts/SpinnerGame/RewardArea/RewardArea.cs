using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RewardArea : MonoBehaviour
{
    [Header("Module References")] [SerializeField]
    private RewardAreaModuleSpawner rewardAreaModuleSpawner;

    [Header("Child References")] [SerializeField]
    private RewardCollectButton rewardCollectButton;

    private Dictionary<string, int> rewardContainer;

    private Action onRewardsCollected;

    public void Init(Action onRewardsCollected)
    {
        this.onRewardsCollected = onRewardsCollected;
        rewardCollectButton.Init(CollectRewards);
        CloseRewardButton(false);
    }

    private void CollectRewards()
    {
        CloseRewardButton(true);
        ClaimRewardArea();
        onRewardsCollected?.Invoke();
    }

    public void OpenRewardButton(bool useAnim)
    {
        rewardCollectButton.SetActive(true, useAnim);
    }

    public void CloseRewardButton(bool useAnim)
    {
        rewardCollectButton.SetActive(false, useAnim);
    }

    public Vector3 GetRewardUiPosition(string id)
    {
        return rewardAreaModuleSpawner.GetRewardUiPosition(id);
    }

    public bool IsRewardedCreated(string id)
    {
        if (rewardContainer == null)
            return false;
        return rewardContainer.ContainsKey(id);
    }

    public void UpdateItem(string id, int amount)
    {
        if (rewardContainer.ContainsKey(id))
        {
            rewardContainer[id] += amount;
            rewardAreaModuleSpawner.UpdateContent(id, rewardContainer[id]);
        }
        else
        {
            Debug.LogError("Rewarded Item Data Not Created");
        }
    }

    public void AddItem(RewardItemData rewardItemData)
    {
        if (rewardContainer == null)
            rewardContainer = new Dictionary<string, int>();

        if (!rewardContainer.ContainsKey(rewardItemData.itemId))
        {
            rewardContainer.Add(rewardItemData.itemId, 0);
            rewardAreaModuleSpawner.SpawnContent(rewardItemData);
        }
        else
        {
            Debug.LogError("Rewarded Item Data Already Created");
        }
    }

    [ContextMenu("Clear")]
    public void ClearRewardArea()
    {
        rewardContainer?.Clear();
        rewardAreaModuleSpawner.DestroyCreatedRewardUIs();
    }

    private void ClaimRewardArea()
    {
        //Todo write claim functions temporary reset Reward Area
        ClearRewardArea();
    }
}