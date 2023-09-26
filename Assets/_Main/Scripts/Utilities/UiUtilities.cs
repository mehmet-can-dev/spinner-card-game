using UnityEngine;

public static class UiUtilities
{
    public static void SetSizeDeltaFromImageSprite(RectTransform rect, Sprite sprite, int maxHeight, int maxWidth)
    {
        var widthRatio = sprite.rect.width / maxWidth;
        var heightRatio = sprite.rect.height / maxHeight;
        if (widthRatio > heightRatio)
        {
            rect.sizeDelta = new Vector2(maxWidth, sprite.rect.height / widthRatio);
        }
        else
        {
            rect.sizeDelta = new Vector2(sprite.rect.width / heightRatio, maxHeight);
        }
    }
}