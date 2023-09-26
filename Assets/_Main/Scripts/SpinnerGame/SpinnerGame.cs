using UnityEngine;

public class SpinnerGame : MonoBehaviour
{
    [Header("Module References")] [SerializeField]
    private SpinnerGameModuleNavigator spinnerGameModuleNavigator;

    [Header("Child References")] [SerializeField]
    private Spinner spinner;

    [SerializeField] private RewardArea rewardArea;
    [SerializeField] private TierArea tierArea;

    [Header("Project References")] [SerializeField]
    private SpinnerSettingsSO spinnerSettingsSo;

    private void Start()
    {
        spinnerGameModuleNavigator.Init();
        spinner.Init();
        rewardArea.Init(OnCollectRewards);
        tierArea.Init(spinnerSettingsSo);
        SpawnNewSpinner();
    }

    private void OnSpinStarted()
    {
        rewardArea.CloseRewardButton(true);
    }

    private void OnSpinEnded(ItemData itemData, SpinnerContentUi spinnerContentUi)
    {
        if (itemData is RewardItemData rwData)
        {
            spinnerGameModuleNavigator.NavigateRewards(rewardArea, rwData, spinnerContentUi, TierUpGame);
        }
        else if (itemData is BombItemData)
        {
            ResetGame();
        }
    }

    private void TierUpGame()
    {
        rewardArea.OpenRewardButton(true);
        tierArea.IncreaseTier();
        SpawnNewSpinner();
    }

    private void ResetGame()
    {
        tierArea.ResetTier();
        rewardArea.ClearRewardArea();
        SpawnNewSpinner();
    }

    private void SpawnNewSpinner()
    {
        spinner.SpawnSpinner(tierArea.CurrentTier, spinnerSettingsSo, OnSpinStarted, OnSpinEnded);
    }

    private void OnCollectRewards()
    {
        ResetGame();
    }
}