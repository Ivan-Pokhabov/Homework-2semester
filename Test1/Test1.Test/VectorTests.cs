using MyVector;

namespace Test1.Test;

public class VectorTests
{
    private Vector? vector;

    [SetUp]
    public void SetUp()
    {
        var array = new int[10] { 0, 0, 0, 1, 2, 3, 0, 0, 0, 0 };
        vector = new(array);
    }

    [Test]
    public void Size_ShouldReturnCorrectResult()
        => Assert.That(vector!.Size, Is.EqualTo(10));

    [Test]
    public void Indexer_WithCorrectInput_ShouldReturnExpectedResult()
    {
        var array = new int[10] { 0, 0, 0, 1, 2, 3, 0, 0, 0, 0 };
        for (int i = 0; i < 10; ++i)
        {
            Assert.That(vector![i], Is.EqualTo(array[i]));
        }

        array[4] = 0;
        vector![4] = 0;

        array[8] = 9;
        vector![8] = 9;

        for (int i = 0; i < 10; ++i)
        {
            Assert.That(vector![i], Is.EqualTo(array[i]));
        }
    }

    [Test]
    public void ScalarProduct_WithCorrectInput_ShouldReturnExpectedResult()
    {
        Assert.That(vector!.ScalarProduct(vector!), Is.EqualTo(14));
    }
}