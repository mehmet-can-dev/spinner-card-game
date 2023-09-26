using SpinnerGame.RewardArea;
using SpinnerGame.Spinner;
using SpinnerGame.TierArea;
using UnityEngine;
using UnityEngine.Serialization;

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

        [SerializeField] private SpinnerSeedSettingsSO spinnerSeedSettingsSo;

        private void Start()
        {
            SpinnerLogic.SetSeed(spinnerSeedSettingsSo);

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
            switch (itemData)
            {
                case RewardItemData rwData:
                    spinnerGameModuleNavigator.NavigateRewards(rewardAreaBase, rwData, spinnerContentUi, TierUpGame);
                    break;
                case BombItemData:
                    ResetGame();
                    break;
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