using LZW;
namespace LZW.Tests;

public class Tests
{

    [TestCase("../../../TestFiles/M.-R.-Carey-The-Girl-With-All-the-Gifts.pdf")]
    [TestCase("../../../TestFiles/Толстой Лев. Война и мир. Книга 1 - royallib.ru.txt")]
    [TestCase("../../../TestFiles/blablabla.png")]
    public void EncodeAndDecodeWithBWT_ShouldNotChangeTheFile(string path)
    {
        var expected = File.ReadAllBytes(path);
        LZWTransformer.Encode(path, true);
        LZWTransformer.Decode(path + ".zipped", true);

        var actual = File.ReadAllBytes(path);

        Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
    }

    [TestCase("../../../TestFiles/M.-R.-Carey-The-Girl-With-All-the-Gifts.pdf")]
    [TestCase("../../../TestFiles/Толстой Лев. Война и мир. Книга 1 - royallib.ru.txt")]
    [TestCase("../../../TestFiles/blablabla.png")]
    public void EncodeAndDecodeWithoutBWT_ShouldNotChangeTheFile(string path)
    {
        var expected = File.ReadAllBytes(path);
        LZWTransformer.Encode(path);
        LZWTransformer.Decode(path + ".zipped");

        var actual = File.ReadAllBytes(path);

        Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
    }

    [TestCase("../../../LZW/TestFiles/empty.txt")]
    public void EncodeEmptyFile_ShouldThrowException(string path)
    {
        Assert.Throws<ArgumentException>(() => LZWTransformer.Encode(path));
    }

    [TestCase("../../../LZW/TestFiles/empty.txt")]
    public void DecodeEmptyFile_ShouldThrowException(string path)
    {
        Assert.Throws<ArgumentException>(() => LZWTransformer.Decode(path));
    }

    [TestCase("../../../LZW/TestFiles/happines.exe")]
    public void EncodeNotExistingFile_ShouldThrowException(string path)
    {
        Assert.Throws<ArgumentException>(() => LZWTransformer.Encode(path));
    }

    [TestCase("../../../LZW/TestFiles/happines.exe")]
    public void DecodeNotExistingFile_ShouldThrowException(string path)
    {
        Assert.Throws<ArgumentException>(() => LZWTransformer.Decode(path));
    }
}