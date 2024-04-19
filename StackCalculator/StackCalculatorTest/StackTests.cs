namespace StackCalculatorTest;

using StackCalculator;

public class StackTests
{
    private static IEnumerable<TestCaseData> Stack()
    {
        yield return new TestCaseData(new StackArray<double>());
        yield return new TestCaseData(new StackList<double>());
    }

    [TestCaseSource(nameof(Stack))]
    public void Pop_FromEmptyStack_ShouldThrowException(IStack<double> stack)
    {
        Assert.Throws(typeof(InvalidOperationException), () => stack.Pop());
    }

    [TestCaseSource(nameof(Stack))]
    public void Empty_AfterPop_ShouldReturnTrue(IStack<double> stack)
    {
        var emptyStackResult = stack.IsEmpty();

        stack.Push(231231);
        var stackAfterPushResult = stack.IsEmpty();

        stack.Pop();
        var stackAfterPopResult = stack.IsEmpty();

        Assert.That(emptyStackResult && !stackAfterPushResult && stackAfterPopResult);
    }

    [TestCaseSource(nameof(Stack))]
    public void PopAndPush_WorksCorrectly(IStack<double> stack)
    {
        stack.Push(2);
        stack.Push(3);

        Assert.That(stack.Pop() == 3 && stack.Pop() == 2);
    }
}