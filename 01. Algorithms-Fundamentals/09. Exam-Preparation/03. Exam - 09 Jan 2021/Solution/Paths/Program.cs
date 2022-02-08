using System;
using System.Collections.Generic;
using System.Linq;

namespace Paths
{
    class Program
    {
        private static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        private static List<int> path;

        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());

            ReadGraph(nodesCount);

            foreach (var node in graph.Keys)
            {
                path = new List<int>();
                Dfs(node);
            }
        }

        private static void Dfs(int node)
        {
            if (path.Contains(node))
            {
                return;
            }

            path.Add(node);

            foreach (var child in graph[node])
            {
                if (!path.Contains(child))
                {
                    Dfs(child);

                    if (graph[child].Count == 0)
                    {
                        Console.WriteLine(string.Join(" ", path));
                    }

                    var last = path.Last();
                    path.Remove(last);
                }
            }
        }

        private static void ReadGraph(int nodesCount)
        {
            for (int i = 0; i < nodesCount; i++)
            {
                var input = Console.ReadLine();

                var inputData = string.IsNullOrEmpty(input)
                    ? new int[0]
                    : input.Split(" ").Select(int.Parse).ToArray();

                graph.Add(i, new List<int>(inputData));
            }
        }
    }
}
