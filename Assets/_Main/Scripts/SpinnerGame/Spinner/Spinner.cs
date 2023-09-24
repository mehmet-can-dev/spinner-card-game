using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spinner : MonoBehaviour
{
    [SerializeField] private SpinnerAnimation _spinnerAnimation;
    [SerializeField] private SpinnerSpawner _spinnerSpawner;
    [SerializeField] private SpinnerInput _spinnerInput;

    private Action<ItemData,SpinnerContentUi> onSpinEnded;

    public void Init()
    {
        _spinnerAnimation.Init();
        _spinnerSpawner.Init();
        _spinnerInput.Init(OnSpinnerClicked);
    }

    public void SpawnSpinner(int tier, Action<ItemData,SpinnerContentUi> onSpinEnded)
    {
        this.onSpinEnded = onSpinEnded;
        _spinnerInput.SetActive(true);
        _spinnerSpawner.CreateTier(tier);
    }

    private void OnSpinnerClicked()
    {
        _spinnerInput.SetActive(false);
        var targetHole = SpinnerUtilities.SelectTargetIndex();
        _spinnerAnimation.StartAnimation(targetHole, () => OnSpinCompleted(targetHole));
    }

    private void OnSpinCompleted(int targetHole)
    {
        var item = _spinnerSpawner.GetRewardDataFromIndex(targetHole);
        var contentUi = _spinnerSpawner.GetContentUiFromIndex(targetHole);
        onSpinEnded?.Invoke(item,contentUi);
    }
}