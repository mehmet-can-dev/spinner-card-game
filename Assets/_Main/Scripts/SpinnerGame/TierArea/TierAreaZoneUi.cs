using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TierAreaZoneUi : MonoBehaviour
{
    [SerializeField] private RectTransform zoneRectTransform;
    [SerializeField] private TextMeshProUGUI valueText;

    public RectTransform ZoneRectTransform => zoneRectTransform;

    public void Init(string value)
    {
        valueText.SetText(value);
    }
}