﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD_Progra_Projet_grille
{
    class Program
    {

        //Initialisation des paramètres du jeu
        ////Paramètres modifiables
        static int nbligne = 10;
        static int nbcolonne = 10;
        static int[] taillesBateaux = { 5, 4, 3, 3, 2 };
        static int[,] mesBateaux;
        static int[,] bateauxAdverse;


        ////Paramètres à ne pas modifier
        static int[,] emplacementsBateaux = new int[17, 17];
        static int absdeb = 0;
        static int orddeb = 0;
        static int dir = 0;


        static void Main(string[] args)
        {

            //menuPrincipal();

            sauvegarderPartie();




        }

        // 
        public static void débutBateaux(int taillebateau, ref int absdeb, ref int orddeb, ref int dir)
        {
            Random random = new Random();

            absdeb = random.Next(0, 10);
            orddeb = random.Next(0, 10);
            dir = random.Next(1, 5);
            /* 
             * Direction : (sens horaire depuis le haut)
             * 1  -> haut
             * 2 ->  droite
             * 3 ->  bas
             * 4 ->  gauche */

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

        //La fonction DébutBateaux crée une première case occupée par le bateau ainsi qu'une direction vers laquelle il s'étend
        public static void débutBateaux(int taillebateau, ref int absdeb, ref int orddeb, ref int dir, int nbligne, int nbcolonne)
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

        //La fonction suivante sera utilisée dans créerBateaux 
        //Elle permet de savoir si une case (une abscisse et une ordonnée) entrée en paramètre est déjà occupée par un bateau
        public static bool dansEmplacementsBateaux(int abs, int ord, int longueur, int[,] emplacementsBateaux)
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

        public static int[,] créerBateaux(ref int[,] emplacementsBateaux, ref int absdeb, ref int orddeb, ref int dir, int[] taillesBateaux, int nbligne, int nbcolonne)
        {

            int débutBateau = 0;

            for (int i = 0; i < taillesBateaux.Length; i++)  //une boucle correspond à un bateau
            {
                bool erreur = true;

                while (erreur)
                {
                    erreur = false;
                    débutBateaux(taillesBateaux[i], ref absdeb, ref orddeb, ref dir, nbligne, nbcolonne);

                    if (dansEmplacementsBateaux(absdeb, orddeb, débutBateau, emplacementsBateaux))   //Le paramètre longueur (ici débutBateau) est la somme des tailles des bateaux précédents
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
                                if (dansEmplacementsBateaux(absdeb, orddeb - j, débutBateau, emplacementsBateaux))
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
                                if (dansEmplacementsBateaux(absdeb + j, orddeb, débutBateau, emplacementsBateaux))
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
                                if (dansEmplacementsBateaux(absdeb, orddeb + j, débutBateau, emplacementsBateaux))
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
                                if (dansEmplacementsBateaux(absdeb, orddeb - j, débutBateau, emplacementsBateaux))
                                {
                                    erreur = true;
                                }

                                else
                                {
                                    emplacementsBateaux[0, débutBateau + j] = absdeb - j;
                                    emplacementsBateaux[1, débutBateau + j] = orddeb;
                                }
                                j++;
                            }
                        }
                    }
                }

                débutBateau += taillesBateaux[i];
            }


            int[,] generationBateaux = new int[10, 10];

            for (int i = 0; i < emplacementsBateaux.GetLength(0); ++i)
            {
                generationBateaux[emplacementsBateaux[0, i], emplacementsBateaux[1, i]] = 1;
            }

            débutBateau = 0;
            return generationBateaux;
        }

        // Affiche le caractère selon le numéro obtenu dans la grille 
        static string affichageCaractere(int caractere, string vue)
        // default: vide ; 1: bateau intacte ; 2: bateau touché ; 3: tir manqué;
        // si vue = "adversaire" dans la fonction afficheCarte ==> le joueur ne voit pas les bateaux adverses
        {
            string rendu;
            switch (caractere)
            {
                case 3:
                    rendu = "0";
                    return rendu;
                case 1:
                    if (vue == "adversaire")
                    {
                        rendu = " ";
                        return rendu;
                    }
                    else
                    {
                        rendu = "▩";
                        return rendu;
                    }
                case 2:
                    rendu = "X";
                    return rendu;
                default:
                    rendu = " ";
                    return rendu;
            }
        }

        //Demande à l'utilisateur de rentrer des coordonnées de tir
        public static void tirer(ref int[,] bateauxAdverse)
        {
            Console.WriteLine("Dans quelle ligne voulez-vous tirer ? (de A à J)");
            string saisie = Console.ReadLine();
            char lettre = Convert.ToChar(saisie);
            int ligne = char.ToUpper(lettre) - 65;

            Console.WriteLine("Dans quelle colonne voulez-vous tirer ? (de 1 à 10)");
            saisie = Console.ReadLine();
            int colonne = Convert.ToInt32(saisie) - 1;

            switch (bateauxAdverse[ligne, colonne])
            {
                case 0:
                    bateauxAdverse[ligne, colonne] = 3;
                    break;
                case 1:
                    bateauxAdverse[ligne, colonne] = 2;
                    break;
                default:
                    Console.WriteLine("Vous avez déjà tiré ici, veuillez appuyer sur Espace et ensuite indiquer une autre ligne puis une autre colonne");
                    tirer(ref bateauxAdverse);
                    break;
            }

        }


        public static void iaTresFacile(ref int[,] bateauxAdverse)
        {
            Random random = new Random();
            int ligne = random.Next(0, 10);
            int colonne = random.Next(0, 10);

            switch (bateauxAdverse[ligne, colonne])
            {
                case 0:
                    bateauxAdverse[ligne, colonne] = 3;
                    break;
                case 1:
                    bateauxAdverse[ligne, colonne] = 2;
                    break;
                default:
                    iaTresFacile(ref bateauxAdverse);
                    break;
            }
        }

        public static void iaFacile(ref int[,] bateauxAdverse, ref int touche) //pas terminée
        {
            Random random = new Random();
            int ligne = 0;
            int colonne = 0;

            if (touche > 0)
            {
            }
            else
            {
                ligne = random.Next(0, 10);
                colonne = random.Next(0, 10);
            }
            switch (bateauxAdverse[ligne, colonne])
            {
                case 0:
                    bateauxAdverse[ligne, colonne] = 3;
                    break;
                case 1:
                    bateauxAdverse[ligne, colonne] = 2;
                    break;
                default:
                    iaFacile(ref bateauxAdverse, ref touche);
                    break;
            }
        }
        //Interface Menu Principal
        public static void menuPrincipal()
        {
            Console.WriteLine("\t\t\tBataille Navale");
            for (int i = 0; i < 60; ++i)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n\n\t\t\t1.Reprendre Partie");
            Console.WriteLine("\n\t\t\t2.Nouvelle Partie");
            Console.WriteLine("\n\t\t\t3.Règles du jeu");
            Console.WriteLine("\n\t\t\t4.Quitter le jeu\n");
            for (int i = 0; i < 60; ++i)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n\nQue voulez vous faire ? Entrez un chiffre.\n");
            Console.Write("> ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Restauration de la sauvegarde en cours...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Création d'une nouvelle partie en cours...");
                    Console.Clear();
                    lancementPartie();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Voila les règles du jeu...");
                    Console.WriteLine("Pour retourner au menu principal appuyez sur une touche.");
                    Console.ReadKey();
                    Console.Clear();
                    menuPrincipal();
                    break;
                case "4":
                    Console.WriteLine("Merci d'avoir joué à la Bataille Navale");
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    menuPrincipal();
                    break;
            }
        }

        public static void sauvegardePartie()
        {
            // Sauvegarde dans un fichier texte
            Console.Write("|");
            for (int i = 0; i <= 10; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    Console.Write("=");
                }
                Console.Write("> {0}0%", i);
                Console.SetCursorPosition(1, Console.BufferHeight - 1);
                System.Threading.Thread.Sleep(100);
            }
        }

        //Fonction qui lance la partie
        public static void lancementPartie()
        {
            changementGrille();
            partie();


        }

        public static void partie()
        {
            affichageCarte(bateauxAdverse, mesBateaux);
            Console.WriteLine("\n==========================================================================================================================================");

            for (int i = 0; i < 65; ++i)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n\n\t\t\t1.Tirer");
            Console.WriteLine("\n\t\t\t2.Sauvegarder");
            Console.WriteLine("\n\t\t\t3.Quitter la Partie");
            for (int i = 0; i < 65; ++i)
            {
                Console.Write("=");
            }

            // Demande à l'utilisateur son choix
            Console.WriteLine("\n\nQue voulez vous faire ? Entrez un chiffre.\n");
            Console.Write("> ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    // Fonction pour tirer
                    tirer(ref bateauxAdverse);
                    Console.Clear();
                    partie();
                    break;
                case "2":
                    Console.WriteLine("Sauvegarde en cours... ");
                    sauvegardePartie();
                    Console.Write("\a");
                    Console.WriteLine("\nVotre partie a été sauvegardé");
                    Console.WriteLine("Pour retourner à la partie, appuyez sur une touche.");
                    Console.ReadKey();
                    Console.Clear();
                    partie();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("\a /!\\ Attention vous allez quitter la partie sans avoir sauvegarder.");
                    Console.WriteLine("Voulez vous vraiment quitter sans sans sauvegarder ? ");
                    Console.WriteLine("\n\n\t\t\t1.Oui");
                    Console.WriteLine("\n\t\t\t2.Non");
                    string choix_quitter = Console.ReadLine();
                    if (choix_quitter == "1")
                    {
                        Console.Clear();
                        menuPrincipal();
                        break;
                    }
                    partie();
                    break;
                default:
                    partie();
                    break;
            }
        }

        // Afficahge cote à cote
        public static void affichageCarte(int[,] emplacementsBateauxAdversaire, int[,] emplacementsBateauxJoueur)
        // Code revu et adapté de Credit: Raphael Bres
        {
            Console.WriteLine("\n\t\tADVERSAIRE\t\t\t\t\t\t\t\t\t\tMES BATEAUX"); //Affiche la grille du joueur et la grille cachée de l'adversaire à l'horizontale
            Console.Write("\n\t  A   B   C   D   E   F   G   H   I   J \t\t\t\t\t  A   B   C   D   E   F   G   H   I   J\n");
            for (int i = 0; i < emplacementsBateauxAdversaire.GetLength(0); i++)
            {
                Console.Write("\t+---+---+---+---+---+---+---+---+---+---+"); Console.Write("\t\t|\t\t"); Console.WriteLine("\t+---+---+---+---+---+---+---+---+---+---+");
                for (int j = 0; j <= emplacementsBateauxAdversaire.GetLength(1); j++)
                {
                    if (j == 0) { Console.Write("\t"); }
                    if (j == 10) { Console.Write("|"); }
                    else { Console.Write("| {0} ", affichageCaractere(emplacementsBateauxAdversaire[i, j], "adversaire")); }
                }
                Console.Write(" " + (i + 1));

                Console.Write("\t\t|\t\t");

                for (int j = 0; j <= emplacementsBateauxJoueur.GetLength(0); j++)
                {
                    if (j == 0) { Console.Write("\t"); }
                    if (j == 10) { Console.Write("|"); }
                    else { Console.Write("| {0} ", affichageCaractere(emplacementsBateauxJoueur[i, j], "joueur")); }

                }
                Console.WriteLine(" " + (i + 1));
            }
            Console.Write("\t+---+---+---+---+---+---+---+---+---+---+"); Console.Write("\t\t|\t\t"); Console.WriteLine("\t+---+---+---+---+---+---+---+---+---+---+\n");
        }

        // Premier lancement de la partie avec le choix de sa grille de départ
        public static void changementGrille()
        {
            string choixGrille;
            do
            {
                mesBateaux = créerBateaux(ref emplacementsBateaux, ref absdeb, ref orddeb, ref dir, taillesBateaux, nbligne, nbcolonne);
                bateauxAdverse = créerBateaux(ref emplacementsBateaux, ref absdeb, ref orddeb, ref dir, taillesBateaux, nbligne, nbcolonne);

                affichageCarte(mesBateaux, bateauxAdverse);
                Console.WriteLine("\n==========================================================================================================================================");

                //Demande à l'utilisateur
                Console.WriteLine("\n\tVoulez vous changer votre grille de jeu ? Entrez un chiffre.");
                Console.WriteLine("\n\n\t\t\t1.Oui");
                Console.WriteLine("\n\t\t\t2.Non");
                Console.Write("> ");
                choixGrille = Console.ReadLine();
            }
            while (choixGrille == "1");
        }

        // Sauvegarde les emplacements des bateaux dans un fichier texte
        public static void sauvegarderPartie()
        {
            int[,] test = new int[,] { { 5, 4, 3, 3, 2 }, { 1, 2, 3, 4, 5 } };
            string[] save = new string[test.Length];
            int s = 0;

            for (int i = 0; i < test.GetLength(0); ++i)
            {
                for (int j = 0; j < test.GetLength(1); ++j)
                {
                    save[s] = Convert.ToString(test[i, j]);
                    ++s;
                }
            }
            String toSave = string.Join("", save);
            StreamWriter file2 = new StreamWriter(@"\file.txt");
            file2.WriteLine(toSave);
            file2.Close();
            Console.WriteLine(toSave);

            int[,] resume = new int[2, 5];
            for (int i = 0; i < resume.GetLength(0); ++i)
            {
                for (int j = 0; j < resume.GetLength(1); ++j)
                {
                    resume[i, j] = Convert.ToInt32(toSave[i + j]);
                    Console.WriteLine("{0}-{1}", resume[i, j], toSave[i + j]);
                }
            }

            Console.WriteLine(resume[0, 0]);

        }

    }
}
