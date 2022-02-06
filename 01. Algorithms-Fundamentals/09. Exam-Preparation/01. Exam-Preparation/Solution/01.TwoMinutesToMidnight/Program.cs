using System;
using System.Collections.Generic;

namespace _01.TwoMinutesToMidnight
{
    class Program
    {
        private static Dictionary<string, long> cache = new Dictionary<string, long>();

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            var result = GetBinom(n, k);

            Console.WriteLine(result);
        }

        private static long GetBinom(int row, int col)
        {
            var id = $"{row} {col}";

            if (cache.ContainsKey(id))
            {
                return cache[id];
            }

            if (col == 0 || col == row)
            {
                return 1;
            }

            var result = GetBinom(row - 1, col) + GetBinom(row - 1, col - 1);
            cache[id] = result;

            return result;
        }
    }
}
