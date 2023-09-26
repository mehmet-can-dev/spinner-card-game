using SpinnerGame.RewardArea;
using SpinnerGame.Spinner;
using SpinnerGame.TierArea;
using UnityEngine;

namespace SpinnerGame
{
    public class SpinnerGameBase : MonoBehaviour
    {
        [Header("Module References")] [SerializeField]
        private SpinnerGameModuleNavigator spinnerGameModuleNavigator;

        [Header("Child References")] [SerializeField]
        private SpinnerBase spinnerBase;

        [SerializeField] private RewardAreaBase rewardAreaBase;
        [SerializeField] private TierAreaBase tierAreaBase;

        [Header("Project References")] [SerializeField]
        private SpinnerSettingsSO spinnerSettingsSo;

        private void Start()
        {
            spinnerGameModuleNavigator.Init();
            spinnerBase.Init();
            rewardAreaBase.Init(OnCollectRewards);
            tierAreaBase.Init(spinnerSettingsSo);
            SpawnNewSpinner();
        }

        private void OnSpinStarted()
        {
            rewardAreaBase.CloseRewardButton(true);
        }

        private void OnSpinEnded(ItemData itemData, SpinnerContentUi spinnerContentUi)
        {
            if (itemData is RewardItemData rwData)
            {
                spinnerGameModuleNavigator.NavigateRewards(rewardAreaBase, rwData, spinnerContentUi, TierUpGame);
            }
            else if (itemData is BombItemData)
            {
                ResetGame();
            }
        }

        private void TierUpGame()
        {
            rewardAreaBase.OpenRewardButton(true);
            tierAreaBase.IncreaseTier();
            SpawnNewSpinner();
        }

        private void ResetGame()
        {
            tierAreaBase.ResetTier();
            rewardAreaBase.ClearRewardArea();
            SpawnNewSpinner();
        }

        private void SpawnNewSpinner()
        {
            spinnerBase.SpawnSpinner(tierAreaBase.CurrentTier, spinnerSettingsSo, OnSpinStarted, OnSpinEnded);
        }

        private void OnCollectRewards()
        {
            ResetGame();
        }
    }
}