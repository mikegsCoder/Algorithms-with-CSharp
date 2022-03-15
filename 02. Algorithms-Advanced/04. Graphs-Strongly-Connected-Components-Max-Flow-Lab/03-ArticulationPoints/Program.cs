using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_ArticulationPoints // Hapcroft-Tarjan Algorithm
{
    public class Program
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowpoint;
        private static int[] parent;
        private static bool[] visited;
        private static List<int> articulationPoints = new List<int>();
        private static int nodesCount;
        private static int linesCount;

        public static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            linesCount = int.Parse(Console.ReadLine());

            ReadGraph();

            Initialize();

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoint(node, 1);
                }
            }

            Console.WriteLine($"Articulation points: {string.Join(", ", articulationPoints)}");
        }

        private static void Initialize()
        {
            depths = new int[nodesCount];
            lowpoint = new int[nodesCount];
            parent = new int[nodesCount];
            visited = new bool[nodesCount];

            Array.Fill(parent, -1);
        }

        private static void FindArticulationPoint(int node, int depth)
        {
            visited[node] = true;
            lowpoint[node] = depth;
            depths[node] = depth;

            var childCount = 0;
            var isArticulationPoint = false;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parent[child] = node;
                    FindArticulationPoint(child, depth + 1);
                    childCount += 1;

                    if (lowpoint[child] >= depth)
                    {
                        isArticulationPoint = true;
                    }

                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);
                }
                else if (parent[node] != child)
                {
                    lowpoint[node] = Math.Min(lowpoint[node], depths[child]);
                }
            }

            if ((parent[node] == - 1 && childCount > 1) ||
                (parent[node] != -1 && isArticulationPoint))
            {
                articulationPoints.Add(node);
            }
        }

        private static void ReadGraph()
        {
            graph = new List<int>[nodesCount];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
            }

            for (int i = 0; i < linesCount; i++)
            {
                var parts = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var first = parts[0];

                for (int j = 1; j < parts.Length; j++)
                {
                    var second = parts[j];

                    graph[first].Add(second);
                    graph[second].Add(first);
                }
            }
        }
    }
}
