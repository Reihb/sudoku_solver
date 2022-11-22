using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver_API
{
    class SudokuAPI
    {
        public static int[,] RetrieveGrid(string Xpath)
        {
            int lin,col;
            string[] txt;
            int[,] res = new int[10,10];

            txt = File.ReadAllLines(Xpath);

            lin = 0;
            col = 0;

            foreach(string val in txt)
            {
                for(int i=0; i<val.Length; i++)
                {
                    res[lin,col] = val[i];
                    col ++;
                }

                lin++;
            }

            return res;
        }

        public static int[,] ShowGrid(int[,] tab)
        {
            for(int i=0; i<tab.GetLength(0); i++)
            {
                for(int j=0; j<tab.GetLength(1); j++)
                {
                    Console.Write("[" + tab[i,j] + "]\t");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}