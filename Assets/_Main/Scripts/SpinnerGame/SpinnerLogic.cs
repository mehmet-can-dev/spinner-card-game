
    using UnityEngine;

    public static class SpinnerLogic
    {
        public const int HOLECOUNT = 8;
        public const float TWOPIRAD = 360;
        public const float PERCOUNTANGLE = TWOPIRAD / HOLECOUNT;
        
        public static int SelectTargetIndex()
        {
            return Random.Range(0, HOLECOUNT);
        }
    }
