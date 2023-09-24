
    using System.Text;
    using UnityEngine;

    public class ItemData
    {
        public Sprite itemSprite;
        public string itemId;

        public virtual StringBuilder ToStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("itemID :");
            sb.AppendLine(itemId);
            sb.Append(" itemSprite :");
            sb.AppendLine(itemSprite.name);
            return sb;
        }
    }
