using UnityEngine;

[CreateAssetMenu(fileName = "TierAreaSlidingAnimationSettingsSO",
    menuName = "AnimationSettings/TierAreaSlidingAnimationSettings", order = 0)]
public class TierAreaSlidingAnimationSettingsSO : ScriptableObject
{
    public float duration = 1;
    public AnimationCurve curve;
}