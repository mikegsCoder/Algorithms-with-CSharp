using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._MoveDownRight
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            var numbers = new int[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                var elements = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (int c = 0; c < elements.Length; c++)
                {
                    numbers[r, c] = elements[c];
                }
            }

            // move down right
            var sums = new int[rows, cols];
            sums[0, 0] = numbers[0, 0];

            for (int c = 1; c < cols; c++)
            {
                sums[0, c] = sums[0, c - 1] + numbers[0, c];
            }

            for (int r = 1; r < rows; r++)
            {
                sums[r, 0] = sums[r - 1, 0] + numbers[r, 0];
            }

            for (int r = 1; r < rows; r++)
            {
                for (int c = 1; c < cols; c++)
                {
                    var upperCell = sums[r - 1, c];
                    var leftCell = sums[r, c - 1];

                    sums[r, c] = Math.Max(upperCell, leftCell) + numbers[r, c];
                }
            }

            // get result
            var row = rows - 1;
            var col = cols - 1;

            var path = new Stack<string>();
            path.Push($"[{row}, {col}]");

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

                path.Push($"[{row}, {col}]");
            }

            while (row > 0)
            {
                row -= 1;
                path.Push($"[{row}, {col}]");
            }

            while (col > 0)
            {
                col -= 1;
                path.Push($"[{row}, {col}]");
            }

            Console.WriteLine(string.Join(" ", path));
        }
    }
}
