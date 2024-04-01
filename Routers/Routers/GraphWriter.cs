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
            var stringOutput = new StringBuilder($"{i + 1} : ");
            foreach (var (neighbour, edgeWeight) in graph.GetNeighbours(i))
            {
                if (neighbour >= i)
                {
                    stringOutput.Append($"{neighbour + 1} ({edgeWeight}) ");
                }
            }

            if (stringOutput.Length > $"{i + 1} : ".Length)
            {
                output.AppendLine(stringOutput.ToString());
            }
        }

        File.WriteAllText(filePath, output.ToString());
    }
}