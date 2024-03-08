namespace StackCalculatorTest;

using StackCalculator;

public class StackTests
{
    private static IEnumerable<TestCaseData> Stack()
    {
        yield return new TestCaseData(new StackArray());
        yield return new TestCaseData(new StackList());
    }

    [TestCaseSource(nameof(Stack))]
    public void Pop_FromEmptyStack_ShouldThrowException(IStack stack)
    {
        Assert.Throws(typeof(InvalidOperationException), () => stack.Pop());
    }

    [TestCaseSource(nameof(Stack))]
    public void Empty_AfterPop_ShouldReturnTrue(IStack stack)
    {
        var emptyStackResult = stack.IsEmpty();

        stack.Push(231231);
        var stackAfterPushResult = stack.IsEmpty();

        stack.Pop();
        var stackAfterPopResult = stack.IsEmpty();

        Assert.That(emptyStackResult && !stackAfterPushResult && stackAfterPopResult);
    }

    [TestCaseSource(nameof(Stack))]
    public void PopAndPush_WorksCorrectly(IStack stack)
    {
        stack.Push(2);
        stack.Push(3);

        Assert.That(stack.Pop() == 3 && stack.Pop() == 2);
    }
}