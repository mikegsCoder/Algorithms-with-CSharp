using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _03._Travel_Optimizer // Dijkstra
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    class Program
    {
        private static int edgesCount;
        private static List<Edge> graph = new List<Edge>();
        private static int start;
        private static int end;
        private static int stops;
        private static int maxNode = 0;
        private static double[] distances;
        private static int[] prev;
        private static bool[] visited;
        private static int[] stopsByNode;
        private static Stack<int> path = new Stack<int>();

        static void Main(string[] args)
        {
            edgesCount = int.Parse(Console.ReadLine());

            ReadGraph();

            start = int.Parse(Console.ReadLine());
            end = int.Parse(Console.ReadLine());
            stops = int.Parse(Console.ReadLine());

            Initialize();

            Dijkstra();

            ReconstructPath();

            PrintResult();
        }

        private static void PrintResult()
        {
            if (path.Count == 0)
            {
                Console.WriteLine("There is no such path.");

                return;
            }

            Console.WriteLine(string.Join(" ", path));
        }

        private static void Initialize()
        {
            distances = new double[maxNode + 1];
            prev = new int[maxNode + 1];
            visited = new bool[maxNode + 1];
            stopsByNode = new int[maxNode + 1];

            for (int i = 0; i <= maxNode; i++)
            {
                distances[i] = double.PositiveInfinity;
                prev[i] = -1;
                visited[i] = false;
                stopsByNode[i] = 0;
            }

            distances[start] = 0;
        }

        private static void ReconstructPath()
        {
            if (double.IsPositiveInfinity(distances[end]))
            {
                return;
            }

            var current = end;

            while (current >= 0)
            {
                path.Push(current);
                current = prev[current];
            }
        }

        private static void ReadGraph()
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var input = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                maxNode = Math.Max(maxNode, Math.Max(input[0], input[1]));

                Edge edge = new Edge
                {
                    First = input[0],
                    Second = input[1],
                    Weight = input[2]
                };

                graph.Add(edge);
            }
        }

        public static void Dijkstra()
        {
            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));

            queue.Add(start);

            while (queue.Count > 0)
            {
                var minNode = queue.RemoveFirst();
                visited[minNode] = true;

                if (minNode == end)
                {
                    break;
                }

                foreach (var edge in graph)
                {
                    if (edge.First != minNode && edge.Second != minNode)
                    {
                        continue;
                    }

                    var childNode = edge.First == minNode ? edge.Second : edge.First;

                    if (!visited[childNode] && (stopsByNode[minNode] < stops || (stopsByNode[minNode] == stops && childNode == end)))
                    {
                        queue.Add(childNode);

                        var newDistance = distances[minNode] + edge.Weight;

                        if (newDistance < distances[childNode])
                        {
                            distances[childNode] = newDistance;
                            prev[childNode] = minNode;
                            stopsByNode[childNode] = stopsByNode[minNode] + 1;

                            queue = new OrderedBag<int>(
                                queue,
                                Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
                        }
                    }
                }
            }
        }
    }
}