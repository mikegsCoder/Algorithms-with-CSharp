using System;
using System.Collections.Generic;
using System.Linq;

namespace Climbing
{
    class Program
    {
        private static int[,] matrix;
        private static int[,] sums;
        private static int rows;
        private static int cols;

        static void Main(string[] args)
        {
            rows = int.Parse(Console.ReadLine());
            cols = int.Parse(Console.ReadLine());

            ReadMatrix();

            MoveDownRight();

            GetResult();
        }

        private static void GetResult()
        {
            var row = rows - 1;
            var col = cols - 1;

            var path = new List<int>();
            path.Add(matrix[row, col]);

            while (row > 0 && col > 0)
            {
                var upper = sums[row - 1, col];
                var left = sums[row, col - 1];

                if (upper > left)
                {
                    row -= 1;
                }
                else
                {
                    col -= 1;
                }

                path.Add(matrix[row, col]);
            }

            while (row > 0)
            {
                row -= 1;
                path.Add(matrix[row, col]);
            }

            while (col > 0)
            {
                col -= 1;
                path.Add(matrix[row, col]);
            }

            Console.WriteLine(sums[rows - 1, cols - 1]);
            Console.WriteLine(string.Join(" ", path));
        }

        private static void MoveDownRight()
        {
            sums = new int[rows, cols];
            sums[0, 0] = matrix[0, 0];

            for (int c = 1; c < cols; c++)
            {
                sums[0, c] = sums[0, c - 1] + matrix[0, c];
            }

            for (int r = 1; r < rows; r++)
            {
                sums[r, 0] = sums[r - 1, 0] + matrix[r, 0];
            }

            for (int r = 1; r < rows; r++)
            {
                for (int c = 1; c < cols; c++)
                {
                    var upperCell = sums[r - 1, c];
                    var leftCell = sums[r, c - 1];

                    sums[r, c] = Math.Max(upperCell, leftCell) + matrix[r, c];
                }
            }
        }

        private static void ReadMatrix()
        {
            matrix = new int[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                var line = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

                for (int c = 0; c < cols; c++)
                {
                    matrix[r, c] = line[c];
                }
            }
        }
    }
}
