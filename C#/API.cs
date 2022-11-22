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
        
        //---------------------------------------------------------------------------------------

        /*
        RetrieveGrid : procedure
            procedure that retrieves a sudoku grid from a txt file

        parameters :
            Xpath : string : path of the txt file
        */
        public static int[,] RetrieveGrid(string Xpath)
        {
            int l=0;
            string[] temp;

            temp = File.ReadAllLines(Xpath);

            int[,] res = new int[temp[0].Length,temp[0].Length];

            foreach(string val in temp)
            {
                for(int c=0; i<val.Length; i++)
                {
                    res[l,c] = val[c];
                }
            }

            return res;
        }

        //---------------------------------------------------------------------------------------

        /*
        ShowGrid : procedure
            procedure that displays in the Console the sudoku grid

        parameters :
            Xtab : int[,] : sudoku grid
    
        local :
            i : int : iterator of a for loop
            j : int : iterator of a for loop
        */
        public static int[,] ShowGrid(int[,] Xtab)
        {
            //For each line
            for(int i=0; i<Xtab.GetLength(0); i++)
            {
                //For each column
                for(int j=0; j<Xtab.GetLength(1); j++)
                {
                    //Write the value at line,column
                    Console.Write("[" + Xtab[i,j] + "]\t");
                }

                //Line break
                Console.WriteLine();
            }
        }
    }
}