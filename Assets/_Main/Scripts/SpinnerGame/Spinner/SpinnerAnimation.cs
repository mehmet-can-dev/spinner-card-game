using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class SpinnerAnimation : MonoBehaviour
{
    [SerializeField] private SpinnerAnimationData spinnerAnimationData;

    public void Init()
    {
    }

    public void StartAnimation(int targetHole, Action onComplete)
    {
        StartCoroutine(SpinnerRotationCor(targetHole, onComplete));
    }

    private IEnumerator SpinnerRotationCor(int targetHole, Action onComplete)
    {
        float targetAngle = SpinnerUtilities.PERCOUNTANGLE * targetHole;
        float targetRotateAngle = SpinnerUtilities.TWOPIRAD * spinnerAnimationData.spinCount + targetAngle;


        float currentAngle = Quaternion.Angle(Quaternion.identity, transform.rotation);
        
        //todo to avoid quaternion complexity
        if (transform.eulerAngles.z < 0)
        {
            currentAngle += 180;
        }
        
        yield return RotateSpinner(targetRotateAngle + currentAngle, spinnerAnimationData.animationCurve,
            spinnerAnimationData.startSpeed, 100);
        yield return RotateSpinner(spinnerAnimationData.missAngle, spinnerAnimationData.animationCurve, 100,
            10);
        yield return RotateSpinner(-spinnerAnimationData.missAngle, spinnerAnimationData.animationCurve, 50,
            500);

        transform.rotation = Quaternion.AngleAxis(targetRotateAngle, Vector3.forward);
        yield return null;
        onComplete?.Invoke();
    }

    private IEnumerator RotateSpinner(float rotateAmount, AnimationCurve easeCurve, float startSpeed,
        float finishSpeed)
    {
        float currentRotateAngle = 0;
        float currentSpeed = startSpeed;
        Transform t = transform;
        Vector3 axis = Vector3.forward * Mathf.Sign(rotateAmount);

        while (currentRotateAngle <= Mathf.Abs(rotateAmount))
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