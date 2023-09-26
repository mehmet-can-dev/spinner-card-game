using System.Collections.Generic;
using SpinnerGame.Spinner;
using UnityEngine;

namespace SpinnerGame
{
    public static class SpinnerLogic
    {
        public const int HOLECOUNT = 8;
        public const float TWOPIRAD = 360;
        public const float PERCOUNTANGLE = TWOPIRAD / HOLECOUNT;

        public static void SetSeed(SpinnerSeedSettingsSO spinnerSeedSettingsSo)
        {
            if (spinnerSeedSettingsSo.useSeed)
                Random.InitState(spinnerSeedSettingsSo.seed);
            else
                Random.InitState((int)System.DateTime.Now.Ticks);
        }
        
        public static int SelectTargetIndexLogic()
        {
            return Random.Range(0, HOLECOUNT);
        }

        public static List<SpinnerContentSO> SelectContentsLogic(SpinnerTypeSO typeSo)
        {
            var contents = new List<SpinnerContentSO>();

            for (int i = 0; i < typeSo.definitelyContents.Count; i++)
            {
                contents.Add(typeSo.definitelyContents[i]);
            }

            for (int i = 0; i < typeSo.bombContents.Count; i++)
            {
                contents.Add(typeSo.bombContents[i]);
            }

            var tempList = new List<SpinnerContentItemSO>(typeSo.possibilityContents);
            var bag = new MarbleBag<SpinnerContentItemSO>(tempList);

            var remainingCount = HOLECOUNT - contents.Count;

            for (int i = 0; i < remainingCount; i++)
            {
                var c = bag.PickRandom();
                contents.Add(c);
            }

            return contents;
        }

        public static void ShuffleLogic(List<SpinnerContentSO> contents)
        {
           contents.Shuffle();
        }
    }
}