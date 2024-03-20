namespace MyUniqueList;

using System.Reflection.Metadata.Ecma335;
using Exceptions;

/// <summary>
/// Container that kepps only unique values.
/// </summary>
/// <typeparam name="T">Type of values.</typeparam>
public class UniqueList<T> : List<T>
{
    /// <summary>
    /// Check is contains value in container or not.
    /// </summary>
    /// <param name="value">Value that we checks.</param>
    /// <returns>True if value is in list else false.</returns>
    /// <exception cref="InvalidOperationException">You can't use this function on empty list.</exception>
    public bool Contains(T value)
    {
        if (head is null)
        {
            return false;
        }

        Node currentNode = head;

        for (var i = 0; i < Size - 1; ++i)
        {
            if (currentNode.Value!.Equals(value))
            {
                return true;
            }

            currentNode = currentNode.Next!;
        }

        return currentNode.Value!.Equals(value);
    }

    /// <summary>
    /// Change value by position.
    /// </summary>
    /// <param name="index">Index of element that we change.</param>
    /// <param name="value">Value that we sets.</param>
    /// <exception cref="InvalidChangeOperationException">You can't change to value taht alreade in unique list.</exception>
    public override void ChangeValue(int index, T value)
    {
        if (Contains(value))
        {
            throw new InvalidChangeOperationException("You can't change element to this value, because it already contains in unique list");
        }

        base.ChangeValue(index, value);
    }

    /// <summary>
    /// Insert value to position in list.
    /// </summary>
    /// <param name="index">Index of position.</param>
    /// <param name="value">Value that we insert.</param>
    /// <exception cref="InvalidInsertOperationException">You can't insert value taht alreade in unique list.</exception>
    public override void Insert(int index, T value)
    {
        if (Contains(value))
        {
            throw new InvalidInsertOperationException("You can't insert element that already contains in unique list");
        }

        base.Insert(index, value);
    }
}
