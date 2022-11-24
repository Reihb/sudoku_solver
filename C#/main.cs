//Main program of the sudoku_solver project

using System;
using System.IO;
using System.Collections.Generic;
//Using custom libraries
using Sudoku_solver;
using Sudoku_solver_API;

class Main_program
{
    static void Main()
    {
        Console.Title = "sudoku_solver";

        string path = SudokuAPI.AskFilePath();        
        char[,] grid = SudokuAPI.RetrieveGrid(path);
        List<char> test;
        List<char> test1;
        List<char> test2;

        Debug.ShowGrid(grid);

        test = Sudoku.GetCrossNumbers(grid, 0, 0);

        test1 = Sudoku.GetRegionNumbers(grid, 3);

        test2 = Sudoku.GetPossibilitiesFromList(grid, test, test1);

        Console.ReadLine();
    }
}

class Debug
{
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
