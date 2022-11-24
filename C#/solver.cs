using System;
using System.IO;
using System.Collections.Generic;

namespace Sudoku_solver
{
    class Sudoku
    {
        public static List<char> GetCrossNumbers(char[,] Xtab, int i, int j)
        {
            List<char> res = new List<char>();

            for(int a=0; a<Xtab.GetLength(0); a++)
            {
                if((Xtab[i,a] != '0') && (!res.Contains(Xtab[i,a])))
                {
                    res.Add(Xtab[i,a]);
                }
            }

            for(int a=0; a<Xtab.GetLength(1); a++)
            {
                if((Xtab[a,j] != '0') && (!res.Contains(Xtab[a,j])))
                {
                    res.Add(Xtab[a,j]);
                }
            }

            return res;
        }
        public static int GetRegionNumbers(char[,] Xtab, int i, int j){
            List<char> resultat = new List<char>();
            List<char> possib= new List<char>();
            List<char> possibi= new List<char>();
            int longueur;
            int nb;
            bool trouve=false;
            int region=0;
            if (Xtab.GetLength(0)>9){
                nb=3;
            }
            else{
                nb=2;
            }
            if (Xtab.GetLength(0)%2==0){
                if (Xtab.GetLength(0)<16){
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
                        possib.Add(3,5);
                    }
                    else{
                        possib.Add(3,6,9);
                    }
                }
                else{
                    if(nb==2){
                        possib.Add(2,4);
                    }
                    else{
                        possib.Add(2,5,8);
                    }
                }
            }
            else{
                if(nb==2){
                    possib.Add(1,3);
                }
                else{
                    possib.Add(1,4,7);
                }
            }
            if (j>longueur-1){
                if(j>longueur-1*2){
                    if(j>longueur-1*3){
                        possib.Add(3,5);
                    }
                    else{
                        possib.Add(3,6,9);
                    }
                }
                else{
                    if(nb==2){
                        possib.Add(2,4);
                    }
                    else{
                        possib.Add(2,5,8);
                    }
                }
            }
            else{
                if(nb==2){
                    possib.Add(1,3);
                }
                else{
                    possib.Add(1,4,7);
                }
            }
            foreach(int value in possib){
                foreach(int nombre in possibi){
                    if (value==nombre){
                        trouve=true;
                    }
                }
                if (trouve==false){
                region=value;
                break;
                }
                else{
                    trouve=false;
                }

            }
            return region;
        }
    }
}
