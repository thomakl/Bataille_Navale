using System;
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
        static int nbligne = 10;
        static int nbcolonne = 10;
        static int[] taillesBateaux = { 5, 4, 3, 3, 2 };
        static int[,] mesBateaux;
        static int[,] bateauxAdverse;


        //// Paramètres à ne pas modifier
        static int[,] emplacementsBateaux = new int[17, 17];
        static int absDeb = 0;
        static int ordDeb = 0;
        static int dir = 0;
        static int abstouche = nbcolonne;
        static int ordtouche;
        static int dirtir = 0;

        static void Main(string[] args)
        {

            LancerMenuPrincipal();

            //SauvegarderPartie();
        }

       

        // Crée une première case occupée par le bateau ainsi qu'une direction vers laquelle il s'étend
        public static void CreerCaseBateaux(int tailleBateau, ref int absDeb, ref int ordDeb, ref int dir, int nbligne, int nbcolonne)
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
                    if (nbligne - ordDeb < tailleBateau)    // // En bas à gauche
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
                        if (nbligne - ordDeb < tailleBateau)    // // En bas à droite
                        {                                           // --> Haut_Gauche
                            dir = random.Next(1, 2);
                            dir = dir * 3 - 2;                      //remplace : if (dir == 2){ dir = 4; }
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

                    if (nbligne - ordDeb < tailleBateau) // Cas d'un bateau en bas au milieu
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
        public static int[,] GenererBateaux(ref int[,] emplacementsBateaux, ref int absDeb, ref int ordDeb, ref int dir, int[] taillesBateaux, int nbligne, int nbcolonne)
        {

            int débutBateau = 0;

            for (int i = 0; i < taillesBateaux.Length; i++)  //une boucle correspond à un bateau
            {
                bool erreur = true;

                while (erreur)
                {
                    erreur = false;
                    CreerCaseBateaux(taillesBateaux[i], ref absDeb, ref ordDeb, ref dir, nbligne, nbcolonne);

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

            for (int i = 0; i < emplacementsBateaux.GetLength(0); ++i)
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
        public static void TourHumain(ref int[,] bateauxAdverse)
        {
            Console.WriteLine("Dans quelle ligne voulez-vous Tirer ? (de A à J)");
            string saisie = Console.ReadLine();
            char lettre = Convert.ToChar(saisie);
            int ligne = char.ToUpper(lettre) - 65;

            Console.WriteLine("Dans quelle colonne voulez-vous Tirer ? (de 1 à 10)");
            saisie = Console.ReadLine();
            int colonne = Convert.ToInt32(saisie) - 1;

            if (Tirer(ref bateauxAdverse, ligne, colonne) == 2)
            {
                Console.WriteLine("Vous avez déjà tiré ici, veuillez appuyer sur Entrée et ensuite indiquer une autre ligne puis une autre colonne");
                Console.ReadKey();
                TourHumain(ref bateauxAdverse);
            }            
        }

        public static int Tirer(ref int[,]bateaux, int ligne, int colonne)
        { switch (bateauxAdverse[ligne, colonne])
            {
                case 0:
                    bateauxAdverse[ligne, colonne] = 3;
                    return 0;
                case 1:
                    bateauxAdverse[ligne, colonne] = 2;
                    return 1;
                default:
                    return 2;
            }

        }

        // Niveau de difficulté de l'IA : Très facile
        public static void ParametrerIATresFacile(ref int[,] mesBateaux)
        {
            Random random = new Random();
            int ligne = random.Next(0, 10);
            int colonne = random.Next(0, 10);

            if (Tirer(ref mesBateaux, ligne, colonne) == 2)
            { ParametrerIATresFacile(ref mesBateaux); }
        }

        // Niveau de difficulté de l'IA : Facile
        public static void ParametrerIAFacile(ref int[,] mesBateaux, int nbligne, int nbcolonne, ref int abstouche, ref int ordtouche, ref int dirtir)
        {
            Random random = new Random();

            if (abstouche == nbcolonne)      //aucune case n'est touchée (les cases coulées ne sont pas touchées)
            {
                int ligne = random.Next(0, 10);
                int colonne = random.Next(0, 10);
                int tir = Tirer(ref mesBateaux, ligne, colonne);

                while ( tir == 2)               //Si le tir a eu lieu sur une case déjà jouée -> On recommence un tir
                {
                    ligne = random.Next(0, 10);
                    colonne = random.Next(0, 10);
                    tir = Tirer(ref mesBateaux, ligne, colonne);
                }

                if(tir == 1)  //le tir a touché un bateau
                {
                    abstouche = colonne;
                    ordtouche = ligne;
                }

            }

            else       //une case possède le statut touchée
            {
                if (dirtir == 0)       //Si aucune direction de tir -> tirer autour de la dernière case touchée
                {
                    int tir = 2;
                    int cas;
                    while (tir == 2)
                    {
                        cas = random.Next(1, 5);       //On génère une direction vers laquelle on veut tirer depuis la case de référence                       
                                                       // 1 : haut     2 : droite     3 : bas     4 : gauche
                                                       //Mais toutes les directions ne sont pas valables suivant les case : attetion aux bords
                                                       //On va donc utiliser une méthode proche de celle de CreerCaseBateaux

                        if (ordtouche == 1)     //La case est en haut  
                        {
                            if (abstouche == 1)             //La case est en haut à gauche
                            { cas = random.Next(2, 4); }    //Ne peut aller que vers le bas ou la droite
                            else
                            {
                                if (abstouche == nbcolonne-1)  //La case est en haut à droite
                                { cas = random.Next(3, 5); }   //Gauche ou bas

                                else                           //La case est en haut au millieu
                                { cas = random.Next(2, 5); }   //Gauche,droite ou bas
                            }
                        }

                        else
                        {
                            if(ordtouche == nbligne-1) //La case est en bas
                            {
                                if (abstouche == 1)             //La case est en bas à gauche
                                { cas = random.Next(1, 3); }    //Ne peut aller que vers le haut ou la droite
                                else
                                {
                                    if (abstouche == nbcolonne - 1)  //La case est en bas à droite
                                    {                                //Gauche ou haut
                                        cas = random.Next(1, 3);
                                        cas = cas * 3 - 2;  //ainsi si on avait cas=2 alors cas=4 et si on avait cas = 1 alors cas = 1
                                    }   

                                    else                           //La case est en bas au millieu
                                    {                              //Gauche,droite ou haut
                                        cas = random.Next(1, 4);
                                        if ( cas == 3) { cas = 4; }
                                    }   
                                }
                            }

                            else //La case n'est pas en haut ni en bas mais elle peut encore poser problème en milieu droit et milieu gauche
                            {
                                if(abstouche == 1) //Milieu gauche
                                { cas = random.Next(1, 4); } //Haut, bas ou droite

                                if(abstouche == nbcolonne-1) //Milieu droit
                                {                            //Haut, bas ou gauche
                                    cas = random.Next(2, 5);
                                    if (cas == 2) { cas = 1; }
                                } 
                            }
                        }

                       

                        switch (cas)                   
                        {
                            case 1:
                                tir = Tirer(ref mesBateaux, ordtouche - 1, abstouche);
                                if (tir == 1)
                                {
                                    ordtouche = ordtouche - 1;
                                    dirtir = 1;
                                }
                                break;

                            case 2:
                                tir = Tirer(ref mesBateaux, ordtouche, abstouche + 1);
                                if (tir == 1)
                                {
                                    abstouche = abstouche + 1;
                                    dirtir = 2;
                                }
                                break;

                            case 3:
                                tir = Tirer(ref mesBateaux, ordtouche + 1, abstouche);
                                if (tir == 1)
                                {
                                    ordtouche = ordtouche + 1;
                                    dirtir = -1;
                                }
                                break;

                            case 4:
                                tir = Tirer(ref mesBateaux, ordtouche, abstouche - 1);
                                if (tir==1)
                                {
                                    abstouche = abstouche - 1;
                                    dirtir = -2;
                                }
                                break;
                                // 1 : haut     2 : droite      -1 : bas     -2 : gauche
                        }
                    }

                }

                ////

            }



 
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
            Console.WriteLine("\n\n\t\t\t1.Reprendre LancerMenuPartie");
            Console.WriteLine("\n\t\t\t2.Nouvelle LancerMenuPartie");
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
                    Console.WriteLine("Création d'une nouvelle LancerMenuPartie en cours...");
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

        // Fonction qui lance une nouvelle LancerMenuPartie
        // Elle comprends un changement de grille et la LancerMenuPartie en elle-même
        public static void LancerPartie()
        {
            ChangerGrille();
            LancerMenuPartie();
        }

        // Interface avec la LancerMenuPartie en elle-même
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
                    TourHumain(ref bateauxAdverse);
                    Console.Clear();
                    LancerMenuPartie();
                    break;
                case "2":
                    Console.WriteLine("Sauvegarde en cours... ");
                    SauvegarderPartie();
                    Console.Write("\a");
                    Console.WriteLine("\nVotre LancerMenuPartie a été sauvegardé");
                    Console.WriteLine("Pour retourner à la LancerMenuPartie, appuyez sur une touche.");
                    Console.ReadKey();
                    Console.Clear();
                    LancerMenuPartie();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("\a /!\\ Attention vous allez quitter la LancerMenuPartie sans avoir sauvegarder.");
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
        public static void AfficherGrilleJeu(int[,] bateauxAdversaire, int[,] bateauxJoueur)
        // Code revu et adapté de Credit: Raphael Bres
        {
            Console.WriteLine("\n\t\tADVERSAIRE\t\t\t\t\t\t\t\t\t\tMES BATEAUX"); //Affiche la grille du joueur et la grille cachée de l'adversaire à l'horizontale
            Console.Write("\n\t  A   B   C   D   E   F   G   H   I   J \t\t\t\t\t  A   B   C   D   E   F   G   H   I   J\n");
            for (int i = 0; i < bateauxAdversaire.GetLength(0); i++)
            {
                Console.Write("\t+---+---+---+---+---+---+---+---+---+---+"); Console.Write("\t\t|\t\t"); Console.WriteLine("\t+---+---+---+---+---+---+---+---+---+---+");
                for (int j = 0; j <= bateauxAdversaire.GetLength(1); j++)
                {
                    if (j == 0) { Console.Write("\t"); }
                    if (j == 10) { Console.Write("|"); }
                    else { Console.Write("| {0} ", AfficherCaractere(bateauxAdversaire[i, j], "adversaire")); }
                }
                Console.Write(" " + (i + 1));

                Console.Write("\t\t|\t\t");

                for (int j = 0; j <= bateauxJoueur.GetLength(0); j++)
                {
                    if (j == 0) { Console.Write("\t"); }
                    if (j == 10) { Console.Write("|"); }
                    else { Console.Write("| {0} ", AfficherCaractere(bateauxJoueur[i, j], "joueur")); }

                }
                Console.WriteLine(" " + (i + 1));
            }
            Console.Write("\t+---+---+---+---+---+---+---+---+---+---+"); Console.Write("\t\t|\t\t"); Console.WriteLine("\t+---+---+---+---+---+---+---+---+---+---+\n");
        }

        // lancement du choix de la grille de départ
        public static void ChangerGrille()
        {
            string choixGrille;
            do
            {
                mesBateaux = GenererBateaux(ref emplacementsBateaux, ref absDeb, ref ordDeb, ref dir, taillesBateaux, nbligne, nbcolonne);
                bateauxAdverse = GenererBateaux(ref emplacementsBateaux, ref absDeb, ref ordDeb, ref dir, taillesBateaux, nbligne, nbcolonne);

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

        }

        //Restaure les emplacements des bateaux à partir d'un fichier texte
        public static int[,] RestaurerPartie()
        {
            int[,] sauvegarde = new int[10, 10];

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
        }

    }
}
