namespace Functions.Test;

using Functions;

public class Tests
{
    [Test]
    public void Map_WithCorrectInput_ShouldReturnExpectedList()
    {
        var list = new List<byte> { 19, 8, 255, 18, 0 };

        var expectedResult = new List<int> { 20, 9, 256, 19, 1} ;

        Assert.That(Function.Map(list, a => a + 1), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Map_WithEmptyList_ShouldReturnEmptyListWithCorrectType()
    {
        var list = new List<int>();

        Assert.That(Function.Map(list, a => Math.Sqrt(a)), Is.EqualTo(new List<double>()));
    }

    [Test]
    public void Map_WithNullLinks_ShouldThrowsException()
    {
        List<char> nullList = null!;

        Assert.Throws<ArgumentNullException>(() => Function.Map(nullList, a => a));

        var list = new List<int> { 1, 2, 3 };
        Func<int, double> nullFunction = null!;

        Assert.Throws<ArgumentNullException>(() => Function.Map(list, nullFunction));
    }

    [Test]
    public void Filter_WithCorrectInput_ShouldReturnExpectedList()
    {
        var list = new List<string> { "Ovip", "akdov", "matobes", "home" };

        var expectedResult = new List<string> { "akdov", "matobes" };

        Assert.That(Function.Filter(list, a => a.Contains('a')), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Filter_WithEmptyList_ShouldReturnEmptyListWithCorrectType()
    {
        var list = new List<char>();

        Assert.That(Function.Filter(list, a => true), Is.EqualTo(new List<char>()));
    }

    [Test]
    public void Filter_WithNullLinks_ShouldThrowsException()
    {
        List<char> nullList = null!;

        Assert.Throws<ArgumentNullException>(() => Function.Filter(nullList, a => true));

        var list = new List<int> { 1, 2, 3 };
        Func<int, bool> nullFunction = null!;

        Assert.Throws<ArgumentNullException>(() => Function.Filter(list, nullFunction));
    }

    [Test]
    public void Fold_WithCorrectInput_ShouldReturnExpectedAccumulatedValue()
    {
        var list = new List<int> { 2, 4, 10 };

        var expectedResult = 0.025d;

        Assert.That(Function.Fold(list, (double)2, (a, b) => a / b), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Fold_WithEmptyList_ShouldReturnStartedAccumulator()
    {
        var list = new List<string>();
        var startedAccumulator = "test";

        Assert.That(Function.Fold(list, startedAccumulator, (x, y) => x + y), Is.EqualTo(startedAccumulator));
    }

    [Test]
    public void Fold_WithNullLinks_ShouldThrowsException()
    {
        List<char> nullList = null!;

        Assert.Throws<ArgumentNullException>(() => Function.Fold(nullList, "a", (a, b) => a + b));

        var list = new List<int> { 1, 2, 3 };
        Func<double, int, double> nullFunction = null!;

        Assert.Throws<ArgumentNullException>(() => Function.Fold(list, 0.5d, nullFunction));
    }
}