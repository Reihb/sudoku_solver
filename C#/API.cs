using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver_API
{
    class SudokuAPI
    {
        /*
        AskFilePath : procedure : string
            procedure that asks the user for a file's path until the given path is correct

        local :
            exit : boolean : condition of the while loop
            directory : string : path of the file entered by the user

        return :
            path : string : path of the file entered by the user
        */
        public static string AskFilePath()
        {

            //Declaration of the local variables
            bool exit;
            string path;

            //Initialization of the local variables
            exit = false;
            path = "";
        
            //While exit is false
            while (!exit)
            {
                //Prompting the user to enter a file's path
                Console.WriteLine("Enter a file's path : ");
                path = Console.ReadLine();

                //If the folder exists
                if (File.Exists(path))
                {
                    exit = true;
                }
                //If the file doesn't exist or the path is incorrect
                else
                {
                    //Writing a warning message before asking again
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine("/!\\ This file doesn't exist | The path is incorrect /!\\");
                }
            }

            //Remove the red color of the text
            Console.ResetColor();

            //Clear the console
            Console.Clear();

            //Returning the file's path
            return path;
        }
        
        //---------------------------------------------------------------------------------------

        /*
        RetrieveGrid : procedure : char[,]
            procedure that retrieves a sudoku grid from a txt file

        parameters :
            Xpath : string : path of the txt file
        */
        public static char[,] RetrieveGrid(string Xpath)
        {
            int l=0;
            string[] temp;

            temp = File.ReadAllLines(Xpath);

            char[,] res = new char[temp[0].Length,temp[0].Length];

            foreach(string val in temp)
            {
                for(int c=0; c<val.Length; c++)
                {
                    res[l,c] = val[c];
                }

                l++;
            }

            return res;
        }

        public static string GridToHTML(char[,] Xgrid)
        {
            string temp ="";

            temp += "            <table>";

            for(int i=0; i<Xgrid.GetLength(0); i++)
            {
                temp += "\n                <tr>";

                for(int j=0; j<Xgrid.GetLength(1); j++)
                {
                    temp += "\n                        <td>" + Xgrid[i,j].ToString() + "</td>";                    
                }

                temp += "\n                </tr>";
            }

            temp += "\n            </table>";

            return temp;
        }

        public static void InsertGridsHTML(char[,] Xgrid, char[,] Xgrid2)
        {
            const string PATH = "../index.html";

            FileStream fs = new FileStream(PATH);
        }
    }
}