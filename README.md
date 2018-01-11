# Bataille_Navale
Jeu populaire de la bataille navale, implémenté en C#

BatailleNavale

Le programme réalisé permet à un joueur humain d’affronter l’ordinateur. Il affiche deux grilles : Celle des bateaux du joueur, avec les coups au but de l’adversaire et les bateaux du déjà détruits. Celle des bateaux de l’adversaire, avec les coups au but du joueur et les bateaux déjà détruits. Le placement des bateaux du joueur et de l’ordinateur en début de partie sont aléatoires. Le joueur a la possibilité de visualiser le placement obtenu et d’en demander un autre jusqu’à ce que le résultat lui convienne. La saisie des coups du joueur est contrôlée afin d’éviter toute erreur. De manière générale, l’ergonomie du programme (saisies, affichages, déroulement de la partie) doit être étudiée avec attention. Au niveau de difficulté minimal, l’ordinateur tire au hasard sur les bateaux du joueur. Un niveau supplémentaire optionnel le fait jouer plus “intelligemment”, par exemple en essayant de couler les bateaux touchés. Lors de son tour, le joueur aura la possibilité de sauvegarder une partie qu’il pourra charger ultérieurement. Les positions des bateaux et l’état de la partie seront stockées dans un fichier texte selon une organisation à définir

Bateaux :

  Porte-avions (5 cases)
  Cuirassé (4)
  Sous-marin (3)
  Contre-torpilleur (2)
--------------------------------------------- Fait: ------------------------------------------------------------
Initialisation de la grille + affichage: OK
  --> Synchroniser la génération des bateaux et l'affichage: OK
tant que false --> recréer la disposition de la grille: OK
Placement des bateaux de façon aléatoire (4 types de bateaux différents) (Les bateaux ne se croisent jamais): OK
  Generation de bateaux: OK
    - random: OK
      - position OK
      - direction OK
    - selon les bateaux: (méthodes des bords?) OK
        - si sous marin limité les positions possibles OK
        - si navette... ainsi de suite OK

=> Adapter l'affichage
    - Creer deux griles (Joueur / Adversaire ) OK
    - Utiliser la même grille pour l'IA et le joueur : A Reflechir (Génération de 2 ou 4 grilles (visibles ou pas par le      joueur) OK
      - IU pour le jeu OK
  
--------------------------------------------- A faire: -----------------------------------------------------------

(2 modes: bateaux se touchent ou pas: ) X
différent aspect selon la grille joueur ou grille cible: (Necessaire ?) X

=> Adapter l'affichage
  - Revoir les bugs de présentation

=> Communication au joueur:
  - De ses réuissites et de ses échecs

=> Tir Difficulté
  - TireAuHasard OK
  - Très Facile, Facile, Normal OK

=> Sauvegarde
  - Sauvegarder les positions dans un fichier texte

         
Good Luck and Sink them all !
