using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_Progra_Projet_grille
{
    class Program
    {

        //Voici un exemple de ce que font les fonctions DébutBateaux et CréerBateaux pour le cas du porte-avions

        //C'était une étape préliminaire à la création de ces deux fonctions

        //Les taches de création de  absdeb,  orddeb   et   dir  étant proches pour chaque bateau,
        //elles ont été séparées en une fonction  DébutBateaux


        /*
         public static void CréerPorteAvions(ref int [,] emplacementsBateaux)
        {
         Random random = new Random();

            
            int absdeb = random.Next( 0, 10);
            int orddeb = random.Next( 0, 10);
            int dir = 0;                             // Signification de la direction : (sens horaire depuis le haut)
                                                 //   1  -> haut
                                                 //   2 ->  droite
                                                 //   3 ->  bas
                                                 //   4 ->  gauche

            if (absdeb <4 && orddeb <4)          //en haut à gauche   -> Ne peut aller que vers le bas ou la droite
            { dir = random.Next(2, 4); }

            if (absdeb >5 && orddeb < 4)          //en haut à droite  -> Ne peut aller que vers le bas ou la gauche
            { dir = random.Next(3, 5); }

            if (absdeb < 4 && orddeb > 5)          //en bas à gauche  -> Ne peut aller que vers le haut ou la droite
            { dir = random.Next(1, 3); }

            if (absdeb > 5 && orddeb > 5)          //en bas à droite  -> Ne peut aller que vers le haut ou la gauche
            {
                dir = random.Next(1, 3);
                 if (dir ==2)
                { dir = 4; }
             }


            emplacementsBateaux[0,0] = absdeb;
            emplacementsBateaux[1,0] = orddeb;


            if (dir == 1)
            {
                for (int j = 1; j < 5; j++)
                {
                    emplacementsBateaux[0, j] = absdeb;
                    emplacementsBateaux[1, j] = orddeb - j;
                }
            }

            if (dir == 2)
            {
                for (int j = 1; j < 5; j++)
                {
                    emplacementsBateaux[0, j] = absdeb + j;
                    emplacementsBateaux[1, j] = orddeb;
                }
            }

            if (dir == 3)
            {
                for (int j = 1; j < 5; j++)
                {
                    emplacementsBateaux[0, j] = absdeb;
                    emplacementsBateaux[1, j] = orddeb + j;
                }
            }

            if (dir == 4)
            {
                for (int j = 1; j < 5; j++)
                {
                    emplacementsBateaux[0, j] = absdeb - j;
                    emplacementsBateaux[1, j] = orddeb;
                }
            }
        */

        public static void DébutBateaux(int taillebateau, ref int absdeb, ref int orddeb, ref int dir)
        {

            Random random = new Random();


            absdeb = random.Next(0, 10);
            orddeb = random.Next(0, 10);
            dir = 0;                             // Direction : (sens horaire depuis le haut)
                                                 //   1  -> haut
                                                 //   2 ->  droite
                                                 //   3 ->  bas
                                                 //   4 ->  gauche

            if (absdeb < taillebateau - 1 && orddeb < taillebateau - 1)          //en haut à gauche   -> Ne peut aller que vers le bas ou la droite
            { dir = random.Next(2, 4); }

            if (absdeb > taillebateau && orddeb < taillebateau - 1)          //en haut à droite  -> Ne peut aller que vers le bas ou la gauche
            { dir = random.Next(3, 5); }

            if (absdeb < taillebateau - 1 && orddeb > taillebateau)          //en bas à gauche  -> Ne peut aller que vers le haut ou la droite
            { dir = random.Next(1, 3); }

            if (absdeb > taillebateau && orddeb > taillebateau)          //en bas à droite  -> Ne peut aller que vers le haut ou la gauche
            {
                dir = random.Next(1, 3);
                if (dir == 2)
                { dir = 4; }
            }

        }

        public static bool DansEmplacementsBateaux(int abs, int ord, int longueur, int[,] emplacementsBateaux)
        {
            //longueur : indique jusqu'où on vérifie
            //nécessaire si on veux ne pas confondre un emplacement pas encore défini
            //et un morceau de bateau en abs = 0 et ord = 0

            for (int i = 0; i < longueur; i++)
            {
                if (emplacementsBateaux[0, i] == abs)
                {
                    if (emplacementsBateaux[1, i] == ord)
                    { return true; }
                }
            }

            return false;
        }


        public static void CréerBateaux(ref int[,] emplacementsBateaux, ref int absdeb, ref int orddeb, ref int dir)
        {


            //porte avions
            int taillebateau = 5;
            DébutBateaux(taillebateau, ref absdeb, ref orddeb, ref dir);

            emplacementsBateaux[0, 0] = absdeb;
            emplacementsBateaux[1, 0] = orddeb;


            if (dir == 1)
            {
                for (int j = 1; j < 5; j++)
                {
                    emplacementsBateaux[0, j] = absdeb;
                    emplacementsBateaux[1, j] = orddeb - j;
                }
            }

            if (dir == 2)
            {
                for (int j = 1; j < 5; j++)
                {
                    emplacementsBateaux[0, j] = absdeb + j;
                    emplacementsBateaux[1, j] = orddeb;
                }
            }

            if (dir == 3)
            {
                for (int j = 1; j < 5; j++)
                {
                    emplacementsBateaux[0, j] = absdeb;
                    emplacementsBateaux[1, j] = orddeb + j;
                }
            }

            if (dir == 4)
            {
                for (int j = 1; j < 5; j++)
                {
                    emplacementsBateaux[0, j] = absdeb - j;
                    emplacementsBateaux[1, j] = orddeb;
                }
            }


            //Cuirassé
            taillebateau = 4;
            bool erreur = true;

            while (erreur)
            {
                erreur = false;
                DébutBateaux(taillebateau, ref absdeb, ref orddeb, ref dir);

                if (DansEmplacementsBateaux(absdeb, orddeb, 5, emplacementsBateaux))   //longueur (ici 5) est la somme des tailles des bateaux précédents
                { erreur = true; }

                else
                {
                    emplacementsBateaux[0, 5] = absdeb;
                    emplacementsBateaux[1, 5] = orddeb;



                    if (dir == 1)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb;
                            emplacementsBateaux[1, j] = orddeb - (j - 5);

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 5, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }

                    if (dir == 2)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb + (j - 5);
                            emplacementsBateaux[1, j] = orddeb;

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 5, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }

                    if (dir == 3)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb;
                            emplacementsBateaux[1, j] = orddeb + (j - 5);

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 5, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }

                    if (dir == 4)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb - (j - 5);
                            emplacementsBateaux[1, j] = orddeb;

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 5, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }
                }
            }

            //Sous-marin
            taillebateau = 3;
            erreur = true;

            while (erreur)
            {
                erreur = false;
                DébutBateaux(taillebateau, ref absdeb, ref orddeb, ref dir);

                if (DansEmplacementsBateaux(absdeb, orddeb, 9, emplacementsBateaux))
                { erreur = true; }

                else
                {
                    emplacementsBateaux[0, 9] = absdeb;
                    emplacementsBateaux[1, 9] = orddeb;



                    if (dir == 1)
                    {
                        for (int j = 10; j < 12; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb;
                            emplacementsBateaux[1, j] = orddeb - (j - 9);

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 9, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }

                    if (dir == 2)
                    {
                        for (int j = 10; j < 12; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb + (j - 9);
                            emplacementsBateaux[1, j] = orddeb;

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 9, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }

                    if (dir == 3)
                    {
                        for (int j = 10; j < 12; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb;
                            emplacementsBateaux[1, j] = orddeb + (j - 9);

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 9, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }

                    if (dir == 4)
                    {
                        for (int j = 10; j < 12; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb - (j - 9);
                            emplacementsBateaux[1, j] = orddeb;

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 9, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }
                }

            }

            //Croiseur
            erreur = true;

            while (erreur)
            {
                erreur = false;
                DébutBateaux(taillebateau, ref absdeb, ref orddeb, ref dir);

                if (DansEmplacementsBateaux(absdeb, orddeb, 12, emplacementsBateaux))
                { erreur = true; }

                else
                {
                    emplacementsBateaux[0, 12] = absdeb;
                    emplacementsBateaux[1, 12] = orddeb;

                    if (dir == 1)
                    {
                        for (int j = 13; j < 15; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb;
                            emplacementsBateaux[1, j] = orddeb - (j - 12);

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 12, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }
                    if (dir == 2)
                    {
                        for (int j = 13; j < 15; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb + (j - 12);
                            emplacementsBateaux[1, j] = orddeb;

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 12, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }

                    if (dir == 3)
                    {
                        for (int j = 13; j < 15; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb;
                            emplacementsBateaux[1, j] = orddeb + (j - 12);

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 12, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }

                    if (dir == 4)
                    {
                        for (int j = 13; j < 15; j++)
                        {
                            emplacementsBateaux[0, j] = absdeb - (j - 12);
                            emplacementsBateaux[1, j] = orddeb;

                            if (DansEmplacementsBateaux(emplacementsBateaux[0, j], emplacementsBateaux[1, j], 12, emplacementsBateaux))
                            { erreur = true; }
                        }
                    }
                }

            }

            //Contre-torpilleur
            taillebateau = 2;
            erreur = true;

            while (erreur)
            {
                erreur = false;
                DébutBateaux(taillebateau, ref absdeb, ref orddeb, ref dir);

                if (DansEmplacementsBateaux(absdeb, orddeb, 15, emplacementsBateaux))
                { erreur = true; }

                else
                {
                    emplacementsBateaux[0, 15] = absdeb;
                    emplacementsBateaux[1, 15] = orddeb;



                    if (dir == 1)
                    {

                        emplacementsBateaux[0, 16] = absdeb;
                        emplacementsBateaux[1, 16] = orddeb - 1;

                        if (DansEmplacementsBateaux(emplacementsBateaux[0, 16], emplacementsBateaux[1, 16], 15, emplacementsBateaux))
                        { erreur = true; }

                    }

                    if (dir == 2)
                    {

                        emplacementsBateaux[0, 16] = absdeb + 1;
                        emplacementsBateaux[1, 16] = orddeb;

                        if (DansEmplacementsBateaux(emplacementsBateaux[0, 16], emplacementsBateaux[1, 16], 15, emplacementsBateaux))
                        { erreur = true; }

                    }

                    if (dir == 3)
                    {

                        emplacementsBateaux[0, 16] = absdeb;
                        emplacementsBateaux[1, 16] = orddeb + 1;

                        if (DansEmplacementsBateaux(emplacementsBateaux[0, 16], emplacementsBateaux[1, 16], 15, emplacementsBateaux))
                        { erreur = true; }
                    }
                }

                if (dir == 4)
                {

                    emplacementsBateaux[0, 16] = absdeb - 1;
                    emplacementsBateaux[1, 16] = orddeb;

                    if (DansEmplacementsBateaux(emplacementsBateaux[0, 16], emplacementsBateaux[1, 16], 15, emplacementsBateaux))
                    { erreur = true; }
                }
            }
        }




        /*
            int[] A = { 2 };
            int[] B = { 2 };
            if (A == B)
            {
                Console.WriteLine("true");
            }
            else { Console.WriteLine("False"); }
            Console.ReadKey();
            */




        static void affichageCarte()
        {

            /* Utiliser peut être la fonction swith(vide){ } pour pouvoir afficher l'emplacement des bateaux sans faire des if pour rien
             * en reflexion...
             * 



            // Initialisation des noms des axes (horizontal et vertical)
            string[] NomAxeHorizontal = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            string[] NomAxeVertical = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            int NumColonne = 0;

            // Initialisation d'un caractère dans la grille
            string vide = "☐";

            // Initialisation de la grille (10x10)
            //// Initialisation des couples de lignes (démarcation de grille et intérieur de la grille)
            Console.WriteLine();
            for (int ligne = 0; ligne < 10; ++ligne)
            {
                // Initialisation des colonnes: démarcation extérieur
                for (int colonne = 0; colonne < 10; ++colonne)
                {
                    if (colonne == 0)
                    {
                        Console.Write(" +---");
                    }
                    else { Console.Write("+---"); }
                }
                Console.Write("+");
                Console.WriteLine();

                // Initialisation des colonnes: intérieur
                for (int colonne = 0; colonne < 10; ++colonne)
                {
                    if (colonne == 0)
                    {
                        Console.Write("{0}| {1} ", NomAxeVertical[NumColonne], vide);
                        ++NumColonne;
                    }
                    else { Console.Write("| {0} ", vide); }
                }
                Console.Write("|");
                Console.WriteLine();
            }
            //// Initialisation des colonnes: démarcation extérieur finale
            for (int colonne = 0; colonne < 10; ++colonne)
            {
                if (colonne == 0)
                {
                    Console.Write(" +---");
                }
                else { Console.Write("+---"); }
            }
            Console.Write("+");
            Console.WriteLine();
            // // Affichage de l'axe horizontal
            for (int colonne = 0; colonne < NomAxeHorizontal.Length; ++colonne)
            {
                Console.Write("  {0} ", NomAxeHorizontal[colonne]);
            }
        }
        static int[,] initCarte()
        {
            int[,] carte = new int[10, 10];
            return carte;
        }
        static void Main(string[] args)
        {

            int[,] emplacementsBateaux = new int[17, 17];    //référence la localisation des cases qui contiennent un fragment de bateaux
                                                             // il y a donc (5 + 4 + 3x2 + 2) = 17 cases.  Soit un sous-tableau contenant les 17 abscisses et un autre contenant les 17 ordonnées
                                                             // On commence par les abscisses du bateau le plus long jusqu'au plus petit (idem pour les ordonnéees)
                                                             /*
                                                             int tirsB5 = 0;
                                                             int tirsB4 = 0;
                                                             int tirsB31 = 0;
                                                             int tirsB32 = 0;                                // Référence le nombre de fois qu'un bateau a été touché (pour chaque bateau)
                                                             int tirsB2 = 0;                                 //Evite de vérifier l'état de chaque case et permet de déterminer si un bateau est coulé
                                                             */

            affichageCarte();
            Console.ReadKey();
        }

    }
}


