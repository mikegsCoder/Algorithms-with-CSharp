using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_BellmanFord // Shortest Path in Graph with negative weights
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }
    
    public class Program
    {
        private static List<Edge> edges = new List<Edge>();
        private static double[] distances;
        private static int[] prev;
        private static Stack<int> path = new Stack<int>();
        private static int nodes;
        private static int edgesCount;
        private static int source;
        private static int destination;

        public static void Main(string[] args)
        {
            nodes = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            ReadGaph();

            source = int.Parse(Console.ReadLine());
            destination = int.Parse(Console.ReadLine());

            Initialize();

            BellmanFord();
            
            ReconstructPath();

            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distances[destination]);
        }

        private static void BellmanFord()
        {
            for (int i = 0; i < nodes - 1; i++)
            {
                var updated = false;

                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(edge.From))
                    {
                        continue;
                    }

                    var newDistance = distances[edge.From] + edge.Weight;
                    if (newDistance < distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;

                        updated = true;
                    }
                }

                if (!updated)
                {
                    break;
                }
            }

            foreach (var edge in edges)
            {
                if (double.IsPositiveInfinity(edge.From))
                {
                    continue;
                }

                var newDistance = distances[edge.From] + edge.Weight;
                if (newDistance < distances[edge.To])
                {
                    Console.WriteLine("Negative Cycle Detected");
                    System.Environment.Exit(0);
                }
            }
        }

        private static void Initialize()
        {
            distances = new double[nodes + 1];
            Array.Fill(distances, double.PositiveInfinity);

            distances[source] = 0;

            prev = new int[nodes + 1];
            Array.Fill(prev, -1);
        }

        private static void ReadGaph()
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeData[0];
                var to = edgeData[1];
                var weight = edgeData[2];

                edges.Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }
        }

        private static void ReconstructPath()
        {
            var node = destination;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }
        }
    }
}
