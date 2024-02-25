namespace StackCalculator

/// <summary>
/// Stack, a first-in-first-out container for double values.
/// </summary>
interface IStack
{
    /// <summary>
    /// Add new object to the top of Stack.
    /// </summary>
    /// <param name="element">New element that will be added to stack.</param>
    void Push(double element);
    
    /// <summary>
    /// Get element from the top of stack and removes it.
    /// </summary>
    /// <returns>Element from the top of stack.</returns>
    /// <exception cref="InvalidOperationException">Pop from empty Stack.</exception>
    double Pop();

    /// <summary>
    /// Check is empty stack or not.
    /// </summary>
    /// <returns>True if stack is empty else false</returns>
    bool IsEmpty();
}