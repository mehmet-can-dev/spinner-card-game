using System.Collections.Generic;
using System.Text;
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

    public static StringBuilder ToStringBuilder(this int value)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(value);
        return sb;
    }

    public static string FormatNumber(this int num)
    {
        if (num >= 100000000)
        {
            return (num / 1000000D).ToString("0.#M");
        }

        if (num >= 1000000)
        {
            return (num / 1000000D).ToString("0.##M");
        }

        if (num >= 100000)
        {
            return (num / 1000D).ToString("0.#k");
        }

        if (num >= 10000)
        {
            return (num / 1000D).ToString("0.##k");
        }

        return num.ToString("#,0");
    }
}