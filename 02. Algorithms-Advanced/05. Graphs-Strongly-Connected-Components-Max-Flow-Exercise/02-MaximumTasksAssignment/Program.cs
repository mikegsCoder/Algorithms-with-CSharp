using System;
using System.Collections.Generic;

namespace _02_MaximumTasksAssignment // Max Flow Algorithm
{
    public class Program
    {
        private static int[,] graph;
        private static int[] parents;
        private static int peopleCount;
        private static int tasksCount;
        private static int start = 0;
        private static int nodes;
        private static int target;

        public static void Main(string[] args)
        {
            peopleCount = int.Parse(Console.ReadLine());
            tasksCount = int.Parse(Console.ReadLine());

            // 0, 1, 2, 3, 4, 5, 6, 7
            // s, A, B, C, 1, 2, 3, t

            Initialize();

            ConstructGraph();
            
            while (BFS())
            {
                var node = target;
                
                while (node != start)
                {
                    var parent = parents[node];

                    graph[parent, node] = 0;
                    graph[node, parent] = 1;

                    node = parent;
                }
            }

            for (int person = 1; person <= peopleCount; person++)
            {
                for (int task = peopleCount + 1; task <= peopleCount + tasksCount; task++)
                {
                    if (graph[task, person] > 0)
                    {
                        Console.WriteLine($"{ (char)(64 + person) }-{task - peopleCount}");
                    }
                }
            }
        }

        private static void Initialize()
        {
            nodes = peopleCount + tasksCount + 2;
            start = 0;
            target = nodes - 1;
            parents = new int[nodes];
            Array.Fill(parents, -1);
        }

        private static bool BFS()
        {
            var visited = new bool[graph.GetLength(0)];
            var queue = new Queue<int>();

            visited[start] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == target)
                {
                    return true;
                }

                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (!visited[child] &&
                        graph[node, child] > 0)
                    {
                        parents[child] = node;
                        visited[child] = true;
                        queue.Enqueue(child);
                    }
                }
            }

            return false;
        }

        private static void ConstructGraph()
        {           
            graph = new int[nodes, nodes];

            for (int person = 1; person <= peopleCount; person++)
            {
                graph[start, person] = 1;
            }

            for (int task = peopleCount + 1; task <= peopleCount + tasksCount; task++)
            {
                graph[task, target] = 1;
            }

            for (int person = 1; person <= peopleCount; person++)
            {
                var personCapabilities = Console.ReadLine();

                for (int task = 0; task < personCapabilities.Length; task++)
                {
                    if (personCapabilities[task] == 'Y')
                    {
                        graph[person, peopleCount + 1 + task] = 1;
                    }
                }
            }
        }
    }
}
