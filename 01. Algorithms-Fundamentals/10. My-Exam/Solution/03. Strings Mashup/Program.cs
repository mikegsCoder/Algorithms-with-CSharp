using System;
using System.Collections.Generic;

namespace _03._Strings_Mashup
{
    class Program
    {
        private static HashSet<string> results = new HashSet<string>();

        static void Main(string[] args)
        {
            var elements = Console.ReadLine().ToUpper();

            Permute(elements);

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        private static void Permute(string input)
        {
            int n = input.Length;

            int max = 1 << n;

            input = input.ToLower();

            for (int i = 0; i < max; i++)
            {
                var combination = input.ToCharArray();

                for (int j = 0; j < n; j++)
                {
                    if (Char.IsLetter(combination[j]))
                    {
                        if (((i >> j) & 1) == 1)
                        {
                            combination[j] = (char)(combination[j] - 32);
                        }
                    }
                }

                results.Add(new string(combination));
            }
        }
    }
}