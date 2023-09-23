using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "SpinnerContentHolder", menuName = "SpinnerContent/SpinnerContentHolder", order = 0)]
    public class SpinnerContentHolderSO : ScriptableObject
    {
        public List<SpinnerContentItemSO> itemContents;
        public SpinnerContentBombSO bombSo;
    }
