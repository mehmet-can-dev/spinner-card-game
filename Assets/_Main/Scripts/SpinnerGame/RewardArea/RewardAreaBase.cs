using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpinnerGame.RewardArea
{
    public class RewardAreaBase : MonoBehaviour
    {
        private Dictionary<string, int> rewardContainer;
        private Action onRewardsCollected;
        
        [Header("Module References")] [SerializeField]
        private RewardAreaModuleSpawner rewardAreaModuleSpawner;
        
        [Header("Child References")] [SerializeField]
        private RewardAreaScrollView rewardAreaScrollView;
        [SerializeField] private RewardCollectButton rewardCollectButton;
        
        public void Init(Action onRewardsCollected)
        {
            this.onRewardsCollected = onRewardsCollected;
            rewardCollectButton.Init(CollectRewards);
            rewardAreaScrollView.Init();
            CloseRewardButton(false);
        }

        public void UpdateView(string id)
        {
            if (rewardAreaScrollView.IsNecessaryToIncreaseLayout(rewardContainer.Keys.Count))
            {
                rewardAreaScrollView.IncreaseLayoutWidth();
            }

            rewardAreaScrollView.TryFocusIndex(rewardContainer.Keys.Count,
                rewardContainer.Keys.ToList().IndexOf(id));
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
        
        public void ClearRewardArea()
        {
            rewardContainer?.Clear();
            rewardAreaModuleSpawner.DestroyCreatedRewardUIs();
            rewardAreaScrollView.ResetSize();
        }
        
        public bool IsRewardedCreated(string id)
        {
            if (rewardContainer == null)
                return false;
            return rewardContainer.ContainsKey(id);
        }
        
        public void OpenRewardButton(bool useAnim)
        {
            rewardCollectButton.SetActive(true, useAnim);
        }

        public void CloseRewardButton(bool useAnim)
        {
            rewardCollectButton.SetActive(false, useAnim);
        }

        public Transform GetRewardUiTransform(string id)
        {
            return rewardAreaModuleSpawner.GetRewardUiTransform(id);
        }

        private void CollectRewards()
        {
            CloseRewardButton(true);
            ClaimRewardArea();
            onRewardsCollected?.Invoke();
        }
        
        private void ClaimRewardArea()
        {
            //Todo write claim functions temporary reset Reward Area
            ClearRewardArea();
        }
    }
}