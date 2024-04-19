namespace StackCalculator;

/// <summary>
/// Class that implement stack contains doubles on array and interface IStack.
/// </summary>
public class StackArray<T> : IStack<T>
{
    private int topIndex = -1;

    private int arraySize = 1;

    private T?[] stack = new T?[1];

    /// <inheritdoc />
    public void Push(T element)
    {
        this.ResizeArray();

        ++this.topIndex;

        this.stack[this.topIndex] = element;
    }

    /// <inheritdoc />
    public T Pop()
    {
        if (this.IsEmpty())
        {
            throw new InvalidOperationException("Can't make pop from empty stack");
        }

        --this.topIndex;

        var result = stack[topIndex + 1] ?? throw new ArgumentException("Can't pop from empty stack.");

        stack[topIndex + 1] = default;

        return result;
    }

    /// <inheritdoc />
    public bool IsEmpty() => this.topIndex == -1;

    private void ResizeArray()
    {
        if (this.topIndex + 1 < this.arraySize)
        {
            return;
        }

        this.arraySize *= 2;

        Array.Resize(ref this.stack, this.arraySize);
    }
}