using UnityEngine;

namespace _Main.Scripts.SpinnerGame.SOs
{
    [CreateAssetMenu(fileName = "AnimationSettingsSO", menuName = "AnimationSettings/ContentUiAnimationSettings", order = 0)]
    public class ContentUiAnimationSettingsSO : ScriptableObject
    {
        public AnimationCurve animationCurve;
        public float animationDuration;
    }
}