﻿using System;
using System.Linq;

namespace _06.ConnectingCables
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cables = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[,] table = new int[cables.Length + 1, cables.Length + 1];

            for (int row = 1; row < table.GetLength(0); row++)
            {
                for (int col = 1; col < table.GetLength(1); col++)
                {
                    var cable = cables[row - 1];

                    if (cable == col)
                    {
                        table[row, col] = table[row - 1,col - 1] + 1;
                    }
                    else
                    {
                        table[row, col] = Math.Max(table[row, col - 1], table[row - 1, col]);
                    }
                }
            }

            Console.WriteLine($"Maximum pairs connected: {table[cables.Length, cables.Length]}");
        }
    }
}
