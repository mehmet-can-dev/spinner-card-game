
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
            sb.Append(itemId);
            sb.AppendLine("itemSprite :");
            sb.Append(itemSprite.name);
            return sb;
        }
    }
