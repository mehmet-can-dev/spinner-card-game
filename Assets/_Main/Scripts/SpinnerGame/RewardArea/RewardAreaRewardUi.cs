using System.Text;
using UnityEngine;


public class RewardAreaRewardUi : ContentUiBase
{
    
    private const int IMAGEMAXWIDTH = 100;
    private const int IMAGEMAXHEIGHT = 100;
    
    public void Init(string id, Sprite sprite)
    {
        this.id = id;
        SetSprite(sprite,IMAGEMAXHEIGHT,IMAGEMAXWIDTH);
    }
}