using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_CheapTownTour // MST -> Kruskal Algorithm
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static List<Edge> edges = new List<Edge>();
        private static int nodesCount;
        private static int edgesCount;
        private static int totalCost;

        public static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            ReadEdges();

            Kruskal();

            Console.WriteLine($"Total cost: {totalCost}");
        }

        private static void Kruskal()
        {
            var sortedEdges = edges.OrderBy(e => e.Weight).ToList();

            var root = new int[nodesCount];
            for (int node = 0; node < nodesCount; node++)
            {
                root[node] = node;
            }

            totalCost = 0;

            foreach (var edge in sortedEdges)
            {
                var firstRoot = GetRoot(edge.First, root);
                var secondRoot = GetRoot(edge.Second, root);

                if (firstRoot != secondRoot)
                {
                    root[firstRoot] = secondRoot;
                    totalCost += edge.Weight;
                }
            }
        }

        private static int GetRoot(int node, int[] root)
        {
            while (node != root[node])
            {
                node = root[node];
            }

            return node;
        }

        private static void ReadEdges()
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries)
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
