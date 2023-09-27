using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpinnerGame
{

    //ToDo can be connect singleton so logic
    
    [CreateAssetMenu(fileName = "SpinnerSettingsSO", menuName = "Spinner/SpinnerSettingsSO", order = 0)]
    public class SpinnerSettingsSO : ScriptableObject
    {
        [FormerlySerializedAs("spinnerTypes")] public List<SpinnerTypeSO> spinnerTypeList;
    }
}