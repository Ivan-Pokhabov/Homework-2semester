namespace Routers;

/// <summary>
/// Class of making topology of routers by file.
/// </summary>
public static class MakeTopology
{
    /// <summary>
    /// Build topology.
    /// </summary>
    /// <param name="inputFilePath">Relative path to input file.</param>
    /// <param name="outputFile">Relative path to output file.</param>
    public static void Build(string inputFilePath, string outputFile)
    {
        ArgumentException.ThrowIfNullOrEmpty(inputFilePath);
        ArgumentException.ThrowIfNullOrEmpty(outputFile);

        IGraph graph = GraphReader.ReadGraph(inputFilePath);

        IGraph maxSpanningTree = MaxSpanningTreeMaker.MakeAlgorithmPrima(graph);

        GraphWriter.WriteGraph(maxSpanningTree, outputFile);
    }
}