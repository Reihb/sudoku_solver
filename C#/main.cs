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
        char[,] sudokuGrid = SudokuAPI.RetrieveGrid(path);

        ShowGrid(sudokuGrid);

        
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
        Xtab : int[,] : sudoku grid
    
    local :
        i : int : iterator of a for loop
        j : int : iterator of a for loop
    */
    public static void ShowGrid(char[,] Xtab)
    {
        Console.WriteLine("\nSudoku grid :");
        Console.WriteLine();

        //For each line
        for(int i=0; i<Xtab.GetLength(0); i++)
        {
            //For each column
            for(int j=0; j<Xtab.GetLength(1); j++)
            {
                //Write the value at line,column
                Console.Write("[" + Xtab[i,j] + "]\t");
            }

            //Line break
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
