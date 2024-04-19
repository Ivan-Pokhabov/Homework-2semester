namespace StackCalculatorTests;

using Moq;
using StackCalculator;

public class StackCalculatorTests
{
    private static IEnumerable<TestCaseData> StackCalculator()
    {
        yield return new TestCaseData(new Calculator(new StackArray<double>()));
        yield return new TestCaseData(new Calculator(new StackList<double>()));
    }

    [Test]
    public void MockTest()
    {
        var mock = new Mock<IStack<double>>();
        mock.SetupSequence(t => t.Pop())
            .Returns(2)
            .Returns(1)
            .Returns(3);
        mock.Setup(t => t.IsEmpty()).Returns(true);

        var calculator = new Calculator(mock.Object);

        var expression = "1 2 +";
        const double correctAnswer = 3;

        var (result, isCorrect) = calculator.CalculatePostfixExpression(expression);

        Assert.That(isCorrect && Math.Abs(result - correctAnswer) < (double)1e13);
        mock.Verify(dependency => dependency.Pop(), Times.Exactly(3));
    }

    [TestCaseSource(nameof(StackCalculator))]
    public void CalculatePostfixExpression_WithCorrectExpression_ShouldReturnCorrectResult(Calculator calculator)
    {
        var expression = "1,5 8,5 9 - * 6,12 /";
        const double correctAnswer = -0.122549019607;
        var (result, isCorrect) = calculator.CalculatePostfixExpression(expression);

        Assert.That(isCorrect && Math.Abs(result - correctAnswer) < (double)1e13);
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