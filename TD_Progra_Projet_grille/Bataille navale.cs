﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bataille_Navale
{
    class Program
    {

        // Initialisation des paramètres du jeu
        // Accès des variables à toutes les sous-fonctions du programme
        //// Paramètres modifiables
        static int nbLigne = 10;
        static int nbcolonne = 10;
        static int[] taillesBateaux = { 5, 4, 3, 3, 2 };
        static int[,] mesBateaux;
        static int[,] bateauxAdverse;


        //// Paramètres à ne pas modifier

        static int[,] emplacementsBateaux;
        static int tailleTotale;
        static int absDeb = 0;
        static int ordDeb = 0;
        static int dir = 0;
        static int toucheJoueur;
        static int absTouchePrec = nbcolonne;
        static int ordTouchePrec;
        static int absToucheActuelle;
        static int ordToucheActuelle;
        static int nbtirTour = 5;

        static void Main(string[] args)
        {

            LancerMenuPrincipal();

        }

        // Crée une première case occupée par le bateau ainsi qu'une direction vers laquelle il s'étend 
        public static void CreerCaseBateaux(int tailleBateau, ref int absDeb, ref int ordDeb, ref int dir)
        {
            Random random = new Random();

            absDeb = random.Next(0, 10);
            ordDeb = random.Next(0, 10);
            dir = random.Next(1, 5);
            /* 
             * Direction : (sens horaire depuis le haut)
             * 1  -> haut
             * 2 ->  droite
             * 3 ->  bas
             * 4 ->  gauche */

            if (absDeb < tailleBateau - 1 && ordDeb < tailleBateau - 1)          //Le bateau commence en haut à gauche   ->  Il ne peut s'étendre que vers le bas ou la droite
            { dir = random.Next(2, 4); }

            if (absDeb > tailleBateau && ordDeb < tailleBateau - 1)          //en haut à droite  -> Vers le bas ou la gauche
            { dir = random.Next(3, 5); }

            if (absDeb < tailleBateau - 1 && ordDeb > tailleBateau)          //en bas à gauche  -> Vers le haut ou la droite
            { dir = random.Next(1, 3); }

            if (absDeb > tailleBateau && ordDeb > tailleBateau)          //en bas à droite  -> Vers le haut ou la gauche
            {
                dir = random.Next(1, 3);
                if (dir == 2)
                { dir = 4; }
            }
        }

        // Crée une première case occupée par le bateau ainsi qu'une direction vers laquelle il s'étend
        public static void CreerCaseBateaux(int tailleBateau, ref int absDeb, ref int ordDeb, ref int dir, int nbLigne, int nbcolonne)
        {
            Random random = new Random();

            absDeb = random.Next(0, 10);
            ordDeb = random.Next(0, 10);
            dir = random.Next(1, 5);                             // Direction : (sens horaire depuis le haut)
                                                                 //   1  -> haut
                                                                 //   2 ->  droite
                                                                 //   3 ->  bas
                                                                 //   4 ->  gauche

            if (absDeb < tailleBateau - 1)              // Cas d'un bateau à gauche
            {
                if (ordDeb < tailleBateau - 1)                // // En haut à gauche
                { dir = random.Next(2, 4); }               // --> Le bateau ne peut s'étendre que vers le bas ou vers la droite
                else
                {
                    if (nbLigne - ordDeb < tailleBateau)    // // En bas à gauche
                    { dir = random.Next(1, 3); }               // --> Que vers le haut ou la droite
                    else                                       // //  Au milieu gauche
                    { dir = random.Next(1, 4); }              // --> Haut_Bas_Droite
                }
            }

            else
            {
                if (nbcolonne - absDeb < tailleBateau)  // Cas d'un bateau à droite
                {
                    if (ordDeb < tailleBateau - 1)                // // En haut à droite
                    { dir = random.Next(3, 5); }               // --> Le bateau ne peut s'étendre que vers le bas ou vers la gauche

                    else
                    {
                        if (nbLigne - ordDeb < tailleBateau)    // // En bas à droite
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
                    if (ordDeb < tailleBateau - 1)         // Cas d'un bateau en haut au milieu
                    { dir = random.Next(2, 5); }           // --> Bas_Gauche_Droite

                    if (nbLigne - ordDeb < tailleBateau) // Cas d'un bateau en bas au milieu
                    {                                        //--> Haut_Gauche_Droite
                        dir = random.Next(1, 4);
                        if (dir == 3)
                        { dir = 4; }
                    }
                }
            }
        }

        // Permet de savoir si une case (une abscisse et une ordonnée) entrée en paramètre est déjà occupée par un bateau
        // La fonction suivante sera utilisée dans GenererBateaux 
        public static bool VerifierBateaux(int abs, int ord, int longueur, int[,] emplacementsBateaux)
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

        // Génére des emplacements de bateaux dans un tableau à 2 dimensions
        public static int[,] GenererBateaux(ref int[,] emplacementsBateaux, ref int absDeb, ref int ordDeb, ref int dir, int[] taillesBateaux, int nbLigne, int nbcolonne)
        {

            int débutBateau = 0;

            for (int i = 0; i < taillesBateaux.Length; i++)  //une boucle correspond à un bateau
            {
                bool erreur = true;

                while (erreur)
                {
                    erreur = false;
                    CreerCaseBateaux(taillesBateaux[i], ref absDeb, ref ordDeb, ref dir, nbLigne, nbcolonne);

                    if (VerifierBateaux(absDeb, ordDeb, débutBateau, emplacementsBateaux))   //Le paramètre longueur (ici débutBateau) est la somme des tailles des bateaux précédents
                    {
                        erreur = true;
                    }

                    else
                    {
                        emplacementsBateaux[0, débutBateau] = absDeb;
                        emplacementsBateaux[1, débutBateau] = ordDeb;


                        if (dir == 1)
                        {
                            int j = 1;
                            while (j < taillesBateaux[i] && erreur == false)
                            {
                                if (VerifierBateaux(absDeb, ordDeb - j, débutBateau, emplacementsBateaux))
                                { erreur = true; }

                                else
                                {
                                    emplacementsBateaux[0, débutBateau + j] = absDeb;
                                    emplacementsBateaux[1, débutBateau + j] = ordDeb - j;
                                }
                                j++;
                            }
                        }

                        if (dir == 2)
                        {
                            int j = 1;
                            while (j < taillesBateaux[i] && erreur == false)
                            {
                                if (VerifierBateaux(absDeb + j, ordDeb, débutBateau, emplacementsBateaux))
                                {
                                    erreur = true;
                                }

                                else
                                {
                                    emplacementsBateaux[0, débutBateau + j] = absDeb + j;
                                    emplacementsBateaux[1, débutBateau + j] = ordDeb;
                                }
                                j++;
                            }
                        }

                        if (dir == 3)
                        {
                            int j = 1;
                            while (j < taillesBateaux[i] && erreur == false)
                            {
                                if (VerifierBateaux(absDeb, ordDeb + j, débutBateau, emplacementsBateaux))
                                {
                                    erreur = true;
                                }

                                else
                                {
                                    emplacementsBateaux[0, débutBateau + j] = absDeb;
                                    emplacementsBateaux[1, débutBateau + j] = ordDeb + j;
                                }
                                j++;
                            }
                        }

                        if (dir == 4)
                        {
                            int j = 1;
                            while (j < taillesBateaux[i] && erreur == false)
                            {
                                if (VerifierBateaux(absDeb, ordDeb - j, débutBateau, emplacementsBateaux))
                                {
                                    erreur = true;
                                }

                                else
                                {
                                    emplacementsBateaux[0, débutBateau + j] = absDeb - j;
                                    emplacementsBateaux[1, débutBateau + j] = ordDeb;
                                }
                                j++;
                            }
                        }
                    }
                }

                débutBateau += taillesBateaux[i];
            }


            int[,] generationBateaux = new int[10, 10];

            for (int i = 0; i < emplacementsBateaux.GetLength(1); ++i)
            {
                generationBateaux[emplacementsBateaux[0, i], emplacementsBateaux[1, i]] = 1;
            }

            débutBateau = 0;
            return generationBateaux;
        }

        // Affiche le caractère selon le numéro obtenu dans la grille 
        static string AfficherCaractere(int caractere, string vue)
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

        // Demande à l'utilisateur de rentrer des coordonnées de tir
        public static void TourHumain(ref int[,] bateauxAdverse, ref int toucheJoueur)
        {
            Console.WriteLine("Dans quelle ligne voulez-vous Tirer ? (de A à J)");
            string saisie = Console.ReadLine();
            char lettre = Convert.ToChar(saisie);
            int ligne = char.ToUpper(lettre) - 65;

            Console.WriteLine("Dans quelle colonne voulez-vous Tirer ? (de 1 à 10)");
            saisie = Console.ReadLine();
            int colonne = Convert.ToInt32(saisie) - 1;

            switch (Tirer(ref bateauxAdverse, ligne, colonne))
            {
                case 2:

                    Console.WriteLine("Vous avez déjà tiré ici, veuillez appuyer sur Entrée et ensuite indiquer une autre ligne puis une autre colonne");
                    Console.ReadKey();
                    TourHumain(ref bateauxAdverse, ref toucheJoueur);
                    break;

                case 1:
                    toucheJoueur++;
                    break;
            }
        }

        public static void ResultatTourHumain(ref int toucheJoueur)
        {
            if (toucheJoueur == 0)
            { Console.WriteLine("Désolé vous n'avez pas touché de bateaux, appuyez sur Entrée"); }
            else
            { Console.WriteLine("Bravo vous avez touché {0} fois, appuyez sur Entrée", toucheJoueur); }
            toucheJoueur = 0;
        }

        public static int Tirer(ref int[,] bateaux, int ligne, int colonne)
        {
            switch (bateaux[ligne, colonne])
            {
                case 0:
                    bateaux[ligne, colonne] = 3;
                    return 0;
                case 1:
                    bateaux[ligne, colonne] = 2;
                    return 1;
                default:
                    return 2;
            }

        }

        // Niveau de difficulté de l'IA : Très facile
        public static void ParametrerIATresFacile(ref int[,] bateauxAdverse)
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
                    ParametrerIATresFacile(ref bateauxAdverse);
                    break;
            }
        }

        // Niveau de difficulté de l'IA : Facile
        public static void ParametrerIAFacile(ref int[,] mesBateaux, int nbLigne, int nbcolonne, ref int absTouchePrec, ref int ordTouchePrec, ref int absToucheActuelle, ref int ordToucheActuelle, ref int nbtirTour)
        {

            Random random = new Random();

            if (absTouchePrec == nbcolonne)      //aucune case n'est touchée (les cases coulées ne sont pas touchées)
            {
                while (nbtirTour > 0)
                {
                    int ligne = random.Next(0, 10);
                    int colonne = random.Next(0, 10);
                    int tir = Tirer(ref mesBateaux, ligne, colonne);

                    while (tir == 2)               //Si le tir a eu lieu sur une case déjà jouée -> On recommence un tir
                    {
                        ligne = random.Next(0, 10);
                        colonne = random.Next(0, 10);
                        tir = Tirer(ref mesBateaux, ligne, colonne);
                    }

                    if (tir == 1)  //le tir a touché un bateau, il faut donc stocker ses coordonnées
                    {
                        absToucheActuelle = colonne;
                        ordToucheActuelle = ligne;
                    }
                    nbtirTour--;
                }

                absTouchePrec = absToucheActuelle;
                ordTouchePrec = ordToucheActuelle;
            }



            else       //une case possède le statut touchée
            {
                int tir;
                if (ordTouchePrec != 0)    //Commence par tirer vers le haut, mais pour cela il faut vérifier qu'il existe une case au dessus
                {
                    tir = Tirer(ref mesBateaux, absTouchePrec, ordTouchePrec - 1);
                    if (tir != 2)
                    {
                        if (tir == 1)  //le tir a touché un bateau, il faut donc stocker ses coordonnées
                        {
                            ordToucheActuelle = ordTouchePrec - 1;
                        }
                        nbtirTour--;
                    }
                }

                if (ordTouchePrec != nbLigne && nbtirTour > 0)     //Ensuite tir vers le bas et vérifie l'existence de la case
                {
                    tir = Tirer(ref mesBateaux, absTouchePrec, ordTouchePrec + 1);
                    if (tir != 2)
                    {
                        if (tir == 1)  //le tir a touché un bateau, il faut donc stocker ses coordonnées
                        {
                            ordToucheActuelle = ordTouchePrec + 1;
                        }
                        nbtirTour--;
                    }
                }

                if (absTouchePrec != nbcolonne && nbtirTour > 0)     //Ensuite tir vers la droite et vérifie l'existence de la case
                {
                    tir = Tirer(ref mesBateaux, absTouchePrec + 1, ordTouchePrec);
                    if (tir != 2)
                    {
                        if (tir == 1)  //le tir a touché un bateau, il faut donc stocker ses coordonnées
                        {
                            absToucheActuelle = absTouchePrec + 1;
                        }
                        nbtirTour--;
                    }
                }

                if (absTouchePrec != 0 && nbtirTour > 0)     //Enfin tir vers la gauche et vérifie l'existence de la case
                {
                    tir = Tirer(ref mesBateaux, absTouchePrec - 1, ordTouchePrec);
                    if (tir != 2)
                    {
                        if (tir == 1)  //le tir a touché un bateau, il faut donc stocker ses coordonnées
                        {
                            absToucheActuelle = absTouchePrec - 1;
                        }
                        nbtirTour--;
                    }
                }

                while (nbtirTour > 0)
                {
                    int ligne = random.Next(0, 10);
                    int colonne = random.Next(0, 10);
                    tir = Tirer(ref mesBateaux, ligne, colonne);

                    while (tir == 2)               //Si le tir a eu lieu sur une case déjà jouée -> On recommence un tir
                    {
                        ligne = random.Next(0, 10);
                        colonne = random.Next(0, 10);
                        tir = Tirer(ref mesBateaux, ligne, colonne);
                    }

                    if (tir == 1)  //le tir a touché un bateau, il faut donc stocker ses coordonnées
                    {
                        absToucheActuelle = colonne;
                        ordToucheActuelle = ligne;
                    }
                    nbtirTour--;
                }


                absTouchePrec = absToucheActuelle;
                ordTouchePrec = ordToucheActuelle;
            }
            nbtirTour = 5;
        }

        // Niveau de difficulté de l'IA : Normale
        public static void ParametrerIANormale(ref int[,] bateauxAdverse, int nbLigne, int nbcolonne, ref int absTouchePrec, ref int ordTouchePrec, ref int absToucheActuelle, ref int ordToucheActuelle, ref int nbtirTour)
        {
        }

        //Interface Menu Principal
        public static void LancerMenuPrincipal()
        {

            // Creation de l'interface
            Console.WriteLine("\t\t\tBataille Navale");
            for (int i = 0; i < 60; ++i)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n\n\t\t\t1.Reprendre partie");
            Console.WriteLine("\n\t\t\t2.Nouvelle partie");
            Console.WriteLine("\n\t\t\t3.Règles du jeu");
            Console.WriteLine("\n\t\t\t4.Quitter le jeu\n");
            for (int i = 0; i < 60; ++i)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n\nQue voulez vous faire ? Entrez un chiffre.\n");
            Console.Write("> ");
            string choix = Console.ReadLine();

            // Menu choix d'action
            switch (choix)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Restauration de la sauvegarde en cours...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Création d'une nouvelle Partie en cours...");
                    Console.Clear();
                    LancerPartie();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Voila les règles du jeu...");
                    Console.WriteLine("Pour retourner au menu principal appuyez sur une touche.");
                    Console.ReadKey();
                    Console.Clear();
                    LancerMenuPrincipal();
                    break;
                case "4":
                    Console.WriteLine("Merci d'avoir joué à la Bataille Navale");
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    LancerMenuPrincipal();
                    break;
            }
        }

        // Fonction qui lance une nouvelle partie
        // Elle comprends un changement de grille et la partie en elle-même
        public static void LancerPartie()
        {
            ChangerGrille();
            LancerMenuPartie();
        }

        // Interface avec la partie en elle-même
        // Composé d'un menu d'actions
        public static void LancerMenuPartie()
        {



            AfficherGrilleJeu(bateauxAdverse, mesBateaux);
            Console.WriteLine("\n==========================================================================================================================================");

            for (int i = 0; i < 65; ++i)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n\n\t\t\t1.Tirer");
            Console.WriteLine("\n\t\t\t2.Sauvegarder");
            Console.WriteLine("\n\t\t\t3.Quitter la partie");
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
                    // Fonction pour Tirer
                    for (int i = 0; i < 5; i++)
                    { TourHumain(ref bateauxAdverse, ref toucheJoueur); }
                    ResultatTourHumain(ref toucheJoueur);
                    Console.ReadKey();
                    ParametrerIAFacile(ref mesBateaux, nbLigne, nbcolonne, ref absTouchePrec, ref ordTouchePrec, ref absToucheActuelle, ref ordToucheActuelle, ref nbtirTour);
                    //Console.Clear();
                    LancerMenuPartie();
                    break;
                case "2":
                    Console.WriteLine("Sauvegarde en cours... ");
                    SauvegarderPartie();
                    Console.Write("\a");
                    Console.WriteLine("\nVotre partie a été sauvegardé");
                    Console.WriteLine("Pour retourner à la partie, appuyez sur une touche.");
                    Console.ReadKey();
                    Console.Clear();
                    LancerMenuPartie();
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
                        LancerMenuPrincipal();
                        break;
                    }
                    LancerMenuPartie();
                    break;
                default:
                    LancerMenuPartie();
                    break;
            }
        }

        // Afficahge cote à cote
        public static void AfficherGrilleJeu(int[,] emplacementsBateauxAdversaire, int[,] emplacementsBateauxJoueur)
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
                    else { Console.Write("| {0} ", AfficherCaractere(emplacementsBateauxAdversaire[i, j], "adversaire")); }
                }
                Console.Write(" " + (i + 1));

                Console.Write("\t\t|\t\t");

                for (int j = 0; j <= emplacementsBateauxJoueur.GetLength(0); j++)
                {
                    if (j == 0) { Console.Write("\t"); }
                    if (j == 10) { Console.Write("|"); }
                    else { Console.Write("| {0} ", AfficherCaractere(emplacementsBateauxJoueur[i, j], "joueur")); }

                }
                Console.WriteLine(" " + (i + 1));
            }
            Console.Write("\t+---+---+---+---+---+---+---+---+---+---+"); Console.Write("\t\t|\t\t"); Console.WriteLine("\t+---+---+---+---+---+---+---+---+---+---+\n");
        }

        // lancement du choix de la grille de départ
        public static void ChangerGrille()
        {
            for (int i = 0; i < taillesBateaux.Length; i++)             //on commence par initialiser le tableau vide des emplacements de bateaux
            {                                                          //Pour cela il faut savoir la longueur de tous les bateaux mis bouts à bouts
                tailleTotale = tailleTotale + taillesBateaux[i];
            }
            emplacementsBateaux = new int[3, tailleTotale];

            for (int i = 0; i < tailleTotale; i++)
            { emplacementsBateaux[2, i] = 0; }

            string choixGrille;
            do
            {
                mesBateaux = GenererBateaux(ref emplacementsBateaux, ref absDeb, ref ordDeb, ref dir, taillesBateaux, nbLigne, nbcolonne);
                bateauxAdverse = GenererBateaux(ref emplacementsBateaux, ref absDeb, ref ordDeb, ref dir, taillesBateaux, nbLigne, nbcolonne);

                AfficherGrilleJeu(mesBateaux, bateauxAdverse);
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
        public static void SauvegarderPartie()
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

            // Ecriture dans un fichier save.txt
            String toSave = string.Join("", save);
            StreamWriter file = new StreamWriter(Path.GetFullPath("save.txt"));
            file.WriteLine(toSave);
            file.Close();

            ///// To Restore 
            /// 
            /// 

            string text = System.IO.File.ReadAllText(Path.GetFullPath("save.txt"));

            Console.WriteLine(text);




            int[] resume = new int[5];

            for (int i = 0; i < 5; ++i)
            {
                resume[i] = Convert.ToInt32(text[i]);
            }

            /*
            for (int i = 0; i < resume.GetLength(0); ++i)
            {
                for (int j = 0; j < resume.GetLength(1); ++j)
                {
                    resume[i, j] = Convert.ToInt32(text[i + j]);

                    Console.WriteLine("{0}-{1}", resume[i, j], text[i + j]);
                }
            }
            */
            Console.WriteLine(resume[0]);
            Console.ReadKey();
            /*
            // Animation de la barre de progression de la sauvegarde
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
            */
        }
        /*
        //Restaure les emplacements des bateaux à partir d'un fichier texte
        public static int[,] RestaurerPartie()
        {
            int[,] sauvegarde;

            // Animation de la barre de progression de la sauvegarde
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
            return sauvegarde;
        }*/

    }
}
