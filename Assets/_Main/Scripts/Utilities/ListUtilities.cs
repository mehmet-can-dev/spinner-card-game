
    using System.Collections.Generic;

    public static class ListUtilities
    {
        public static int GetModdedIndex<T>(List<T> targetList,int index)
        {
            var _index = index > targetList.Count ? index % targetList.Count : index;
            return _index;
        }
    }
