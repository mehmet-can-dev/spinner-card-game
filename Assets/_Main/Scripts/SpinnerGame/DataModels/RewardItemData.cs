using System.Text;
using UnityEngine;

public class RewardItemData : ItemData
{
    public int rewardAmount;

    public override StringBuilder ToStringBuilder()
    {
        var sb = base.ToStringBuilder();

        sb.AppendLine("rewardAmount :");
        sb.Append(rewardAmount);

        return sb;
    }
}