using System;
using System.Collections;
using UnityEngine;

public class SpinnerGame : MonoBehaviour
{
    [SerializeField] private SpinnerGameNavigator spinnerGameNavigator;
    [SerializeField] private Spinner spinner;
    [SerializeField] private RewardArea rewardArea;
    [SerializeField] private TierArea tierArea;


    private void Start()
    {
        spinner.Init();
        rewardArea.Init();
        spinnerGameNavigator.Init();
        tierArea.Init();

        spinner.SpawnSpinner(tierArea.CurrentTier, OnSpinEnded);
    }

    private void OnSpinEnded(ItemData obj, SpinnerContentUi spinnerContentUi)
    {
        if (obj is RewardItemData rwData)
        {
            spinnerGameNavigator.NavigateRewards(rewardArea, rwData, spinnerContentUi, SpawnTest);
            tierArea.IncreaseTier();
        }
        else if (obj is BombItemData)
        {
            tierArea.ResetTier();
            SpawnTest();
        }
    }


    [ContextMenu("TestSpin")]
    private void SpawnTest()
    {
        spinner.SpawnSpinner(tierArea.CurrentTier, OnSpinEnded);
    }
}