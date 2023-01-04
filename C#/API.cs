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

        public static List<string> GridToHTML(char[,] Xgrid)
        {
            List<string> temp = new List<string>();

            temp.Add("            <table>");

            for(int i=0; i<Xgrid.GetLength(0); i++)
            {
                temp.Add("                <tr>");

                for(int j=0; j<Xgrid.GetLength(1); j++)
                {
                    if(Xgrid[i,j] != '0')
                    {
                        temp.Add("                        <td>" + Xgrid[i,j].ToString() + "</td>");
                    }
                    else
                    {
                        temp.Add("                        <td> "+"</td>");
                    }
                }

                temp.Add("                </tr>");
            }

            temp.Add("            </table>");

            return temp;
        }

        public static void InsertGridsHTML(char[,] Xgrid, char[,] Xgrid2)
        {
            int i;
            const string PATH = "../index.html";
            string[] content = File.ReadAllLines(PATH);
            List<string> gridHTML = GridToHTML(Xgrid);
            List<string> grid2HTML = GridToHTML(Xgrid2);

            for(i=0; i<content.Length; i++)
            {
                Console.WriteLine(content[i]);
            }

            File.WriteAllText(PATH,"");

            for(i=0; i<content.Length;i++)
            {
                File.AppendAllText(PATH,content[i]);

                File.AppendAllText(PATH,"\n");

                if(content[i] == "        <p>Grilles ICI</p>")
                {
                    File.AppendAllText(PATH,"        <div class=\"tableaux\">\n");

                    for(int j=0; j<gridHTML.Count;j++)
                    {
                        File.AppendAllText(PATH,gridHTML[j]);
                        File.AppendAllText(PATH,"\n");
                    }
                    for(int k=0; k<grid2HTML.Count;k++)
                    {
                        File.AppendAllText(PATH,grid2HTML[k]);
                        File.AppendAllText(PATH,"\n");
                    }

                    File.AppendAllText(PATH,"        </div>\n");
                }
            }
        }
    }
}