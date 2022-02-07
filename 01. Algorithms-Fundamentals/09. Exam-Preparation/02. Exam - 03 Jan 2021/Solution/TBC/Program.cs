using System;

namespace TBC
{
    class Program
    {
        private static int rows;
        private static int cols;
        private static char[,] matrix;

        static void Main(string[] args)
        {
            rows = int.Parse(Console.ReadLine());
            cols = int.Parse(Console.ReadLine());
            matrix = new char[rows, cols];

            ReadMatrix();

            int tunnels = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (matrix[row, col] == 't')
                    {
                        FindTunnels(row, col);
                        tunnels++;
                    }
                }
            }

            Console.WriteLine(tunnels);
        }

        private static void FindTunnels(int row, int col)
        {
            if (!IsValidMove(row, col))
            {
                return;
            }

            matrix[row, col] = 'v';

            FindTunnels(row - 1, col); // up;
            FindTunnels(row + 1, col); // down;
            FindTunnels(row, col - 1); // left;
            FindTunnels(row, col + 1); // right;
            FindTunnels(row - 1, col - 1); // up-left;
            FindTunnels(row - 1, col + 1); // up-right;
            FindTunnels(row + 1, col - 1); // down-left;
            FindTunnels(row + 1, col + 1); // down-right;
        }

        private static bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < rows && col >= 0 && col < cols && matrix[row, col] == 't';
        }

        private static void ReadMatrix()
        {
            for (int r = 0; r < rows; r++)
            {
                var line = Console.ReadLine();

                for (int c = 0; c < cols; c++)
                {
                    matrix[r, c] = line[c];
                }
            }
        }
    }
}
