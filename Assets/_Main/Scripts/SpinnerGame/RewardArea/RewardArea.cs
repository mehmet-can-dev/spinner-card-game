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

    public Vector3 GetRewardUiPosition(string id)
    {
        return rewardAreaSpawner.GetRewardUiPosition(id);
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
            rewardAreaSpawner.UpdateContent(id, rewardContainer[id]);
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
            rewardAreaSpawner.SpawnContent(rewardItemData);
        }
        else
        {
            Debug.LogError("Rewarded Item Data Already Created");
        }
    }
}