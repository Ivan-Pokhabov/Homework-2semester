namespace MyUniqueList;

using Exceptions;

/// <summary>
/// Values container.
/// </summary>
/// <typeparam name="T">Type of values.</typeparam>
public class List<T>
{
    /// <summary>
    /// Head of list.
    /// </summary>
    private protected Node? head = null;

    /// <summary>
    /// Gets size of list.
    /// </summary>
    public int Size { get; private set; } = 0;

    /// <summary>
    /// Get element of list by index.
    /// </summary>
    /// <param name="index">Int index of element.</param>
    /// <returns>T value.</returns>
    public T this[int index]
    {
        get => GetValue(index);
        set => ChangeValue(index, value);
    }

    /// <summary>
    /// Insert element to list in any position.
    /// </summary>
    /// <param name="index">Index of set position.</param>
    /// <param name="value">Value to set.</param>
    /// <exception cref="IndexOutOfRangeException"><Index should be in range from 0 to size of list.</exception>
    public virtual void Insert(int index, T value)
    {
        if (index < 0 || index > Size)
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        if (index == 0)
        {
            head = new Node(value, head);

            ++Size;
            return;
        }

        var previousNode = GetNodeByIndex(index - 1);

        var newNode = new Node(value, previousNode.Next);

        previousNode.Next = newNode;

        ++Size;
    }

    /// <summary>
    /// Delete element from list.
    /// </summary>
    /// <param name="index">Index of deleting element.</param>
    /// <exception cref="InvalidDeleteOperationException">List shouldn't be empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index should be in range from 0 to size of list - 1.</exception>
    public void Delete(int index)
    {
        if (Size == 0)
        {
            throw new InvalidDeleteOperationException("Can't delete element from empty list");
        }

        if (!IsValidIndex(index))
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        --Size;

        if (index == 0)
        {
            head = head!.Next;
            return;
        }

        var previousNode = GetNodeByIndex(index - 1);
        previousNode.Next = previousNode.Next!.Next;
    }

    /// <summary>
    /// Change value by index.
    /// </summary>
    /// <param name="index">Int.</param>
    /// <param name="value">Value that we can set.</param>
    /// <exception cref="IndexOutOfRangeException">Index should be in range from 0 to size of list - 1.</exception>
    public virtual void ChangeValue(int index, T value)
    {
        if (!IsValidIndex(index))
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        GetNodeByIndex(index).Value = value;
    }

    private T GetValue(int index)
    {
        if (!IsValidIndex(index))
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        return GetNodeByIndex(index).Value;
    }

    private bool IsValidIndex(int index) => index < Size && index >= 0;

    private Node GetNodeByIndex(int index)
    {
        if (!IsValidIndex(index))
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        var current = head;
        for (var i = 0; i < index; ++i)
        {
            current = current!.Next;
        }

        return current!;
    }

    /// <summary>
    /// Class of list nodes.
    /// </summary>
    /// <param name="value">Value of node.</param>
    /// <param name="next">Link to next node in list.</param>
    private protected class Node(T value, Node? next)
    {
        /// <summary>
        /// Gets or sets value of node.
        /// </summary>
        public T Value { get; set; } = value;

        /// <summary>
        /// Gets or sets next node.
        /// </summary>
        public Node? Next { get; set; } = next;
    }
}
