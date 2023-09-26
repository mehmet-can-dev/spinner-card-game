using System.Text;

namespace SpinnerGame
{
    public class RewardItemData : ItemData
    {
        public int rewardAmount;

        public override StringBuilder ToStringBuilder()
        {
            var sb = base.ToStringBuilder();

            sb.Append("rewardAmount :");
            sb.Append(rewardAmount);

            return sb;
        }
    }
}