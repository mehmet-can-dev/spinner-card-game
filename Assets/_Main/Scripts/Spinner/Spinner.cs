using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spinner : MonoBehaviour
{
    [SerializeField] private SpinnerAnimation _spinnerAnimation;
    [SerializeField] private SpinnerSpawner _spinnerSpawner;
    [SerializeField] private SpinnerInput _spinnerInput;


    private int currentTier = 0;

    private void Start()
    {
        _spinnerAnimation.Init();
        _spinnerSpawner.Init();
        _spinnerInput.Init(OnSpinnerClicked);
        _spinnerInput.SetActive(true);
        SpawnSpinner(currentTier);
    }

    public void SpawnSpinner(int tier)
    {
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
        _spinnerInput.SetActive(true);
        currentTier++;
        SpawnSpinner(currentTier);
    }
}