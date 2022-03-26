using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_BlackFriday // MST
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    // Kruskal Algorithm
    public class Program
    {
        private static List<Edge> edges = new List<Edge>();
        private static int edgesCount;
        private static List<Edge> sortedEdges;
        private static HashSet<int> nodes;
        private static int[] parents;
        private static int cost;

        public static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            ReadEdges();

            Initialization();

            Kruskal();

            Console.WriteLine(cost);
        }

        private static void Kruskal()
        {
            foreach (var edge in sortedEdges)
            {
                var firstNodeRoot = GetRoot(parents, edge.First);
                var secondNodeRoot = GetRoot(parents, edge.Second);

                if (firstNodeRoot == secondNodeRoot)
                {
                    continue;
                }

                cost += edge.Weight;
                parents[firstNodeRoot] = secondNodeRoot;
            }
        }

        private static void Initialization()
        {
            sortedEdges = edges.OrderBy(edge => edge.Weight).ToList();

            nodes = edges.Select(edge => edge.First)
                .Union(edges.Select(edge => edge.Second))
                .ToHashSet();

            parents = Enumerable.Repeat(-1, nodes.Max() + 1).ToArray();

            foreach (var node in nodes)
            {
                parents[node] = node;
            }
        }

        private static int GetRoot(int[] parents, int node)
        {
            while (node != parents[node])
            {
                node = parents[node];
            }

            return node;
        }

        private static void ReadEdges()
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                edges.Add(new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                });
            }
        }
    }
}
