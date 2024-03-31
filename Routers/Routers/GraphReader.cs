namespace Routers;

/// <summary>
/// Class of file graph reader.
/// </summary>
public class GraphReader
{
    /// <summary>
    /// Rea graph from file.
    /// </summary>
    /// <param name="filePath">String with relative path to file.</param>
    /// <returns>IGraph graph from file.</returns>
    /// <exception cref="FileNotFoundException">File should exist.</exception>
    public static IGraph ReadGraph(string filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(filePath));

        return (!File.Exists(filePath)) ? throw new FileNotFoundException($"File not found: {filePath}") : ParseTopology(filePath);
    }

    private static IGraph ParseTopology(string filePath)
    {
        var topology = File.ReadAllText(filePath);

        var parsedTopology = new List<(int, int, int)>();

        var vertexes = new HashSet<int>();

        foreach (var line in topology.Split("\n"))
        {
            var splittedLine = line.Split(":");
            if (splittedLine.Length != 2)
            {
                throw new ArgumentException("Incorrect topology");
            }

            var fromVertex = splittedLine[0];
            var toVertexes = splittedLine[1].Split(",");

            if (!int.TryParse(fromVertex, out var firstVertex))
            {
                throw new ArgumentException("Incorrect topology");
            }

            vertexes.Add(firstVertex);

            foreach (var incidentVertex in toVertexes)
            {
                var splittedIncidentVertex = incidentVertex.Split("(");
                var vertex = splittedIncidentVertex[0];
                if (!int.TryParse(vertex, out var secondVertex))
                {
                    throw new ArgumentException("Incorrect topology");
                }

                vertexes.Add(secondVertex);

                if (!int.TryParse(splittedIncidentVertex[1].Split(")")[0], out var weight) || weight <= 0)
                {
                    throw new ArgumentException("Incorrect topology");
                }

                parsedTopology.Add(new (firstVertex - 1, secondVertex - 1, weight));
            }
        }

        var graph = new MyGraph(vertexes.Count);

        foreach (var edge in parsedTopology)
        {
            graph.AddEdge(edge.Item1, edge.Item2, edge.Item3);
        }

        return graph;
    }
}
