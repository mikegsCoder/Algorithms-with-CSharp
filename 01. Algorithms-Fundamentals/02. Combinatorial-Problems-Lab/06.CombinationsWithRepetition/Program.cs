﻿using System;
using System.Linq;

namespace _06.CombinationsWithRepetition
{
    internal class Program
    {
        private static string[] elements;
        private static string[] combinatitions;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
            int k = int.Parse(Console.ReadLine());

            combinatitions = new string[k];

            Combinations();
        }

        private static void Combinations(int index = 0, int start = 0)
        {
            if (index >= combinatitions.Length)
            {
                Console.WriteLine(string.Join(" ", combinatitions));
                return;
            }

            for (int i = start; i < elements.Length; i++)
            {
                combinatitions[index] = elements[i];
                Combinations(index + 1, i);
            }
        }
    }
}
