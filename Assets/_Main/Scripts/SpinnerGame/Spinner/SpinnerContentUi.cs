using UnityEngine;

namespace SpinnerGame.Spinner
{
    public class SpinnerContentUi : ContentUiBase
    {
        private const int IMAGEMAXWIDTH = 70;
        private const int IMAGEMAXHEIGHT = 70;

        public new void Init(string id, Sprite sprite, int? amount = null)
        {
            this.id = id;

            SetSprite(sprite, IMAGEMAXHEIGHT, IMAGEMAXWIDTH);

            SetText(amount);
        }
    }
}