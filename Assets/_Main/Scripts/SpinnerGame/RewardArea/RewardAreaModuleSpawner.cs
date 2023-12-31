﻿using System.Collections.Generic;
using UnityEngine;

namespace SpinnerGame.RewardArea
{

    public class RewardAreaModuleSpawner : MonoBehaviour
    {
        private Dictionary<string, RewardAreaRewardUi> createdRewardUiContainer;
        
        [Header("Project References")] [SerializeField]
        private RewardAreaRewardUi rewardAreaRewardUiPrefab;

        [Header("Child References")] [SerializeField]
        private RectTransform contentParent;
        
        public void SpawnContent(RewardItemData rewardItemData)
        {
            if (createdRewardUiContainer == null)
                createdRewardUiContainer = new Dictionary<string, RewardAreaRewardUi>();

            var content = Instantiate(rewardAreaRewardUiPrefab, contentParent);

            content.Init(rewardItemData.itemId, rewardItemData.itemSprite);
            createdRewardUiContainer.Add(rewardItemData.itemId, content);
        }

        public void UpdateContent(string id, int amount)
        {
            if (!createdRewardUiContainer.ContainsKey(id))
                Debug.LogError("Reward Container Init Error");
            
            var content = createdRewardUiContainer[id];
            content.SetText(amount);
        }

        public Transform GetRewardUiTransform(string id)
        {
            return createdRewardUiContainer[id].transform;
        }

        public void DestroyCreatedRewardUIs()
        {
            if (createdRewardUiContainer == null)
                return;

            foreach (var rewardUIs in createdRewardUiContainer.Values)
            {
                Destroy(rewardUIs.gameObject);
            }

            createdRewardUiContainer.Clear();
        }
    }

}