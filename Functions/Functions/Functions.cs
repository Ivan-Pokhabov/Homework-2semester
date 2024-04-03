namespace Functions;

/// <summary>
/// Class that keeps methods.
/// </summary>
public static class Function
{
    /// <summary>
    /// Function that make new list by applying a function to each element.
    /// </summary>
    /// <typeparam name="T">Type of elements of original list.</typeparam>
    /// <typeparam name="TR">Type of elements of result list.</typeparam>
    /// <param name="list">Original list.</param>
    /// <param name="function">Function that takes T value and returns R value.</param>
    /// <returns>result list.</returns>
    public static List<TR> Map<T, TR>(List<T> list, Func<T, TR> function)
    {
        var resultArray = new List<TR>();

        foreach (var element in list)
        {
            resultArray.Add(function(element));
        }

        return resultArray;
    }

    /// <summary>
    /// Function that make new list from elements from new by rule.
    /// </summary>
    /// <typeparam name="T">Type of list elements.</typeparam>
    /// <param name="list">Original list.</param>
    /// <param name="function">Function that takes T value and returns true or false.</param>
    /// <returns>New list.</returns>
    public static List<T> Filter<T>(List<T> list, Func<T, bool> function)
    {
        var resultArray = new List<T>();

        foreach (var element in list)
        {
            if (function(element))
            {
                resultArray.Add(element);
            }
        }

        return resultArray;
    }

    /// <summary>
    /// Function that accepts a list, an initial value, and a function that takes
    /// the current accumulated value and the current list item,
    /// and returns the next accumulated value.
    /// </summary>
    /// <typeparam name="TR">Type that returns function.</typeparam>
    /// <typeparam name="T">Type of list elements.</typeparam>
    /// <param name="list">List.</param>
    /// <param name="startValue">Initial value.</param>
    /// <param name="function">Function that takes TR and R value and returns TR value.</param>
    /// <returns>Final accumulated value.</returns>
    public static TR Fold<TR, T>(List<T> list, TR startValue, Func<TR, T, TR> function)
    {
        TR result = startValue;

        foreach (var element in list)
        {
            result = function(result, element);
        }

        return result;
    }
}
