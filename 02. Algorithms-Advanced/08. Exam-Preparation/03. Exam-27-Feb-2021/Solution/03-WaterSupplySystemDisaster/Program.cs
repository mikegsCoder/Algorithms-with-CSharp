using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_WaterSupplySystemDisaster
{
    // Articulation Points
    public class Program
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowpoint;
        private static int[] parent;
        private static bool[] visited;
        private static List<int> articulationPoints = new List<int>();
        private static int nodesCount;
        private static int separatedParts;
        private static int components;

        public static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            separatedParts = int.Parse(Console.ReadLine());

            ReadGraph();

            Initialize();

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoint(node, 1);
                }
            }

            var resultFound = false;

            foreach (var articulationPoint in articulationPoints)
            {                
                var newGraph = RemoveArticulationPoint(articulationPoint);

                var sorted = TopologicalSorting();

                components = 0;

                StronglyConnectedComponents(sorted, newGraph);

                if (components - 1 == separatedParts)
                {
                    resultFound = true;
                    Console.WriteLine(articulationPoint + 1);
                }                
            }

            if (!resultFound)
            {
                Console.WriteLine(0);
            }
        }

        private static void StronglyConnectedComponents(Stack<int> sorted, List<int>[] newGraph)
        {
            var visited = new bool[nodesCount];

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                if (visited[node])
                {
                    continue;
                }

                var component = new Stack<int>();

                DFS(node, visited, component, newGraph);

                components++;
            }
        }

        private static void Initialize()
        {
            depths = new int[nodesCount];
            lowpoint = new int[nodesCount];
            parent = new int[nodesCount];
            visited = new bool[nodesCount];

            Array.Fill(parent, -1);
        }

        private static Stack<int> TopologicalSorting()
        {
            var result = new Stack<int>();
            var visited = new bool[graph.Length];

            for (int node = 0; node < graph.Length; node++)
            {
                DFS(node, visited, result, graph);
            }

            return result;
        }

        private static void DFS(int node, bool[] visited, Stack<int> stack, List<int>[] graph)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(child, visited, stack, graph);
            }

            stack.Push(node);
        }

        private static List<int>[] RemoveArticulationPoint(int articulationPoint)
        {
            var result = new List<int>[graph.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<int>();
            }

            for (int i = 0; i < graph.Length; i++)
            {
                if (i != articulationPoint)
                {
                    foreach (var node in graph[i])
                    {
                        if (node != articulationPoint)
                        {
                            result[i].Add(node);
                        }
                    }
                }
            }

            return result;
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

            if ((parent[node] == -1 && childCount > 1) ||
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

            for (int i = 1; i < nodesCount + 1; i++)
            {
                var first = i - 1;
                
                var parts = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                for (int j = 0; j < parts.Length; j++)
                {
                    var second = parts[j] - 1;

                    graph[first].Add(second);
                    graph[second].Add(first);
                }
            }
        }
    }
}
