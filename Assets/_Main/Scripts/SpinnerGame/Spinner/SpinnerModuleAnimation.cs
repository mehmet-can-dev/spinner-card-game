using System;
using System.Collections;
using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.Serialization;


public class SpinnerModuleAnimation : MonoBehaviour
{
    [Header("Project References")] [SerializeField]
    private SpinnerSpinAnimationSettings spinnerSpinAnimationSettings;

    [Header("Child References")] [SerializeField]
    private Transform rotateTarget;

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
        float targetAngle = SpinnerLogic.PERCOUNTANGLE * targetHole;
        float targetRotateAngle = SpinnerLogic.TWOPIRAD * spinnerSpinAnimationSettings.spinCount + targetAngle;

        yield return RotateSpinner(targetRotateAngle, spinnerSpinAnimationSettings.animationCurve,
            spinnerSpinAnimationSettings.startSpeed, spinnerSpinAnimationSettings.finishSpeed);
        
        yield return RotateSpinner(spinnerSpinAnimationSettings.missAngle, spinnerSpinAnimationSettings.animationCurve,
            spinnerSpinAnimationSettings.finishSpeed,
            spinnerSpinAnimationSettings.missingRecoverySpeed);
        
        yield return RotateSpinner(-spinnerSpinAnimationSettings.missAngle, spinnerSpinAnimationSettings.animationCurve,
            spinnerSpinAnimationSettings.missingRecoverySpeed,
            spinnerSpinAnimationSettings.startSpeed);

        rotateTarget.rotation = Quaternion.AngleAxis(targetRotateAngle, Vector3.forward);
        yield return null;
        uiShiny.Play();
        onComplete?.Invoke();
    }

    private IEnumerator RotateSpinner(float rotateAmount, AnimationCurve easeCurve, float startSpeed,
        float finishSpeed)
    {
        float currentRotateAngle = 0;
        float currentSpeed = startSpeed;
        Transform t = rotateTarget;
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