using UnityEngine;

namespace _Main.Scripts.Spinner.SOs
{
    [CreateAssetMenu(fileName = "SpinnerContentCurrencySO", menuName = "SpinnerContent/SpinnerContentCurrencySO", order = 1)]
    public class SpinnerContentCurrencySO : ScriptableObject
    {
        public SpinnerContentCurrencyData spinnerContentCurrencyData;
    }
}