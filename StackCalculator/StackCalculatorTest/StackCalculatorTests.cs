namespace StackCalculatorTests;

using StackCalculator;

public class StackCalculatorTests
{
    private static IEnumerable<TestCaseData> StackCalculator()
    {
        yield return new TestCaseData(new Calculator(new StackArray()));
        yield return new TestCaseData(new Calculator(new StackList()));
    }

    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatorWithCorrectExpressionShouldReturnCorrectResult(Calculator calculator)
    {
        var expression = "1,5 8,5 9 - * 6,12 /";
        var (result, isCorrect) = calculator.CalculatePostfixExpression(expression);

        Assert.That(isCorrect && Math.Abs(result + 0.122549019607) < 1e-12);
    }

    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatorWithDivisionByZeroShouldReturnFalse(Calculator calculator)
    {
        var expression = "1,5 0 /";
        var (result, isCorrect) = calculator.CalculatePostfixExpression(expression);

        Assert.That(!isCorrect);
    }

    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatorWithIncorrectExpressionShouldThrowException(Calculator calculator)
    {
        var expression = "1,5 9 8 + - 9";

        Assert.Throws<ArgumentException>(() => calculator.CalculatePostfixExpression(expression));
    }


    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatorWithEmptyExpressionShouldThrowException(Calculator calculator)
    {
        Assert.Throws<ArgumentException>(() => calculator.CalculatePostfixExpression(String.Empty));
    }

    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatorWithNullShouldThrowException(Calculator calculator)
    {
        Assert.Throws<ArgumentNullException>(() => calculator.CalculatePostfixExpression(null));
    }
}