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

        string path = SudokuAPI.AskFilePath();        
        char[,] grid = SudokuAPI.RetrieveGrid(path);
        Dictionary<string,int> regionDictionnary = Sudoku.CreateRegionDictionary(grid);
        Console.WriteLine(Sudoku.GetRegionID(regionDictionnary, 0,0));

        Debug.ShowGrid(grid);

        Console.ReadLine();
    }
}