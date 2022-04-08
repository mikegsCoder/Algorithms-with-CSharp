using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.Data_Transfer // Max Flow
{
    class Program
    {
        private static int[,] graph;
        private static int[] prev;
        private static int nodesCount;
        private static int edgesCount;
        private static int source;
        private static int destination;
        private static int maxFlow = 0;

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

            ReadGraph();

            MaxFlow();            

            Console.WriteLine(maxFlow);
        }

        private static void MaxFlow()
        {
            prev = new int[nodesCount];
            
            while (BFS())
            {
                var currentFlow = GetCurrentFlow();

                ModifyCapacities(currentFlow);

                maxFlow += currentFlow;
            }
        }

        private static void ModifyCapacities(int currentFlow)
        {
            var node = destination;

            while (node != source)
            {
                var parent = prev[node];
                graph[parent, node] -= currentFlow;
                node = parent;
            }
        }

        private static int GetCurrentFlow()
        {
            int capacity = int.MaxValue;
            var node = destination;

            while (node != source)
            {
                var parent = prev[node];
                var currentFlow = graph[parent, node];

                if (currentFlow < capacity)
                {
                     capacity=currentFlow;
                }

                node = parent;
            }

            return capacity;
        }

        private static bool BFS()
        {
            var queue = new Queue<int>();
            var visited = new bool[graph.GetLength(0)];
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == destination)
                {
                    return true;
                }

                visited[node] = true;

                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (!visited[child] && graph[node, child] > 0)
                    {
                        visited[child] = true;
                        queue.Enqueue(child);
                        prev[child] = node;
                    }
                }
            }

            return false;
        }

        private static void ReadGraph()
        {
            graph = new int[nodesCount, nodesCount];

            for (int i = 0; i < edgesCount; i++)
            {
                var line = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                int parent = line[0];
                int child = line[1];
                int capacity = line[2];

                graph[parent, child] = capacity;
            }
        }
    }
}
