using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpinnerGame.TierArea
{
    public class TierAreaZoneUi : MonoBehaviour
    {
        [SerializeField] private RectTransform zoneRectTransform;
        [SerializeField] private Image zoneBackgroundImage;
        [SerializeField] private TextMeshProUGUI valueText;

        public RectTransform ZoneRectTransform => zoneRectTransform;

        public void Init(string value)
        {
            valueText.SetText(value);
        }

        public void SetColor(Color col)
        {
            zoneBackgroundImage.color = col;
        }
    }
}