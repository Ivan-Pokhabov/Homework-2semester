namespace StackCalculator;

/// <summary>
/// Class that implement stack contains doubles on List and interface IStack.
/// </summary>
public class StackList : IStack
{
    private List<double> stack = new List<double>();

    /// <inheritdoc />
    public void Push(double element)
    {
        this.stack.Add(element);
    }

    /// <inheritdoc />
    public double Pop()
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