/*
 * Copyright (C) 2024 The Hong-Jin Investment Company.
 * This file is part of the OKX Trading Server.
 * File created at 2024-12-13
 */
namespace HongJinInvestment.OKX.Server;

public static class ListExtension
{
    public static int LowerBound<T>(this List<T> sortedList, T value) where T : IComparable<T>
    {
        int index = SortedListBinarySearch(sortedList, value, true);
        return index >= 0 ? index : ~index;
    }

    public static int UpperBound<T>(this List<T> sortedList, T value) where T : IComparable<T>
    {
        int index = SortedListBinarySearch(sortedList, value, false);
        return index >= 0 ? index : ~index;
    }

    private static int SortedListBinarySearch<T>(List<T> sortedList, T value, bool lowerBound) where T : IComparable<T>
    {
        int lower = 0;
        int upper = sortedList.Count - 1;
        int index = -1;

        while (lower <= upper)
        {
            index = lower + (upper - lower) / 2;
            var comparisonResult = sortedList[index].CompareTo(value);

            if (comparisonResult == 0)
            {
                if (lowerBound)
                {
                    // 如果找到相等的元素，并且我们需要 lower_bound，则直接返回
                    return index;
                }
                else
                {
                    // 如果找到相等的元素，并且我们需要 upper_bound，则继续在右边部分搜索
                    upper = index - 1;
                }
            }
            else if (comparisonResult < 0)
            {
                lower = index + 1;
            }
            else
            {
                upper = index - 1;
            }
        }

        return lowerBound ? lower : upper;
    }
}