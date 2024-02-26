namespace BWTTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    [DataRow("ABACABA", "BCABAAA", 3), DataRow("abcd", "dabc", 1), DataRow("", "", 1)]
    public void BWTTransformTest(string word, string expectedBWTWord, int expectedWordIndex)
    {
        var (BWTWord, originalWordIndex) = BWT.Transform(word);
        Assert.IsTrue(BWTWord == expectedBWTWord && expectedWordIndex == originalWordIndex);
    }

    [TestMethod]
    [DataRow("ABACABA", "BCABAAA", 3), DataRow("abcd", "dabc", 1), DataRow("", "", 1)]
    public void BWTReverseTransformTest(string expectedOriginalWord, string BWTWord, int originalWordIndex)
    {
        var originalWord = BWT.ReverseTransform(BWTWord, originalWordIndex);
        Assert.IsTrue(expectedOriginalWord == originalWord);
    }
}