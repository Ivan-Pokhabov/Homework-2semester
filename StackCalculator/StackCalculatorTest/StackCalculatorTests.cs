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
    public void CalculatePostfixExpression_WithCorrectExpression_ShouldReturnCorrectResult(Calculator calculator)
    {
        var expression = "1,5 8,5 9 - * 6,12 /";
        const double correctAnswer = -0.122549019607;
        var (result, isCorrect) = calculator.CalculatePostfixExpression(expression);

        Assert.That(isCorrect && Math.Abs(result - correctAnswer) < 0.0000000001);
    }

    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatePostfixExpression_WithDivisionByZero_ShouldReturnFalse(Calculator calculator)
    {
        var expression = "1,5 0 /";
        var (result, isCorrect) = calculator.CalculatePostfixExpression(expression);

        Assert.That(!isCorrect);
    }

    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatePostfixExpression_WithIncorrectExpression_ShouldThrowException(Calculator calculator)
    {
        var expression = "1,5 9 8 + - 9";

        Assert.Throws<ArgumentException>(() => calculator.CalculatePostfixExpression(expression));
    }


    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatePostfixExpression_WithEmptyExpression_ShouldThrowException(Calculator calculator)
    {
        Assert.Throws<ArgumentException>(() => calculator.CalculatePostfixExpression(string.Empty));
    }

    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatePostfixExpression_WithNullExpression_ShouldThrowException(Calculator calculator)
    {
        Assert.Throws<ArgumentNullException>(() => calculator.CalculatePostfixExpression(null));
    }
}