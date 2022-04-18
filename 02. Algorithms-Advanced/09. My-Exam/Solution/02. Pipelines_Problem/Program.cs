using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Pipelines_Problem // Maximum Task Assignment
{
    class Program
    {
        private static int agentsCount;
        private static int pipelinesCount;
        private static string[] agents;
        private static string[] pipelines;
        private static int nodesCount;
        private static int[,] graph;
        private static int startNode;
        private static int[] parents;
        private static bool[] visited;

        static void Main(string[] args)
        {
            agentsCount = int.Parse(Console.ReadLine());
            pipelinesCount = int.Parse(Console.ReadLine());

            Initialize();

            ReadAgents();

            ReadPipelines();

            ReadAssignments();            

            while (BFS())
            {
                int node = nodesCount - 1;

                while (parents[node] >= 0)
                {
                    graph[parents[node], node] = 0;
                    graph[node, parents[node]] = 1;
                    node = parents[node];
                }
            }

            PrintResult();
        }

        private static void PrintResult()
        {
            for (int i = 0; i < agentsCount; ++i)
            {
                for (int j = 0; j < pipelinesCount; ++j)
                {
                    if (graph[agentsCount + j, i] > 0)
                    {
                        Console.WriteLine($"{agents[i]} - {pipelines[j]}");
                    }
                }
            }
        }

        private static void Initialize()
        {
            agents = new string[agentsCount];
            pipelines = new string[pipelinesCount];
            nodesCount = agentsCount + pipelinesCount + 2;
            graph = new int[nodesCount, nodesCount];
            startNode = nodesCount - 2;
            parents = new int[nodesCount];

            for (int i = 0; i < agentsCount; i++)
            {
                graph[startNode, i] = 1;
            }

            for (int i = 0; i < pipelinesCount; i++)
            {
                graph[agentsCount + i, nodesCount - 1] = 1;
            }
        }

        private static void ReadAssignments()
        {
            for (int i = 0; i < agentsCount; i++)
            {
                var input = Console.ReadLine().Split(", ").ToArray();

                int agentIdx = Array.IndexOf(agents, input[0]);

                for (int j = 1; j < input.Length; j++)
                {
                    int pipelineIdx = Array.IndexOf(pipelines, input[j]);

                    graph[agentIdx, agentsCount + pipelineIdx] = 1;
                }
            }
        }

        private static void ReadPipelines()
        {
            for (int i = 0; i < pipelinesCount; i++)
            {
                pipelines[i] = Console.ReadLine();
            }
        }

        private static void ReadAgents()
        {
            for (int i = 0; i < agentsCount; i++)
            {
                agents[i] = Console.ReadLine();
            }
        }

        private static bool BFS()
        {
            for (int i = 0; i < nodesCount; i++)
            {
                parents[i] = -1;
            }

            visited = new bool[nodesCount];

            Queue<int> queue = new Queue<int>();

            queue.Enqueue(startNode);

            visited[startNode] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (!visited[i] && graph[node, i] == 1)
                    {
                        queue.Enqueue(i);
                        visited[i] = true;
                        parents[i] = node;
                    }
                }
            }

            return visited[nodesCount - 1];
        }
    }
}