using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spinner : MonoBehaviour
{
    [SerializeField] private SpinnerAnimation _spinnerAnimation;
    [SerializeField] private SpinnerSpawner _spinnerSpawner;
    [SerializeField] private SpinnerInput _spinnerInput;

    public const int HOLECOUNT = 8;
    public const float TWOPIRAD = 360;
    public const float PERCOUNTANGLE = TWOPIRAD / HOLECOUNT;

    [SerializeField] private int targetHole = 0;

    private void Start()
    {
        _spinnerAnimation.Init();
        _spinnerSpawner.Init();
        _spinnerInput.Init(OnSpinnerClicked);
        _spinnerInput.SetActive(true);
    }

    private void OnSpinnerClicked()
    {
        _spinnerInput.SetActive(false);
        _spinnerAnimation.StartAnimation(() => _spinnerInput.SetActive(true));
    }
}