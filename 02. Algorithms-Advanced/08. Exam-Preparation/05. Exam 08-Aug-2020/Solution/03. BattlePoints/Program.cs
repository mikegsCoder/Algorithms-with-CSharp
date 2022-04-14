using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.BattlePoints // Knapsack
{
    public class Battle
    {
        public int Cost { get; set; }

        public int Gain { get; set; }
    }

    public class Program
    {
        private static List<Battle> battles = new List<Battle>();
        private static int[,] table;
        private static int[] costs;
        private static int[] gains;
        private static int maxEnergy;

        static void Main(string[] args)
        {
            costs = Console.ReadLine().Split().Select(int.Parse).ToArray();

            gains = Console.ReadLine().Split().Select(int.Parse).ToArray();

            maxEnergy = int.Parse(Console.ReadLine());

            SelectBattles();

            Knapsack();            

            Console.WriteLine(table[battles.Count, maxEnergy]);
        }

        private static void Knapsack()
        {
            table = new int[battles.Count + 1, maxEnergy + 1];

            for (int row = 1; row < table.GetLength(0); row++)
            {
                var battle = battles[row - 1];

                for (int energy = 1; energy <= maxEnergy; energy++)
                {
                    var skip = table[row - 1, energy];

                    if (battle.Cost > energy)
                    {
                        table[row, energy] = skip;
                        continue;
                    }

                    var take = battle.Gain + table[row - 1, energy - battle.Cost];

                    table[row, energy] = Math.Max(take, skip);
                }
            }
        }

        private static void SelectBattles()
        {
            for (int i = 0; i < costs.Length; i++)
            {
                var cost = costs[i];

                if (cost <= maxEnergy)
                {
                    var gain = gains[i];

                    battles.Add(new Battle { Cost = cost, Gain = gain });
                }
            }
        }
    }
}
