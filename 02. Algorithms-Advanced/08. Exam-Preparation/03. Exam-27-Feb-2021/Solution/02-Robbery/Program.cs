using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _02_Robbery
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Distance { get; set; }
    }

    // Dijkstra Algorithm
    public class Program
    {
        private static List<Edge>[] graph;
        private static bool[] cameras;
        private static int nodesCount;
        private static int edgesCount;
        private static int startNode;
        private static int endNode;
        private static double[] distances;

        public static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());
            
            ReadGraph();

            ReadCameras();

            startNode = int.Parse(Console.ReadLine());
            endNode = int.Parse(Console.ReadLine());

            Initialize();

            Dijkstra();

            Console.WriteLine(distances[endNode]);
        }

        private static void Dijkstra()
        {
            var queue = new OrderedBag<int>(
               Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));

            queue.Add(startNode);

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();

                if (node == endNode)
                {
                    break;
                }

                foreach (var edge in graph[node])
                {
                    var child = edge.First == node
                        ? edge.Second
                        : edge.First;

                    if (cameras[child])
                    {
                        continue;
                    }

                    if (double.IsPositiveInfinity(distances[child]))
                    {
                        queue.Add(child);
                    }

                    var newDistance = distances[node] + edge.Distance;

                    if (newDistance < distances[child])
                    {
                        distances[child] = newDistance;

                        queue = new OrderedBag<int>(
                            queue,
                            Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
                    }
                }
            }
        }

        private static void Initialize()
        {
            distances = new double[nodesCount];

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = double.PositiveInfinity;
            }

            distances[startNode] = 0;
        }

        private static void ReadCameras()
        {
            cameras = new bool[nodesCount];

            var line = Console.ReadLine().Split();

            for (int i = 0; i < line.Length; i++)
            {
                var blackOrWhite = line[i][1];

                if (blackOrWhite == 'b')
                {
                    cameras[i] = false;
                }
                else
                {
                    cameras[i] = true;
                }
            }
        }

        private static void ReadGraph()
        {
            graph = new List<Edge>[nodesCount];

            for (int node = 0; node < graph.Length; node++)
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
                var distance = edgeData[2];

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Distance = distance
                };

                graph[first].Add(edge);
                graph[second].Add(edge);
            }
        }
    }
}
