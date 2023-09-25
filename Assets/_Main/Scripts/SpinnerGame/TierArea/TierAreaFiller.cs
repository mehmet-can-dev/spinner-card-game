using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierAreaFiller : MonoBehaviour
{
    private List<TierAreaZoneUi> zones;
    private TierAreaZoneUi selectedRightZone;
    private TierAreaZoneUi selectedLeftZone;

    public void Init(List<TierAreaZoneUi> zones, TierAreaZoneUi selectedRightZone,TierAreaZoneUi selectedLeftZone )
    {
        this.zones = zones;
        this.selectedRightZone = selectedRightZone;
        this.selectedLeftZone = selectedLeftZone;
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

        selectedLeftZone.Init(currentTier.ToString());
        selectedRightZone.Init(currentTier.ToString());
    }
}