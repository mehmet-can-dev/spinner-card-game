using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class RewardAreaSpawner : MonoBehaviour
{
    [SerializeField] private RewardAreaRewardUi rewardAreaRewardUiPrefab;
    [SerializeField] private RectTransform contentParent;

    private Dictionary<string, RewardAreaRewardUi> createdRewardUiContainer;

    public void SpawnContent(RewardItemData rewardItemData)
    {
        if (createdRewardUiContainer == null)
            createdRewardUiContainer = new Dictionary<string, RewardAreaRewardUi>();

        var content = Instantiate(rewardAreaRewardUiPrefab, contentParent);

        content.Init(rewardItemData.itemId, rewardItemData.itemSprite,
            rewardItemData.rewardAmount);
        createdRewardUiContainer.Add(rewardItemData.itemId, content);
    }


    public void UpdateContent(RewardItemData rewardItemData)
    {
        if (!createdRewardUiContainer.ContainsKey(rewardItemData.itemId))
            Debug.LogError("Reward Container Init Error");

        var content = createdRewardUiContainer[rewardItemData.itemId];
        content.SetText(rewardItemData.rewardAmount);
    }
}