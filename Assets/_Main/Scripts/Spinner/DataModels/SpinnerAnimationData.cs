using System;
using UnityEngine;

[Serializable]
public class SpinnerAnimationData
{
    public int spinCount = 3;
    public float startSpeed = 500;
    public float missAngle = 10;
    public AnimationCurve animationCurve;
}