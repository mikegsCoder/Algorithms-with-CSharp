using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01.Food_Programme // Dijkstra
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    class Program
    {
        private static List<Edge>[] edges;
        private static int[] prev;
        private static int nodesCount;
        private static int edgesCount;
        private static int source;
        private static int destination;
        private static double[] distances;
        private static Stack<int> path = new Stack<int>();

        static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            edgesCount = int.Parse(Console.ReadLine());

            var input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            source = input[0];
            destination = input[1];

            ReaEdges();

            Initialize();

            Dijkstra();

            ReconstructPath();

            Console.WriteLine(string.Join(" ",path));
            Console.WriteLine(distances[destination]);
        }

        private static void ReconstructPath()
        {            
            var node = destination;

            while(node!=-1)
            {
                path.Push(node);
                node = prev[node];
            }
        }
        
        private static void Dijkstra()
        {
            var queue = new OrderedBag<int>(
               Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));

            queue.Add(source);

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();

                if (node == destination)
                {
                    break;
                }

                foreach (var edge in edges[node])
                {
                    var child = edge.First == node
                        ? edge.Second
                        : edge.First;
                    
                    if (double.IsPositiveInfinity(distances[child]))
                    {
                        queue.Add(child);
                    }

                    var newDistance = distances[node] + edge.Weight;

                    if (newDistance < distances[child])
                    {
                        distances[child] = newDistance;
                        prev[child] = node;

                        queue = new OrderedBag<int>(
                            queue,
                            Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
                    }
                }
            }
        }

        private static void Initialize()
        {
            prev = Enumerable.Repeat(-1, nodesCount).ToArray();

            distances = new double[nodesCount];

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = double.PositiveInfinity;
            }

            distances[source] = 0;
        }

        private static void ReaEdges()
        {
            edges = new List<Edge>[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                edges[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var line = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var edge = new Edge
                {
                    First = line[0],
                    Second = line[1],
                    Weight = line[2]
                };

                edges[line[0]].Add(edge);
                edges[line[1]].Add(edge);
            }
        }
    }
}