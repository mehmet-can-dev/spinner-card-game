using System;
using System.Collections;
using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.Serialization;


public class SpinnerAnimation : MonoBehaviour
{
    [Header("Prefab References")] [SerializeField]
    private Transform rotationTarget;
    
    [Header("Project References")]
    [SerializeField] private SpinnerAnimationData spinnerAnimationData;
    [SerializeField] private UIShiny uiShiny;
    
    public void Init()
    {
    }

    public void StartAnimation(int targetHole, Action onComplete)
    {
        StartCoroutine(SpinnerRotationCor(targetHole, onComplete));
    }

    private IEnumerator SpinnerRotationCor(int targetHole, Action onComplete)
    {
        uiShiny.Stop();
        float targetAngle = SpinnerUtilities.PERCOUNTANGLE * targetHole;
        float targetRotateAngle = SpinnerUtilities.TWOPIRAD * spinnerAnimationData.spinCount + targetAngle;
        
        yield return RotateSpinner(targetRotateAngle, spinnerAnimationData.animationCurve,
            spinnerAnimationData.startSpeed, 100);
        yield return RotateSpinner(spinnerAnimationData.missAngle, spinnerAnimationData.animationCurve, 100,
            10);
        yield return RotateSpinner(-spinnerAnimationData.missAngle, spinnerAnimationData.animationCurve, 50,
            500);

        rotationTarget.rotation = Quaternion.AngleAxis(targetRotateAngle, Vector3.forward);
        yield return null;
        uiShiny.Play();
        onComplete?.Invoke();
    }

    private IEnumerator RotateSpinner(float rotateAmount, AnimationCurve easeCurve, float startSpeed,
        float finishSpeed)
    {
        float currentRotateAngle = 0;
        float currentSpeed = startSpeed;
        Transform t = rotationTarget;
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