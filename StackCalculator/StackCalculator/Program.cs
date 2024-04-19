using StackCalculator;

Console.WriteLine("Enter your postfix expression(it should include only real numbers and operations separated by space): ");

var expression = Console.ReadLine();

var stackArrayCalculator = new Calculator(new StackArray<double>());
var stackListCalculator = new Calculator(new StackList<double>());

var (result1, isCorrect1) = stackArrayCalculator.CalculatePostfixExpression(expression);
var (result2, isCorrect2) = stackListCalculator.CalculatePostfixExpression(expression);

if ((result1, isCorrect1) != (result2, isCorrect2))
{
    Console.WriteLine("Program doesn't work correct");
    return;
}

if (!isCorrect1)
{
    Console.WriteLine("Expression contains division by zero");
    return;
}

Console.WriteLine($"Result of expression: {result1}");