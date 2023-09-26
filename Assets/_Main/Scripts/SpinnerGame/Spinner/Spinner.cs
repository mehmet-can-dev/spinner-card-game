using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Spinner : MonoBehaviour
{
    [Header("Module References")] [SerializeField]
    private SpinnerModuleAnimation spinnerModuleAnimation;

    [SerializeField] private SpinnerModuleSpawner spinnerModuleSpawner;
    [SerializeField] private SpinnerModuleInput spinnerModuleInput;

    private Action<ItemData, SpinnerContentUi> onSpinEnded;

    public void Init()
    {
        spinnerModuleAnimation.Init();
        spinnerModuleSpawner.Init();
        spinnerModuleInput.Init(OnSpinnerClicked);
    }

    public void SpawnSpinner(int tier,SpinnerSettingsSO spinnerSettingsSo, Action<ItemData, SpinnerContentUi> onSpinEnded)
    {
        this.onSpinEnded = onSpinEnded;
        spinnerModuleInput.SetActive(true);
        spinnerModuleSpawner.CreateTier(tier, spinnerSettingsSo);
    }

    private void OnSpinnerClicked()
    {
        spinnerModuleInput.SetActive(false);
        var targetHole = SpinnerUtilities.SelectTargetIndex();
        spinnerModuleAnimation.StartAnimation(targetHole, () => OnSpinCompleted(targetHole));
    }

    private void OnSpinCompleted(int targetHole)
    {
        var item = spinnerModuleSpawner.GetRewardDataFromIndex(targetHole);
        var contentUi = spinnerModuleSpawner.GetContentUiFromIndex(targetHole);
        onSpinEnded?.Invoke(item, contentUi);
    }
}