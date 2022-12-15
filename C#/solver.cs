using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver
{
    class Sudoku
    {
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
                if((Xgrid[i,a] != '0') && (!res.Contains(Xgrid[i,a])))
                {
                    res.Add(Xgrid[i,a]);
                }
            }

            for(int a=0; a<Xgrid.GetLength(1); a++)
            {
                if((Xgrid[a,j] != '0') && (!res.Contains(Xgrid[a,j])))
                {
                    res.Add(Xgrid[a,j]);
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
                        for(int l=j; l<(j+regionHeight); l++)
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

        public static int GetRegionID(int[,] XregionTable, int i, int j)
        {
            int res;

            res = XregionTable[i,j];

            return res;
        }

        //---------------------------------------------------------------------------------------

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

        //On pourrait directement intégrer la table des régions ici 

        public static List<char> GetRegionNumbers(char[,] Xgrid, int XregionID)
        {
            List<char> res = new List<char>();

            int regionWidth, regionHeight;

            SetRegionWH(Xgrid, out regionWidth, out regionHeight);

            int i = (XregionID/regionHeight) * (regionHeight);
            int j = (regionHeight * XregionID) % (regionHeight * regionWidth);

            for(int k=0; k<regionHeight; k++)
            {
                for(int l=0; l<regionWidth; l++)
                {

                    if((Xgrid[i+k, j+l] != '0') && (!res.Contains(Xgrid[i+k, j+l])))
                    {
                        res.Add(Xgrid[i+k, j+l]);
                    }
                }
            }

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

            for(int i = 1; i<=Xgrid.GetLength(0); i++)
            {
                res.Add(char.Parse(i.ToString()));
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
        public static char[,] Solverr(char[,] Xgrid, int[,] XregionTable){
            random r=new Random();
            List<char> possibi= new List<char>;
            bool possible;
            bool solved=false;
            char[,] template=Copy2DTableChar(Xgrid);
            while(solved==false){
                for(int i=0;i<Xgrid.GetLength(0) && possible==true;i++){
                    for(int j=0;j<Xgrid.GetLength(1) && possible==true;j++){
                        if(Xgrid[i,j]=='0'){
                            possibi=GetPossibilitiesFromPos(Xgrid,XregionTable,i,j);
                            if(possibi.Count==0){
                                possible=false;
                            }
                            else{
                                int number=r.Next(possibi.Count-1);
                                Xgrid[i,j]=possibi[number];
                            }
                        }
                    }
                }
                for(int i=0;i<Xgrid.GetLength(0) && solved==true;i++){
                    for(int j=0;j<Xgrid.GetLength(1) && solved==true;j++){
                        if(Xgrid[i,j]=='0'){
                            solved=false;
                        }
                    }
                }
            }
            return Xgrid;
        }
    }
}
