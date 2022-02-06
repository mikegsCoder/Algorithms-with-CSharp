using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.TheStoryTelling
{
    class Program
    {
        private static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        private static Dictionary<string, int> dependencies = new Dictionary<string, int>();
        private static List<string> sorted = new List<string>();

        static void Main(string[] args)
        {
            ReadGraph();

            ExtractDependencies();

            TopologicalSorting();

            Console.WriteLine(string.Join(" ", sorted));
        }

        private static void TopologicalSorting()
        {
            while (dependencies.Count > 0)
            {
                var nodeToRemove = dependencies.LastOrDefault(x => x.Value == 0);

                if (nodeToRemove.Key == null)
                {
                    break;
                }

                var node = nodeToRemove.Key;
                var children = graph[node];

                sorted.Add(node);

                foreach (var child in children)
                {
                    dependencies[child] -= 1;
                }

                dependencies.Remove(nodeToRemove.Key);
            }
        }

        private static void ExtractDependencies()
        {
            foreach (var kvp in graph)
            {
                var node = kvp.Key;
                var children = kvp.Value;

                if (!dependencies.ContainsKey(node))
                {
                    dependencies.Add(node, 0);
                }

                foreach (var child in children)
                {
                    if (!dependencies.ContainsKey(child))
                    {
                        dependencies.Add(child, 1);
                    }
                    else
                    {
                        dependencies[child] += 1;
                    }
                }
            }
        }

        private static void ReadGraph()
        {
            string input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                var elements = input.Split("->", StringSplitOptions.RemoveEmptyEntries).ToArray();

                var node = elements[0].Trim();
                var children = elements.Length == 1
                    ? new string[0]
                    : elements[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

                graph.Add(node, new List<string>(children));
            }
        }
    }
}
