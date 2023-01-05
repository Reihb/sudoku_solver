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
            
            local :
                l : int : line number
                temp : string[] : lines of the txt file
                res : char[,] : sudoku grid
                c : int : iterator of the "for each caracter" loop
            
            return :
                res : char[,] : sudoku grid
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

        //---------------------------------------------------------------------------------------

        /*
            GridToHTML : function : List<string>
                Function that converts a sudoku grid into a HTML list of strings

            parameters : 
                Xgrid : sudoku grid
            
            local : 
                temp : List<string> : list that contains all the lines of the grid in HTML
                i : int : iterator of the "for each line of the grid" loop
                j : int : iterator of the "for each column of the grid" loop

            return :
                temp : List<string> : list of the grid in HTML lines
        */
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

        //---------------------------------------------------------------------------------------

        /*
            InsertGridsHTML : procedure
                Procedure that insert the given grids into the HTML page
                The procedure doesn't really "insert" but rewrites all the html file
            
            parameters :
                Xgrid : char[,] : 1st sudoku grid
                Xgrid2 : char[,] : 2nd sudoku grid

            local : 
                i : int : general iterator
                j : int : general iterator
                c : int : general iterator
                k : int : general iterator
                PATH : const string : path of the HTML page
                content : string[] : all lines of the HTML page
                gridHTML : List<string> : lines of the 1st grid in HTML
                grid2HTML : List<string> : lines if the 2nd grid in HTML
                gridPathWords : List<string> : words of the given path
        */
        public static void InsertGridsHTML(char[,] Xgrid, char[,] Xgrid2, string XgridPath)
        {
            int i;
            string word = "";
            const string PATH = "../index.html";
            string[] content = File.ReadAllLines(PATH);
            List<string> gridHTML = GridToHTML(Xgrid);
            List<string> grid2HTML = GridToHTML(Xgrid2);
            List<string> gridPathWords = new List<string>();

            File.WriteAllText(PATH,"");

            for(int c = 0; c<XgridPath.Length; c++)
            {
                if((XgridPath[c] != '/') && (XgridPath[c] != '\\'))
                {
                    word += XgridPath[c];
                }
                else
                {
                    gridPathWords.Add(word);
                    word = "";
                }
            }

            for(i=0; i<content.Length;i++)
            {
                File.AppendAllText(PATH,content[i] + "\n");

                if(content[i] == "        <!--Généré automatiquement-->")
                {
                    File.AppendAllText(PATH,"        <div class=\"tableaux\">\n");

                    for(int j=0; j<gridHTML.Count;j++)
                    {
                        File.AppendAllText(PATH,gridHTML[j] + "\n");
                    }

                    File.AppendAllText(PATH,"            <p>Difficulté : " + gridPathWords[gridPathWords.Count-1] + "<br>Taille : "
                    + Xgrid.GetLength(0) + "x" + Xgrid.GetLength(0) + "</p>");

                    for(int k=0; k<grid2HTML.Count;k++)
                    {
                        File.AppendAllText(PATH,grid2HTML[k] + "\n");
                    }

                    File.AppendAllText(PATH,"        </div>\n");
                }
            }
        }
    }
}