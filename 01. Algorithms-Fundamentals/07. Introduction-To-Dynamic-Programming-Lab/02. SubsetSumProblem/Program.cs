using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._SubsetSumProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = new int[] { 3, 5, 1, 4, 2 };
            var target = 15;

            var sums = GetAllSums(nums);

            if (!sums.ContainsKey(target))
            {
                Console.WriteLine("Sum does not exist!");
            }
            else
            {
                while (target != 0)
                {
                    var number = sums[target];
                    target -= number;

                    Console.WriteLine(number);
                }
            }
        }

        private static Dictionary<int, int> GetAllSums(int[] nums)
        {
            var sums = new Dictionary<int, int> { { 0, 0 } };

            foreach (var num in nums)
            {
                var currentSums = sums.Keys.ToArray();

                foreach (var sum in currentSums)
                {
                    var newSum = sum + num;

                    if (!sums.ContainsKey(newSum))
                    {
                        sums.Add(newSum, num);
                    }
                }
            }

            return sums;
        }
    }
}
