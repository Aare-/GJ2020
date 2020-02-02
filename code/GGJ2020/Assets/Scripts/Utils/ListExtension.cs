
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ListExtension {
    public static void Shuffle<T>(this IList<T> list) {
        var n = list.Count;
        while (n > 1) {
            n--;

            var k = UnityEngine.Random.Range(0, n + 1);
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void EnsureSize<T>(this IList<T> list, int size, Func<int, T> defGen = null) {
        list.EnsureSizeAtLeast(size, defGen);

        while (list.Count > size)
            list.RemoveAt(list.Count - 1);
    }

    public static void EnsureSizeAtLeast<T>(this IList<T> list, int size, Func<int, T> defGen = null) {
        while (list.Count < size)
            list.Add(defGen == null ? default(T) : defGen(list.Count));
    }

    public static T SafeGet<T>(this IList<T> list, int position, Func<int, T> defGen = null) {
        if (position < 0) {
            return defGen == null ? default(T) : defGen(list.Count());
        }

        list.EnsureSizeAtLeast(position + 1, defGen);
        return list[position];
    }

    public static void Fill<T>(this IList<T> list, T value) {
        for (var i = 0; i < list.Count; i++)
            list[i] = value;
    }

    public static T GetRandom<T>(this IList<T> list) {
        if (list.Count == 0) return default(T);
        return list[UnityEngine.Random.Range(0, list.Count)];
    }    

    public static T GetByIndexModulo<T>(this IList<T> list, int index, int count = -1) {
        if (list.Count == 0) return default(T);
        if (count == -1) count = list.Count;
        return list[index % count];
    }

    public static Color SetAlpha(this Color color, float alpha) {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static T Pop<T>(this IList<T> list) {
        if(list.Count == 0) return default(T);
        var value = list[0];
        list.RemoveAt(0);
        return value;
    }
}
