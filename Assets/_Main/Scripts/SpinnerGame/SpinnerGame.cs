using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

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
        rewardArea.Init();
        tierArea.Init(spinnerSettingsSo);
        spinner.SpawnSpinner(tierArea.CurrentTier, spinnerSettingsSo, OnSpinEnded);
    }

    private void OnSpinEnded(ItemData itemData, SpinnerContentUi spinnerContentUi)
    {
        if (itemData is RewardItemData rwData)
        {
            spinnerGameModuleNavigator.NavigateRewards(rewardArea, rwData, spinnerContentUi, () =>
            {
                tierArea.IncreaseTier();
                SpawnNewSpinner();
            });
        }
        else if (itemData is BombItemData)
        {
            tierArea.ResetTier();
            rewardArea.ClearRewardArea();
            SpawnNewSpinner();
        }
    }

    [ContextMenu("TestSpin")]
    private void SpawnNewSpinner()
    {
        spinner.SpawnSpinner(tierArea.CurrentTier, spinnerSettingsSo, OnSpinEnded);
    }
}