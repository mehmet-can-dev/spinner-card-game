using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CurrencyParticle
{
    public class CurrencyParticleUi : MonoBehaviour
    {
        private const int IMAGEMAXWIDTH = 100;
        private const int IMAGEMAXHEIGHT = 100;

        [SerializeField] private Image imgCurrency;
        [SerializeField] private RectTransform imgRect;

        public void FetchData(Vector3 pos, Sprite sprite)
        {
            imgRect.transform.position = pos;
            imgCurrency.sprite = sprite;
            UiUtilities.SetSizeDeltaFromImageSprite(imgRect, sprite, IMAGEMAXHEIGHT, IMAGEMAXWIDTH);
        }

        // Magic Number animation , should be change
        public IEnumerator AddForce(Vector2 spawnCenter, Vector2 direction, float movementDuration, float collectDelay,
            Transform targetTransform,
            float targetMovementDuration, AnimationCurve targetMovementEase, Vector3 targetRotate,
            float targetRotateDuration,
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

            yield return TransformUtilities.MoveTransform(transform, targetTransform, targetMovementDuration,
                targetMovementEase, collectAction);
            
        }

        public void FinishSequence()
        {
        }
    }
}