using System;
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
            Debug.Log(rwData.ToStringBuilder());

            var targetPos = rewardArea.GetRewardUiPosition(rwData.itemId);
            CurrencyParticleController.Instance.Create(spinnerContentUi.transform.position, rwData.itemSprite,
                rwData.rewardAmount,
                targetPos, () => spinner.SpawnSpinner(spinnerTier, OnSpinEnded));

            spinnerTier++;
        }
        else if (obj is BombItemData)
        {
            spinnerTier = 0;
            Debug.Log(obj.ToStringBuilder());
            SpawnTest();
        }
    }

    [ContextMenu("TestSpin")]
    private void SpawnTest()
    {
        spinner.SpawnSpinner(spinnerTier, OnSpinEnded);
    }
}