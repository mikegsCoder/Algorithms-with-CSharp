using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _02.Picker_Prim // MST Prim Algorithm
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
        private static List<Edge>[] graph;
        private static HashSet<int> forest = new HashSet<int>();
        private static int nodesCount;
        private static int edgesCount;
        private static OrderedBag<Edge> included = new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

        static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            ReadGraph();

            for (int i = 0; i < nodesCount; i++)
            {
                if (!forest.Contains(i))
                {
                    Prim(i);
                }
            }

            PrintResult();
        }

        private static void PrintResult()
        {
            included.ForEach(x => Console.WriteLine(x.ToString()));

            Console.WriteLine(included.Sum(x => x.Weight));
        }

        private static void Prim(int node)
        {
            forest.Add(node);

            var queue = new OrderedBag<Edge>(graph[node], Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            
            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();
                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second);

                if (nonTreeNode != -1)
                {
                    included.Add(edge);
                    forest.Add(nonTreeNode);
                    queue.AddMany(graph[nonTreeNode]);
                }
            }
        }

        private static int GetNonTreeNode(int first, int second)
        {
            if (forest.Contains(first) && !forest.Contains(second))
            {
                return second;
            }

            if (!forest.Contains(first) && forest.Contains(second))
            {
                return first;
            }

            return -1;
        }

        private static void ReadGraph()
        {
            graph = new List<Edge>[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var line = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                int first = line[0];
                int second = line[1];
                int weight = line[2];

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