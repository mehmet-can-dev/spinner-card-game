#if UNITY_EDITOR


using UnityEditor;

namespace SpinnerGame.Spinner.Editor
{
    public static class SpinnerMenuItemEditor
    {
        [MenuItem("SpinnerGame/MonoSo/SpinnerSettings", false, 1)]
        public static void SpinnerSettings()
        {
            var settings = SpinnerEditorUtilities.LoadSpinnerSettings();
            if (settings != null)
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = settings;
            }
        }

        [MenuItem("SpinnerGame/MonoSo/SeedSettings", false, 2)]
        public static void SeedSettings()
        {
            var settings = SpinnerEditorUtilities.LoadSeedSettings();
            if (settings != null)
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = settings;
            }
        }

        [MenuItem("SpinnerGame/MonoSo/LoadBombSettings", false, 3)]
        public static void BombSettings()
        {
            var settings = SpinnerEditorUtilities.LoadBombSettings();
            if (settings != null)
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = settings;
            }
        }
    }
}
#endif