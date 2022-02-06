using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.Time
{
    class Program
    {
        private static int[] first;
        private static int[] second;
        private static Stack<int> result = new Stack<int>();

        static void Main(string[] args)
        {
            first = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            second = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            Lcs();

            Console.WriteLine(string.Join(" ", result));
            Console.WriteLine(result.Count);
        }

        private static void Lcs()
        {
            var table = new int[first.Length + 1, second.Length + 1];

            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    if (first[r - 1] == second[c - 1])
                    {
                        table[r, c] = table[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        table[r, c] = Math.Max(table[r - 1, c], table[r, c - 1]);
                    }
                }
            }

            var row = first.Length;
            var col = second.Length;

            while (row > 0 && col > 0)
            {
                if (first[row - 1] == second[col - 1])
                {
                    row -= 1;
                    col -= 1;

                    result.Push(first[row]);
                }
                else if (table[row - 1, col] > table[row, col - 1])
                {
                    row -= 1;
                }
                else
                {
                    col -= 1;
                }
            }
        }
    }
}