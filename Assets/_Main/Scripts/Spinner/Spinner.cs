using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spinner : MonoBehaviour
{
    [SerializeField] private SpinnerAnimation _spinnerAnimation;
    [SerializeField] private SpinnerSpawner _spinnerSpawner;
    
    public const int HOLECOUNT = 8;
    public const float TWOPIRAD = 360;
    public const float PERCOUNTANGLE = TWOPIRAD / HOLECOUNT;

    [SerializeField] private int targetHole = 0;

    private void Start()
    {
        _spinnerAnimation.Init();
        _spinnerSpawner.Init();
    }
}