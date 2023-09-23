using UnityEngine;

namespace _Main.Scripts.Spinner.SOs
{
    [CreateAssetMenu(fileName = "SpinnerContentItemSO", menuName = "SpinnerContent/SpinnerContentItemSO", order = 0)]
    public class SpinnerContentItemSO : ScriptableObject
    {
        public SpinnerContentItemData spinnerContentItemData;
    }
}