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

        Debug.ShowGrid(Sudoku.UniquePartialSolve(grid,regionTable));

        Console.ReadLine();
        
        Debug.ShowGrid(Sudoku.BruteforceSolve(grid,regionTable));

        Console.ReadLine();
        
        Debug.ShowGrid(Sudoku.DumbBruteforceSolve(grid, regionTable));


        /*int i,j;

        i=0;

        Debug.ShowGrid(grid);

        while(i != 10)
        {
            Console.WriteLine("Entrez i puis j");
            i = int.Parse(Console.ReadLine());
            j = int.Parse(Console.ReadLine());

            Console.WriteLine("Liste des possibilités : ");
            Debug.ShowListChar(Sudoku.GetPossibilitiesFromPos(grid, RegionTable, i, j));
            Console.WriteLine();
        }*/

        Console.ReadLine();
    }
}
