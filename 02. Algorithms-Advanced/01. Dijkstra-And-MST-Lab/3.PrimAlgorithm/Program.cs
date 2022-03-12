using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _3.PrimAlgorithm // Minimum Spanning Tree
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNode = new Dictionary<int, List<Edge>>();
        private static HashSet<int> forest = new HashSet<int>();
        private static int edgesCount;

        public static void Main(string[] args)
        {
            edgesCount = int.Parse(Console.ReadLine());

            ReadGraph();

            foreach (var node in edgesByNode.Keys)
            {
                if (!forest.Contains(node))
                {
                    Prim(node);
                }
            }
        }

        private static void Prim(int node)
        {
            forest.Add(node);

            var queue = new OrderedBag<Edge>(
                edgesByNode[node],
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();

                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                Console.WriteLine($"{edge.First} - {edge.Second}");

                forest.Add(nonTreeNode);
                queue.AddMany(edgesByNode[nonTreeNode]);
            }
        }

        private static int GetNonTreeNode(int first, int second)
        {
            var nonTreeNode = -1;

            if (forest.Contains(first) && !forest.Contains(second))
            {
                nonTreeNode = second;
            }
            else if (forest.Contains(second) && !forest.Contains(first))
            {
                nonTreeNode = first;
            }

            return nonTreeNode;
        }

        private static void ReadGraph()
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                if (!edgesByNode.ContainsKey(first))
                {
                    edgesByNode.Add(first, new List<Edge>());
                }

                if (!edgesByNode.ContainsKey(second))
                {
                    edgesByNode.Add(second, new List<Edge>());
                }

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                edgesByNode[first].Add(edge);
                edgesByNode[second].Add(edge);
            }
        }
    }
}