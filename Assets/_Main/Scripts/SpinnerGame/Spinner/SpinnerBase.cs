using System;
using UnityEngine;

namespace SpinnerGame.Spinner
{

    public class SpinnerBase : MonoBehaviour
    {
        [Header("Module References")] [SerializeField]
        private SpinnerModuleAnimation spinnerModuleAnimation;

        [SerializeField] private SpinnerModuleSpawner spinnerModuleSpawner;
        [SerializeField] private SpinnerModuleInput spinnerModuleInput;

        private Action<ItemData, SpinnerContentUi> onSpinEnded;
        private Action onSpinStarted;

        public void Init()
        {
            spinnerModuleAnimation.Init();
            spinnerModuleSpawner.Init();
            spinnerModuleInput.Init(OnSpinnerClicked);
        }

        public void SpawnSpinner(int tier, SpinnerSettingsSO spinnerSettingsSo, Action onSpinStarted,
            Action<ItemData, SpinnerContentUi> onSpinEnded)
        {
            this.onSpinEnded = onSpinEnded;
            this.onSpinStarted = onSpinStarted;
            spinnerModuleInput.SetActive(true);
            spinnerModuleSpawner.CreateTier(tier, spinnerSettingsSo);
        }

        private void OnSpinnerClicked()
        {
            spinnerModuleInput.SetActive(false);
            var targetHole = SpinnerLogic.SelectTargetIndexLogic();
            spinnerModuleAnimation.StartAnimation(targetHole, () => OnSpinCompleted(targetHole));
            onSpinStarted?.Invoke();
        }

        private void OnSpinCompleted(int targetHole)
        {
            var item = spinnerModuleSpawner.GetRewardDataFromIndex(targetHole);
            var contentUi = spinnerModuleSpawner.GetContentUiFromIndex(targetHole);
            onSpinEnded?.Invoke(item, contentUi);
        }
    }
}