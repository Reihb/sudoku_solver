using System;
using System.IO;
using System.Collections.Generic;
using Sudoku_solver;
using Sudoku_solver_API;

//Compilation command : mcs -out:main.exe main.cs first_API.cs first_solver.cs

class Main_program
{
    int[,] list;

    list = RetrieveGrid(grid.txt);

    ShowGrid(list);
}