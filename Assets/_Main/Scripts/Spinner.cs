using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpinnerAnimationSettings
{
    public int spinCount = 5;
    public float startSpeed = 500;
    public float missAngle = 10;
    public AnimationCurve animationCurve;
}

public class Spinner : MonoBehaviour
{
    public const int HOLECOUNT = 8;
    public const float TWOPIRAD = 360;
    public const float PERCOUNTANGLE = TWOPIRAD / HOLECOUNT;

    [SerializeField] private int targetHole = 0;

    [SerializeField] private SpinnerAnimationSettings _spinnerAnimationSettings;

    private void Start()
    {
        StartCoroutine(SpinnerRotationCor());
    }

    public IEnumerator SpinnerRotationCor()
    {
        float targetAngle = PERCOUNTANGLE * targetHole;
        float targetRotateAngle = TWOPIRAD * _spinnerAnimationSettings.spinCount + targetAngle;

        yield return RotateSpinner(targetRotateAngle, _spinnerAnimationSettings.animationCurve,
            _spinnerAnimationSettings.startSpeed, 100);
        yield return RotateSpinner(_spinnerAnimationSettings.missAngle, _spinnerAnimationSettings.animationCurve, 100,
            10);
        yield return RotateSpinner(-_spinnerAnimationSettings.missAngle, _spinnerAnimationSettings.animationCurve, 10,
            500);
        yield return null;
    }

    private IEnumerator RotateSpinner(float rotateAmount, AnimationCurve easeCurve, float startSpeed,
        float finishSpeed)
    {
        float currentRotateAngle = 0;
        float currentSpeed = startSpeed;
        Transform t = transform;
        Vector3 axis = Vector3.forward * Mathf.Sign(rotateAmount);

        while (currentRotateAngle < Mathf.Abs(rotateAmount))
        {
            currentSpeed = Mathf.Lerp(startSpeed, finishSpeed,
                easeCurve.Evaluate(currentRotateAngle / rotateAmount));

            float rotationDelta = Time.deltaTime * currentSpeed;

            currentRotateAngle += rotationDelta;
            t.rotation = Quaternion.AngleAxis(rotationDelta, axis) * t.rotation;
            yield return null;
        }
    }
}