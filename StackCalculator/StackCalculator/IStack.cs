namespace StackCalculator;

/// <summary>
/// Stack, a last-in-first-out container for double values.
/// </summary>
public interface IStack<T>
{
    /// <summary>
    /// Add new object to the top of Stack.
    /// </summary>
    /// <param name="element">New element that will be added to stack.</param>
    void Push(T element);

    /// <summary>
    /// Get element from the top of stack and removes it.
    /// </summary>
    /// <returns>Element from the top of stack.</returns>
    /// <exception cref="InvalidOperationException">Pop from empty Stack.</exception>
    T Pop();

    /// <summary>
    /// Check is empty stack or not.
    /// </summary>
    /// <returns>True if stack is empty else false.</returns>
    bool IsEmpty();
}