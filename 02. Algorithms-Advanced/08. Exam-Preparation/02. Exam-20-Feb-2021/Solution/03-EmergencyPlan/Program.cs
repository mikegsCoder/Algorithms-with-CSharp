using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Wintellect.PowerCollections;

namespace _03_EmergencyPlan
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public TimeSpan Weight { get; set; }
    }

    // Dijkstra Algorithm
    public class Program
    {
        private static int nodes;
        private static int edges;
        private static List<Edge>[] graph;
        private static double[] bestExitTime;
        private static HashSet<int> exits;
        private static Dictionary<int, HashSet<int>> roomsByExit = new Dictionary<int, HashSet<int>>();
        private static TimeSpan maxTime;

        public static void Main(string[] args)
        {
            nodes = int.Parse(Console.ReadLine());
            exits = new HashSet<int>(Console.ReadLine().Split().Select(int.Parse));
            edges = int.Parse(Console.ReadLine());

            Initialize();

            ReadGraph();

            maxTime = TimeSpan.ParseExact(Console.ReadLine(), "mm\\:ss", CultureInfo.InvariantCulture);

            Dijkstra();

            PrintResult();
        }

        private static void Initialize()
        {
            bestExitTime = new double[nodes];

            for (int room = 0; room < nodes; room++)
            {
                if (exits.Contains(room))
                {
                    bestExitTime[room] = 0;
                    roomsByExit[room] = new HashSet<int>();
                }
                else
                {
                    bestExitTime[room] = double.PositiveInfinity;
                }
            }
        }

        private static void PrintResult()
        {
            for (int room = 0; room < bestExitTime.Length; room++)
            {
                if (exits.Contains(room))
                {
                    continue;
                }

                if (double.IsPositiveInfinity(bestExitTime[room]))
                {
                    Console.WriteLine($"Unreachable {room} (N/A)");
                    continue;
                }

                var bestEvacuationTime = TimeSpan.FromSeconds(bestExitTime[room]);

                if (bestEvacuationTime > maxTime)
                {
                    Console.WriteLine($"Unsafe {room} ({bestEvacuationTime.ToString("hh\\:mm\\:ss")})");
                }
                else
                {
                    Console.WriteLine($"Safe {room} ({bestEvacuationTime.ToString("hh\\:mm\\:ss")})");
                }
            }
        }

        private static void Dijkstra()
        {
            // Go from each exit into each room
            foreach (var exit in exits)
            {
                var queue = new OrderedBag<int>(
                        Comparer<int>.Create((f, s) => bestExitTime[f].CompareTo(bestExitTime[s])));

                queue.Add(exit);

                for (int room = 0; room < nodes; room++)
                {
                    while (queue.Count > 0)
                    {
                        var node = queue.RemoveFirst();

                        roomsByExit[exit].Add(node);

                        if (node == room)
                        {
                            break;
                        }

                        foreach (var edge in graph[node])
                        {
                            var child = edge.First == node
                                ? edge.Second
                                : edge.First;

                            if (double.IsPositiveInfinity(bestExitTime[child]) ||
                                !roomsByExit[exit].Contains(child))
                            {
                                queue.Add(child);
                            }

                            var newDistance = bestExitTime[node] + edge.Weight.TotalSeconds;

                            if (newDistance < bestExitTime[child])
                            {
                                bestExitTime[child] = newDistance;

                                queue = new OrderedBag<int>(
                                    queue,
                                    Comparer<int>.Create((f, s) => bestExitTime[f].CompareTo(bestExitTime[s])));
                            }
                        }
                    }
                }
            }
        }

        private static void ReadGraph()
        {
            graph = new List<Edge>[nodes];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int e = 0; e < edges; e++)
            {
                var edgeData = Console.ReadLine().Split();

                var firstNode = int.Parse(edgeData[0]);
                var secondNode = int.Parse(edgeData[1]);
                var distance = TimeSpan.ParseExact(edgeData[2], "mm\\:ss", CultureInfo.InvariantCulture);

                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = distance
                };

                graph[firstNode].Add(edge);
                graph[secondNode].Add(edge);
            }
        }
    }
}
