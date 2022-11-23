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
        
        char[,] list;

        string path = SudokuAPI.AskFilePath();

        list = SudokuAPI.RetrieveGrid(path);

        SudokuAPI.ShowGrid(list);

        Console.WriteLine("GetCrossNumber [0,0]");

        Debug.ShowListChar(Sudoku.GetCrossNumbers(list, 0, 0));

        Console.ReadLine();
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