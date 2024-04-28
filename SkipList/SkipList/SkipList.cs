namespace MySkipList;

using System.Collections;

/// <summary>
/// Skip list data structure implementation.
/// </summary>
/// <typeparam name="T">Type of elements in skip list.</typeparam>
public class SkipList<T> : IList<T>
    where T : IComparable<T>
{
    private const int MaxLevel = 4;

    private readonly SkipListElement nil = new (default, default, default);

    private readonly Random random = new ();

    private SkipListElement downHead;

    private int state = 0;

    private SkipListElement head;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class.
    /// </summary>
    public SkipList()
    {
        head = new (default, nil, default);
        var current = head;
        for (int i = 0; i < MaxLevel - 1; ++i)
        {
            current.Down = new SkipListElement(default, nil, default);
            current = current.Down;
        }

        downHead = current;
    }

    /// <inheritdoc/>
    public int Count { get; private set; }

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var current = downHead;

            for (int i = 0; i < index + 1; ++i)
            {
                current = current.Next;

                if (current is null)
                {
                    throw new InvalidOperationException("Class is broken");
                }
            }

            if (current.Item is null)
            {
                throw new InvalidOperationException("Class is broken");
            }

            return current.Item;
        }
        set => throw new NotSupportedException();
    }

    /// <inheritdoc/>
    public void Add(T item)
    {
        SkipListElement? newSkipListElement = InsertValue(head, item);
        if (newSkipListElement is not null)
        {
            head.Next = new SkipListElement(item, nil, newSkipListElement);
        }

        ++state;
        ++Count;
    }

    /// <inheritdoc/>
    public void Clear()
    {
        head = new (default, nil, default);
        var current = head;
        for (int i = 0; i < MaxLevel - 1; ++i)
        {
            current.Down = new SkipListElement(default, nil, default);
        }

        downHead = current;
        ++state;
        Count = 0;
    }

    /// <inheritdoc/>
    public bool Contains(T item)
    {
        var foundValue = FindValue(head, item);
        if (foundValue == downHead)
        {
            return false;
        }

        return item.CompareTo(foundValue.Item) == 0;
    }

    /// <inheritdoc/>
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (arrayIndex < 0 || arrayIndex >= array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException("Skip List larger than gap to copy in.", nameof(arrayIndex));
        }

        var current = downHead.Next;

        while (current != nil)
        {
            if (current is null || current.Item is null)
            {
                throw new InvalidOperationException("Class is broken");
            }

            array[arrayIndex] = current.Item;

            ++arrayIndex;
            current = current.Next;
        }
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
        => new Enumerator(this);

    /// <inheritdoc/>
    public int IndexOf(T item)
    {
        var current = downHead.Next;

        var index = 0;
        while (current != nil)
        {
            if (current is null)
            {
                throw new InvalidOperationException("Class is broken");
            }

            if (item.CompareTo(current.Item) == 0)
            {
                return index;
            }

            current = current.Next;

            ++index;
        }

        return -1;
    }

    /// <inheritdoc/>
    public void Insert(int index, T item)
        => throw new NotSupportedException();

    /// <inheritdoc/>
    public bool Remove(T item)
    {
        var wasDelete = false;
        RemoveValue(head, item, ref wasDelete);

        ++state;
        if (wasDelete)
        {
            --Count;
        }

        return wasDelete;
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
        => Remove(this[index]);

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => new Enumerator(this);

    private void RemoveValue(SkipListElement element, T item, ref bool wasDelete)
    {
        if (element.Next is null)
        {
            throw new InvalidOperationException("Class is broken");
        }

        while (element.Next != nil && item.CompareTo(element.Next.Item) > 0)
        {
            element = element.Next;

            if (element.Next is null)
            {
                throw new InvalidOperationException("Class is broken");
            }
        }

        if (element.Down is not null)
        {
            RemoveValue(element.Down, item, ref wasDelete);
        }

        if (element.Next != nil && item.CompareTo(element.Next.Item) == 0)
        {
            element.Next = element.Next.Next;
            wasDelete = true;
        }
    }

    private SkipListElement? InsertValue(SkipListElement result, T item)
    {
        if (result.Next is null)
        {
            throw new InvalidOperationException("Class is broken");
        }

        while (result.Next != nil && item.CompareTo(result.Next.Item) > 0)
        {
            result = result.Next;

            if (result.Next is null)
            {
                throw new InvalidOperationException("Class is broken");
            }
        }

        SkipListElement? downElement;
        if (result.Down is null)
        {
            downElement = null;
        }
        else
        {
            downElement = InsertValue(result.Down, item);
        }

        if (downElement != null || result.Down is null)
        {
            result.Next = new SkipListElement(item, result.Next, downElement);

            if (CoinFlip())
            {
                return result.Next;
            }
        }

        return null;
    }

    private SkipListElement FindValue(SkipListElement result, T item)
    {
        if (result.Next is null)
        {
            throw new InvalidOperationException("Class is broken");
        }

        while (result.Next != nil && item.CompareTo(result.Next.Item) >= 0)
        {
            result = result.Next;

            if (result.Next is null)
            {
                throw new InvalidOperationException("Class is broken");
            }
        }

        if (result.Down is null)
        {
            return result;
        }

        return FindValue(result.Down, item);
    }

    private bool CoinFlip() => random.Next() % 2 == 0;

    private class Enumerator : IEnumerator<T>
    {
        private readonly SkipList<T>? skiplist;

        private SkipListElement? current;

        private int state;

        public Enumerator(SkipList<T> skiplist)
        {
            current = skiplist.downHead;
            this.skiplist = skiplist;
            state = this.skiplist.state;
        }

        /// <inheritdoc/>
        public object Current
        {
            get
            {
                if (current is null || current.Item is null)
                {
                    throw new InvalidOperationException("Class is broken");
                }

                return current.Item;
            }
        }

        /// <inheritdoc/>
        T IEnumerator<T>.Current
        {
            get
            {
                if (current is null || current.Item is null)
                {
                    throw new InvalidOperationException("Class is broken");
                }

                return current.Item;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
            => GC.SuppressFinalize(this);

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (current is null || skiplist is null)
            {
                throw new InvalidOperationException("Class is broken");
            }

            if (state != skiplist.state)
            {
                throw new InvalidOperationException("Can't modify skiplist during iteration");
            }

            if (current.Next == skiplist.nil)
            {
                current = skiplist.downHead;
                return false;
            }

            current = current.Next;

            return true;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            if (skiplist is null)
            {
                throw new InvalidOperationException("Class is broken");
            }

            if (state != skiplist.state)
            {
                throw new InvalidOperationException("Can't modify skiplist during iteration");
            }

            current = skiplist.downHead;
        }
    }

    private class SkipListElement(T? item, SkipListElement? next, SkipListElement? down)
    {
        public T? Item { get; set; } = item;

        public SkipListElement? Next { get; set; } = next;

        public SkipListElement? Down { get; set; } = down;
    }
}