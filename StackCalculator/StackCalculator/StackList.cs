namespace StackCalculator;

/// <summary>
/// Class that implement stack contains Ts on List and interface IStack.
/// </summary>
public class StackList<T> : IStack<T>
{
    private List<T> stack = new();

    /// <inheritdoc />
    public void Push(T element)
    {
        this.stack.Add(element);
    }

    /// <inheritdoc />
    public T Pop()
    {
        if (this.IsEmpty())
        {
            throw new InvalidOperationException("Can't to make pop from empty stack");
        }

        var temp = this.stack[^1];
        this.stack.RemoveAt(this.stack.Count - 1);

        return temp;
    }

    /// <inheritdoc />
    public bool IsEmpty() => !this.stack.Any();
}