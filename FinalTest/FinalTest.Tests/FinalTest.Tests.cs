using MyBubbleSort;

namespace FinalTest.Tests;
public class BubbleSortTests
{
    [Test]
    public void BubbleSort_WithSortedArray_ShouldWorksCorrectly()
    {
        var list = new List<int>() { 1, 4, 8, 10, 12 };


        GenericBubbleSort<int>.BubbleSort(list, Comparer<int>.Default);


        var expectedResult = new List<int>() { 1, 4, 8, 10, 12 };
        Assert.That(list, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BubbleSort_WithReverseSotedArray_ShouldWorksCorrectly()
    {
        var list = new List<int>() { 1, 4, 8, 10, 12 };
        list.Reverse();


        GenericBubbleSort<int>.BubbleSort(list, Comparer<int>.Default);


        var expectedResult = new List<int>() { 1, 4, 8, 10, 12 };
        Assert.That(list, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BubbbleSort_WithListOfStrings_ShouldWorksCorrectly()
    {
        var list = new List<string>() { "lol", "lamtmeh", "whyNot" };


        GenericBubbleSort<string>.BubbleSort(list, Comparer<string>.Default);


        var expectedResult = new List<string>() { "lamtmeh", "lol", "whyNot" };
        Assert.That(list, Is.EqualTo(expectedResult));
    }

    [Test]
    public void BubbleSort_WithEmptyList_ShouldWorksCorrectly()
    {
        var list = new List<double>();


        GenericBubbleSort<double>.BubbleSort(list, Comparer<double>.Default);


        Assert.That(list, Is.Empty);
    }

    [Test]
    public void BubbleSort_WithNullList_ShouldThrowsException()
    {
        List<byte> list = null!;


        Assert.Throws<ArgumentNullException>(() => GenericBubbleSort<byte>.BubbleSort(list, Comparer<byte>.Default));
    }

    [Test]
    public void BubbleSort_WithNullComparer_ShouldThrowsException()
    {
        var list = new List<byte>();


        Assert.Throws<ArgumentNullException>(() => GenericBubbleSort<byte>.BubbleSort(list, null!));
    }
}