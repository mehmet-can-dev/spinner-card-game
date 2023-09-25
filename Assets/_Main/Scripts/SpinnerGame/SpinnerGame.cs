using System;
using System.Collections;
using UnityEngine;

public class SpinnerGame : MonoBehaviour
{
    [SerializeField] private SpinnerGameNavigator spinnerGameNavigator;
    [SerializeField] private Spinner spinner;
    [SerializeField] private RewardArea rewardArea;

    private int spinnerTier = 0;

    private void Start()
    {
        spinner.Init();
        rewardArea.Init();
        spinnerGameNavigator.Init();

        spinner.SpawnSpinner(spinnerTier, OnSpinEnded);
    }

    private void OnSpinEnded(ItemData obj, SpinnerContentUi spinnerContentUi)
    {
        if (obj is RewardItemData rwData)
        {
            spinnerGameNavigator.NavigateRewards(rewardArea,rwData, spinnerContentUi,SpawnTest);
            spinnerTier++;
        }
        else if (obj is BombItemData)
        {
            spinnerTier = 0;
            SpawnTest();
        }
    }


    [ContextMenu("TestSpin")]
    private void SpawnTest()
    {
        spinner.SpawnSpinner(spinnerTier, OnSpinEnded);
    }
}