using DG.Tweening;
using UnityEngine;

public class RewardCollectButtonModuleAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private DoTweenBasicSettings openTweenSettings;
    [SerializeField] private DoTweenBasicSettings closeTweenSettings;

    public void Init()
    {
    }

    public void Close(bool useAnim)
    {
        var targetPos = Vector2.down * rectTransform.rect.height;
        if (useAnim)
        {
            // Todo Check multiple call condition
            rectTransform.DOAnchorPos(targetPos, closeTweenSettings.duration).SetEase(closeTweenSettings.ease)
                .SetLink(gameObject).SetId(GetInstanceID());
        }
        else
        {
            rectTransform.anchoredPosition = targetPos;
        }
    }

    public void Open(bool useAnim)
    {
        var targetPos = Vector2.zero;
        if (useAnim)
        {
            // Todo Check multiple call condition
            rectTransform.DOAnchorPos(targetPos, openTweenSettings.duration).SetEase(openTweenSettings.ease)
                .SetLink(gameObject).SetId(GetInstanceID());
        }
        else
        {
            rectTransform.anchoredPosition = targetPos;
        }
    }
}