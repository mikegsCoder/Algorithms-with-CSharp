using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _1.DijkstraAlgorithm // Shortest Path in Graph
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNode;
        private static int[] distances;
        private static int[] prev;

        static void Main(string[] args)
        {
            var edgesCount = int.Parse(Console.ReadLine());

            ReadGraph(edgesCount);

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            Dijkstra(start, end);

            PrintResult(end);
        }

        private static void PrintResult(int end)
        {
            if (distances[end] == int.MaxValue)
            {
                Console.WriteLine("There is no such path.");
            }
            else
            {
                Console.WriteLine(distances[end]);

                ReconstructPath(end);
            }
        }

        private static void ReadGraph(int edgesCount)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var firstNode = edgeData[0];
                var secondNode = edgeData[1];
                var weight = edgeData[2];

                if (!result.ContainsKey(firstNode))
                {
                    result.Add(firstNode, new List<Edge>());
                }

                if (!result.ContainsKey(secondNode))
                {
                    result.Add(secondNode, new List<Edge>());
                }

                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = weight
                };

                result[firstNode].Add(edge);
                result[secondNode].Add(edge);
            }

            edgesByNode = result;
        }

        private static void Dijkstra(int start, int end)
        {
            var maxNode = edgesByNode.Keys.Max();

            distances = new int[maxNode + 1];

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            distances[start] = 0;

            prev = new int[maxNode + 1];
            prev[start] = -1;

            var queue = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => distances[f] - distances[s]));

            queue.Add(start);

            while (queue.Count > 0)
            {
                var minNode = queue.RemoveFirst();
                var minNodeEdges = edgesByNode[minNode];

                if (minNode == end)
                {
                    break;
                }

                foreach (var edge in minNodeEdges)
                {
                    var childNode = edge.First == minNode ? edge.Second : edge.First;

                    if (distances[childNode] == int.MaxValue)
                    {
                        queue.Add(childNode);
                    }

                    var newDistance = edge.Weight + distances[minNode];

                    if (newDistance < distances[childNode])
                    {
                        distances[childNode] = newDistance;

                        prev[childNode] = minNode;

                        queue = new OrderedBag<int>(queue, 
                            Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }
                }
            }
        }

        private static void ReconstructPath(int end)
        {
            var path = new Stack<int>();

            var node = end;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            Console.WriteLine(string.Join(" ", path));
        }
    }
}
