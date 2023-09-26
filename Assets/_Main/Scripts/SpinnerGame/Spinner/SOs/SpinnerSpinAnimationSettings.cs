using UnityEngine;


[CreateAssetMenu(fileName = "SpinnerSpinAnimationSettingsSO",
    menuName = "AnimationSettings/SpinnerSpinAnimationSettings", order = 0)]
public class SpinnerSpinAnimationSettings : ScriptableObject
{
    public int spinCount = 3;
    public float startSpeed = 500;
    public float finishSpeed = 100;
    public float missAngle = 10;
    public float missingRecoverySpeed = 100;
    public AnimationCurve animationCurve;
}