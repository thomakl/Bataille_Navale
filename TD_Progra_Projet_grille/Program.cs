public static void AfficherPlateauDeJeu(string[,] tabJoueur, string[,] tabOpposant)
        //Fonction bonus: combinaison plus esthétique et pratique (double lecture d'informations) des deux précédentes fonctions
        {
            Console.WriteLine("\n\tVotre grille\t\t\t\t     Grille de l'adversaire"); //Affiche la grille du joueur et la grille cachée de l'adversaire à l'horizontale
            Console.Write("\n  A  B  C  D  E  F  G  H  I  J\t\t\t  A  B  C  D  E  F  G  H  I  J\n");
            for (int i = 0; i < tabJoueur.GetLength(0); i++)
            {
                Console.Write("+--+--+--+--+--+--+--+--+--+--+"); Console.Write("\t\t|\t"); Console.WriteLine("+--+--+--+--+--+--+--+--+--+--+");
                for (int j = 0; j < tabJoueur.GetLength(1); j++)
                {
                    if (tabJoueur[i, j] == "bc") { Console.Write("|><"); continue; }
                    Console.Write("|" + tabJoueur[i, j]);
                    if (j == 9) Console.Write("|");
                }
                Console.Write(" " + (i + 1));

                Console.Write("\t|\t");

                for (int j = 0; j < tabOpposant.GetLength(1); j++)
                {
                    if ((tabOpposant[i, j] == "  ") || (tabOpposant[i, j] == "><") || (tabOpposant[i, j] == "bc"))
                    {
                        if (tabOpposant[i, j] == "bc") { Console.Write("|><"); continue; }
                        Console.Write("|" + tabOpposant[i, j]);
                    }
                    else
                    {
                        Console.Write("|  ");
                    }

                    if (j == 9)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine(" " + (i + 1));

            }
            Console.Write("+--+--+--+--+--+--+--+--+--+--+"); Console.Write("\t\t|\t"); Console.WriteLine("+--+--+--+--+--+--+--+--+--+--+\n");
        }
