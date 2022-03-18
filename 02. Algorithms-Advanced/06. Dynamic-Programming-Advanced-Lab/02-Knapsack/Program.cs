using System;
using System.Collections.Generic;

namespace _02_Knapsack
{
    public class Item
    {
        public string Name { get; set; }

        public int Weight { get; set; }

        public int Value { get; set; }
    }

    public class Program
    {
        private static List<Item> items = new List<Item>();
        private static int[,] table;
        private static int maxCapacity;
        private static int usedWeight = 0;
        private static int totalValue = 0;
        private static SortedSet<string> selectedItems = new SortedSet<string>();

        public static void Main(string[] args)
        {
            maxCapacity = int.Parse(Console.ReadLine());

            ReadInput();

            FillTable();

            Knapsack();

            PrintResult();
        }

        private static void PrintResult()
        {
            Console.WriteLine($"Total Weight: {usedWeight}");
            Console.WriteLine($"Total Value: {totalValue}");

            foreach (var itemName in selectedItems)
            {
                Console.WriteLine(itemName);
            }
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

                    selectedItems.Add(selectedItem.Name);
                    usedWeight += selectedItem.Weight;
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
            while (true)
            {
                var line = Console.ReadLine();

                if (line == "end")
                {
                    break;
                }

                var parts = line.Split();
                var name = parts[0];
                var weight = int.Parse(parts[1]);
                var value = int.Parse(parts[2]);

                items.Add(new Item
                {
                    Name = name,
                    Weight = weight,
                    Value = value
                });
            }
        }
    }
}
