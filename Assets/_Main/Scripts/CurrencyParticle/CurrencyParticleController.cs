using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CurrencyParticle
{
//ToDo must be connect dependency injection system
    public class CurrencyParticleController : Singleton<CurrencyParticleController>
    {
        public const int MAXCOUNT = 15;
        
        // Magic Animation Curve animation , should be change So
        private static AnimationCurve movementAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private readonly List<CurrencyParticleUi> activeCurrencyParticles = new();

        private readonly Queue<CurrencyParticleUi> pooledCurrencyParticles = new();
        
        [Header("Project References")] [SerializeField]
        private CurrencyParticleUi currencyParticleUiPrefab;
        [Header("Child References")]
        [SerializeField] private Transform parent;
        
        
        private void Start()
        {
            // possibility of being called too many times instantiate max count * 2 
            for (int i = 0; i < MAXCOUNT * 2; i++)
            {
                var currencyParticle = Instantiate(currencyParticleUiPrefab, parent);
                currencyParticle.gameObject.SetActive(false);
                pooledCurrencyParticles.Enqueue(currencyParticle);
            }
        }

        public void Create(CurrencyCreateData currencyCreateData, Action perIconReached = null,
            Action onComplete = null)
        {
            StartCoroutine(
                CreateCurrency(currencyCreateData, 150, movementAnimationCurve, perIconReached, 0, 0.5f, onComplete));
        }

        private IEnumerator CreateCurrency(CurrencyCreateData currencyCreateData, float force,
            AnimationCurve movementAnimationCurve,
            Action collectAction, float collectDelay = 0f, float movementSpeed = 0.5f,
            Action onComplete = null)
        {
            yield return new WaitForSeconds(0.1f);

            currencyCreateData.spawnCount = Mathf.Min(currencyCreateData.spawnCount, MAXCOUNT);

            for (var i = 0; i < currencyCreateData.spawnCount; i++)
            {
                var currencyParticle = pooledCurrencyParticles.Dequeue();
                currencyParticle.FetchData(currencyCreateData.spawnPos, currencyCreateData.sprite);
                currencyParticle.gameObject.SetActive(true);

                var rndDir = Random.insideUnitSphere * force;
                rndDir.z = 0;
                var rndRot = Random.insideUnitSphere * force;
                rndRot.y = 0;
                rndRot.x = 0;

                if (i == currencyCreateData.spawnCount - 1)
                {
                    yield return StartCoroutine(currencyParticle.AddForce(currencyCreateData.spawnPos, rndDir,
                        movementSpeed, collectDelay,
                        currencyCreateData.targetTransform, 0.7f,
                        movementAnimationCurve, rndRot,
                        0.7f, () =>
                        {
                            activeCurrencyParticles.Remove(currencyParticle);
                            pooledCurrencyParticles.Enqueue(currencyParticle);
                            collectAction?.Invoke();
                            currencyParticle.gameObject.SetActive(false);
                        }));
                }
                else
                {
                    StartCoroutine(currencyParticle.AddForce(currencyCreateData.spawnPos, rndDir, movementSpeed,
                        collectDelay,
                        currencyCreateData.targetTransform, 0.7f,
                        movementAnimationCurve, rndRot,
                        0.7f, () =>
                        {
                            activeCurrencyParticles.Remove(currencyParticle);
                            pooledCurrencyParticles.Enqueue(currencyParticle);
                            collectAction?.Invoke();
                            currencyParticle.gameObject.SetActive(false);
                        }));
                }

                activeCurrencyParticles.Add(currencyParticle);
            }

            yield return new WaitForSeconds(0.1f);

            onComplete?.Invoke();
        }

       
    }
}