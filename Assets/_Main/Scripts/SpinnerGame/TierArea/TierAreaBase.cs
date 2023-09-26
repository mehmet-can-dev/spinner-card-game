using System.Collections.Generic;
using UnityEngine;

public class TierAreaBase : MonoBehaviour
{
    [Header("Module References")] [SerializeField]
    private TierAreaModuleFiller tierAreaModuleFiller;

    [SerializeField] private TierAreaModuleAnimation tierAreaModuleAnimation;
    [SerializeField] private TierAreaModuleColorSetter tierAreaModuleColorSetter;

    [Header("Child References")] [SerializeField]
    private RectTransform zoneSliderParent;

    [SerializeField] private List<TierAreaZoneUi> slidingZones;
    [SerializeField] private TierAreaZoneUi selectedZoneRight;
    [SerializeField] private TierAreaZoneUi selectedZoneLeft;

    private int currentTier = 0;

    public int CurrentTier => currentTier;

    private SpinnerSettingsSO spinnerSettingsSo;

    public void Init(SpinnerSettingsSO spinnerSettingsSo)
    {
        this.spinnerSettingsSo = spinnerSettingsSo;
        tierAreaModuleFiller.Init(slidingZones, selectedZoneRight, selectedZoneLeft);
        tierAreaModuleAnimation.Init();
        tierAreaModuleColorSetter.Init();
        ResetTier();
    }

    public void IncreaseTier()
    {
        currentTier++;

        UpdateTierArea(true);
    }

    private void UpdateTierArea(bool useAnim)
    {
        UpdateColors();

        tierAreaModuleFiller.UpdateRightZoneText(currentTier + 1);

        void OnAnimationComplete()
        {
            tierAreaModuleFiller.FillZones(currentTier + 1);
            ResetSlider();
            tierAreaModuleFiller.UpdateLeftZoneText(currentTier + 1);
        }

        if (useAnim)
        {
            tierAreaModuleAnimation.StartSlidingAnimation(zoneSliderParent, selectedZoneLeft, selectedZoneRight,
                OnAnimationComplete);
        }
        else
        {
            OnAnimationComplete();
        }
    }

    private void UpdateColors()
    {
        var rightColor = spinnerSettingsSo
            .spinnerTypes[ListUtilities.GetModdedIndex(spinnerSettingsSo.spinnerTypes, currentTier)].spinnerMainColor;

        var leftColor = Color.white;
        if (currentTier - 1 >= 0)
            leftColor = spinnerSettingsSo
                .spinnerTypes[ListUtilities.GetModdedIndex(spinnerSettingsSo.spinnerTypes, currentTier - 1)]
                .spinnerMainColor;

        tierAreaModuleColorSetter.SetZoneColor(selectedZoneRight, rightColor);
        tierAreaModuleColorSetter.SetZoneColor(selectedZoneLeft, leftColor);
    }

    private void ResetSlider()
    {
        zoneSliderParent.anchoredPosition = Vector2.zero;
    }

    public void ResetTier()
    {
        currentTier = 0;
        UpdateTierArea(false);
    }
}