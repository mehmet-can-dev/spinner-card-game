using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace SpinnerGame.Test
{
    public class SpinnerReferenceTest
    {
        [Test]
        public void SimpleCountTest()
        {
            var spinnerSettingsSo = SpinnerTestUtilities.LoadSpinnerSettings();
            Assert.AreEqual(spinnerSettingsSo.spinnerTypes.Count, 50);
        }

        [Test]
        public void SimpleContentsCountTest()
        {
            var spinnerSettingsSo = SpinnerTestUtilities.LoadSpinnerSettings();
            var contents = SpinnerLogic.SelectContentsLogic(spinnerSettingsSo.spinnerTypes[0]);
            Assert.AreEqual(contents.Count, SpinnerLogic.HOLECOUNT);
        }

        [Test]
        public void CheckSoReferences()
        {
            var spinnerSettingsSo = SpinnerTestUtilities.LoadSpinnerSettings();
            var contents = SpinnerLogic.SelectContentsLogic(spinnerSettingsSo.spinnerTypes[0]);

            LogAssert.Expect(LogType.Exception, "Exception");

            for (int i = 0; i < spinnerSettingsSo.spinnerTypes.Count; i++)
            {
                if (spinnerSettingsSo.spinnerTypes[i] == null)
                {
                    Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i + "] is null"));
                    Assert.Fail();
                }
                else
                {
                    if (spinnerSettingsSo.spinnerTypes[i].spinnerSprite == null)
                    {
                        Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                         "].spinnerSprite is null"));
                        Assert.Fail();
                    }

                    if (spinnerSettingsSo.spinnerTypes[i].indicatorSprite == null)
                    {
                        Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                         "].indicatorSprite is null"));
                        Assert.Fail();
                    }

                    if (spinnerSettingsSo.spinnerTypes[i].bombContents == null)
                    {
                        Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                         "].bombContents is null"));
                        Assert.Fail();
                    }

                    for (int j = 0; j < spinnerSettingsSo.spinnerTypes[i].bombContents.Count; j++)
                    {
                        if (spinnerSettingsSo.spinnerTypes[i].bombContents[j] == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i + "].bombContents[" +
                                                             j + "] is null"));
                            Assert.Fail();
                        }

                        if (string.IsNullOrEmpty(spinnerSettingsSo.spinnerTypes[i].bombContents[j].contentId))
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i + "].bombContents[" +
                                                             j + "].contentId is empty"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypes[i].bombContents[j].contentSprite == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i + "].bombContents[" +
                                                             j + "].contentSprite is null"));
                            Assert.Fail();
                        }
                    }

                    for (int j = 0; j < spinnerSettingsSo.spinnerTypes[i].definitelyContents.Count; j++)
                    {
                        if (spinnerSettingsSo.spinnerTypes[i].definitelyContents[j] == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].definitelyContents[" + j + "] is null"));
                            Assert.Fail();
                        }

                        if (string.IsNullOrEmpty(spinnerSettingsSo.spinnerTypes[i].definitelyContents[j].contentId))
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].definitelyContents[" + j + "].contentId is empty"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypes[i].definitelyContents[j].contentSprite == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].definitelyContents[" + j + "].contentSprite is null"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypes[i].definitelyContents[j].tierGainList == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].definitelyContents[" + j + "].tierGainList is null"));
                            Assert.Fail();
                        }
                    }

                    for (int j = 0; j < spinnerSettingsSo.spinnerTypes[i].possibilityContents.Count; j++)
                    {
                        if (spinnerSettingsSo.spinnerTypes[i].possibilityContents[j] == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].possibilityContents[" + j + "] is null"));
                            Assert.Fail();
                        }

                        if (string.IsNullOrEmpty(spinnerSettingsSo.spinnerTypes[i].possibilityContents[j].contentId))
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].possibilityContents[" + j + "].contentId is empty"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypes[i].possibilityContents[j].contentSprite == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].possibilityContents[" + j + "].contentSprite is null"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypes[i].possibilityContents[j].tierGainList == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].possibilityContents[" + j + "].tierGainList is null"));
                            Assert.Fail();
                        }
                    }
                }
            }

            Assert.Pass();
        }
    }
}