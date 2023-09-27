using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace SpinnerGame.Test
{
    public class SpinnerReferenceTest
    {
        [Test]
        public void Simple50CountTest()
        {
            var spinnerSettingsSo = SpinnerTestUtilities.LoadSpinnerSettings();
            Assert.AreEqual(spinnerSettingsSo.spinnerTypeList.Count, 50);
        }

        [Test]
        public void SimpleContentsCountTest()
        {
            var spinnerSettingsSo = SpinnerTestUtilities.LoadSpinnerSettings();
            var contents = SpinnerLogic.SelectContentsLogic(spinnerSettingsSo.spinnerTypeList[0]);
            Assert.AreEqual(contents.Count, SpinnerLogic.HOLECOUNT);
        }

        [Test]
        public void CheckSoReferences()
        {
            var spinnerSettingsSo = SpinnerTestUtilities.LoadSpinnerSettings();
           
            LogAssert.Expect(LogType.Exception, "Exception");

            for (int i = 0; i < spinnerSettingsSo.spinnerTypeList.Count; i++)
            {
                if (spinnerSettingsSo.spinnerTypeList[i] == null)
                {
                    Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i + "] is null"));
                    Assert.Fail();
                }
                else
                {
                    if (spinnerSettingsSo.spinnerTypeList[i].spinnerSprite == null)
                    {
                        Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                         "].spinnerSprite is null"));
                        Assert.Fail();
                    }

                    if (spinnerSettingsSo.spinnerTypeList[i].indicatorSprite == null)
                    {
                        Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                         "].indicatorSprite is null"));
                        Assert.Fail();
                    }

                    if (spinnerSettingsSo.spinnerTypeList[i].bombContents == null)
                    {
                        Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                         "].bombContents is null"));
                        Assert.Fail();
                    }

                    for (int j = 0; j < spinnerSettingsSo.spinnerTypeList[i].bombContents.Count; j++)
                    {
                        if (spinnerSettingsSo.spinnerTypeList[i].bombContents[j] == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i + "].bombContents[" +
                                                             j + "] is null"));
                            Assert.Fail();
                        }

                        if (string.IsNullOrEmpty(spinnerSettingsSo.spinnerTypeList[i].bombContents[j].contentId))
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i + "].bombContents[" +
                                                             j + "].contentId is empty"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypeList[i].bombContents[j].contentSprite == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i + "].bombContents[" +
                                                             j + "].contentSprite is null"));
                            Assert.Fail();
                        }
                    }

                    for (int j = 0; j < spinnerSettingsSo.spinnerTypeList[i].definitelyContents.Count; j++)
                    {
                        if (spinnerSettingsSo.spinnerTypeList[i].definitelyContents[j] == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].definitelyContents[" + j + "] is null"));
                            Assert.Fail();
                        }

                        if (string.IsNullOrEmpty(spinnerSettingsSo.spinnerTypeList[i].definitelyContents[j].contentId))
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].definitelyContents[" + j + "].contentId is empty"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypeList[i].definitelyContents[j].contentSprite == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].definitelyContents[" + j + "].contentSprite is null"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypeList[i].definitelyContents[j].tierGainList == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].definitelyContents[" + j + "].tierGainList is null"));
                            Assert.Fail();
                        }
                    }

                    for (int j = 0; j < spinnerSettingsSo.spinnerTypeList[i].possibilityContents.Count; j++)
                    {
                        if (spinnerSettingsSo.spinnerTypeList[i].possibilityContents[j] == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].possibilityContents[" + j + "] is null"));
                            Assert.Fail();
                        }

                        if (string.IsNullOrEmpty(spinnerSettingsSo.spinnerTypeList[i].possibilityContents[j].contentId))
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].possibilityContents[" + j + "].contentId is empty"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypeList[i].possibilityContents[j].contentSprite == null)
                        {
                            Debug.LogException(new Exception("spinnerSettingsSo.spinnerTypes[" + i +
                                                             "].possibilityContents[" + j + "].contentSprite is null"));
                            Assert.Fail();
                        }

                        if (spinnerSettingsSo.spinnerTypeList[i].possibilityContents[j].tierGainList == null)
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