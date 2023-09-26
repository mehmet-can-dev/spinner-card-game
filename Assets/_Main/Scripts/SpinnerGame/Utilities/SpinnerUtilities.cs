using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SpinnerGame
{
    public static class SpinnerUtilities
    {
        public static void LogContentList(List<SpinnerContentSO> spinnerContents)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < spinnerContents.Count; i++)
            {
                sb.AppendLine(spinnerContents[i].contentId);
            }

            Debug.Log(sb);
        }

        //todo Check int max
        public static int CalculateItemGainAmount(SpinnerContentItemSO spinnerContentItemSo, int tier)
        {
            var tierList = spinnerContentItemSo.tierGainList;
            if (tier < tierList.Count)
            {
                return tierList[tier];
            }
            else
            {
                var lastGainAmount = tierList[^1];
                return Mathf.RoundToInt(lastGainAmount * spinnerContentItemSo.increaseRatioAfterListEnded *
                                        (tier - tierList.Count + 1));
            }
        }
    }
}