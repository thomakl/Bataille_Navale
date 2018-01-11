using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_Progra_Projet_grille
{
    class Program
    {
        static void Main(string[] args)
        {

            menuPrincipal();
            //int[,] carte = new int[10, 10];
            //affichageCarte(carte, "joeur");

        }

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

        // Affiche les emplacements des bateaux et des tirs sur une grille
        static void affichageCarte(int[,] emplacementsBateaux, string vue)
        {
            // paramètre vue : adversaire (l'utilisateur ne voit pas que les tirs effectués sur la grille de son adversaire) 
            //                 / joeur (le l'utilisateur voit tous les éléments de sa grille)

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
                        Console.Write("\t +---");
                    }
                    else { Console.Write("+---"); }
                }
                Console.Write("+\n");

                // Initialisation des colonnes: intérieur
                for (int colonne = 0; colonne < 10; ++colonne)
                {
                    if (colonne == 0)
                    {
                        Console.Write("\t{0}| {1} ", NomAxeVertical[NumColonne], affichageCaractere(emplacementsBateaux[ligne, colonne], vue));
                        ++NumColonne;
                    }
                    else
                    {
                        Console.Write("| {0} ", affichageCaractere(emplacementsBateaux[ligne, colonne], vue));

                    }
                }
                Console.Write("|\n");
            }
            //// Initialisation des colonnes: démarcation extérieur finale
            for (int colonne = 0; colonne < 10; ++colonne)
            {
                if (colonne == 0)
                {
                    Console.Write("\t +---");
                }
                else { Console.Write("+---"); }
            }
            Console.Write("+\n");
            // // Affichage de l'axe horizontal
            for (int colonne = 0; colonne < NomAxeHorizontal.Length; ++colonne)
            {
                if (colonne == 0)
                {
                    Console.Write("\t  {0} ", NomAxeHorizontal[colonne]);
                }
                else
                {
                    Console.Write("  {0} ", NomAxeHorizontal[colonne]);
                }
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
            int ligne = char.ToUpper(lettre) - 64;

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

        //
        public static void tourIA()
        {

        }

        //Interface Menu Principal
        public static void menuPrincipal()
        {
        Menu_principal:
            Console.WriteLine("\t\t\tBataille Navale");
            for (int i = 0; i < 60; ++i)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n\n\t\t\t1.Reprendre Partie");
            Console.WriteLine("\n\t\t\t2.Nouvelle Partie");
            Console.WriteLine("\n\t\t\t3.Paramètre");
            Console.WriteLine("\n\t\t\t4.Règles du jeu");
            Console.WriteLine("\n\t\t\t5.Quitter le jeu\n");
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
                    Console.WriteLine("Quelle difficulté ?");
                    Console.WriteLine("Pour retourner au menu principal appuyez sur une touche.");
                    Console.ReadKey();
                    Console.Clear();
                    goto Menu_principal;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Voila les règles du jeu...");
                    Console.WriteLine("Pour retourner au menu principal appuyez sur une touche.");
                    Console.ReadKey();
                    Console.Clear();
                    goto Menu_principal;
                case "5":
                    Console.WriteLine("Merci d'avoir joué à la Bataille Navale");
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    goto Menu_principal;
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
            //Initialisation des paramètres du jeu
            ////Paramètres modifiables
            int nbligne = 10;
            int nbcolonne = 10;
            int[] taillesBateaux = { 5, 4, 3, 3, 2 };
            int[,] mesBateaux;
            int[,] bateauxAdverse;
            string choix_grille;

            ////Paramètres à ne pas modifier
            int[,] emplacementsBateaux = new int[17, 17];
            int absdeb = 0;
            int orddeb = 0;
            int dir = 0;

            // Premier lancement de la partie avec le choix de sa grille de départ
            do
            {
                mesBateaux = créerBateaux(ref emplacementsBateaux, ref absdeb, ref orddeb, ref dir, taillesBateaux, nbligne, nbcolonne);
                bateauxAdverse = créerBateaux(ref emplacementsBateaux, ref absdeb, ref orddeb, ref dir, taillesBateaux, nbligne, nbcolonne);

                //Affichage des grilles
                Console.WriteLine("\t\t\tNouvelle Partie");
                Console.Write("============================================================\n");
                Console.Write("\t\t\tGRILLE ADVERSE");
                affichageCarte(bateauxAdverse, "adversaire");
                Console.Write("\n\n============================================================\n");
                Console.Write("\t\t\tMES BATEAUX");
                affichageCarte(mesBateaux, "joueur");
                Console.WriteLine("\n============================================================");

                //Demande à l'utilisateur
                Console.WriteLine("\n\tVoulez vous changer votre grille de jeu ? Entrez un chiffre.");
                Console.WriteLine("\n\n\t\t\t1.Oui");
                Console.WriteLine("\n\t\t\t2.Non");
                Console.Write("> ");
                choix_grille = Console.ReadLine();
            }
            while (choix_grille == "1");


        Partie:  //Point d'ancrage pour les renvoies
            // Interface Utilisateur
            Console.WriteLine("\t\t\tNouvelle Partie\n");
            Console.Write("============================================\n");
            Console.Write("              GRILLE ADVERSE");
            affichageCarte(bateauxAdverse, "adversaire");
            Console.Write("\n\n============================================\n");
            Console.Write("              MES BATEAUX");
            affichageCarte(mesBateaux, "joueur");
            Console.WriteLine("\n============================================");

            for (int i = 0; i < 60; ++i)
            {
                Console.Write("=");
            }
            Console.WriteLine("\n\n\t\t\t1.Tirer");
            Console.WriteLine("\n\t\t\t2.Sauvegarder");
            Console.WriteLine("\n\t\t\t3.Quitter la Partie");
            for (int i = 0; i < 60; ++i)
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
                    goto Partie;
                case "2":
                    Console.WriteLine("En cours de sauvegarde: ");
                    sauvegardePartie();
                    Console.Write("\a");
                    Console.WriteLine("\nVotre partie a été sauvegardé");
                    Console.WriteLine("Pour retourner à la partie, appuyez sur une touche.");
                    Console.ReadKey();
                    Console.Clear();
                    goto Partie;
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
                    goto Partie;
                default:
                    goto Partie;
            }
        }
    }
}

