using System.Collections.Generic;
using UnityEngine;

namespace SpinnerGame
{

    [CreateAssetMenu(fileName = "SpinnerContentItemSO", menuName = "Spinner/SpinnerContent/SpinnerContentItemSO",
        order = 0)]
    public class SpinnerContentItemSO : SpinnerContentSO
    {
        public List<int> tierGainList;
        public float increaseRatioAfterListEnded;
    }
}