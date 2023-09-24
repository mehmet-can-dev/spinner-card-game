using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentUiBase : MonoBehaviour
{
    protected string id;

    private const int IMAGEMAXWIDTH = 50;
    private const int IMAGEMAXHEIGHT = 50;
    private const char PREFIX = 'X';

    [Header("References")] [SerializeField]
    private Image uiImageSpinnerContent;

    [SerializeField] private RectTransform uiRectSpinnerContent;
    [SerializeField] private TextMeshProUGUI uiTextSpinnerContent;

    public void Init(string id, Sprite sprite, int? amount)
    {
        this.id = id;

        SetSprite(sprite, IMAGEMAXHEIGHT, IMAGEMAXWIDTH);

        SetText(amount);
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

        var widthRatio = sprite.rect.width / maxWidth;
        var heightRatio = sprite.rect.height / maxHeight;
        if (widthRatio > heightRatio)
        {
            uiRectSpinnerContent.sizeDelta = new Vector2(maxWidth, sprite.rect.height / widthRatio);
        }
        else
        {
            uiRectSpinnerContent.sizeDelta = new Vector2(sprite.rect.width / heightRatio, maxHeight);
        }
    }
}