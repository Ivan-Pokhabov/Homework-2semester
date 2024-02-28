namespace StackCalculator;

/// <summary>
/// Class that realize stack contains doubles on List and interface IStack
/// </summary>
public class StackList : IStack
{
    /// <summary>
    /// List that keeps stack elements
    /// </summary>
    private List<double> stack = new List<double>();

    /// <inheritdoc />
    public void Push(double element)
    {
        stack.Add(element);
    }

    /// <inheritdoc />
    public double Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Can't to make pop from empty stack");
        }

        var temp = stack[^1];
        stack.RemoveAt(stack.Count - 1);

        return temp;
    }

    /// <inheritdoc />
    public bool IsEmpty() => !stack.Any();
}