using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_BigTrip // Longest Path Algorithm
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.From} {this.To} {this.Weight}";
        }
    }

    public class Program
    {
        private static List<Edge>[] graph;
        private static int nodesCount;
        private static int edgesCount;
        private static int source;
        private static int destination;
        private static Stack<int> path = new Stack<int>();
        private static int[] prev;
        private static double[] distances;
        private static Stack<int> sortedNodes = new Stack<int>();

        public static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            ReadGraph();

            Initialize();

            LongestPath();

            ReconstructPath();

            PrintResult();
        }

        private static void PrintResult()
        {
            Console.WriteLine(distances[destination]);

            Console.WriteLine(string.Join(" ", path));
        }

        private static void LongestPath()
        {
            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                foreach (var edge in graph[node])
                {
                    var newDistance = distances[node] + edge.Weight;

                    if (newDistance > distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                    }
                }
            }
        }

        private static void Initialize()
        {
            source = int.Parse(Console.ReadLine());
            destination = int.Parse(Console.ReadLine());

            TopologicalSorting();

            distances = new double[graph.Length];
            Array.Fill(distances, double.NegativeInfinity);

            distances[source] = 0;

            prev = new int[nodesCount + 1];
            Array.Fill(prev, -1);
        }

        private static void ReconstructPath()
        {
            var node = destination;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }
        }

        private static void TopologicalSorting()
        {
            var visited = new bool[graph.Length];

            for (int node = 1; node < graph.Length; node++)
            {
                DFS(node, visited, sortedNodes);
            }
        }

        private static void DFS(int node, bool[] visited, Stack<int> sorted)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var edge in graph[node])
            {
                DFS(edge.To, visited, sorted);
            }

            sorted.Push(node);
        }

        private static void ReadGraph()
        {
            graph = new List<Edge>[nodesCount + 1];

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
                var weight = edgeData[2];

                graph[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }
        }
    }
}
