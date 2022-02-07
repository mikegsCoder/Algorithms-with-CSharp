using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    class Program
    {
        private static List<int>[] graph;

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());

            ReadGraph(nodes);

            var paths = int.Parse(Console.ReadLine());
            var result = new List<string>();

            for (int i = 0; i < paths; i++)
            {
                result.Add(CheckPaths(paths));
            }

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static string CheckPaths(int paths)
        {
            var result = new List<string>();

            var pathExists = true;
            var path = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            for (int i = 0; i < path.Length - 1; i++)
            {
                var current = path[i];
                var next = path[i + 1];

                if (!graph[current].Contains(next))
                {
                    pathExists = false;
                    break;
                }
            }

            return pathExists ? "yes" : "no";
        }

        private static void ReadGraph(int nodes)
        {
            graph = new List<int>[nodes];

            for (int i = 0; i < nodes; i++)
            {
                var input = Console.ReadLine();

                var inputData = string.IsNullOrEmpty(input)
                    ? new int[0]
                    : input.Split(" ").Select(int.Parse).ToArray();

                graph[i] = new List<int>(inputData);
            }
        }
    }
}
