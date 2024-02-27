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
        stack.Insert(0, element);
    }

    /// <inheritdoc />
    public double Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Can't to take top from empty stack");
        }

        var temp = stack[0];
        stack.RemoveAt(0);

        return temp;
    }

    /// <inheritdoc />
    public bool IsEmpty() => !stack.Any();
}