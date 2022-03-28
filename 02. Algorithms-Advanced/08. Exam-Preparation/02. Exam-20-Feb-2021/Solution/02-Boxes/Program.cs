using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Boxes
{
    public class Box
    {
        public int Width { get; set; }

        public int Depth { get; set; }

        public int Height { get; set; }

        public override string ToString()
        {
            return $"{Width} {Depth} {Height}";
        }
    }

    // Longest Increasing Subsequence
    public class Program

    {
        private static Box[] boxes;
        private static Stack<Box> lis = new Stack<Box>();

        public static void Main(string[] args)
        {
            int boxesCount = int.Parse(Console.ReadLine());

            ReadBoxes(boxesCount);

            LIS();

            Console.WriteLine(string.Join(Environment.NewLine, lis));
        }

        private static void ReadBoxes(int boxesCount)
        {
            boxes = new Box[boxesCount];

            for (int i = 0; i < boxesCount; i++)
            {
                var input = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

                Box box = new Box
                {
                    Width = input[0],
                    Depth = input[1],
                    Height = input[2]
                };

                boxes[i] = box;
            }
        }

        private static void LIS()
        {
            var len = new int[boxes.Length];
            var prev = new int[boxes.Length];

            Array.Fill(prev, -1);

            var bestLen = 0;
            var lastIdx = 0;

            for (int i = 0; i < boxes.Length; i++)
            {
                var currentNumber = boxes[i];
                var currentBestSeq = 1;

                for (int j = i - 1; j >= 0; j--)
                {
                    var prevNumber = boxes[j];

                    if (prevNumber.Width < currentNumber.Width
                        && prevNumber.Depth < currentNumber.Depth
                        && prevNumber.Height < currentNumber.Height
                        && len[j] + 1 >= currentBestSeq)
                    {
                        currentBestSeq = len[j] + 1;
                        prev[i] = j;
                    }
                }

                if (currentBestSeq > bestLen)
                {
                    bestLen = currentBestSeq;
                    lastIdx = i;
                }

                len[i] = currentBestSeq;
            }

            while (lastIdx != -1)
            {
                lis.Push(boxes[lastIdx]);
                lastIdx = prev[lastIdx];
            }
        }
    }
}
