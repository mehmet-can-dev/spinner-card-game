using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpinnerGame
{
    [CreateAssetMenu(fileName = "SpinnerTypeSO", menuName = "Spinner/SpinnerTypeSO", order = 0)]
    public class SpinnerTypeSO : ScriptableObject
    {
        public string id;
        public Sprite spinnerSprite;
        public Sprite indicatorSprite;
        public Color spinnerMainColor;

        public List<SpinnerContentItemSO> definitelyContents;
        public List<SpinnerContentItemSO> possibilityContents;
        public List<SpinnerContentBombSO> bombContents;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (definitelyContents.Count + bombContents.Count > SpinnerLogic.HOLECOUNT)
            {
                EditorUtility.DisplayDialog("Count Error",
                    "definitelyContents.Count + bombContents.Count must be lower " + SpinnerLogic.HOLECOUNT, "ok");

                return;
            }

            if (possibilityContents.Count + bombContents.Count + possibilityContents.Count < SpinnerLogic.HOLECOUNT)
            {
                EditorUtility.DisplayDialog("Count Error",
                    "possibilityContents.Count + bombContents.Count + possibilityContents.Count must be higher " +
                    SpinnerLogic.HOLECOUNT, "ok");

                return;
            }
        }
#endif
    }
}