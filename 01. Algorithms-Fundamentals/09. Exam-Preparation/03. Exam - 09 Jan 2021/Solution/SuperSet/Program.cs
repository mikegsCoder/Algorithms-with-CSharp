using System;
using System.Linq;

namespace SuperSet
{
    class Program
    {
        private static string[] elements;
        private static string[] combinations;
        
        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split(", ").ToArray();

            for (int i = 0; i <= elements.Length; i++)
            {
                combinations = new string[i];

                GenerateSuperSets(0, 0);
            }
        }

        private static void GenerateSuperSets(int combIdx, int elementsStartIdx)
        {
            if (combIdx >= combinations.Length)
            {
                Console.WriteLine(string.Join(" ", combinations));
                return;
            }

            for (int i = elementsStartIdx; i < elements.Length; i++)
            {
                combinations[combIdx] = elements[i];
                GenerateSuperSets(combIdx + 1, i + 1);
            }
        }
    }
}
