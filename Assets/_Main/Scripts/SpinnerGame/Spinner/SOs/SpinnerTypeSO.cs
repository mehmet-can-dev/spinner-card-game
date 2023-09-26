using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpinnerTypeSO", menuName = "Spinner/SpinnerTypeSO", order = 0)]
public class SpinnerTypeSO : ScriptableObject
{
    public string id;
    public Sprite spinnerSprite;
    public Sprite indicatorSprite;
    public Color spinnerMainColor;

    public List<SpinnerContentItemSO> definitelyContents;
    public List<SpinnerContentItemSO> possibilityContents;
    public List<SpinnerContentBombSO> bombContents;
}