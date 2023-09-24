using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyParticleUi : MonoBehaviour
{
    private const int IMAGEMAXWIDTH = 100;
    private const int IMAGEMAXHEIGHT = 100;

    [SerializeField] private Image imgCurrency;
    [SerializeField] private RectTransform imgRect;

    private void Awake()
    {
    }

    public void FetchData(Sprite sprite)
    {
        imgCurrency.sprite = sprite;
        UiUtilities.SetSizeDeltaFromImageSprite(imgRect, sprite, IMAGEMAXHEIGHT, IMAGEMAXWIDTH);
    }

    public IEnumerator AddForce(Vector2 spawnCenter, Vector2 direction, float movementDuration, float collectDelay,
        Vector2 targetPos,
        float targetMovementDuration, Ease targetMovementEase, Vector3 targetRotate, float targetRotateDuration,
        Action collectAction)
    {
        targetMovementDuration *= UnityEngine.Random.Range(1, 1.5f);
        transform.DOKill();
        transform.localScale = Vector3.zero;

        var movementDelay = UnityEngine.Random.Range(collectDelay, collectDelay * 3f);
        transform.DOMove(spawnCenter + direction, movementDuration)
            .SetEase(Ease.Unset);
        transform.DORotate(targetRotate, targetRotateDuration)
            .SetEase(Ease.Unset);
        transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);


        yield return new WaitForSeconds(movementDuration);

        transform.DORotate(Vector3.zero, targetRotateDuration)
            .SetEase(Ease.Unset);
        transform.DOMove(targetPos, targetMovementDuration)
            .SetEase(targetMovementEase)
            .OnComplete(() => { collectAction?.Invoke(); });
    }

    public void FinishSequence()
    {
    }
}