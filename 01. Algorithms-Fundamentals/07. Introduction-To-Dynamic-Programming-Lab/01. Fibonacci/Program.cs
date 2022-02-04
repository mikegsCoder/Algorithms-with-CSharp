using System;
using System.Collections.Generic;

namespace IntroductionDynamicProgramming_Lab
{
    class Program
    {
        private static Dictionary<int, long> memo;

        static void Main(string[] args)
        {
            memo = new Dictionary<int, long>();

            int n = int.Parse(Console.ReadLine());

            Console.WriteLine(GetFibonacci(n));
        }

        private static long GetFibonacci(int n)
        {
            if (memo.ContainsKey(n))
            {
                return memo[n];
            }

            if (n <= 2)
            {
                return 1;
            }

            long result = GetFibonacci(n - 1) + GetFibonacci(n - 2);
            memo[n] = result;

            return result;
        }
    }
}
