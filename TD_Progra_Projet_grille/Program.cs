using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int nbligne = 10;
            int nbcolonne = 10;

            int[] taillesBateaux = { 5, 4, 3, 3, 2 };

            int nbEmplacements = 0;
            for (int i = 0; i < taillesBateaux.Length; i++)
            { nbEmplacements += taillesBateaux[i]; }

            int[,] emplacementsBateaux = new int[nbEmplacements, nbEmplacements];
            //référence la localisation des cases qui contiennent un fragment de bateaux
            // il y a donc (5 + 4 + 3x2 + 2) = 17 cases.  Soit un sous-tableau contenant les 17 abscisses et un autre contenant les 17 ordonnées
            // On commence par les abscisses du bateau le plus long jusqu'au plus petit (idem pour les ordonnéees)

            int tirsB5 = 0;
            int tirsB4 = 0;
            int tirsB31 = 0;
            int tirsB32 = 0;                                // Référence le nombre de fois qu'un bateau a été touché (pour chaque bateau)
            int tirsB2 = 0;                                 //Evite de vérifier l'état de chaque case et permet de déterminer si un bateau est coulé

            int absdeb = 0;
            int orddeb = 0;
            int dir = 0;

<<<<<<< HEAD
            
            
            
            int[,] emplacementGenere = CréerBateaux(ref emplacementsBateaux, ref absdeb, ref orddeb, ref dir, taillesBateaux);


            for (int i = 0; emplacementGenere.)
=======
<<<<<<< HEAD

            CréerBateaux(ref emplacementsBateaux, ref absdeb, ref orddeb, ref dir, taillesBateaux, nbligne, nbcolonne);
=======
            //CréerBateaux(ref emplacementsBateaux, ref absdeb, ref orddeb, ref dir, taillesBateaux);
>>>>>>> 2edc163aef85a1371072da1df2cbcd6a8b069ba6
>>>>>>> d3ab661caac1fc4df98073425c2e5ee317143787

            affichageCarte(emplacementsBateaux);

            //affichageCarte(CréerBateaux(ref emplacementsBateaux, ref absdeb, ref orddeb, ref dir, taillesBateaux));

            Console.ReadKey();
        }


        static void affichageCarte(int[,] emplacementsBateaux)
        {
            // Initialisation des noms des axes (horizontal et vertical)
            string[] NomAxeHorizontal = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            string[] NomAxeVertical = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            int NumColonne = 0;

            // Initialisation d'un caractère dans la grille
            string vide = " ";
            string bateau = "Q";

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
                        if (DansEmplacementsBateaux(ligne, colonne, 17, emplacementsBateaux))
                        {
                            Console.Write("{0}| {1} ", NomAxeVertical[NumColonne], bateau);
                        }
                        else { Console.Write("{0}| {1} ", NomAxeVertical[NumColonne], vide); }
                        ++NumColonne;
                    }
                    else
                    {
                        if (DansEmplacementsBateaux(ligne, colonne, 17, emplacementsBateaux))
                        {
                            Console.Write("| {0} ", bateau);
                        }
                        else { Console.Write("| {0} ", vide); }

                    }
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

        //La fonction DébutBateaux crée une première case occupée par le bateau ainsi qu'une direction vers laquelle il s'étend

        public static void DébutBateaux(int taillebateau, ref int absdeb, ref int orddeb, ref int dir, int nbligne, int nbcolonne )
        {

            Random random = new Random();


            absdeb = random.Next(0, 10);
            orddeb = random.Next(0, 10);
            dir = random.Next(1, 5);                             // Direction : (sens horaire depuis le haut)
                                                                 //   1  -> haut
                                                                 //   2 ->  droite
                                                                 //   3 ->  bas
                                                                 //   4 ->  gauche


            if (absdeb < taillebateau - 1)              // Cas d'un bateau à gauche
            {
                if (orddeb < taillebateau - 1)                // // En haut à gauche
                { dir = random.Next(2, 4); }               // --> Le bateau ne peut s'étendre que vers le bas ou vers la droite

                else
                {
                    if (nbligne - orddeb < taillebateau)    // // En bas à gauche
                    { dir = random.Next(1, 3); }               // --> Que vers le haut ou la droite

                    else                                       // //  Au milieu gauche
                    { dir = random.Next(1, 4); }              // --> Haut_Bas_Droite
                }
            }

            else
            {
                if (nbcolonne - absdeb < taillebateau)  // Cas d'un bateau à droite
                {
                    if (orddeb < taillebateau - 1)                // // En haut à droite
                    { dir = random.Next(3, 5); }               // --> Le bateau ne peut s'étendre que vers le bas ou vers la gauche

                    else
                    {
                        if (nbligne - orddeb < taillebateau)    // // En bas à droite
                        {                                           // --> Haut_Gauche
                            dir = random.Next(1, 2);
                            if (dir == 2)
                            { dir = 4; }
                        }

                        else                                       // //  Au milieu droit
                        {                                          // --> Haut_Bas_Gauche
                            dir = random.Next(1, 4);
                            if (dir == 2) { dir = 4; }
                        }

                    }
                }

                else
                {
                    if (orddeb < taillebateau - 1)         // Cas d'un bateau en haut au milieu
                    { dir = random.Next(2, 5); }           // --> Bas_Gauche_Droite

                    if (nbligne - orddeb < taillebateau) // Cas d'un bateau en bas au milieu
                    {                                        //--> Haut_Gauche_Droite
                        dir = random.Next(1, 4);
                        if (dir == 3)
                        { dir = 4; }
                    } 
                        
                    
                }
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


<<<<<<< HEAD
        public static void CréerBateaux(ref int[,] emplacementsBateaux, ref int absdeb, ref int orddeb, ref int dir, int[] taillesBateaux, int nbligne, int nbcolonne)
=======
        public static int[,] CréerBateaux(ref int[,] emplacementsBateaux, ref int absdeb, ref int orddeb, ref int dir, int[] taillesBateaux)
>>>>>>> 2edc163aef85a1371072da1df2cbcd6a8b069ba6
        {

            int débutBateau = 0;

            for (int i = 0; i < taillesBateaux.Length; i++)  //une boucle correspond à un bateau
            {
                bool erreur = true;

                while (erreur)
                {
                    erreur = false;
                    DébutBateaux(taillesBateaux[i], ref absdeb, ref orddeb, ref dir, nbligne, nbcolonne);

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
                                    emplacementsBateaux[0, débutBateau + j] = absdeb-j;
                                    emplacementsBateaux[1, débutBateau + j] = orddeb;
                                }
                                j++;
                            }
                        }
                    }
                }

                débutBateau += taillesBateaux[i];
            }
<<<<<<< HEAD


=======
            
            int[,] generationBateaux = new int[10, 10];
            /*
            for (int i=0; i < 10; ++i)
            {
                for (int j = 0; i < 10; ++j)
                {
                    for(int k=0; k< emplacementsBateaux.GetLength(0);++k)
                    {
                        if ((emplacementsBateaux[k,0] == i) & (emplacementsBateaux[k,1] == j))
                        {
                            generationBateaux[i, j] = 1;
                        }
                    }
                }
            }
            */
            for (int i = 0; i < emplacementsBateaux.GetLength(0); ++i)
            {
                generationBateaux[emplacementsBateaux[i, 0], emplacementsBateaux[i, 1]] = 1;
            }

            débutBateau = 0;
            return generationBateaux;

            // fin boucle de la création d'un bateau
>>>>>>> 2edc163aef85a1371072da1df2cbcd6a8b069ba6
        }

     
         

        static string affichageCaractere(int caractere)
        // Affiche le caractère selon le numéro obtenu dans la grille 
        // default: vide ; 1: bateau intacte ; 2: bateau touché ; 3: tir manqué;
        {
            string rendu;
            switch (caractere)
            {
                case 3:
                    return rendu = "0";
                case 1:
                    return rendu = "#";
                case 2:
                    return rendu = "X";
                default:
                    return rendu = " ";
            }
        }

    }
}
