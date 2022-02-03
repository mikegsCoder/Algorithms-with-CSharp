﻿using System;
using System.Collections.Generic;

namespace _02.AreasInMatrix
{
    public class Node
    {
        public int Row { get; set; }

        public int Col { get; set; }
    }

    class Program
    {
        private static char[,] matrix;
        private static bool[,] visited;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());

            matrix = ReadMatrix(n, m);
            visited = new bool[n, m];
                        
            var areas = new SortedDictionary<char, int>();
            int totalAreas = 0;

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (visited[r, c])
                    {
                        continue;
                    }

                    DFS(r, c);

                    totalAreas += 1;

                    var key = matrix[r, c];

                    if (!areas.ContainsKey(key))
                    {
                        areas.Add(key, 1);
                    }
                    else
                    {
                        areas[key] += 1;
                    }
                }
            }

            Console.WriteLine($"Areas: {totalAreas}");

            foreach (var area in areas)
            {
                Console.WriteLine($"Letter '{area.Key}' -> {area.Value}");
            }
        }

        private static void DFS(int row, int col)
        {
            visited[row, col] = true;

            var children = GetChildren(row, col);

            foreach (var child in children)
            {
                DFS(child.Row, child.Col);
            }
        }

        private static List<Node> GetChildren(int row, int col)
        {
            var children = new List<Node>();

            if (IsInside(row - 1, col) && IsChild(row, col, row - 1, col) && !IsVisited(row - 1, col))
            {
                children.Add(new Node { Row = row - 1, Col = col });
            }

            if (IsInside(row + 1, col) && IsChild(row, col, row + 1, col) && !IsVisited(row + 1, col))
            {
                children.Add(new Node { Row = row + 1, Col = col });
            }

            if (IsInside(row, col - 1) && IsChild(row, col, row, col - 1) && !IsVisited(row, col - 1))
            {
                children.Add(new Node { Row = row, Col = col - 1 });
            }

            if (IsInside(row, col + 1) && IsChild(row, col, row, col + 1) && !IsVisited(row, col + 1))
            {
                children.Add(new Node { Row = row, Col = col + 1 });
            }

            return children;
        }

        private static bool IsVisited(int row, int col)
        {
            return visited[row, col];
        }

        private static bool IsChild(int row1, int col1, int row2, int col2)
        {
            return matrix[row1, col1] == matrix[row2, col2];
        }

        private static bool IsInside(int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0)
                && col >= 0 && col < matrix.GetLength(1);
        }

        private static char[,] ReadMatrix(int row, int col)
        {
            var result = new char[row, col];

            for (int r = 0; r < row; r++)
            {
                var elements = Console.ReadLine();

                for (int c = 0; c < elements.Length; c++)
                {
                    result[r, c] = elements[c];
                }
            }

            return result;
        }
    }
}