namespace SkipListTests;

using MySkipList;

public class SkipListTests
{
    SkipList<int> skiplist;

    [SetUp]
    public void SetUp()
        => skiplist = [];

    [Test]
    public void CountWithAdd_ShouldReturnExpectedResult()
    {
        Array.ForEach(new int[] { 1, 7, 4, 5, 6 }, skiplist.Add);


        var expectedResultAfterAdd = 5;
        Assert.That(skiplist, Has.Count.EqualTo(expectedResultAfterAdd));
    }

    [Test]
    public void CountWithRemove_ShouldReturnExpectedResult()
    {
        Array.ForEach(new int[] { 1, 7, 4, 5, 6 }, skiplist.Add);


        skiplist.Remove(4);
        skiplist.Remove(10);
        skiplist.Remove(1);
        skiplist.Remove(-1);


        var expectedResultAfterRemove = 3;
        Assert.That(skiplist, Has.Count.EqualTo(expectedResultAfterRemove));
    }

    [Test]
    public void IsReadOnly_ShouldReturnFalse()
        => Assert.That(!skiplist.IsReadOnly);

    [Test]
    public void IndexerWithAddAndRemove_WithCorrectIndex_ShouldReturnExpectedResult()
    {
        Array.ForEach(new int[] { 1, 7, 4, 5, 12 }, skiplist.Add);


        skiplist.Remove(4);
        skiplist.Remove(7);
        skiplist.Remove(10);
        skiplist.Remove(-1);


        var expectedArray = new int[] { 1, 5, 12 };
        for (int i = 0; i < expectedArray.Length; ++i)
        {
            Assert.That(skiplist[i], Is.EqualTo(expectedArray[i]));
        }
    }

    [Test]
    public void SetByIndex_ShouldThrowsNotSupportedException()
        => Assert.Throws<NotSupportedException>(() => skiplist[0] = 10);

    [Test]
    public void GetByIndex_WithIncorrectIndex_ShouldThrowArgumentOutOfRangeException()
    {
        skiplist.Add(1);
        var value = 0;


        Assert.Throws<ArgumentOutOfRangeException>(() => value = skiplist[1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => value = skiplist[-1]);
    }

    [Test]
    public void Contains_WithEmptySkipList_ShouldReturnFalse()
        => Assert.That(skiplist, Does.Not.Contain(10));

    [Test]
    public void ContainsWithAddAndRemove_WithCorrectInput_ShouldReturnExpectedResult()
    {
        Array.ForEach(new int[] { 9, 7, 5, 12 }, skiplist.Add);


        skiplist.Remove(9);
        skiplist.Remove(7);


        Assert.That(skiplist, Does.Contain(5));
        Assert.That(skiplist, Does.Contain(12));
    }

    [Test]
    public void Clear_ShouldWorksCorrectly()
    {
        Array.ForEach(new int[] { 9, 7, 5, 12 }, skiplist.Add);


        skiplist.Clear();


        Assert.That(skiplist, Has.Count.EqualTo(0));
        Assert.That(skiplist, Does.Not.Contain(5));
    }

    [Test]
    public void CopyTo_WithCorrectInput_ShouldReturnExpectedResult()
    {
        Array.ForEach(new int[] { 1, 7, 4, 5, 12 }, skiplist.Add);


        var array = new int[7];
        skiplist.CopyTo(array, 2);


        var expectedArray = new int[] { 0, 0, 1, 4, 5, 7, 12 };
        Assert.That(array, Is.EqualTo(expectedArray));
    }

    [Test]
    public void CopyTo_WithNullArray_ShouldThrowArgumentNullException()
    {
        int[] array = null!;


        Assert.Throws<ArgumentNullException>(() => skiplist.CopyTo(array, 0));
    }

    [Test]
    public void CopyTo_WithIncorrectArrayOrIndex_ShouldThrowArgumnetException()
    {
        Array.ForEach(new int[] { 1, 7, 4, 5, 12 }, skiplist.Add);
        var array = new int[7];


        Assert.Throws<ArgumentException>(() => skiplist.CopyTo(array, 6));


        Array.ForEach(new int[] {1, 2, 3}, skiplist.Add);


        Assert.Throws<ArgumentException>(() => skiplist.CopyTo(array, 0));
    }

    [Test]
    public void IterationByForEach_WithoutModifyingSkiplist_ShoudWorksCorrectly()
    {
        var array = new int[] { 4, 6, 17, 19, 10001 };

        Array.ForEach(array, skiplist.Add);

        var iterationArray = new List<int>();


        foreach (var item in skiplist)
        {
            iterationArray.Add(item);
        }


        Assert.That(array, Is.EqualTo(iterationArray));
    }

    public void AddToSkipListDuringIteration()
    {
        Array.ForEach(new int[] { 4, 6, 17, 19, 10001 }, skiplist.Add);

        foreach (var item in skiplist)
        {
            skiplist.Add(0);
        }
    }

    public void RemoveFromSkipListDuringIteration()
    {
        Array.ForEach(new int[] { 4, 6, 17, 19, 10001 }, skiplist.Add);

        foreach (var item in skiplist)
        {
            skiplist.Remove(0);
        }
    }

    [Test]
    public void IterationByForEach_WithModifyingSkiplist_ShouldThrowInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(AddToSkipListDuringIteration);
        Assert.Throws<InvalidOperationException>(RemoveFromSkipListDuringIteration);
    }

    [Test]
    public void IndexOf_ShouldReturnExpectedResults()
    {
        Array.ForEach(new int[] { 4, 6, 17, 19, 10, 27, 1 }, skiplist.Add);

        var requests = new int[] { 27, 10, 1, 4, 29, 2, 27, 0 };
        var expectedResults = new int[] { 6, 3, 0, 1, -1, -1, 6, -1 };

        var results = new List<int>();


        foreach (var item in requests)
        {
            results.Add(skiplist.IndexOf(item));
        }


        Assert.That(results, Is.EqualTo(expectedResults));
    }

    [Test]
    public void Insert_ShouldThrowNotSupportedException()
        => Assert.Throws<NotSupportedException>(() => skiplist.Insert(0, 0));

    [Test]
    public void Remove_ShouldReturnExpectedResult()
    {
        Array.ForEach(new int[] { 9, 7, 1, 10, 12 }, skiplist.Add);


        Assert.Multiple(() =>
        {
            skiplist.Remove(9);
            skiplist.Remove(1);
            skiplist.Remove(12);
        });


        Assert.That(!skiplist.Remove(100));
    }

    [Test]
    public void RemoveAt_WithCorrectInput_ShouldWorkCorrectly()
    {
        Array.ForEach(new int[] { 9, 7, 1, 10, 12, 12, 3 }, skiplist.Add);


        skiplist.RemoveAt(6);
        skiplist.RemoveAt(0);
        skiplist.RemoveAt(4);


        var expectedArray = new int[] { 3, 7, 9, 10 };

        var result = new List<int>();
        foreach (var item in skiplist)
        {
            result.Add(item);
        }

        Assert.That(result, Is.EqualTo(expectedArray));
    }
}