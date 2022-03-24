﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_RoadTrip
{
    public class Item
    {
        public int Value { get; set; }

        public int Weight { get; set; }
    }

    // Knapsack Algorithm
    public class Program
    {
        private static List<Item> items = new List<Item>();
        private static int[,] table;
        private static int maxCapacity;
        private static int totalValue = 0;

        public static void Main(string[] args)
        {
            ReadInput();

            maxCapacity = int.Parse(Console.ReadLine());            

            FillTable();

            Knapsack();

            Console.WriteLine($"Maximum value: {totalValue}");
        }

        private static void Knapsack()
        {
            var row = table.GetLength(0) - 1;
            var col = table.GetLength(1) - 1;

            while (row > 0 && col > 0)
            {
                if (table[row, col] != table[row - 1, col])
                {
                    var selectedItem = items[row - 1];

                    totalValue += selectedItem.Value;

                    col -= selectedItem.Weight;
                }

                row -= 1;
            }
        }

        private static void FillTable()
        {
            table = new int[items.Count + 1, maxCapacity + 1];

            for (int itemIndex = 1; itemIndex < table.GetLength(0); itemIndex++)
            {
                var currentItem = items[itemIndex - 1];

                for (int capacity = 1; capacity < table.GetLength(1); capacity++)
                {
                    if (capacity < currentItem.Weight)
                    {
                        table[itemIndex, capacity] = table[itemIndex - 1, capacity];
                    }
                    else
                    {
                        table[itemIndex, capacity] = Math.Max(
                            table[itemIndex - 1, capacity],
                            table[itemIndex - 1, capacity - currentItem.Weight] + currentItem.Value);
                    }
                }
            }
        }

        private static void ReadInput()
        {
            var values = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            var weights = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i];
                var weight = weights[i];

                items.Add(new Item 
                {
                    Value = value,
                    Weight = weight
                });
            }
        }
    }
}
