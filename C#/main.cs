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
        List<char>[,] tab = new List<char>[sudoku_tab.GetLength(0),sudoku_tab.GetLength(1)];

        string path = SudokuAPI.AskFilePath();

        sudoku_tab = SudokuAPI.RetrieveGrid(path);

        Sudoku.UpdateCrossNumbersTab(sudoku_tab, tab);

        SudokuAPI.ShowGrid(sudoku_tab);

        Debug.ShowListChar(tab[0,0]);
        Debug.ShowListChar(tab[0,1]);

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
}