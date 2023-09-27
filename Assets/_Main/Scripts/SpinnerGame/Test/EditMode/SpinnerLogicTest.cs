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


namespace SpinnerGame.Test
{
    public class SpinnerLogicTest
    {

        [Test]
        public void Reach2048Test()
        {
            var spinnerSettingsSo = SpinnerTestUtilities.LoadSpinnerSettings();
            var bombSo = SpinnerTestUtilities.LoadBombSO();
            var seedSo = SpinnerTestUtilities.LoadSeedSO();
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

            var spinnerSettingsSo =  SpinnerTestUtilities.LoadSpinnerSettings();
            var bombSo = SpinnerTestUtilities.LoadBombSO();
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
        
    }
}