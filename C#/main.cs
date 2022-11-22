//Main program of the sudoku_solver project

using System;
using System.IO;
using System.Collections.Generic;
//Using custom libraries
//using Sudoku_solver;
using Sudoku_solver_API;

class Main_program
{
    static void Main()
    {
        int[,] list;

        list = SudokuAPI.RetrieveGrid("grid.txt");

        SudokuAPI.ShowGrid(list);
    }
}