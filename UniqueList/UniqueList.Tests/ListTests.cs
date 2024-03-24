namespace ListTest;

using MyUniqueList;
using Exceptions;

public class ListTests
{
    public static bool AreEqual(List<int> list, int[] array)
    {
        if (list.Size != array.Length)
        {
            return false;
        }

        for (var i = 0; i < list.Size; ++i)
        {
            if (list[i] != array[i])
            {
                return false;
            }
        }

        return true;
    }

    public static IEnumerable<TestCaseData> Lists
    {
        get
        {
            var lists = new List<int>[]
            {
                new (),
                new UniqueList<int>()
            };

            var data = new[] { 1, 2, 3, 4, 5 };
            var position = new[] { 0, 1, 2, 1, 4 };
            var result = new System.Collections.Generic.List<TestCaseData>();

            foreach (var list in lists)
            {
                for (var i = 0; i < data.Length; ++i)
                {
                    list.Insert(position[i], data[i]);
                }

                result.Add(new TestCaseData(list));
            }

            return result;
        }
    }

    public static IEnumerable<TestCaseData> UniqueListData
    {
        get
        {
            var uniqueList = new UniqueList<int>();

            var data = new[] { 1, 2, 3, 4, 5 };
            var position = new[] { 0, 1, 2, 1, 4 };

            for (var i = 0; i < data.Length; ++i)
            {
                uniqueList.Insert(position[i], data[i]);
            }

            yield return new TestCaseData(uniqueList);
        }
    }

    [TestCaseSource(nameof(Lists))]
    public void AddAndIndexerAndSize_WithCorrectInput_ShouldPerformExpectedResult(List<int> list)
    {
        var expectedResultArray = new[] { 1, 4, 2, 3, 5 };

        Assert.That(AreEqual(list, expectedResultArray));
    }

    [TestCaseSource(nameof(Lists))]
    public void Delete_WithCorrectInput_ShouldDeleteElementFromList(List<int> list)
    {
        var expectedResultArray = new[] { 4, 2, 3 };

        list.Delete(0);
        list.Delete(3);

        Assert.That(AreEqual(list, expectedResultArray));
    }

    [TestCaseSource(nameof(Lists))]
    public void Delete_OutOfRangePosition_ShouldThrowIndexOutOfRangeException(List<int> list)
    {
        Assert.Throws<IndexOutOfRangeException>(() => list.Delete(-1));
        Assert.Throws<IndexOutOfRangeException>(() => list.Delete(list.Size));
        Assert.Throws<IndexOutOfRangeException>(() => list.Delete(list.Size + 1));
    }

    [TestCaseSource(nameof(Lists))]
    public void IndexerAndChangeValue_WithCorrectInput_ShouldPerformExpectedResult(List<int> list)
    {
        var expectedResultArray = new[] { 0, 4, 2, -1, 5 };

        list.ChangeValue(0, 0);
        list[3] = -1;

        Assert.That(AreEqual(list, expectedResultArray));
    }

    [TestCaseSource(nameof(Lists))]
    public void Indexer_WithCorrectInput_ShouldPerformExpectedResult(List<int> list)
    {
        var expectedResultArray = new[] { 1, 4, 2, 3, 5 };
        var isPassed = true;

        for (var i = 0; i < expectedResultArray.Length; ++i)
        {
            isPassed &= list[i] == expectedResultArray[i];
        }

        Assert.That(isPassed);
    }

    [TestCaseSource(nameof(UniqueListData))]
    public void Contains_WithCorrectInput_ShouldPerformExpectedResult(UniqueList<int> uniqueList)
    {
        var expectedResultArray = new[] { true, false, true, true, false };
        var requests = new[] { 5, -1, 4, 1, 1000 };
        var isPassed = true;

        
        for (var i = 0; i < expectedResultArray.Length; ++i)
        {
            isPassed &= uniqueList.Contains(requests[i]) == expectedResultArray[i];
        }

        Assert.That(isPassed);

    }

    [Test]
    public void Delete_FromEmptyList_ShouldThrowInvalidDeleteOperation()
    {
        var emptyList = new List<byte>();
        var emptyUniqueList = new UniqueList<byte>();

        Assert.Throws<InvalidDeleteOperationException>(() => emptyUniqueList.Delete(0));
        Assert.Throws<InvalidDeleteOperationException>(() => emptyList.Delete(0));
    }

    [TestCaseSource(nameof(UniqueListData))]
    public void Insert_AlreadyExistElements_ShouldThrowInvalidInsertOperation(UniqueList<int> uniqueList)
    {
        Assert.Throws<InvalidInsertOperationException>(() => uniqueList.Insert(1, 3));
        Assert.Throws<InvalidInsertOperationException>(() => uniqueList.Insert(3, 2));
    }

    [TestCaseSource(nameof(UniqueListData))]
    public void Change_ElementToAlreadyExistElements_ShouldThrowInvalidChangeOperation(UniqueList<int> uniqueList)
    {
        Assert.Throws<InvalidChangeOperationException>(() => uniqueList[1] = 3);
        Assert.Throws<InvalidChangeOperationException>(() => uniqueList[3] = 2);
    }
}