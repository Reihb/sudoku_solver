using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver_API
{
    class SudokuAPI
    {

        /*
        AskDirectory : procedure
            procedure that asks the user for a folders path until the given path is correct
    
        local :
            exit : boolean : condition of the while loop
            directory : string : path of the directory entered by the user
        return :
            directory : string : path of the directory entered by the user
        */
        public static string AskDirectory()
        {

            //Declaration of the local variables
            bool exit;
            string directory;

            //Initialization of the local variables
            exit = false;
            directory = "";
        
            //While exit is false
            while (!exit)
            {
                //Prompting the user to enter a folder's path
                Console.WriteLine("Enter a folder's path : ");
                directory = Console.ReadLine();

                //If the folder exists
                if (Directory.Exists(directory))
                {
                    exit = true;
                }
                //If the fodler doesn't exist or the path is incorrect
                else
                {
                    //Writing a warning message before asking again
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine("/!\\ This folder doesn't exist | The path is incorrect /!\\");
                }
            }

            //Remove the red color of the text
            Console.ResetColor();

            //Returning the directory
            return directory;
        }
        public static int[,] RetrieveGrid(string Xpath)
        {
            
        }

        public static int[,] ShowGrid(int[,] tab)
        {
            for(int i=0; i<tab.GetLength(0); i++)
            {
                for(int j=0; j<tab.GetLength(1); j++)
                {
                    Console.Write("[" + tab[i,j] + "]\t");
                }

                Console.WriteLine();
            }
        }
    }
}