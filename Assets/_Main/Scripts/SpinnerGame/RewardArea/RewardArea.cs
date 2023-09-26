using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RewardArea : MonoBehaviour
{
    [Header("Module References")] [SerializeField]
    private RewardAreaModuleSpawner rewardAreaModuleSpawner;

    private Dictionary<string, int> rewardContainer;

    public void Init()
    {
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
        rewardContainer.Clear();
        rewardAreaModuleSpawner.DestroyCreatedRewardUIs();
    }
}