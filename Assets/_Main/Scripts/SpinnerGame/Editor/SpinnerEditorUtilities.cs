using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR


namespace SpinnerGame.Spinner.Editor
{
    public static class SpinnerEditorUtilities
    {
        // This methods can be generic
        public static SpinnerSettingsSO LoadSpinnerSettings()
        {
            string scriptableObjectName = "Mono_SpinnerSettings";
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpinnerSettingsSO)} {scriptableObjectName}");
            if (guids.Length == 0)
                Debug.LogError($"No {nameof(SpinnerSettingsSO)} found named {scriptableObjectName}");
            if (guids.Length > 1)
                Debug.LogWarning(
                    $"More than one {nameof(SpinnerSettingsSO)} found named {scriptableObjectName}, taking first one");

            return (SpinnerSettingsSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),
                typeof(SpinnerSettingsSO));
        }

        public static SpinnerContentBombSO LoadBombSettings()
        {
            string scriptableObjectName = "Mono_SpinnerContent_Bomb";
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpinnerContentBombSO)} {scriptableObjectName}");
            if (guids.Length == 0)
                Debug.LogError($"No {nameof(SpinnerContentBombSO)} found named {scriptableObjectName}");
            if (guids.Length > 1)
                Debug.LogWarning(
                    $"More than one {nameof(SpinnerContentBombSO)} found named {scriptableObjectName}, taking first one");

            return (SpinnerContentBombSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),
                typeof(SpinnerContentBombSO));
        }

        public static SpinnerSeedSettingsSO LoadSeedSettings()
        {
            string scriptableObjectName = "Mono_Spinner_SeedSettings";
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpinnerSeedSettingsSO)} {scriptableObjectName}");
            if (guids.Length == 0)
                Debug.LogError($"No {nameof(SpinnerSeedSettingsSO)} found named {scriptableObjectName}");
            if (guids.Length > 1)
                Debug.LogWarning(
                    $"More than one {nameof(SpinnerSeedSettingsSO)} found named {scriptableObjectName}, taking first one");

            return (SpinnerSeedSettingsSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),
                typeof(SpinnerSeedSettingsSO));
        }
    }
}

#endif