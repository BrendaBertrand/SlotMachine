# SlotMachine

Design a game where the user can play a make-believe slot machine. The user will be asked to make a wager to play various lines in a 3 x 3 grid. They can play center line, all three horizontal lines, all vertical lines and diagonals.
For instance the user can enter $3 dollars and play all three horizontal lines. If the top line hits a winning combination, they earn $1 dollar for that line.

Rocket Tips: The mention of a grid here should be a dead giveaway that you need a 2D array. You will also need functionality that can check a horizontal line, a vertical line and a diagonal. Depending on the number of lines they play, you may need to execute all three of these statements one or multiple times to look for winning lines. If they are playing three lines, you would call your horizontal line check function three times... one for the top row, one for the center row and one for the bottom row. Each of these row checking algorithms will then need to look for winning combinations. The result is then dumped into the player’s money total. As for the mechanism to determine what the wheels produce per spin, use a random number generating function.


## French version 

Concevez un jeu dans lequel l'utilisateur peut jouer à une machine à sous imaginaire. L'utilisateur sera invité à faire un pari pour jouer différentes lignes dans une grille 3 x 3. Ils peuvent jouer sur la ligne centrale, les trois lignes horizontales, toutes les lignes verticales et diagonales.
Par exemple, l'utilisateur peut saisir 3 dollars et jouer sur les trois lignes horizontales. Si la première ligne correspond à une combinaison gagnante, ils gagnent 1 dollar pour cette ligne.

Conseils : La mention d'une grille ici devrait être un signe évident que vous avez besoin d'un tableau 2D. Vous aurez également besoin d’une fonctionnalité permettant de vérifier une ligne horizontale, une ligne verticale et une diagonale. En fonction du nombre de lignes jouées, vous devrez peut-être exécuter ces trois instructions une ou plusieurs fois pour rechercher des lignes gagnantes. S'ils jouent trois lignes, vous appelleriez votre fonction de vérification de ligne horizontale trois fois... une pour la rangée du haut, une pour la rangée du centre et une pour la rangée du bas. Chacun de ces algorithmes de vérification de lignes devra alors rechercher des combinaisons gagnantes. Le résultat est ensuite ajouté au total d’argent du joueur. Quant au mécanisme permettant de déterminer ce que les roues produisent par tour, utilisez une fonction de génération de nombres aléatoires.