using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_Progra_Projet_grille
{
    class Program
    {
        //La fonction DébutBateaux crée une première case occupée par le bateau ainsi qu'une direction vers laquelle il s'étend

        public static void DébutBateaux(int taillebateau, ref int absdeb, ref int orddeb, ref int dir)
        {

            Random random = new Random();


            absdeb = random.Next(0, 10);
            orddeb = random.Next(0, 10);
            dir = random.Next(1, 5);                             // Direction : (sens horaire depuis le haut)
                                                                 //   1  -> haut
                                                                 //   2 ->  droite
                                                                 //   3 ->  bas
                                                                 //   4 ->  gauche

            if (absdeb < taillebateau - 1 && orddeb < taillebateau - 1)          //Le bateau commence en haut à gauche   ->  Il ne peut s'étendre que vers le bas ou la droite
            { dir = random.Next(2, 4); }

            if (absdeb > taillebateau && orddeb < taillebateau - 1)          //en haut à droite  -> Vers le bas ou la gauche
            { dir = random.Next(3, 5); }

            if (absdeb < taillebateau - 1 && orddeb > taillebateau)          //en bas à gauche  -> Vers le haut ou la droite
            { dir = random.Next(1, 3); }

            if (absdeb > taillebateau && orddeb > taillebateau)          //en bas à droite  -> Vers le haut ou la gauche
            {
                dir = random.Next(1, 3);
                if (dir == 2)
                { dir = 4; }
            }

        }


        //La fonction suivante sera utilisée dans CréerBateaux 
        //Elle permet de savoir si une case (une abscisse et une ordonnée) entrée en paramètre est déjà occupée par un bateau

        public static bool DansEmplacementsBateaux(int abs, int ord, int longueur, int[,] emplacementsBateaux)
        {
            //longueur : indique jusqu'où on vérifie
            //nécessaire si on veux ne pas confondre un emplacement pas encore défini (une colonne du tableau vide) et un morceau de bateau en abs = 0 et ord = 0

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


        public static void CréerBateaux(ref int[,] emplacementsBateaux, ref int absdeb, ref int orddeb, ref int dir, int[] taillesBateaux)
        {

            int débutBateau = 0;

            for (int i = 0; i < taillesBateaux.Length; i++)  //une boucle correspond à un bateau
            {
                bool erreur = true;

                while (erreur)
                {
                    erreur = false;
                    DébutBateaux(taillesBateaux[i], ref absdeb, ref orddeb, ref dir);

                    if (DansEmplacementsBateaux(absdeb, orddeb, débutBateau, emplacementsBateaux))   //Le paramètre longueur (ici débutBateau) est la somme des tailles des bateaux précédents
                    {
                        erreur = true;
                    }

                    else
                    {
                        emplacementsBateaux[0, débutBateau] = absdeb;
                        emplacementsBateaux[1, débutBateau] = orddeb;


                        if (dir == 1)
                        {
                            int j = 1;
                            while (j < taillesBateaux[i] && erreur == false)
                            {
                                if (DansEmplacementsBateaux(absdeb, orddeb - j, débutBateau, emplacementsBateaux))
                                { erreur = true; }

                                else
                                {
                                    emplacementsBateaux[0, débutBateau + j] = absdeb;
                                    emplacementsBateaux[1, débutBateau + j] = orddeb - j;
                                }
                                j++;
                            }
                        }

                        if (dir == 2)
                        {
                            int j = 1;
                            while (j < taillesBateaux[i] && erreur == false)
                            {
                                if (DansEmplacementsBateaux(absdeb + j, orddeb, débutBateau, emplacementsBateaux))
                                {
                                    erreur = true;
                                }

                                else
                                {
                                    emplacementsBateaux[0, débutBateau + j] = absdeb + j;
                                    emplacementsBateaux[1, débutBateau + j] = orddeb;
                                }
                                j++;
                            }
                        }

                        if (dir == 3)
                        {
                            int j = 1;
                            while (j < taillesBateaux[i] && erreur == false)
                            {
                                if (DansEmplacementsBateaux(absdeb, orddeb + j, débutBateau, emplacementsBateaux))
                                {
                                    erreur = true;
                                }

                                else
                                {
                                    emplacementsBateaux[0, débutBateau + j] = absdeb;
                                    emplacementsBateaux[1, débutBateau + j] = orddeb + j;
                                }
                                j++;
                            }
                        }

                        if (dir == 4)
                        {
                            int j = 1;
                            while (j < taillesBateaux[i] && erreur == false)
                            {
                                if (DansEmplacementsBateaux(absdeb, orddeb - j, débutBateau, emplacementsBateaux))
                                {
                                    erreur = true;
                                }

                                else
                                {
                                    emplacementsBateaux[0, débutBateau + j] = absdeb;
                                    emplacementsBateaux[1, débutBateau + j] = orddeb - j;
                                }
                                j++;
                            }
                        }
                    }
                }

                débutBateau += taillesBateaux[i];
            }
        }



        static string affichageCaractere(int caractere)
        // Affiche le caractère selon le numéro obtenu dans la grille 
        // 0: vide ; 1: bateau intacte ; 2: bateau touché ; 3: tir manqué;
        {
            string rendu;
            switch (caractere)
            {
                case 3:
                    return rendu = "0";
                case 1:
                    return rendu = "☐";
                case 2:
                    return rendu = "X";
                default:
                    return rendu = " ";
            }
        }

        static void affichageCarte(int[,] tableauBateaux)
        {
            // Initialisation des noms des axes (horizontal et vertical)
            string[] NomAxeHorizontal = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            string[] NomAxeVertical = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            int NumColonne = 0;

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
                        Console.Write("{0}| {1} ", NomAxeVertical[NumColonne], affichageCaractere(tableauBateaux[ligne, colonne]));
                        ++NumColonne;
                    }
                    else { Console.Write("| {0} ", affichageCaractere(tableauBateaux[ligne, colonne])); }
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
                                                             //-->>  PAS COMPRIS//---------------------------------->>>>>>>>>          // 5 bateaux + 4 ...       
                                                             // On commence par les abscisses du bateau le plus long jusqu'au plus petit (idem pour les ordonnéees)


            /*
                                                             int tirsB5 = 0;
                                                             int tirsB4 = 0;
                                                             int tirsB31 = 0;
                                                             int tirsB32 = 0;                                // Référence le nombre de fois qu'un bateau a été touché (pour chaque bateau)
                                                             int tirsB2 = 0;                                //Evite de vérifier l'état de chaque case et permet de déterminer si un bateau est coulé
                                                             */



            // test de la génération de bataux selon le tableau ci-dessous
            // On voit bien le porte avion de A2 - E2 ; touché en D2;
            int[,] testTableau = new int[,] { { 0, 1, 0, 1, 2, 3, 0, 2, 0, 1 }, { 0, 1, 0, 1, 2, 3, 0, 2, 0, 1 }, { 0, 1, 0, 1, 2, 3, 0, 2, 0, 1 }, { 0, 2, 0, 1, 2, 3, 0, 2, 0, 1 }, { 0, 1, 0, 1, 2, 3, 0, 2, 0, 1 }, { 0, 0, 0, 1, 2, 3, 0, 2, 0, 1 }, { 0, 0, 0, 1, 2, 3, 0, 2, 0, 1 }, { 0, 0, 0, 1, 2, 3, 0, 2, 0, 1 }, { 0, 0, 0, 1, 2, 3, 0, 2, 0, 1 }, { 0, 0, 0, 1, 2, 3, 0, 2, 0, 1 } };
            affichageCarte(testTableau);
            Console.WriteLine();

            Console.ReadKey();
        }

    }
}


