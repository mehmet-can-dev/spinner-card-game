using System;
using System.Collections;
using UnityEngine;

public class SpinnerGame : MonoBehaviour
{
    [SerializeField] private Spinner spinner;
    [SerializeField] private RewardArea rewardArea;

    private int spinnerTier = 0;

    private void Start()
    {
        spinner.Init();
        rewardArea.Init();

        spinner.SpawnSpinner(spinnerTier, OnSpinEnded);
    }

    private void OnSpinEnded(ItemData obj, SpinnerContentUi spinnerContentUi)
    {
        if (obj is RewardItemData rwData)
        {
            rewardArea.AdjustItem(rwData);

            StartCoroutine(StartRewardedAnimation(spinnerContentUi, rwData));

            spinnerTier++;
        }
        else if (obj is BombItemData)
        {
            spinnerTier = 0;
            SpawnTest();
        }
    }

    private IEnumerator StartRewardedAnimation(SpinnerContentUi spinnerContentUi, RewardItemData rwData)
    {
        yield return new WaitForEndOfFrame();
        var targetPos = rewardArea.GetRewardUiPosition(rwData.itemId);
        CurrencyParticleController.Instance.Create(spinnerContentUi.transform.position, rwData.itemSprite,
            rwData.rewardAmount,
            targetPos, () => spinner.SpawnSpinner(spinnerTier, OnSpinEnded));
    }

    [ContextMenu("TestSpin")]
    private void SpawnTest()
    {
        spinner.SpawnSpinner(spinnerTier, OnSpinEnded);
    }
}