namespace StackCalculator;

/// <summary>
/// Class that realize stack contains doubles on array and interface IStack
/// </summary>
public class StackArray : IStack
{
    /// <summary>
    /// Index to the top element of stack in array
    /// Equal -1 if array is empty
    /// </summary>
    private int topIndex = -1;

    /// <summary>
    /// Size of array that keeps stack elements
    /// </summary>
    private int arraySize = 1;

    /// <summary>
    /// Array that keeps stack elements
    /// </summary>
    private double[] stack = new double[1];

    /// <inheritdoc />
    public void Push(double element)
    {
        ResizeArray();

        ++topIndex;

        stack[topIndex] = element;
    }

    /// <inheritdoc />
    public double Pop()
    {
        if (IsEmpty()) 
        {
            throw new InvalidOperationException("Can't make pop from empty stack");
        }

        --topIndex;

        return stack[topIndex - 1];
    }

    /// <inheritdoc />
    public bool IsEmpty() => topIndex == -1;

    /// <summary>
    /// Method to resize array if it needs
    /// </summary>
    private void ResizeArray()
    {
        if (topIndex + 1 < arraySize) 
        {
            return;
        }

        arraySize *= 2;

        var temp = new double[arraySize];
        stack.CopyTo(temp, 0);

        stack = temp;
    }
}