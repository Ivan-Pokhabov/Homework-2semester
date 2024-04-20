namespace Routers;

using System.Text;

/// <summary>
/// Class of file graph writer.
/// </summary>
public class GraphWriter
{
    /// <summary>
    /// Method that print graph in file.
    /// </summary>
    /// <param name="graph">IGraph graph.</param>
    /// <param name="filePath">Relative path to file.</param>
    public static void WriteGraph(IGraph graph, string filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(filePath));

        var output = new StringBuilder();
        for (var i = 0; i < graph.Size; ++i)
        {
            var currentVertex = i;
            var neighboursArray = graph.GetNeighbours(currentVertex);

            var stringOutput = new StringBuilder($"{currentVertex + 1} : ");
            foreach (var (neighbour, edgeWeight) in neighboursArray)
            {
                if (neighbour >= i)
                {
                    stringOutput.Append($"{neighbour + 1} ({edgeWeight}) ");
                }
            }

            if (stringOutput.Length > $"{i + 1} : ".Length)
            {
                output.Append(stringOutput);
                output.Append('\n');
            }
        }

        File.WriteAllText(filePath, output.ToString());
    }
}