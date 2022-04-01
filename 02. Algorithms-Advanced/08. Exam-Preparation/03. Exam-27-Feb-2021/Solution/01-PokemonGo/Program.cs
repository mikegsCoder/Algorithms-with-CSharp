using System;
using System.Collections.Generic;

namespace _01_PokemonGo
{
    public class Street
    {
        public string Name { get; set; }

        public int Length { get; set; }

        public int Value { get; set; }
    }

    // Knapsack
    public class Program
    {
        private static List<Street> streets = new List<Street>();
        private static int[,] table;
        private static int maxFuel;
        private static SortedSet<string> selectedStreets = new SortedSet<string>();
        private static int usedFuel = 0;
        private static int totalValue = 0;

        public static void Main(string[] args)
        {
            maxFuel = int.Parse(Console.ReadLine());

            ReadInput();

            FillTable();

            Knapsack();

            PrintResult();            
        }

        private static void PrintResult()
        {
            if (selectedStreets.Count > 0)
            {
                Console.WriteLine(string.Join(" -> ", selectedStreets));
            }

            Console.WriteLine($"Total Pokemon caught -> {totalValue}");
            Console.WriteLine($"Fuel Left -> {maxFuel - usedFuel}");
        }

        private static void Knapsack()
        {
            var row = table.GetLength(0) - 1;
            var col = table.GetLength(1) - 1;

            while (row > 0 && col > 0)
            {
                if (table[row, col] != table[row - 1, col])
                {
                    var selectedItem = streets[row - 1];

                    selectedStreets.Add(selectedItem.Name);
                    usedFuel += selectedItem.Length;
                    totalValue += selectedItem.Value;

                    col -= selectedItem.Length;
                }

                row -= 1;
            }
        }

        private static void FillTable()
        {
            table = new int[streets.Count + 1, maxFuel + 1];

            for (int itemIndex = 1; itemIndex < table.GetLength(0); itemIndex++)
            {
                var currentItem = streets[itemIndex - 1];

                for (int capacity = 1; capacity < table.GetLength(1); capacity++)
                {
                    if (capacity < currentItem.Length)
                    {
                        table[itemIndex, capacity] = table[itemIndex - 1, capacity];
                    }
                    else
                    {
                        table[itemIndex, capacity] = Math.Max(
                            table[itemIndex - 1, capacity],
                            table[itemIndex - 1, capacity - currentItem.Length] + currentItem.Value);
                    }
                }
            }
        }

        private static void ReadInput()
        {
            string input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                var parts = input.Split(", ");

                var name = parts[0];
                var value = int.Parse(parts[1]);
                var lenght = int.Parse(parts[2]);

                streets.Add(new Street
                {
                    Name = name,
                    Length = lenght,
                    Value = value
                });
            }
        }
    }
}
