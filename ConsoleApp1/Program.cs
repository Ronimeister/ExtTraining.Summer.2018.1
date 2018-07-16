using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibrary;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] map = new int[,]
            {
                { -1, -1, -1, -1, -1, -1 },
                {  0,  0,  0, -1,  0,  0 },
                { -1,  0, -1, -1,  0, -1 },
                { -1,  0, -1, -1,  0,  0 },
                {  0,  0,  0,  0,  0, -1 },
                { -1, -1, -1, -1,  -1, -1 }
            };

            int startX = 3;
            int startY = 5;

            MazeSolver solver = new MazeSolver(map, startX, startY);
            solver.PassMaze();
            solver.PrintMap();
        }
    }
}
