using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TierAreaZoneUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;

    public void Init(string value)
    {
        valueText.SetText(value);
    }
}