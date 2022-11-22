using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver_API
{
    class SudokuAPI
    {
        public static int[,] RetrieveGrid(string Xpath)
        {
            string txt;
            int[,] res;

            txt = File.ReadAllLines(Xpath);

            Console.WriteLine(txt);

        }
    }
}