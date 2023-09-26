using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SpinnerGame;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

public class SpinnerLogicTest
{
    private string seed = "vertigo";

    [Test]
    public void SimpleCountTest()
    {
        var spinnerSettingsSo = LoadSpinnerSettings();
        Assert.AreEqual(spinnerSettingsSo.spinnerTypes.Count, 50);
    }

    [Test]
    public void SimpleContentsCountTest()
    {
        var spinnerSettingsSo = LoadSpinnerSettings();
        var contents = SpinnerLogic.SelectContentsLogic(spinnerSettingsSo.spinnerTypes[0]);
        Assert.AreEqual(contents.Count, SpinnerLogic.HOLECOUNT);
    }

    [Test]
    public void ReachTest()
    {
        var spinnerSettingsSo = LoadSpinnerSettings();
        var bombSo = LoadBombSO();
        Random.InitState(seed.GetHashCode());
        List<SpinnerContentSO> contents;
        int index = 0;
        int reachCount = 0;
        do
        {
            reachCount++;
           var _reachCount= ListUtilities.GetModdedIndex(spinnerSettingsSo.spinnerTypes, reachCount);
            contents = SpinnerLogic.SelectContentsLogic(spinnerSettingsSo.spinnerTypes[_reachCount]);
            SpinnerLogic.ShuffleLogic(contents);
            index = SpinnerLogic.SelectTargetIndexLogic();

            if (reachCount > 2048)
            {
                break;
            }
            
        } while (contents[index].contentId != bombSo.contentId);

        LogAssert.Expect(LogType.Exception, "Exception");
        Debug.LogException(new Exception("Reach "+reachCount));
        
        
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

    private SpinnerContentBombSO LoadBombSO()
    {
        string scriptableObjectName = "SpinnerContent_Bomb";
        string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpinnerContentBombSO)} {scriptableObjectName}");
        if (guids.Length == 0)
            Assert.Fail($"No {nameof(SpinnerContentBombSO)} found named {scriptableObjectName}");
        if (guids.Length > 1)
            Debug.LogWarning(
                $"More than one {nameof(SpinnerContentBombSO)} found named {scriptableObjectName}, taking first one");

        return (SpinnerContentBombSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),
            typeof(SpinnerContentBombSO));
    }
}