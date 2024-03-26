using ParseTree;
namespace ParseTreeTests;

public class ParseTreeTests
{
    private ParseTree.ParseTree parseTree = new();

    [SetUp]
    public void Setup()
    {
        parseTree = new();
    }

    [TestCase("(* (+ 1 1) (* 5 (/ -10 8)))", ExpectedResult = -12.5)]
    [TestCase("(/ (+ (* 13 3) 1) (* 2 (/ (- 20 8) 6)))", ExpectedResult = 10)]
    [TestCase("/ 0 -5", ExpectedResult = 0)]
    public double BuildTreeAndCalculateTree_WithCorrectExpression_ShouldReturnExpectedResult(string expression)
    {
        parseTree.BuildTree(expression);
        return parseTree.CalculateTree();
    }

    [TestCase("(* (+ 1 1) (* 5 (/ -10 8)))")]
    [TestCase("(/ (+ (* 13 3) 1) (* 2 (/ (- 20 8) 6)))")]
    [TestCase("(/ 0 -5)")]
    public void BuildTreeAndPrint_WithCorrectExpression_ShouldReturnExpectedResult(string expression)
    {
        parseTree.BuildTree(expression);
        Assert.That(expression, Is.EqualTo(parseTree.Print()));
    }

    [Test]
    public void BuildTreeAndCalculateTree_WithDivisionByZero_ShouldThrowsArgumentException()
    {
        var expression = "( / 1 0 )";

        parseTree.BuildTree(expression);
        Assert.Throws<ArgumentException>(() => parseTree.CalculateTree()); ;
    }

    [Test]
    public void BuildTree_WithIncorrectNumber_ShouldThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => parseTree.BuildTree("(+ (* 1A5 2) 2)"));
    }

    [TestCase("2 + 2")]
    [TestCase("2 + ")]
    public void BuildTree_WithIncorrectExpression_ShouldThrowsArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => parseTree.BuildTree(expression));
    }

    [Test]
    public void BuildTree_EmptyExpression_ShouldThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => parseTree.BuildTree(""));
    }

    [Test]
    public void BuildTree_WithNull_ShouldThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => parseTree.BuildTree(null!));
    }

    [Test]
    public void CalculateTree_WithDidNotBuildTree_ShouldThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => parseTree.CalculateTree());
    }

    [Test]
    public void PrintTree_WithDidNotBuildTree_ShouldThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => parseTree.Print());
    }
}