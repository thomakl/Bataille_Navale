# Bataille_Navale
Jeu populaire de la bataille navale, implémenté en C#

BatailleNavale

Le programme réalisé permet à un joueur humain d’affronter l’ordinateur. Il affiche deux grilles : Celle des bateaux du joueur, avec les coups au but de l’adversaire et les bateaux du déjà détruits. Celle des bateaux de l’adversaire, avec les coups au but du joueur et les bateaux déjà détruits. Le placement des bateaux du joueur et de l’ordinateur en début de partie sont aléatoires. Le joueur a la possibilité de visualiser le placement obtenu et d’en demander un autre jusqu’à ce que le résultat lui convienne. La saisie des coups du joueur est contrôlée afin d’éviter toute erreur. De manière générale, l’ergonomie du programme (saisies, affichages, déroulement de la partie) doit être étudiée avec attention. Au niveau de difficulté minimal, l’ordinateur tire au hasard sur les bateaux du joueur. Un niveau supplémentaire optionnel le fait jouer plus “intelligemment”, par exemple en essayant de couler les bateaux touchés. Lors de son tour, le joueur aura la possibilité de sauvegarder une partie qu’il pourra charger ultérieurement. Les positions des bateaux et l’état de la partie seront stockées dans un fichier texte selon une organisation à définir

Bateaux :

  Porte-avions (5 cases)
  Cuirassé (4)
  Sous-marin (3)
  Contre-torpilleur (2)


--------------------------------------------- A faire: ------------------------------------------------------------

Initialisation de la grille + affichage: OK
  --> Synchroniser la génération des bateaux et l'affichage: OK
tant que false --> recréer la disposition de la grille:
différent aspect selon la grille joueur ou grille cible:
Placement des bateaux de façon aléatoire (4 types de bateaux différents) (Les bateaux ne se croisent jamais): 
2 modes: bateaux se touchent ou pas: 
TireAuHasard

  Generation de bateaux:
    - random:
      - position
      - direction
    - selon les bateaux: (méthodes des bords?)
        - si sous marin limité les positions possibles
        - si navette... ainsi de suite
        
        
Good Luck and Sink them all !
