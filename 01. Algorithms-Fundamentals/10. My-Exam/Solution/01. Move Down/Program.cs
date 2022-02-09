using System;
using System.Collections.Generic;

namespace _01._Move_Down
{
    class Program
    {
        private static Dictionary<string, long> cache = new Dictionary<string, long>();

        static void Main(string[] args)
        {
            cache = new Dictionary<string, long>();

            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            Console.WriteLine(NumberOfPaths(rows, cols));
        }

        private static long NumberOfPaths(int row, int col)
        {
            var id = $"{row} {col}";

            if (cache.ContainsKey(id))
            {
                return cache[id];
            }

            if (row == 1 || col == 1)
            {
                return 1;
            }

            var result = NumberOfPaths(row - 1, col) + NumberOfPaths(row, col - 1);
            cache[id] = result;

            return result;
        }
    }
}
