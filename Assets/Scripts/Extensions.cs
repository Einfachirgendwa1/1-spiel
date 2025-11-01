using System;
using System.Collections.Generic;
using UnityEngine;

internal static class Extensions {
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
        foreach (T item in source) {
            action(item);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<int, T> action) {
        int i = 0;
        foreach (T item in source) {
            action(i++, item);
        }
    }

    public static Color Rgba(byte r, byte g, byte b, byte a) => new(r / 255f, g / 255f, b / 255f, a / 255f);
}