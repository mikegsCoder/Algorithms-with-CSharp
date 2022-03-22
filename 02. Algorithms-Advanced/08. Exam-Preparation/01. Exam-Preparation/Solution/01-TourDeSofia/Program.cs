using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01_TourDeSofia
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Distance { get; set; }
    }

    // Dijkstra Algorithm !!!
    public class Program
    {
        private static List<Edge>[] graph;
        private static int nodesCount;
        private static int edgesCount;
        private static int startNode;
        private static double[] distances;
        private static HashSet<int> visited;

        public static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());
            startNode = int.Parse(Console.ReadLine());

            ReadGraph();

            Initialize();

            Dijkstra();

            PrintResult();
        }

        private static void PrintResult()
        {
            if (double.IsPositiveInfinity(distances[startNode]))
            {
                Console.WriteLine(visited.Count);
            }
            else
            {
                Console.WriteLine(distances[startNode]);
            }
        }

        private static void Dijkstra()
        {
            var queue = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));

            foreach (var edge in graph[startNode])
            {
                distances[edge.To] = edge.Distance;
                queue.Add(edge.To);
            }

            visited = new HashSet<int> { startNode };

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();
                visited.Add(node);

                if (node == startNode)
                {
                    break;
                }

                foreach (var edge in graph[node])
                {
                    var child = edge.To;

                    if (double.IsPositiveInfinity(distances[child]))
                    {
                        queue.Add(child);
                    }

                    var newDistance = distances[node] + edge.Distance;

                    if (newDistance < distances[child])
                    {
                        distances[child] = newDistance;

                        queue = new OrderedBag<int>(
                            queue,
                            Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
                    }
                }
            }
        }

        private static void Initialize()
        {
            distances = new double[nodesCount];

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = double.PositiveInfinity;
            }
        }

        private static void ReadGraph()
        {
            graph = new List<Edge>[nodesCount];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeData[0];
                var to = edgeData[1];
                var distance = edgeData[2];

                graph[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Distance = distance
                });
            }
        }
    }
}
