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
                if((Xtab[i,a] != '0') && (!res.Contains(Xtab[i,a])))
                {
                    res.Add(Xtab[i,a]);
                }
            }

            for(int a=0; a<Xtab.GetLength(1); a++)
            {
                if((Xtab[a,j] != '0') && (!res.Contains(Xtab[a,j])))
                {
                    res.Add(Xtab[a,j]);
                }
            }

            return res;
        }

        public static void UpdateCrossNumbersTab(char[,] Xsrc, List<char>[,] Xout)
        {
            for(int i=0; i<Xsrc.GetLength(0); i++)
            {
                for(int j=0; j<Xsrc.GetLength(1); j++)
                {
                    Xout[i,j] = GetCrossNumbers(Xsrc, i, j);
                }
            }
        }
    }
}