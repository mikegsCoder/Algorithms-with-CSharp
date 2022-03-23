using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _02_ChainLightning
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Distance { get; set; }
    }
    
    // Prim Algorithm
    public class Program
    {
        private static int nodes;
        private static int edges;
        private static int lightnings;
        private static List<Edge>[] graph;
        private static Dictionary<int, Dictionary<int, int>> treeByNode = new Dictionary<int, Dictionary<int, int>>(); 
        private static Dictionary<int, int> damageByNode = new Dictionary<int, int>(); 
        
        public static void Main(string[] args)
        {
            nodes = int.Parse(Console.ReadLine());
            edges = int.Parse(Console.ReadLine());
            lightnings = int.Parse(Console.ReadLine());

            ReadGraph();

            Prim();

            Console.WriteLine(damageByNode.Max(kvp => kvp.Value));
        }

        private static void Prim()
        {
            for (int i = 0; i < lightnings; i++)
            {
                var lightningData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var node = lightningData[0];
                var damage = lightningData[1];

                if (!treeByNode.ContainsKey(node))
                {
                    treeByNode[node] = Prim(node);
                }

                var tree = treeByNode[node];

                foreach (var kvp in tree)
                {
                    // kvp.Key -> node
                    // kvp.Value -> depth

                    var currentDamage = CalculateDamage(damage, kvp.Value);

                    if (!damageByNode.ContainsKey(kvp.Key))
                    {
                        damageByNode.Add(kvp.Key, 0);
                    }

                    damageByNode[kvp.Key] += currentDamage;
                }
            }
        }

        private static int CalculateDamage(int damage, int depth)
        {
            for (int i = 0; i < depth - 1; i++)
            {
                damage /= 2;
            }

            return damage;
        }

        private static Dictionary<int, int> Prim(int startNode)
        {
            // node -> depth
            var tree = new Dictionary<int, int>
            {
                { startNode, 1 }
            };

            var queue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Distance - s.Distance));

            queue.AddMany(graph[startNode]);

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();

                var nonTreeNode = GetNonTreeNode(tree, edge);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                var treeNode = GetTreeNode(tree, edge);

                // node -> depth
                tree.Add(nonTreeNode, tree[treeNode] + 1);

                queue.AddMany(graph[nonTreeNode]);
            }

            return tree;
        }

        private static int GetTreeNode(Dictionary<int, int> tree, Edge edge)
        {
            if (tree.ContainsKey(edge.First))
            {
                return edge.First;
            }
            else
            {
                return edge.Second;
            }
        }

        private static int GetNonTreeNode(Dictionary<int, int> tree, Edge edge)
        {
            if (tree.ContainsKey(edge.First) && 
                !tree.ContainsKey(edge.Second))
            {
                return edge.Second;
            }
            
            if (tree.ContainsKey(edge.Second) &&
                !tree.ContainsKey(edge.First))
            {
                return edge.First;
            }
            
            return -1;
        }

        private static void ReadGraph()
        {
            graph = new List<Edge>[nodes];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
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
