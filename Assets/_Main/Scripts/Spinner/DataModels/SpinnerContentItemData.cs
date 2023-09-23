using System;
using System.Collections.Generic;

[Serializable]
public class SpinnerContentItemData : SpinnerContentData
{
    public string contentId;
    public List<int> tierGainList;
    public int increaseAmountAfterListEnded;
}