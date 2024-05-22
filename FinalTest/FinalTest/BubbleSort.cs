// Copyright (c) Ivan-Pokhabov, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace MyBubbleSort;

/// <summary>
/// Static class of Bubble sort generic type.
/// </summary>
/// <typeparam name="T">Type of elements that we will sort.</typeparam>
public static class GenericBubbleSort<T>
{
    /// <summary>
    /// Implementation of bubble sort.
    /// </summary>
    /// <param name="list">Collection that implement generic interface IList.</param>
    /// <param name="comparer">Comparer that implements generic interface IComparer.</param>
    /// <exception cref="ArgumentNullException">list and comparer shouldn't be null.</exception>
    public static void BubbleSort(IList<T> list, IComparer<T> comparer)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(comparer);

        for (int i = 0; i < list.Count; ++i)
        {
            for (int j = 0; j < list.Count - i - 1; ++j)
            {
                if (comparer.Compare(list[j], list[j + 1]) > 0)
                {
                    (list[j + 1], list[j]) = (list[j], list[j + 1]);
                }
            }
        }
    }
}