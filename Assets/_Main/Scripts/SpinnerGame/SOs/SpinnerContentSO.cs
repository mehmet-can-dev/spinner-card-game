using UnityEngine;

namespace SpinnerGame
{

    [CreateAssetMenu(fileName = "SpinnerContentBaseSO", menuName = "Spinner/SpinnerContent/SpinnerContentBaseSO",
        order = 0)]
    public class SpinnerContentSO : ScriptableObject
    {
        public Sprite contentSprite;
        public string contentId;
    }
}