using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Best_Team // Longest Increasing Subsequence
{
    class Program
    {
        private static int[] prev;
        private static int[] lengths;
        private static int maxLength;
        private static int maxLenIdx;
        private static List<int> soldiers;
        private static Stack<int> increasing;
        private static Stack<int> decreasing;

        static void Main(string[] args)
        {
            soldiers = Console.ReadLine().Split().Select(int.Parse).ToList();

            increasing = LIS(soldiers, false);

            soldiers.Reverse();

            decreasing = LIS(soldiers, true);

            PrintResult();
        }

        private static void PrintResult()
        {
            if (increasing.Count > decreasing.Count)
            {
                Console.WriteLine(string.Join(" ", increasing));

                return;
            }

            Console.WriteLine(string.Join(" ", decreasing.Reverse()));
        }

        private static Stack<int> LIS(List<int> sequence, bool reversed)
        {
            Initialize(sequence.Count);

            for (int i = 1; i <= sequence.Count - 1; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (sequence[j] <= sequence[i] && lengths[j] + 1 > lengths[i])
                    {
                        lengths[i] = lengths[i] + 1;

                        if (lengths[i] > maxLength || (lengths[i] == maxLength && reversed))
                        {
                            maxLength = lengths[i];
                            maxLenIdx = i;
                        }

                        prev[i] = j;
                    }
                }
            }

            Stack<int> result = new Stack<int>();

            int idx = maxLenIdx;

            while (idx >= 0)
            {
                result.Push(sequence[idx]);
                idx = prev[idx];
            }

            return result;
        }

        private static void Initialize(int count)
        {
            lengths = new int[count];
            prev = new int[count];

            maxLength = 0;
            maxLenIdx = 0;

            for (int i = 0; i < count; i++)
            {
                prev[i] = -1;
            }
        }
    }
}
