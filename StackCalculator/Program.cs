using StackCalculator;

Console.WriteLine("Enter your postfix expression: ");

var expression = Console.ReadLine();

// Can use StackList instead of StackArray
var calculator = new Calculator(new StackArray());

var (result, isCorrect) = calculator.CalculatePostfixExpression(expression);
if (!isCorrect)
{
    Console.WriteLine("Expression contains division by zero(");
    return;
}

Console.WriteLine($"Result of expression: {result}");
