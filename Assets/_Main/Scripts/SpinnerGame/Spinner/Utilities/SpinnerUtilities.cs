using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class SpinnerUtilities
{
    public const int HOLECOUNT = 8;
    public const float TWOPIRAD = 360;
    public const float PERCOUNTANGLE = TWOPIRAD / HOLECOUNT;

    public static StringBuilder LogContentList(List<SpinnerContentSO> spinnerContents)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < spinnerContents.Count; i++)
        {
            sb.AppendLine(spinnerContents[i].contentId);
        }

        return sb;
    }

    public static int SelectTargetIndex()
    {
        return Random.Range(0, HOLECOUNT);
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