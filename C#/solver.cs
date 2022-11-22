using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver
{
    class Sudoku
    {
        public static List<char> GetCrossNumbers(char[,] Xtab, int i, int j)
        {
            List<int> res = new List<int();

            for(int a=0; a<Xtab.GetLength(0); a++)
            {
                if(!res.Contains(Xtab[i,a]))
                {
                    res.Add(Xtab[i,a]);
                }
            }

            for(int a=0; a<Xtab.GetLength(1); a++)
            {
                if(!res.Contains(Xtab[a,j]))
                {
                    res.Add(Xtab[a,j]);
                }
            }

            return res;
        }
    }
}