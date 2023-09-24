using System;
using System.Text;
using _Main.Scripts.SpinnerGame.SOs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentUiBase : MonoBehaviour
{
    protected string id;

    private const int IMAGEMAXWIDTH = 50;
    private const int IMAGEMAXHEIGHT = 50;
    private const char PREFIX = 'X';

    [Header("ChildReferences")] [SerializeField]
    private Image uiImageSpinnerContent;

    [SerializeField] private RectTransform uiRectSpinnerContent;
    [SerializeField] private TextMeshProUGUI uiTextSpinnerContent;

    [Header("ProjectReferences")] [SerializeField]
    private ContentUiAnimationSettingsSO openingAnimationSettings;
    
    public void Init(string id, Sprite sprite, int? amount)
    {
        this.id = id;

        SetSprite(sprite, IMAGEMAXHEIGHT, IMAGEMAXWIDTH);

        SetText(amount);
    }


    public void StartOpeningAnimation()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, openingAnimationSettings.animationDuration)
            .SetEase(openingAnimationSettings.animationCurve);
    }

    public virtual void SetText(int? value)
    {
        if (value == null)
        {
            uiTextSpinnerContent.gameObject.SetActive(false);
        }
        else
        {
            uiTextSpinnerContent.gameObject.SetActive(true);
            uiTextSpinnerContent.SetText(PREFIX + value.Value.FormatNumber());
        }
    }

    protected virtual void SetSprite(Sprite sprite, int maxHeight, int maxWidth)
    {
        uiImageSpinnerContent.sprite = sprite;

        UiUtilities.SetSizeDeltaFromImageSprite(uiRectSpinnerContent, sprite, maxHeight, maxWidth);
    }
}