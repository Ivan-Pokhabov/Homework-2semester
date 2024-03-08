namespace StackCalculator;

/// <summary>
/// Class of calculating postfix expressionn.
/// </summary>
public class Calculator
{
    private readonly IStack stack;

    /// <summary>
    /// Initializes a new instance of the <see cref="Calculator"/> class.
    /// </summary>
    /// <param name="stack">Stack for counting.</param>
    /// <exception cref="ArgumentNullException">Stack can't be null.</exception>
    public Calculator(IStack stack)
    {
        this.stack = stack ?? throw new ArgumentNullException(nameof(stack));
    }

    /// <summary>
    /// Calculate expression in postfix format.
    /// </summary>
    /// <param name="expression">String with math expression in postfix format.</param>
    /// <returns>(0, false) if expression contains division by zero else (result of calculating, true).</returns>
    /// <exception cref="ArgumentNullException">Expression can't be null.</exception>
    /// <exception cref="ArgumentException">Not valid expression.</exception>
    public (double, bool) CalculatePostfixExpression(string expression)
    {
        ArgumentException.ThrowIfNullOrEmpty(expression);

        var expressionElements = expression.Split();

        foreach (var element in expressionElements)
        {
            if (double.TryParse(element, out double result))
            {
                this.stack.Push(result);
            }
            else
            {
                if (!this.TryMakeOperarion(expression, element[0]))
                {
                    return (0D, false);
                }
            }
        }

        double answer;
        try
        {
            answer = this.stack.Pop();
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Expression is invalid", nameof(expression));
        }

        if (!this.stack.IsEmpty())
        {
            throw new ArgumentException("Expression is invalid", nameof(expression));
        }

        return (answer, true);
    }

    private bool TryMakeOperarion(string expression, char operation)
    {
        if (!this.IsOperation(operation))
            {
                throw new ArgumentException(nameof(expression), "Can't contains anything except numbers and operations");
            }

        double firstNumber;
        double secondNumber;

        try
            {
                secondNumber = this.stack.Pop();
                firstNumber = this.stack.Pop();
            }
        catch (InvalidOperationException)
            {
                throw new ArgumentException("Expression is invalid", nameof(expression));
            }

        var (operationResult, success) = this.TryCalculateOperation(firstNumber, secondNumber, operation);

        this.stack.Push(operationResult);

        return success;
    }

    private bool IsOperation(char symbol)
        => symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/';

    private (double, bool) TryCalculateOperation(double firstNumber, double secondNumber, char operation)
    {
        switch (operation)
        {
            case '+':
                return (firstNumber + secondNumber, true);

            case '-':
                return (firstNumber - secondNumber, true);

            case '*':
                return (firstNumber * secondNumber, true);

            case '/':
            {
                return Math.Abs(secondNumber) < 0.0000000000001D ? (0D, false) : (firstNumber / secondNumber, true);
            }

            default:
                throw new ArgumentException("Not operation sign", nameof(operation));
        }
    }
}