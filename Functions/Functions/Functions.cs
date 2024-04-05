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
    /// <typeparam name="TResult">Type of elements of result list.</typeparam>
    /// <param name="list">Original list.</param>
    /// <param name="function">Function that takes T value and returns R value.</param>
    /// <returns>result list.</returns>
    public static List<TResult> Map<T, TResult>(List<T> list, Func<T, TResult> function)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(function);

        var resultArray = new List<TResult>();

        foreach (var element in list)
        {
            resultArray.Add(function(element));
        }

        return resultArray;
    }

    /// <summary>
    /// Function that make new list from elements from original list by consistent application of function.
    /// </summary>
    /// <typeparam name="T">Type of list elements.</typeparam>
    /// <param name="list">Original list.</param>
    /// <param name="function">Function that takes T value and returns true or false.</param>
    /// <returns>New list.</returns>
    public static List<T> Filter<T>(List<T> list, Func<T, bool> function)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(function);

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
    /// <typeparam name="TResult">Type that returns function.</typeparam>
    /// <typeparam name="T">Type of list elements.</typeparam>
    /// <param name="list">List.</param>
    /// <param name="startValue">Initial value.</param>
    /// <param name="function">Function that takes TR and R value and returns TR value.</param>
    /// <returns>Final accumulated value.</returns>
    public static TResult Fold<TResult, T>(List<T> list, TResult startValue, Func<TResult, T, TResult> function)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(function);

        TResult result = startValue;

        foreach (var element in list)
        {
            result = function(result, element);
        }

        return result;
    }
}
