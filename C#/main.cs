//Main program of the sudoku_solver project

using System;
using System.IO;
using System.Collections.Generic;
//Using custom libraries
using Sudoku_solver;
using Sudoku_solver_API;
using Sudoku_solver_Debug;

class Main_program
{
    static void Main()
    {
        Console.Title = "sudoku_solver";

        //Declaration + initialization of the path the grid and the table of region
        string path = SudokuAPI.AskFilePath();        
        char[,] grid = SudokuAPI.RetrieveGrid(path);
        int[,] regionTable=Sudoku.GenerateRegionTable(grid);

        while(input != 4)
        {
            //Choix de la résolution
        }

        Debug.ShowGrid(Sudoku.UniquePartialSolve(grid,regionTable));

        
        Debug.ShowGrid(Sudoku.DumbBruteforceSolve(grid, regionTable));

        Console.ReadLine();
    }
}
