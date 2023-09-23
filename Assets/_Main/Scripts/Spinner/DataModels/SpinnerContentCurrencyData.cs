using System;
using System.Collections.Generic;

[Serializable]
public class SpinnerContentCurrencyData : SpinnerContentData
{
    public CurrencyType currencyType;
    public List<int> tierGainList;
    public float increaseRatioAfterListEnded;
}