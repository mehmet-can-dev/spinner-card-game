using UnityEngine;

public class TierAreaModuleColorSetter : MonoBehaviour
{
    public void Init()
    {
    }

    public void SetZoneColor(TierAreaZoneUi tierAreaZoneUi, Color col)
    {
        tierAreaZoneUi.SetColor(col);
    }
}