﻿using System.Collections.Generic;
using UnityEngine;

namespace SpinnerGame
{

    [CreateAssetMenu(fileName = "SpinnerSettingsSO", menuName = "Spinner/SpinnerSettingsSO", order = 0)]
    public class SpinnerSettingsSO : ScriptableObject
    {
        public List<SpinnerTypeSO> spinnerTypes;
    }
}