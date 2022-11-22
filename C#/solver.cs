using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver
{
    class Sudoku
    {
        public static List<char> GetCrossNumbers(char[,] Xtab, int i, int j)
        {
            List<char> res = new List<char>();

            for(int a=0; a<Xtab.GetLength(0); a++)
            {
                if((Xtab[i,a] != 0) && (!res.Contains(Xtab[i,a])))
                {
                    res.Add(Xtab[i,a]);
                }
            }

            for(int a=0; a<Xtab.GetLength(1); a++)
            {
                if((Xtab[i,a] != 0) && (!res.Contains(Xtab[a,j])))
                {
                    res.Add(Xtab[a,j]);
                }
            }

            return res;
        }
    }
}