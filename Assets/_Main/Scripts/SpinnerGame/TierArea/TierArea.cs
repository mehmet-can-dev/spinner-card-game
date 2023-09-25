using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierArea : MonoBehaviour
{
    [SerializeField] private TierAreaFiller tierAreaFiller;
    [SerializeField] private TierAreaAnimation tierAreaAnimation;
    [SerializeField] private RectTransform parentMask;

    [SerializeField] private List<TierAreaZoneUi> zones;
    [SerializeField] private TierAreaZoneUi selectedZoneRight;
    [SerializeField] private TierAreaZoneUi selectedZoneLeft;

    private int currentTier = 0;

    public int CurrentTier => currentTier;

    public void Init()
    {
        tierAreaFiller.Init(zones, selectedZoneRight, selectedZoneLeft);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            StartCoroutine(tierAreaAnimation.SlidingAnimation(parentMask, selectedZoneLeft,selectedZoneRight));
    }

    [ContextMenu("TestAnimation")]
    private void TestAnimation()
    {
    }
}