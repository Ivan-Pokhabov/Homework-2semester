namespace Routers.Test;

using Routers;

public class MakeTopologyTests
{
    [TestCase("../../../TestFiles/correctInput1.txt", "../../../TestFiles/expectedOutput1.txt")]
    [TestCase("../../../TestFiles/correctInput2.txt", "../../../TestFiles/expectedOutput2.txt")]
    public void Build_WithCorrectInput_ShouldWorkedCorrectly(string inputFile, string expectedFile)
    {
        MakeTopology.Build(inputFile, "../../../TestFiles/output.txt");

        Assert.That(File.ReadAllText("../../../TestFiles/output.txt") == File.ReadAllText(expectedFile));
    }

    [Test]
    public void Build_WithNullorEmptyStrings_ShouldThrowsException()
    {
        Assert.Throws<ArgumentException>(() => MakeTopology.Build(string.Empty, "lol"));
        Assert.Throws<ArgumentNullException>(() => MakeTopology.Build(null, "kek"));
        Assert.Throws<ArgumentException>(() => MakeTopology.Build("loh", string.Empty));
        Assert.Throws<ArgumentNullException>(() => MakeTopology.Build("ya", null));
    }

    [Test]
    public void Build_WithUnexistedFile_ShouldThrowsExceptions()
        => Assert.Throws<FileNotFoundException>(() => MakeTopology.Build("MatematikaUbivaet.pomogite", "hochuPiva.txt"));

    [TestCase("../../../TestFiles/disconnectedGraph1.txt")]
    [TestCase("../../../TestFiles/disconnectedGraph2.txt")]
    public void Build_WithDisconnectedGraph_ShouldThrowsException(string inputFile)
    {
        Assert.Throws<GraphNotConnectedException>(() => MakeTopology.Build(inputFile, "../../../TestFiles/output.txt"));
    }

    [TestCase("../../../TestFiles/incorrectInput1.txt")]
    [TestCase("../../../TestFiles/incorrectInput2.txt")]
    [TestCase("../../../TestFiles/incorrectInput3.txt")]
    public void Build_WithIncorrectInput_ShouldThrowsException(string inputFile)
    {
        Assert.Throws<ArgumentException>(() => MakeTopology.Build(inputFile, "../../../TestFiles/output.txt"));
    }
}