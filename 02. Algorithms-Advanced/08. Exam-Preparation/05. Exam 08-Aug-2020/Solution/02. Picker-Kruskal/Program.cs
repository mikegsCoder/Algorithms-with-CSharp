using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.Picker_Kruskal // MST Kruskal Algorithm
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{First} {Second}";
        }
    }
    class Program
    {
        private static List<Edge> edges = new List<Edge>();
        private static List<Edge> sortedEdges = new List<Edge>();
        private static int edgesCount;
        private static int[] parents;
        private static HashSet<int> nodes;
        private static List<Edge> forest = new List<Edge>();

        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            ReadEdges();

            Initialize();

            Kruskal();

            PrintResult();
        }

        private static void PrintResult()
        {
            forest.ForEach(x => Console.WriteLine(x.ToString()));

            Console.WriteLine(forest.Sum(x => x.Weight));
        }

        private static void Kruskal()
        {
            foreach (var node in nodes)
            {
                parents[node] = node;
            }

            foreach (var edge in sortedEdges)
            {
                var firstNodeRoot = GetRoot(edge.First);
                var secondNodeRoot = GetRoot(edge.Second);

                if (firstNodeRoot == secondNodeRoot)
                {
                    continue;
                }

                forest.Add(edge);
                parents[firstNodeRoot] = secondNodeRoot;
            }
        }

        private static int GetRoot(int node)
        {
            while (node != parents[node])
            {
                node = parents[node];
            }

            return node;
        }

        private static void Initialize()
        {
            sortedEdges = edges.OrderBy(edge => edge.Weight).ToList();

            nodes = edges.Select(edge => edge.First)
                .Union(edges.Select(edge => edge.Second))
                .ToHashSet();

            parents = Enumerable.Repeat(-1, nodes.Max() + 1).ToArray();
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
