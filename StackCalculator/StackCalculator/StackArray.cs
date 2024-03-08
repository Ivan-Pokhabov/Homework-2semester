namespace StackCalculator;

/// <summary>
/// Class that implement stack contains doubles on array and interface IStack.
/// </summary>
public class StackArray : IStack
{
    private int topIndex = -1;

    private int arraySize = 1;

    private double[] stack = new double[1];

    /// <inheritdoc />
    public void Push(double element)
    {
        this.ResizeArray();

        ++this.topIndex;

        this.stack[this.topIndex] = element;
    }

    /// <inheritdoc />
    public double Pop()
    {
        if (this.IsEmpty())
        {
            throw new InvalidOperationException("Can't make pop from empty stack");
        }

        --this.topIndex;

        return this.stack[this.topIndex + 1];
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