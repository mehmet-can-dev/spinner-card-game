using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SpinnerGame;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

public class SpinnerLogicTest
{
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
    public void Reach2048Test()
    {
        var spinnerSettingsSo = LoadSpinnerSettings();
        var bombSo = LoadBombSO();
        var seedSo = LoadSeedSO();
        SpinnerLogic.SetSeed(seedSo);

        List<SpinnerContentSO> contents;
        StringBuilder sb = new StringBuilder();
        StringBuilder sortedSb = new StringBuilder();

        Dictionary<string, long> contentContainer = new Dictionary<string, long>();

        int index = 0;
        int reachCount = 0;
        do
        {
            reachCount++;

            var _reachCount = ListUtilities.GetModdedIndex(spinnerSettingsSo.spinnerTypes, reachCount);
            contents = SpinnerLogic.SelectContentsLogic(spinnerSettingsSo.spinnerTypes[_reachCount]);
            SpinnerLogic.ShuffleLogic(contents);

            index = SpinnerLogic.SelectTargetIndexLogic();

            if (contents[index] is SpinnerContentItemSO contentItemSo)
            {
                var rewardAmount = SpinnerUtilities.CalculateItemGainAmount(contentItemSo, reachCount);
                if (contentContainer.ContainsKey(contentItemSo.contentId))
                {
                    contentContainer[contentItemSo.contentId] += rewardAmount;
                }
                else
                {
                    contentContainer.Add(contentItemSo.contentId, rewardAmount);
                }

                sortedSb.Append("tier :");
                sortedSb.Append(reachCount);
                sortedSb.Append(" ");
                sortedSb.Append(contentItemSo.contentId);
                sortedSb.Append(":");
                sortedSb.Append(rewardAmount);
                sortedSb.Append(" - ");
            }

            if (reachCount > 2048)
            {
                // for break infinity loop
                break;
            }
        } while (contents[index].contentId != bombSo.contentId);

        sb.Append("Reach ");
        sb.Append(reachCount);
        sb.Append("  ");
        sb.Append(contentContainer.ToStringBuilder());

        LogAssert.Expect(LogType.Exception, "Exception");
        Debug.LogException(new Exception(sb.ToString()));
        Debug.LogException(new Exception(sortedSb.ToString()));

        Assert.AreEqual(reachCount, 2048);
    }

    [Test]
    public void Find100Seed_TakeLongTimeDontWorry()
    {
        var reachTarget = 100;

        var spinnerSettingsSo = LoadSpinnerSettings();
        var bombSo = LoadBombSO();
        List<SpinnerContentSO> contents;

        int seed = 0;

        int reachCount = 0;
        do
        {
            Random.InitState(seed);
            seed++;
            reachCount = 0;
            int index = 0;

            do
            {
                reachCount++;

                var _reachCount = ListUtilities.GetModdedIndex(spinnerSettingsSo.spinnerTypes, reachCount);
                contents = SpinnerLogic.SelectContentsLogic(spinnerSettingsSo.spinnerTypes[_reachCount]);
                SpinnerLogic.ShuffleLogic(contents);

                index = SpinnerLogic.SelectTargetIndexLogic();

                if (reachCount > 2048)
                {
                    LogAssert.Expect(LogType.Exception, "Exception");
                    Debug.LogException(new Exception("break "));
                    // for break infinity loop
                    break;
                }
            } while (contents[index].contentId != bombSo.contentId);
        } while (reachCount < reachTarget);

        LogAssert.Expect(LogType.Exception, "Exception");
        Debug.LogException(new Exception("seed " + (seed - 1).ToString()));

        Assert.AreEqual(reachCount, reachTarget);
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

    private SpinnerSeedSettingsSO LoadSeedSO()
    {
        string scriptableObjectName = "Spinner_SeedSettings";
        string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpinnerSeedSettingsSO)} {scriptableObjectName}");
        if (guids.Length == 0)
            Assert.Fail($"No {nameof(SpinnerSeedSettingsSO)} found named {scriptableObjectName}");
        if (guids.Length > 1)
            Debug.LogWarning(
                $"More than one {nameof(SpinnerSeedSettingsSO)} found named {scriptableObjectName}, taking first one");

        return (SpinnerSeedSettingsSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),
            typeof(SpinnerSeedSettingsSO));
    }
}