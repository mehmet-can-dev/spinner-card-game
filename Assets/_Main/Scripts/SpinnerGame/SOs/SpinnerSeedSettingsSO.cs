using UnityEngine;

namespace SpinnerGame
{
    //ToDo can be connect singleton so logic
    [CreateAssetMenu(fileName = "SpinnerSeedSettingsSO", menuName = "Spinner/SpinnerSeedSettings", order = 0)]
    public class SpinnerSeedSettingsSO : ScriptableObject
    {
        public bool useSeed = false;
        public int seed;
    }
}