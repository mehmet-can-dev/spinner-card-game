using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spinner : MonoBehaviour
{
    [SerializeField] private SpinnerAnimation _spinnerAnimation;
    [SerializeField] private SpinnerSpawner _spinnerSpawner;
    [SerializeField] private SpinnerInput _spinnerInput;

    [SerializeField] private int targetHole = 0;

    private void Start()
    {
        _spinnerAnimation.Init();
        _spinnerSpawner.Init();
        _spinnerInput.Init(OnSpinnerClicked);
        _spinnerInput.SetActive(true);
    }

    public void SpawnSpinner(int tier)
    {
        
    }

    private void OnSpinnerClicked()
    {
        _spinnerInput.SetActive(false);
        _spinnerAnimation.StartAnimation(targetHole, () => _spinnerInput.SetActive(true));
    }
}