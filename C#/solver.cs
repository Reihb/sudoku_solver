using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver
{
    class Sudoku
    {
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

        public static List<char> GetRegionNumbers(char[,] Xgrid, int XregionID)
        {
            List<char> res = new List<char>();

            SetRegionWH(out regionWidth, out regionWidth);

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

        // SI racine pile utiliser la racine sinon diviser par 2 puis par 2
        /*
        public static int GetRegionNumbers(char[,] Xgrid, int i, int j){
            List<char> resultat = new List<char>();
            List<int> possib= new List<int>();
            List<int> possibi= new List<int>();
            int longueur;
            int nb;
            bool trouve=false;
            int region=0;
            if (Xgrid.GetLength(0)>9){
                nb=3;
            }
            else{
                nb=2;
            }
            if (Xgrid.GetLength(0)%2==0){
                if (Xgrid.GetLength(0)<16){
                    longueur=2;
                }
                else{
                    longueur=4;
                }
            }
            else{
                longueur=3;
            }
            if (i>longueur-1){
                if(i>longueur-1*2){
                    if(i>longueur-1*3){
                        if(nb==2){
                            possib.Add(6);
                            possib.Add(7);
                        }
                        else{
                            possib.Add(7);
               
                if(j>longueur-1*2){
                    if(j>longueur-1*3){
                        if (nb==2){
                        possibi.Add(3);
                        possibi.Add(5);
                        }
                        else{
                            possibi.Add(3);
                            possibi.Add(6);
                            possibi.Add(9);
                        }
                    }
                }
                else{
                    if(nb=             possib.Add(8);
                            possib.Add(9);
                        }
                    }
                }
                else{
                    if(nb==2){
                        possib.Add(3);
                        possib.Add(4);
                    }
                    else{
                        possib.Add(4);
                        possib.Add(5);
                        possib.Add(6);
                    }
                }
            }
            else{
                if(nb==2){
                    possib.Add(1);
                    possib.Add(2);
                }
                else{
                    possib.Add(1);
                    possib.Add(2);
                    possib.Add(3);
                }
            }
            if (j>longueur-1){=2){
                        possibi.Add(2);
                        possibi.Add(4);
                    }
                    else{
                        possibi.Add(2);
                        possibi.Add(5);
                        possibi.Add(8);
                    }
                }
            }
            else{
                if(nb==2){
                    possibi.Add(1);
                    possibi.Add(3);
                }
                else{
                    possibi.Add(1);
                    possibi.Add(4);
                    possibi.Add(7);
                }
            }
            foreach(int value in possib){
                foreach(int nombre in possibi){
                    if (value==nombre){
                        trouve=true;
                    }
                }
                if (trouve==true){
                region=value;
                }
            }
            Console.WriteLine(nb,longueur);
            return region;
        }*/
    }
}
