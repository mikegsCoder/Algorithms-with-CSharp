using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01_MostReliablePath // Dijkstra Algorithm
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.First} {this.Second} {this.Weight}";
        }
    }

    public class Program
    {
        private static List<Edge>[] graph;
        private static int nodesCount;
        private static int edgesCount;
        private static int source;
        private static int destination;
        private static double[] distances;
        private static int[] prev;

        public static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            ReadGraph();

            source = int.Parse(Console.ReadLine());
            destination = int.Parse(Console.ReadLine());
            
            Initialization();

            Dijkstra();

            Console.WriteLine($"Most reliable path reliability: {distances[destination]:F2}%");

            var path = ReconstructPath(prev, destination);
            Console.WriteLine(string.Join(" -> ", path));
        }

        private static void Dijkstra()
        {
            var queue = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f])));

            queue.Add(source);

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();

                if (node == destination)
                {
                    break;
                }

                var children = graph[node];

                foreach (var edge in children)
                {
                    var child = edge.First == node
                        ? edge.Second
                        : edge.First;

                    if (double.IsNegativeInfinity(distances[child]))
                    {
                        queue.Add(child);
                    }

                    var newDistance = distances[node] * edge.Weight / 100.0;
                    if (newDistance > distances[child])
                    {
                        distances[child] = newDistance;
                        prev[child] = node;

                        queue = new OrderedBag<int>(
                            queue,
                            Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f])));
                    }
                }
            }
        }

        private static void Initialization()
        {
            distances = new double[nodesCount];
            prev = new int[nodesCount];

            for (int node = 0; node < nodesCount; node++)
            {
                distances[node] = double.NegativeInfinity;
                prev[node] = -1;
            }

            distances[source] = 100;
        }

        private static Stack<int> ReconstructPath(int[] prev, int destination)
        {
            var path = new Stack<int>();

            while (destination != -1)
            {
                path.Push(destination);
                destination = prev[destination];
            }

            return path;
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
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

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
