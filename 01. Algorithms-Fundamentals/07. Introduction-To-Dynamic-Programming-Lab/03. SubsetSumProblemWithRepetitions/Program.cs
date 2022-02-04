using System;
using System.Collections.Generic;

namespace _03._SubsetSumProblemWithRepetitions
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = new int[] { 3, 5, 2 };
            var target = 17;

            var sums = new bool[target + 1];
            sums[0] = true;

            for (int sum = 0; sum < sums.Length; sum++)
            {
                var currentSum = sums[sum];

                if (currentSum)
                {
                    foreach (var num in nums)
                    {
                        var newSum = sum + num;

                        if (newSum <= target)
                        {
                            sums[newSum] = true;
                        }
                    }
                }
            }

            while (target > 0)
            {
                foreach (var num in nums)
                {
                    var prev = target - num;

                    if (prev >= 0 && sums[prev])
                    {
                        Console.WriteLine(num);

                        target = prev;
                    }
                }
            }
        }
    }
}
