using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _05_CableNetwork // MST -> Prim Algorithm
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static List<Edge>[] graph;
        private static HashSet<int> spanningTree = new HashSet<int>();
        private static int budget;
        private static int nodesCount;
        private static int edgesCount;

        public static void Main(string[] args)
        {
            budget = int.Parse(Console.ReadLine());

            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            ReadGraph();

            Console.WriteLine($"Budget used: {Prim()}");
        }

        private static int Prim()
        {
            var usedBudget = 0;

            var queue = new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            foreach (var node in spanningTree)
            {
                queue.AddMany(graph[node]);
            }

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();

                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                if (edge.Weight > budget)
                {
                    break;
                }

                usedBudget += edge.Weight;
                budget -= edge.Weight;

                spanningTree.Add(nonTreeNode);
                queue.AddMany(graph[nonTreeNode]);
            }

            return usedBudget;
        }

        private static int GetNonTreeNode(int first, int second)
        {
            var nonTreeNode = -1;

            if (spanningTree.Contains(first) &&
                !spanningTree.Contains(second))
            {
                nonTreeNode = second;
            }
            else if (spanningTree.Contains(second) &&
                !spanningTree.Contains(first))
            {
                nonTreeNode = first;
            }

            return nonTreeNode;
        }

        private static void ReadGraph()
        {
            graph = new List<Edge>[nodesCount];

            for (int node = 0; node < nodesCount; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split()
                    .ToArray();

                var first = int.Parse(edgeData[0]);
                var second = int.Parse(edgeData[1]);
                var weight = int.Parse(edgeData[2]);

                if (edgeData.Length == 4)
                {
                    spanningTree.Add(first);
                    spanningTree.Add(second);
                }

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                graph[first].Add(edge);
                graph[second].Add(edge);
            }
        }
    }
}
