using UnityEngine;

[CreateAssetMenu(fileName = "AnimationSettingsSO", menuName = "AnimationSettings/ContentUiAnimationSettings",
    order = 0)]
public class ContentUiAnimationSettingsSO : ScriptableObject
{
    public AnimationCurve animationCurve;
    public float animationDuration;
}