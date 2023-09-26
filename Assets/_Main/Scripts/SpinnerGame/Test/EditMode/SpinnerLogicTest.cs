using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SpinnerGame;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class SpinnerLogicTest
{

    [Test]
    public void SimpleCountTest()
    {
        var spinnerSettingsSo = LoadSpinnerSettings();
        Assert.AreEqual(spinnerSettingsSo.spinnerTypes.Count, 50);
    }

    private SpinnerSettingsSO LoadSpinnerSettings()
    {
        string scriptableObjectName = "SpinnerSettings";
        string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpinnerSettingsSO)} {scriptableObjectName}");
        if (guids.Length == 0)
            Assert.Fail($"No {nameof(SpinnerSettingsSO)} found named {scriptableObjectName}");
        if (guids.Length > 1)
            Debug.LogWarning(
                $"More than one {nameof(SpinnerSettingsSO)} found named {scriptableObjectName}, taking first one");

        return (SpinnerSettingsSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),
            typeof(SpinnerSettingsSO));
    }
    
}