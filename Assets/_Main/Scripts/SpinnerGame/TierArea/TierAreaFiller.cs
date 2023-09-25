using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierAreaFiller : MonoBehaviour
{
    [SerializeField] private List<TierAreaZoneUi> zones;
    [SerializeField] private TierAreaZoneUi selectedZone;

    public void Init()
    {
    }

    public void FillZones(int currentTier)
    {
        var middleIndex = (int)(zones.Count * 0.5f);

        for (int i = 0; i < zones.Count; i++)
        {
            var value = currentTier + (i - middleIndex);
            if (value <= 0)
                zones[i].Init("");
            else
                zones[i].Init(value.ToString());
        }

        selectedZone.Init(currentTier.ToString());
    }
}