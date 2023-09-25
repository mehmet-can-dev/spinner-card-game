using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierArea : MonoBehaviour
{
    [SerializeField] private TierAreaFiller tierAreaFiller;

    private int currentTier = 0;

    public int CurrentTier => currentTier;

    public void Init()
    {
        tierAreaFiller.Init();
        ResetTier();
    }

    public void IncreaseTier()
    {
        currentTier++;
        tierAreaFiller.FillZones(currentTier + 1);
    }

    public void ResetTier()
    {
        currentTier = 0;
        tierAreaFiller.FillZones(currentTier + 1);
    }
}