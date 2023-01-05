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
        char[,] solvedGrid;
        int[,] regionTable=Sudoku.GenerateRegionTable(grid);
        int choice;

        Console.WriteLine("What solving method do you want to use ?");

        Console.WriteLine("-1 Blind Bruteforce\n-2 Possibilities aware Bruteforce\n-3 Possibilities aware + Partail resolution Bruteforce\n-4 Partial resolution with unique possibilities\n");

        choice = int.Parse(Console.ReadLine());

        if(choice == 1)
        {
            solvedGrid = Sudoku.BlindBruteforceSolve(grid, regionTable);
        }
        else if(choice == 2)
        {
            solvedGrid = Sudoku.BruteforceSolve(grid, regionTable);
        }
        else if(choice == 3)
        {
            solvedGrid = Sudoku.PartialAndBruteforceSolve(grid, regionTable);
        }
        else
        {
            solvedGrid = Sudoku.UniquePartialSolve(grid,regionTable);
        }

        Debug.ShowGrid();

        SudokuAPI.InsertGridsHTML(grid, solvedGrid, path);

        Console.WriteLine("The results have been added to the HTML page");

        Console.ReadLine();
    }
}
