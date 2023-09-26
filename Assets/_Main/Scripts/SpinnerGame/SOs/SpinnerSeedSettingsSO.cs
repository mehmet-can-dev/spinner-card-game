using UnityEngine;

namespace SpinnerGame
{
    [CreateAssetMenu(fileName = "SpinnerSeedSettingsSO", menuName = "Spinner/SpinnerSeedSettings", order = 0)]
    public class SpinnerSeedSettingsSO : ScriptableObject
    {
        public bool useSeed = false;
        public string seed;
    }
}