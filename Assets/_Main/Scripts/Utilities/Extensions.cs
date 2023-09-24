
    using System.Collections.Generic;
    using UnityEngine;

    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new global::System.Random(Random.Range(0, 1000));
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
