using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Food_Programme // Topological Sorting
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Edge>[] edges;
        private static double[] distance;
        private static int[] prev;
        private static int nodesCount;
        private static int edgesCount;
        private static int source;
        private static int destination;
        private static Stack<int> path = new Stack<int>();
        private static Stack<int> sortedNodes = new Stack<int>();

        static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            var input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            source = input[0];
            destination = input[1];

            ReaEdges();

            Initialize();
            
            TopologicalSorting();

            CalcDistances();

            ReconstructPath();

            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distance[destination]);
        }

        private static void CalcDistances()
        {
            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                foreach (var edge in edges[node])
                {
                    var child = edge.First == node
                    ? edge.Second
                    : edge.First;

                    var newDistance = edge.Weight + distance[node];

                    if (distance[child] > newDistance)
                    {
                        distance[child] = newDistance;
                        prev[child] = node;
                    }
                }
            }
        }

        private static void Initialize()
        {
            distance = Enumerable.Repeat(double.PositiveInfinity, nodesCount).ToArray();
            prev = Enumerable.Repeat(-1, nodesCount).ToArray();

            distance[source] = 0;
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
            var visited = new bool[edges.Length];

            for (int node = 0; node < edges.Length; node++)
            {
                DFS(node, visited, sortedNodes);
            }
        }

        private static void DFS(int node, bool[] visited, Stack<int> result)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var edge in edges[node])
            {
                var child = edge.First == node
                    ? edge.Second
                    : edge.First;

                DFS(child, visited, result);
            }

            result.Push(node);
        }

        private static void ReaEdges()
        {
            edges = new List<Edge>[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                edges[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var line = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var edge = new Edge
                {
                    First = line[0],
                    Second = line[1],
                    Weight = line[2]
                };
                edges[line[0]].Add(edge);
                edges[line[1]].Add(edge);
            }
        }
    }
}
