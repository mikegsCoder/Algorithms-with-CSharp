using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_LongestPath // Algorithm for Longest/Shortest path in Graph
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
        private static Stack<int> path = new Stack<int>();
        private static int nodes;
        private static int edgesCount;
        private static int source;
        private static int destination;
        private static int[] prev;
        private static double[] distances;
        private static Stack<int> sortedNodes = new Stack<int>();

        public static void Main(string[] args)
        {
            nodes = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            ReadGraph();

            source = int.Parse(Console.ReadLine());
            destination = int.Parse(Console.ReadLine());
            
            TopologicalSorting();

            Initialize();

            LongestShortestPathAlgorithm();

            Console.WriteLine(distances[destination]);

            RaconstructPath();

            Console.WriteLine(string.Join(" ", path));
        }

        private static void LongestShortestPathAlgorithm()
        {
            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                foreach (var edge in graph[node])
                {
                    var newDistance = distances[node] + edge.Weight;

                    if (newDistance > distances[edge.To]) 
                    // if (newDistance < distances[edge.To]) -> Shortest path
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                    }
                }
            }
        }

        private static void Initialize()
        {
            distances = new double[graph.Length];
            Array.Fill(distances, double.NegativeInfinity); 
            // Array.Fill(distances, double.PositiveInfinity); -> Shortest path

            distances[source] = 0;

            prev = new int[nodes + 1];
            Array.Fill(prev, -1);
        }

        private static void ReadGraph()
        {
            graph = new List<Edge>[nodes + 1];

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

        private static void RaconstructPath()
        {
            var currentNode = destination;

            while (currentNode != -1)
            {
                path.Push(currentNode);
                currentNode = prev[currentNode];
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
    }
}
