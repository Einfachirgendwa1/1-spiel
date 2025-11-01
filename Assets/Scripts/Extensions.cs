using System;
using System.Collections.Generic;

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
}