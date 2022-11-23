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
        
        char[,] sudoku_tab;
        List<int>[,] tab;

        string path = SudokuAPI.AskFilePath();

        sudoku_tab = SudokuAPI.RetrieveGrid(path);

        Sudoku.UpdateCrossNumbersTab(sudoku_tab, tab);

        SudokuAPI.ShowGrid(sudoku_tab);

        ShowListChar(tab[0,0]);
        ShowListChar(tab[0,1]);
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
}