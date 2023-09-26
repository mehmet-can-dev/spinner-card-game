using System.Collections.Generic;
using UnityEngine;

public class TierAreaModuleFiller : MonoBehaviour
{
    private List<TierAreaZoneUi> zones;
    private TierAreaZoneUi selectedRightZone;
    private TierAreaZoneUi selectedLeftZone;

    public void Init(List<TierAreaZoneUi> zones, TierAreaZoneUi selectedRightZone, TierAreaZoneUi selectedLeftZone)
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
    }

    public void UpdateRightZoneText(int value)
    {
        selectedRightZone.Init(value.ToString());
    }

    public void UpdateLeftZoneText(int value)
    {
        selectedLeftZone.Init(value.ToString());
    }
}