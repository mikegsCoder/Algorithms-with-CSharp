using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Universes
{
    class Program
    {
        private static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        private static Dictionary<string, bool> visited = new Dictionary<string, bool>();
        private static int edges;

        static void Main(string[] args)
        {
            edges = int.Parse(Console.ReadLine());

            ReadGraph();
            int cnt = 0;

            foreach (var node in graph.Keys)
            {
                if (visited[node])
                {
                    continue;
                }

                var result = new List<string>();
                cnt++;

                BFS(node, result);
            }

            Console.WriteLine(cnt);
        }

        private static void BFS(string startNode, List<string> result)
        {
            var queue = new Queue<string>();
            queue.Enqueue(startNode);

            visited[startNode] = true;
            result.Add(startNode);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var child in graph[node])
                {
                    if (!visited[child])
                    {
                        if (graph.ContainsKey(child))
                        {
                            queue.Enqueue(child);
                            visited[child] = true;
                            result.Add(child);
                        }
                    }
                }
            }
        }
                
        private static void ReadGraph()
        {
            for (int i = 0; i < edges; i++)
            {
                var line = Console.ReadLine().Split(" - ").ToArray();

                if (!graph.ContainsKey(line[0]))
                {
                    graph.Add(line[0], new List<string>());
                }

                if (!graph.ContainsKey(line[1]))
                {
                    graph.Add(line[1], new List<string>());
                }

                if (!visited.ContainsKey(line[0]))
                {
                    visited.Add(line[0], false);
                }

                if (!visited.ContainsKey(line[1]))
                {
                    visited.Add(line[1], false);
                }

                graph[line[0]].Add(line[1]);
                graph[line[1]].Add(line[0]);
            }
        }
    }
}