using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

//ToDo must be connect dependency injection system
public class CurrencyParticleController : Singleton<CurrencyParticleController>
{
    [Header("References")] [SerializeField]
    private CurrencyParticleUi currencyParticleUiPrefab;

    [SerializeField] private Transform parent;

    public const int MAXCOUNT = 15;

    private readonly List<CurrencyParticleUi> activeCurrencyParticles = new();

    private readonly Queue<CurrencyParticleUi> pooledCurrencyParticles = new();

    private void Start()
    {
        for (int i = 0; i < MAXCOUNT; i++)
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
            CreateCurrency(currencyCreateData, 150, Ease.Linear, perIconReached, 0, 0.5f, onComplete));
    }

    private IEnumerator CreateCurrency(CurrencyCreateData currencyCreateData, float force, Ease ease,
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
                yield return StartCoroutine(currencyParticle.AddForce(currencyCreateData.spawnPos, rndDir, movementSpeed, collectDelay,
                    currencyCreateData.targetPos, 0.7f,
                    ease, rndRot,
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
                StartCoroutine(currencyParticle.AddForce(currencyCreateData.spawnPos, rndDir, movementSpeed, collectDelay,
                    currencyCreateData.targetPos, 0.7f,
                    ease, rndRot,
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

    public void CompleteCurrencyParticle()
    {
        for (int i = 0; i < activeCurrencyParticles.Count; i++)
        {
            activeCurrencyParticles[i].FinishSequence();
        }
    }
}