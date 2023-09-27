using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace SpinnerGame.Test
{
    public static class SpinnerTestUtilities
    {
        public static SpinnerSettingsSO LoadSpinnerSettings()
        {
            string scriptableObjectName = "SpinnerSettings";
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpinnerSettingsSO)} {scriptableObjectName}");
            if (guids.Length == 0)
                Assert.Fail($"No {nameof(SpinnerSettingsSO)} found named {scriptableObjectName}");
            if (guids.Length > 1)
                Debug.LogWarning(
                    $"More than one {nameof(SpinnerSettingsSO)} found named {scriptableObjectName}, taking first one");

            return (SpinnerSettingsSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),
                typeof(SpinnerSettingsSO));
        }

        public static  SpinnerContentBombSO LoadBombSO()
        {
            string scriptableObjectName = "SpinnerContent_Bomb";
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpinnerContentBombSO)} {scriptableObjectName}");
            if (guids.Length == 0)
                Assert.Fail($"No {nameof(SpinnerContentBombSO)} found named {scriptableObjectName}");
            if (guids.Length > 1)
                Debug.LogWarning(
                    $"More than one {nameof(SpinnerContentBombSO)} found named {scriptableObjectName}, taking first one");

            return (SpinnerContentBombSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),
                typeof(SpinnerContentBombSO));
        }

        public static  SpinnerSeedSettingsSO LoadSeedSO()
        {
            string scriptableObjectName = "Spinner_SeedSettings";
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpinnerSeedSettingsSO)} {scriptableObjectName}");
            if (guids.Length == 0)
                Assert.Fail($"No {nameof(SpinnerSeedSettingsSO)} found named {scriptableObjectName}");
            if (guids.Length > 1)
                Debug.LogWarning(
                    $"More than one {nameof(SpinnerSeedSettingsSO)} found named {scriptableObjectName}, taking first one");

            return (SpinnerSeedSettingsSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),
                typeof(SpinnerSeedSettingsSO));
        }
    }
}