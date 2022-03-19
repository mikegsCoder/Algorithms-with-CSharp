using System;
using System.Linq;

namespace _02_BattlePoints
{
    public class Program // Knapsack problem
    {
        private static int[] requiredEnergy;
        private static int[] battlePoints;
        private static int initialEnergy;
        private static int[,] dp;

        public static void Main(string[] args)
        {
            requiredEnergy = Console.ReadLine().Split().Select(int.Parse).ToArray();

            battlePoints = Console.ReadLine().Split().Select(int.Parse).ToArray();

            initialEnergy = int.Parse(Console.ReadLine());

            Knapsack();

            Console.WriteLine(dp[requiredEnergy.Length, initialEnergy]);
        }

        private static void Knapsack()
        {
            var enemies = requiredEnergy.Length;

            dp = new int[enemies + 1, initialEnergy + 1];

            for (int row = 1; row < dp.GetLength(0); row++)
            {
                var enemyIdx = row - 1;
                var enemyEnergy = requiredEnergy[enemyIdx];
                var enemyBattlePoints = battlePoints[enemyIdx];

                for (int energy = 1; energy < dp.GetLength(1); energy++)
                {
                    var skip = dp[row - 1, energy];
                    if (enemyEnergy > energy)
                    {
                        dp[row, energy] = skip;
                        continue;
                    }

                    var take = enemyBattlePoints + dp[row - 1, energy - enemyEnergy];

                    dp[row, energy] = Math.Max(skip, take);
                }
            }
        }
    }
}
