using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver_Debug
{
    class Debug
    {
        /*
            ShowListChar : procedure
                Procedure that display a list of char in the Console
            
            parameters :
                Xlist : List<char> : list of char to display

            local :
                val : char : value of the current char in the list
        */
        public static void ShowListChar(List<char> Xlist)
        {
            Console.WriteLine();

            foreach(char val in Xlist)
            {
                Console.Write("[" + val + "]\t");
            }

            Console.WriteLine();
        }

        //---------------------------------------------------------------------------------------

        /*
        ShowGrid : procedure
            procedure that displays in the Console the sudoku grid

        parameters :
            Xgrid : int[,] : sudoku grid

        local :
            i : int : iterator of a for loop
            j : int : iterator of a for loop
        */
        public static void ShowGrid(char[,] Xgrid)
        {
            Console.WriteLine("\nSudoku grid :");
            Console.WriteLine();

            //For each line
            for(int i=0; i<Xgrid.GetLength(0); i++)
            {   
                //For each column
                for(int j=0; j<Xgrid.GetLength(1); j++)
                {
                    //Write the value at line,column
                    Console.Write("[" + Xgrid[i,j] + "]\t");
                }

                //Line break
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}