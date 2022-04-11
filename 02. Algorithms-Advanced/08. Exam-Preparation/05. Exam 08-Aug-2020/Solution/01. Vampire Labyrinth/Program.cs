using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01.VampireLabyrinth // Dijkstra
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Edge>[] graph;
        private static int nodesCount;
        private static int edgesCount;
        private static int source;
        private static int destination;
        private static double[] distances;
        private static int[] prev;
        private static Stack<int> path = new Stack<int>();

        static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();

            source = input[0];
            destination = input[1];

            ReadGraph();

            Dijkstra();

            ReconstructPath();

            Console.WriteLine(string.Join(" ",path));
            Console.WriteLine(distances[destination]);
        }

        private static void ReconstructPath()
        {
            var node = destination;

            while (node!=-1)
            {
                path.Push(node);
                node = prev[node];
            }
        }

        private static void Dijkstra()
        {
            distances = Enumerable.Repeat(double.PositiveInfinity, nodesCount).ToArray();
            prev = Enumerable.Repeat(-1, nodesCount).ToArray();

            var queue = new OrderedBag<int>
                 (Comparer<int>.Create((f, s) => (int)distances[f] - (int)distances[s]));

            queue.Add(source);
            distances[source] = 0;

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();

                if (node == destination)
                {
                    break;
                }

                foreach (var edge in graph[node])
                {
                    int child = edge.From == node
                        ? edge.To
                        : edge.From;

                    if (double.IsPositiveInfinity(distances[child]))
                    {
                        queue.Add(child);
                    }

                    var newDistance = edge.Weight + distances[node];

                    if (newDistance < distances[child])
                    {
                        distances[child] = newDistance;
                        prev[child] = node;
                        queue = new OrderedBag<int>
                                (queue, Comparer<int>
                                .Create((f, s) => (int)distances[f] - (int)distances[s]));
                    }
                }
            }
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

                int from = line[0];
                int to = line[1];
                int weight = line[2];

                graph[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }
        }
    }
}
