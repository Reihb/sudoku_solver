//Main program of the sudoku_solver project

using System;
using System.IO;
using System.Collections.Generic;
/*Using custom libraries
using Sudoku_solver;
using Sudoku_solver_API;
using Sudoku_solver_Debug;*/

class Main_program
{
    static void Main()
    {
        Console.Title = "sudoku_solver";

        //Declaration + initialization of the path the grid and the table of region
        string path = SudokuAPI.AskFilePath();        
        char[,] grid = SudokuAPI.RetrieveGrid(path);
        char[,] solvedGrid;
        int[,] regionTable=Sudoku.GenerateRegionTable(grid);
        int choice;

        Console.WriteLine("What solving method do you want to use ?");

        Console.WriteLine("-1 Blind Bruteforce\n-2 Possibilities aware Bruteforce\n-3 Possibilities aware + Partail resolution Bruteforce\n-4 Partial resolution with unique possibilities\n");

        choice = int.Parse(Console.ReadLine());

        if(choice == 1)
        {
            solvedGrid = Sudoku.BlindBruteforceSolve(grid, regionTable);
        }
        else if(choice == 2)
        {
            solvedGrid = Sudoku.BruteforceSolve(grid, regionTable);
        }
        else if(choice == 3)
        {
            solvedGrid = Sudoku.PartialAndBruteforceSolve(grid, regionTable);
        }
        else
        {
            solvedGrid = Sudoku.UniquePartialSolve(grid,regionTable);
        }

        Debug.ShowGrid(solvedGrid);

        SudokuAPI.InsertGridsHTML(grid, solvedGrid, path);

        Console.WriteLine("The results have been added to the HTML page");

        Console.ReadLine();
    }

    class Sudoku
    {
        /*
            BruteforceSolve : function : char[,]
                Function that solves a sudoku grid by trying every possibility combination
                Every tile is filled with a radom pick between all the possibilities
                The possibilities are calculated by a sub-program

            parameters :
                Xgrid : char[,] : sudoku grid
                XregionTable : int[,] : region table

            local :
                possibilities : List<char> : list of all the possibilities a tile can take
                currentTry : char[,] : copy of the sudoku table
                isSolvable : bool : if the sudoku isn't solvable anymore, retry
                isSolved : bool : if the sudoku isn't solved, retry
                tries : int : number of tries
                r : Random : random number
                i : int : iterator of the "for each line of the grid" loop
                j : int : iterator of the "for each column of the grid" loop

            return :
                currentTry : char[,] : solved sudoku

        */
        public static char[,] BruteforceSolve(char[,] Xgrid, int[,] XregionTable)
        {

            Console.Clear();

            Console.WriteLine("BruteforceSolve :");

            List<char> possibilities= new List<char>();

            char[,] currentTry= new char[Xgrid.GetLength(0),Xgrid.GetLength(1)];

            bool isSolvable = false;
            bool isSolved = false;

            int tries=0;

            Random r=new Random();
            
            while(!isSolved)
            {
                Copy2DTableChar(Xgrid,currentTry);

                isSolvable = true;

                for(int i=0;i<currentTry.GetLength(0) && isSolvable; i++)
                {
                    for(int j=0;j<currentTry.GetLength(1) && isSolvable; j++)
                    {
                        if(currentTry[i,j] == '0')
                        {
                            possibilities = GetPossibilitiesFromPos(currentTry,XregionTable,i,j);

                            if(possibilities.Count == 0)
                            {
                                isSolvable = false;
                            }
                            else
                            {
                                currentTry[i,j]=possibilities[r.Next(possibilities.Count)];
                            }
                        }
                    }
                }

                if(CheckIfSolved(currentTry,XregionTable))
                {
                    isSolved = true;
                }

                tries ++;
            }

            Console.WriteLine("Number of tries : " + tries);

            return currentTry;
        }

        //---------------------------------------------------------------------------------------

        /*
            BlindBruteForceSolve : function : char[,]
                Function that solves a sudoku grid by trying every possibility combination
                Every tile is filled with a radom pick between all the possibilities
                The possibilities are just all the caracters available
                The function doesn't know what caracters can or cannot be put in the tile (blind)
            
            parameters :
                Xgrid : char[,] : sudoku grid
                XregionTable : int[,] : region table
            
            local :
                possibilities : List<char> : List of all the possibilities a tile can take
                over 9 : List<char> : char to use if the size of the sudoku is over 9
                currentTry : char[,] : copy of the sudoku grid (used for each try)
                isSolved : bool : state of the sudoku
                tries : int : number of tries
                v : int : iterator of the possibilities creation loop
                i : int : iterator of the "for each line of the grid" loop
                j : int : iterator of the "for each column of the grid" loop

            return :
                currentTry : char[,] : copy of the sudoku grid (used for each try)
        */
        public static char[,] BlindBruteforceSolve(char[,] Xgrid, int[,] XregionTable)
        {
            Console.Clear();

            Console.WriteLine("BlindBruteforceSolve :");

            List<char> possibilities = new List<char>();
            List<char> over9 = new List<char>(){'A','B','C','D','E','F','G'};

            char[,] currentTry = new char[Xgrid.GetLength(0),Xgrid.GetLength(1)];
            bool isSolved = false;
            int tries = 0;

            Random r = new Random();

            for(int v = 1; v<=Xgrid.GetLength(0); v++)
            {
                if(v <= 9)
                {
                    possibilities.Add(char.Parse(v.ToString()));
                }
                else
                {
                    possibilities.Add(over9[v-10]);
                }
            }

            while(!isSolved)
            {
                Copy2DTableChar(Xgrid,currentTry);

                for(int i=0; i<currentTry.GetLength(0); i++)
                {
                    for(int j=0; j<currentTry.GetLength(1); j++)
                    {
                        if(currentTry[i,j] == '0')
                        {
                            currentTry[i,j] = possibilities[r.Next(possibilities.Count)];
                        }
                    }
                }

                if(CheckIfSolved(currentTry,XregionTable))
                {
                    isSolved = true;
                }

                tries ++;
            }

            return currentTry;
        }

        //---------------------------------------------------------------------------------------

        /*
            PartialAndBruteforceSolve : function : char[,]
                Function that first fill the tile with just one possibility and the bruteforce the sudoku

            parameters :
                Xgrid : char[,] ; sudoku grid
                XregionTable : int[,] : region table

            local :
                partiySolved : char[,] : partialy solved sudoku
                res : char[,] : result of the bruteforce

            return : 
                res : char[,] : result of the bruteforce and partial solving
        */

        public static char[,] PartialAndBruteforceSolve(char[,] Xgrid, int[,] XregionTable)
        {
            char[,] partialySolved = UniquePartialSolve(Xgrid,XregionTable);
            char[,] res = BruteforceSolve(partialySolved, XregionTable);

            Console.WriteLine("PartialAndBruteforceSolve : ");

            return res;
        }

        //---------------------------------------------------------------------------------------

        /*
            UniquePartialSolve : function : char[,]
                Function that partialy solve the sudoku by filling the tiles that have only one possibility
            
            parameters :
                Xgrid : char[,] : sudoku grid
                XregionTalbe : int[,] : region table
            
            local :
                possibilities : List<char> : List of all the possibilities a tile can take
                currentTry : char[,] : copy of the sudoku grid (used for each try)
                i : int : iterator of the "for each line of the grid" loop
                j : int : iterator of the "for each column of the grid" loop

            return :
                currentTry : char[,] : completed sudoku grid
        */
        public static char[,] UniquePartialSolve(char[,] Xgrid, int[,] XregionTable)
        {
            Console.Clear();

            Console.WriteLine("UnicPartialSolve : ");
            
            List<char> possibilities = new List<char>();
            char[,] currentTry = new char[Xgrid.GetLength(0),Xgrid.GetLength(1)];

            Copy2DTableChar(Xgrid,currentTry);

            for(int i=0; i<currentTry.GetLength(0); i++)
            {
                for(int j=0; j<currentTry.GetLength(1); j++)
                {
                    if(currentTry[i,j] == '0')
                    {
                        possibilities = GetPossibilitiesFromPos(currentTry,XregionTable,i,j);
                        if(possibilities.Count == 1)
                        {
                            currentTry[i,j] = possibilities[0];
                        }
                    }
                }
            }

            return currentTry;
        }

        //---------------------------------------------------------------------------------------

        /*
            CheckIfSolved : function : bool
                Function that ckecks if a sudoku is correctly solved by checking if each number has been used
                only once in the cross and the region

            parameters :
                Xgrid : char[,] : sudoku grid to verify
                XregionTable : int[,] : region table

            local :
                res : bool : result of the verifications
                times : int : times a particular as been used in the cross and the region
                i : int : iterator of the "for each line" loop
                j : int : iterator of the "for each column" loop
                val : char : char in the given List

            return :
                res : bool : result of the verification
        */
        public static bool CheckIfSolved(char[,] Xgrid, int[,] XregionTable)
        {
            bool res = true;
            int times = 0;
            
            for(int i=0; i<Xgrid.GetLength(0) && res; i++)
            {
                for(int j=0; j<Xgrid.GetLength(1) && res; j++)
                {
                    if(Xgrid[i,j] == '0')
                    {
                        res = false;
                    }

                    else
                    {
                        foreach(char val in GetCrossNumbers(Xgrid,i,j))
                        {
                            if(val == Xgrid[i,j])
                            {
                                times ++;
                            }
                        }

                        foreach(char val in GetRegionNumbers(Xgrid,GetRegionID(XregionTable,i,j)))
                        {
                            if(val == Xgrid[i,j])
                            {
                                times ++;
                            }
                        }

                        if(times > 2)
                        {
                            res = false;
                        }

                        times = 0;
                    }             
                }
            }

            return res;
        }

        //---------------------------------------------------------------------------------------

        /*
            GetPossibilitiesFromPos : function : List<char>
                Function that get all the possibilities that a tile can take based on its location
            
            parameters :
                Xgrid : char[,] : sudoku grid
                XregionTable : int[,] : region table
                i : int : line of the tile
                j : int : column of the tile

            local :
                cross : List<char> : List of all the numbers used in the cross
                region : List<char> : List of all the numbers used in the region
                res : List<char> : List of all the possibilities of char the tile can take

            return : 
                res : List<char> : List of all the possibilities of char the tile can take
        */
        public static List<char> GetPossibilitiesFromPos(char[,] Xgrid, int[,] XregionTable, int i, int j)
        {
            List<char> cross;
            List<char> region;
            List<char> res;

            cross = GetCrossNumbers(Xgrid, i,j);
            region = GetRegionNumbers(Xgrid, GetRegionID(XregionTable, i, j));

            res = GetPossibilitiesUnion(Xgrid,cross, region);

            return res;
        }

        //---------------------------------------------------------------------------------------

        /*
            GetCrossNumbers : function : List<char>
                Function that returns a list of all the used numbers in a line and column (cross)
            
            parameters :
                Xgrid : char[,] : sudoku grid
                i : int : line of the current box
                j : int : column of the current box
            
            local :
                res : List<char> : list of the caracters already used in the cross
                a : int : iterator of the for 

            return :
                res : List<char> : list of the caracters already used in the cross            
        */
        public static List<char> GetCrossNumbers(char[,] Xgrid, int i, int j)
        {
            List<char> res = new List<char>();

            for(int a=0; a<Xgrid.GetLength(0); a++)
            {
                if((Xgrid[i,a] != '0'))
                {
                    res.Add(Xgrid[i,a]);
                }
            }

            for(int a=0; a<Xgrid.GetLength(1); a++)
            {
                if((Xgrid[a,j] != '0') && (a != i))
                {
                    res.Add(Xgrid[a,j]);
                }
            }

            return res;
        }

        /*
            GetRegionNumbers : function : List<char>
                Function that returns a list of all the used numbers in a region (by it's ID)

            parameters :
                Xgrid : char[,] : sudoku grid
                XregionID : int : region's ID [FROM 0 TO SUDOKU LENGTH]

            local :
                res : List<char> : list of all the caracters used in the region
                regionWidth : int : width of the sudoku's regions
                regionHeight : int : height of the sudoku's regions
                i : int : line of the top left box of the region based on it's ID
                j : int : column of the top left box of the region based on it's ID
                k : int : iterator of the "line" loop
                k : int : iterator of the "column" loop
            
            return :
                res : List<char> : list of all the caracters used in the region
        */

        //On pourrait directement int??grer la table des r??gions ici 
        public static List<char> GetRegionNumbers(char[,] Xgrid, int XregionID)
        {
            List<char> res = new List<char>();

            int regionWidth, regionHeight;

            SetRegionWH(Xgrid, out regionWidth, out regionHeight);

            int i = (XregionID/regionHeight) * (regionHeight);
            int j = (regionWidth * XregionID) % (regionHeight * regionWidth);

            for(int k=0; k<regionHeight; k++)
            {
                for(int l=0; l<regionWidth; l++)
                {

                    if((Xgrid[i+k, j+l] != '0'))
                    {
                        res.Add(Xgrid[i+k, j+l]);
                    }
                }
            }

            return res;
        }

        //---------------------------------------------------------------------------------------

        /*
            GetRegionID : function : int
                Function that returns the region ID of a particular sudoku tile
            
            parameters :
                XregionTable : int[,] : region table
                i : int : line of the tile
                j : column of the tile

            local :
                res : int : region ID

            return :
                res : int : region ID
        */
        public static int GetRegionID(int[,] XregionTable, int i, int j)
        {
            int res;

            res = XregionTable[i,j];

            return res;
        }

        //---------------------------------------------------------------------------------------

        /*
            GetPossibilitiesUnion : function : List<char>
                Function that return a list of caracters off all the possibilities avalaibable given the
                list of used numbers in the cross and the region
            
            parameters :
                Xgrid : char[,] : sudoku's grid
                Xlist1 : List<char> : list of caracters used in the cross
                Xlist2 : List<char> : list of caracters used in the region
            
            local :
                res : List<char> : list numbers from 1 to sudoku's length
                i : int : iterator of the for loop
                length : int : length of the sudoku

            return : 
                res : List<char> : list of possibilities
        */
        public static List<char> GetPossibilitiesUnion(char[,] Xgrid, List<char> Xlist1, List<char> Xlist2)
        {
            List<char> res = new List<char>();
            List<char> over9 = new List<char>(){'A','B','C','D','E','F','G'};

            for(int i = 1; i<=Xgrid.GetLength(0); i++)
            {
                if(i <= 9)
                {
                    res.Add(char.Parse(i.ToString()));
                }
                else
                {
                    res.Add(over9[i-10]);
                }
            }

            int length = Xgrid.GetLength(0);

            for(int i=0; i<length; i++)
            {
                if(Xlist1.Contains(res[i]) || Xlist2.Contains(res[i]))
                {
                    res.RemoveAt(i);
                    length--;
                    i--;
                }
            }

            return res;
        }

        //---------------------------------------------------------------------------------------

        /*
            GenerateRegionTable : function : int[,]
                Function that returns the region table for a given table
            
            parameters :
                Xgrid : char[,] : the source grid

            local :
                res : int[,] : region table
                regionWidth : int : Width of the regions
                regionHeight : int : Height of the regions
                regionID : int : unique ID of the region
                i : int : iterator of the "for each line of region"
                j : int : iterator of the "for each column of region"
                k : int : iterator of the for each line of the region"
                l : int : iterator of the "for each column of the region"

            return :
                res : int[,] : region table
        */
        public static int[,] GenerateRegionTable(char[,] Xgrid)
        {
            int regionWidth, regionHeight, regionID;
            int[,] res = new int[Xgrid.GetLength(0),Xgrid.GetLength(1)];

            SetRegionWH(Xgrid, out regionWidth, out regionHeight);

            regionID = 0;

            for(int i=0; i<=(Xgrid.GetLength(0) - regionHeight); i=i+regionHeight)
            {
                for(int j=0; j<=(Xgrid.GetLength(0) - regionWidth); j=j+regionWidth)
                {
                    for(int k=i; k<(i+regionHeight); k++)
                    {
                        for(int l=j; l<(j+regionWidth); l++)
                        {
                            res[k,l] = regionID;
                        }
                    }
                    regionID++;
                }
            }

            return res;
        }

        //---------------------------------------------------------------------------------------

        /*
            SetRegionWH : procedure
                Procedure that initialize the regionWidth and regionHeight varaibles, wich are the width and the height
                of the sudoku's regions
            
            parameters :
                Xgrid : char[,] : sudoku grid
                regionWidth : int : width of the sudoku's regions
                regionHeight : int : height of the sudoku's regions
        */
        public static void SetRegionWH(char[,] Xgrid, out int regionWidth, out int regionHeight)
        {
            if(Math.Sqrt(Xgrid.GetLength(0))%1 == 0)
            {
                regionWidth = (int)Math.Sqrt(Xgrid.GetLength(0));
                regionHeight = (int)Math.Sqrt(Xgrid.GetLength(0));
            }
            else
            {
                regionWidth = Xgrid.GetLength(0) / 2;
                regionHeight = 2;
            }
        }

        //---------------------------------------------------------------------------------------

        /*
            Copy2DTableChar : procedure
                Procedure that initialize a char table by copying all the values from the first one.
            
            parameters :
                Xtab : char[,] : the table to copy
                Xtab2 : char[,] : the teable to put the values into

            local :
                i : int : iterator of the first for loop
                j : int : iterator of the second for loop
        */
        public static void Copy2DTableChar(char[,] Xtab, char[,] Xtab2)
        {
            int i,j;

            for(i=0; i<Xtab.GetLength(0); i++)
            {
                for(j=0; j<Xtab.GetLength(1); j++)
                {
                    Xtab2[i,j] = Xtab[i,j];
                }
            }
        }
    }

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

                if(content[i] == "        <!--G??n??r?? automatiquement-->")
                {
                    File.AppendAllText(PATH,"        <div class=\"tableaux\">\n");

                    for(int j=0; j<gridHTML.Count;j++)
                    {
                        File.AppendAllText(PATH,gridHTML[j] + "\n");
                    }

                    File.AppendAllText(PATH,"            <p>Difficult?? : " + gridPathWords[gridPathWords.Count-1] + "<br>Taille : "
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

    class Debug
    {
        /*
            ShowListChar : procedure
                Procedure that display a list of char in the Console
            
            parameters :
                Xlist : List<char> : list of char to display

            local :
                val : char : value of the current char in the list
        */
        public static void ShowListChar(List<char> Xlist)
        {
            Console.WriteLine();

            foreach(char val in Xlist)
            {
                Console.Write("[" + val + "]\t");
            }

            Console.WriteLine();
        }

        //---------------------------------------------------------------------------------------

        /*
        ShowGrid : procedure
            procedure that displays in the Console the sudoku grid

        parameters :
            Xgrid : int[,] : sudoku grid

        local :
            i : int : iterator of a for loop
            j : int : iterator of a for loop
        */
        public static void ShowGrid(char[,] Xgrid)
        {
            Console.WriteLine("\nSudoku grid :");
            Console.WriteLine();

            //For each line
            for(int i=0; i<Xgrid.GetLength(0); i++)
            {   
                //For each column
                for(int j=0; j<Xgrid.GetLength(1); j++)
                {
                    //Write the value at line,column
                    Console.Write("[" + Xgrid[i,j] + "]\t");
                }

                //Line break
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        //---------------------------------------------------------------------------------------

        /*
            ShowGridInt : procedure
                Procedure that displays a grid of integers in the console

            parameters :
                Xgrid : int[,] : grid of int

            local : 
                i : int : global iterator
                j : int : global iterator
        */
        public static void ShowGridInt(int[,] Xgrid)
        {
            Console.WriteLine("\nSudoku grid :");
            Console.WriteLine();

            //For each line
            for(int i=0; i<Xgrid.GetLength(0); i++)
            {   
                //For each column
                for(int j=0; j<Xgrid.GetLength(1); j++)
                {
                    //Write the value at line,column
                    Console.Write("[" + Xgrid[i,j] + "]\t");
                }

                //Line break
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}