//Main program of the sudoku_solver project

using System;
using System.IO;
using System.Collections.Generic;
//Using custom libraries
using Sudoku_solver;
using Sudoku_solver_API;

class Main_program
{
    int[,] list;

    list = RetrieveGrid(grid.txt);

    ShowGrid(list);
}