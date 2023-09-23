using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinnerContent : MonoBehaviour
{
    private const int IMAGEMAXWIDTH = 70;
    private const int IMAGEMAXHEIGHT = 70;

    [Header("References")] [SerializeField]
    private Image uiImageSpinnerContent;

    [SerializeField] private RectTransform uiRectSpinnerContent;
    [SerializeField] private TextMeshProUGUI uiTextSpinnerContent;

    public void Init(Sprite sprite, string text)
    {
        uiImageSpinnerContent.sprite = sprite;


        var widthRatio = sprite.rect.width / IMAGEMAXWIDTH;
        var heightRatio = sprite.rect.height / IMAGEMAXHEIGHT;
        if (widthRatio > heightRatio)
        {
            uiRectSpinnerContent.sizeDelta = new Vector2(IMAGEMAXWIDTH, sprite.rect.height / widthRatio);
        }
        else
        {
            uiRectSpinnerContent.sizeDelta = new Vector2(sprite.rect.width / heightRatio, IMAGEMAXHEIGHT);
        }


        uiTextSpinnerContent.SetText(text);
    }
}