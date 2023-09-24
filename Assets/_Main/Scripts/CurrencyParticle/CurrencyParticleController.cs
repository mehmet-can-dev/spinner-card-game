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

    [SerializeField] private int maxCount = 15;

    private readonly List<CurrencyParticleUi> activeCurrencyParticles = new();

    private readonly Queue<CurrencyParticleUi> pooledCurrencyParticles = new();

    private void Start()
    {
        for (int i = 0; i < maxCount; i++)
        {
            var currencyParticle = Instantiate(currencyParticleUiPrefab, parent);
            currencyParticle.gameObject.SetActive(false);
            pooledCurrencyParticles.Enqueue(currencyParticle);
        }
    }

    public void Create(Vector2 spawnPos, Sprite sprite, int spawnCount, Vector3 targetPos,
        Action onComplete = null)
    {
        StartCoroutine(
            CreateCurrency(spawnPos, sprite, spawnCount, 150, Ease.Linear, null, targetPos, 0, 0.5f, 0, onComplete));
    }

    private IEnumerator CreateCurrency(Vector2 spawnPos, Sprite sprite, int spawnCount, float force, Ease ease,
        Action collectAction, Vector3 targetPos, float collectDelay = 0f, float movementSpeed = 0.5f,
        float createSpeed = 0f,
        Action onComplete = null)
    {
        yield return new WaitForSeconds(0.1f);

        spawnCount = spawnCount > maxCount ? maxCount : spawnCount;

        for (var i = 0; i < spawnCount; i++)
        {
            var currencyParticle = pooledCurrencyParticles.Dequeue();
            currencyParticle.FetchData(spawnPos, sprite);
            currencyParticle.gameObject.SetActive(true);

            var rndDir = Random.insideUnitSphere * force;
            rndDir.z = 0;
            var rndRot = Random.insideUnitSphere * force;
            rndRot.y = 0;
            rndRot.x = 0;

            StartCoroutine(currencyParticle.AddForce(spawnPos, rndDir, movementSpeed, collectDelay, targetPos, 0.7f,
                ease, rndRot,
                0.7f, () =>
                {
                    activeCurrencyParticles.Remove(currencyParticle);
                    pooledCurrencyParticles.Enqueue(currencyParticle);
                    collectAction?.Invoke();
                    currencyParticle.gameObject.SetActive(false);
                }));

            activeCurrencyParticles.Add(currencyParticle);
        }

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