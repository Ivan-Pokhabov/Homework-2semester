namespace StackCalculator;

/// <summary>
/// Class that realize stack contains doubles on array and interface IStack.
/// </summary>
public class StackArray : IStack
{
    /// <summary>
    /// Index to the top element of stack in array
    /// Equal -1 if array is empty.
    /// </summary>
    private int topIndex = -1;

    /// <summary>
    /// Size of array that keeps stack elements.
    /// </summary>
    private int arraySize = 1;

    /// <summary>
    /// Array that keeps stack elements.
    /// </summary>
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

    /// <summary>
    /// Method to resize array if it needs.
    /// </summary>
    private void ResizeArray()
    {
        if (this.topIndex + 1 < this.arraySize)
        {
            return;
        }

        this.arraySize *= 2;

        var temp = new double[this.arraySize];
        this.stack.CopyTo(temp, 0);

        this.stack = temp;
    }
}