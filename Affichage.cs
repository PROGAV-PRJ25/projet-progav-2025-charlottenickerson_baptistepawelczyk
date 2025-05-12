using System;
using System.Collections.Generic;

public class Affichage 
{
    // Définition des couleurs d'arrière-plan disponibles
    private readonly ConsoleColor[] availableColors = new ConsoleColor[]
    {
        ConsoleColor.Red,
        ConsoleColor.Green,
        ConsoleColor.Blue,
        ConsoleColor.Yellow,
        ConsoleColor.Magenta
    };
    
    // Grille de parcelles
    private Parcelle[,] parcelles;
    
    // Dimensions du plateau
    private readonly int largeur = 60;
    private readonly int hauteur = 15;
    
    // Parcelle actuellement sélectionnée
    private Parcelle? parcelleSelectionnee = null;
    
    // Position pour l'affichage détaillé d'une parcelle - maintenant positionné pour être sous le tableau de bord
    // Ces valeurs seront ajustées dynamiquement lors de l'affichage
    private int vueDetailleePosX = 5; // Sera calculé pour centrer la vue
    private int vueDetailleePosY = 30; // Sera ajusté en fonction de la position du tableau de bord
    
    // Nombre d'actions disponibles par tour
    private int actionsRestantes = 10;
    
    // Constructeur qui initialise les parcelles une seule fois
    public Affichage()
    {
        // Initialisation de la grille de parcelles
        int cellRows = hauteur / 2;
        int cellCols = largeur / 3;
        parcelles = new Parcelle[cellRows, cellCols];
        
        // Création des parcelles avec des couleurs aléatoires
        Random random = new Random();
        for (int i = 0; i < cellRows; i++) {
            for (int j = 0; j < cellCols; j++) {
                ConsoleColor couleur = availableColors[random.Next(availableColors.Length)];
                parcelles[i, j] = new Parcelle(i, j, couleur);
            }
        }
    }
    
    public void AfficherPlateau() {
        // Effacer la console avant l'affichage
        Console.Clear();
        
        // Afficher le titre du jeu et les informations de base
        Console.WriteLine("POTAGER VIRTUEL - TABLEAU DE BORD PRINCIPAL");
        Console.WriteLine($"Actions restantes ce tour: {actionsRestantes}/10");
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        Console.WriteLine();
        
        // Creation d'un plateau de jeu
        char[,] plateau = new char[hauteur, largeur];
        
        // Initialisation du plateau avec des caractères de grille au lieu d'espaces vides
        for (int i = 0; i < hauteur; i++) {
            for (int j = 0; j < largeur; j++) {
                // Dessin des lignes de la grille
                if (i % 2 == 0 && j % 3 == 0) {
                    plateau[i, j] = '+'; // Points d'intersection
                } else if (i % 2 == 0) {
                    plateau[i, j] = '-'; // Lignes horizontales
                } else if (j % 3 == 0) {
                    plateau[i, j] = '|'; // Lignes verticales
                } else {
                    plateau[i, j] = ' '; // Espaces vides dans les cellules
                }
            }
        }
        
        // Affichage du plateau
        for (int i = 0; i < hauteur; i++) {
            for (int j = 0; j < largeur; j++) {
                // Calcul de la position de la cellule pour la couleur
                int cellRow = i / 2;
                int cellCol = j / 3;
                
                // Vérification si la position actuelle est à l'intérieur d'une cellule (pas une ligne de la grille)
                if (i % 2 != 0 && j % 3 != 0) {
                    // Définition de la couleur d'arrière-plan pour la cellule
                    Console.BackgroundColor = parcelles[cellRow, cellCol].Couleur;
                    
                    // Afficher l'emoji de la plante si au centre de la cellule
                    if (j % 3 == 1 && i % 2 == 1) {
                        Console.Write(parcelles[cellRow, cellCol].ObtenirEmojiPlante());
                    } else {
                        Console.Write(" ");
                    }
                } else {
                    // Les lignes de la grille sont affichées avec l'arrière-plan par défaut
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(plateau[i, j]);
                }
            }
            // Réinitialisation de la couleur à la fin de chaque ligne
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
        
        // Réinitialisation de la couleur après le dessin
        Console.ResetColor();
        
        // Affichage des coins
        Console.SetCursorPosition(0, hauteur - 1);
        Console.Write("+");
        Console.SetCursorPosition(largeur - 1, hauteur - 1);
        Console.Write("+");
        
        // Affichage de la légende
        AfficherLegende(hauteur + 2);
        
        // Affichage des instructions pour la sélection des parcelles
        Console.SetCursorPosition(0, hauteur + 7);
        Console.WriteLine("Entrez les coordonnées x,y séparées par une virgule pour sélectionner une parcelle");
        Console.WriteLine("ou tapez 'next' pour passer au tour suivant: ");
    }
    
    // Méthode pour afficher une légende des emojis utilisés
    private void AfficherLegende(int posY)
    {
        Console.SetCursorPosition(0, posY);
        Console.WriteLine("LÉGENDE:");
        
        Console.SetCursorPosition(0, posY + 1);
        Console.WriteLine("🌱 - Plante en bonne santé");
        
        Console.SetCursorPosition(0, posY + 2);
        Console.WriteLine("🍂 - Plante en mauvaise santé (< 50%)");
        
        Console.SetCursorPosition(0, posY + 3);
        Console.WriteLine("💀 - Plante morte");
        
        Console.SetCursorPosition(30, posY + 1);
        Console.WriteLine("🪟 - Serre installée");
        
        Console.SetCursorPosition(30, posY + 2);
        Console.WriteLine("🧱 - Barrière installée");
        
        Console.SetCursorPosition(30, posY + 3);
        Console.WriteLine("☂️ - Pare-soleil installé");
    }
    
    // Méthode pour afficher la parcelle sélectionnée en dessous du tableau de bord
    public void AfficherParcelleSelectionnee(int tableauDeBordPosY)
    {
        if (parcelleSelectionnee != null)
        {
            // Positionner la vue détaillée sous le tableau de bord
            // Ajouter une marge de 3 lignes après le tableau de bord
            vueDetailleePosY = tableauDeBordPosY + 3;
            
            // Centrer horizontalement
            int largeurParcelle = 30; // Largeur de la vue détaillée de la parcelle élargie
            vueDetailleePosX = (largeur - largeurParcelle) / 2;
            
            // Vérifiez si l'espace est suffisant sur la console
            int requiredHeight = vueDetailleePosY + 20; // Hauteur approximative nécessaire pour la vue détaillée
            int availableHeight = Console.WindowHeight;
            
            // Si l'espace n'est pas suffisant, placez la vue plus haut
            if (requiredHeight > availableHeight)
            {
                // Ajuster la position Y pour s'assurer que tout est visible
                vueDetailleePosY = Math.Max(hauteur + 5, availableHeight - 20);
            }
            
            // Effacer l'écran et réafficher la vue principale
            Console.Clear();
            AfficherPlateau();
            
            // Afficher la parcelle avec une taille plus grande pour plus de détails
            int tailleAffichage = 12; // Augmenter la taille pour une meilleure visibilité
            parcelleSelectionnee.AfficherVueDetaillee(vueDetailleePosX, vueDetailleePosY, largeurParcelle, tailleAffichage);
            
            // Afficher les options d'action pour la parcelle
            AfficherOptionsAction();
        }
    }
    
    // Méthode pour afficher les options d'action
    private void AfficherOptionsAction()
    {
        Console.SetCursorPosition(0, vueDetailleePosY + 25);
        Console.WriteLine("Entrez le numéro de l'action que vous souhaitez effectuer,");
        Console.WriteLine("ou tapez '0' pour revenir au tableau principal: ");
    }
    
    // Méthode pour gérer les actions sur une parcelle
    public bool GererActionParcelle(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;
            
        if (int.TryParse(input, out int choixAction))
        {
            if (choixAction == 0)
            {
                // Retour au tableau principal
                parcelleSelectionnee = null;
                return false;
            }
            
            // Exécuter l'action choisie
            bool actionEffectuee = parcelleSelectionnee.ExecuterAction(choixAction);
            if (actionEffectuee)
            {
                // Décrémenter le compteur d'actions
                actionsRestantes--;
                
                // Vérifier si le tour est terminé
                if (actionsRestantes <= 0)
                {
                    actionsRestantes = 10; // Réinitialiser pour le prochain tour
                    return true; // Tour terminé
                }
            }
            
            // Réafficher la parcelle avec les mises à jour
            AfficherParcelleSelectionnee(hauteur + 2);
        }
        
        return false; // Ne pas passer au tour suivant
    }
    
    // Méthode pour gérer les clics sur les parcelles
    public bool GererClicParcelle(string input) {
        // Vérifier si l'entrée est vide ou "next" pour passer au tour suivant
        if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "next") {
            parcelleSelectionnee = null; // Désélectionner la parcelle actuelle
            actionsRestantes = 10; // Réinitialiser les actions pour le prochain tour
            return true; // Indiquer qu'on veut passer au tour suivant
        }
        
        // Essayer de parser les coordonnées x,y
        string[] coords = input.Split(',');
        if (coords.Length != 2) {
            return false; // Format invalide, ne pas passer au tour suivant
        }
        
        if (!int.TryParse(coords[0].Trim(), out int x) || !int.TryParse(coords[1].Trim(), out int y)) {
            return false; // Coordonnées non numériques, ne pas passer au tour suivant
        }
        
        // Convertir les coordonnées d'écran en indices de parcelles
        int col = x / 3;
        int row = y / 2;
        
        // Vérifier si les coordonnées sont dans les limites
        int cellRows = hauteur / 2;
        int cellCols = largeur / 3;
        
        if (row < 0 || row >= cellRows || col < 0 || col >= cellCols) {
            return false; // Coordonnées hors limites, ne pas passer au tour suivant
        }
        
        // Sélectionner la parcelle
        parcelleSelectionnee = parcelles[row, col];
        
        // Afficher la vue détaillée de la parcelle
        AfficherParcelleSelectionnee(hauteur + 7);
        
        return false; // Ne pas passer au tour suivant, permettre d'autres sélections
    }
    
    // Méthode appelée à chaque nouveau tour pour simuler la croissance des plantes
    public void SimulerCroissancePlantes(Temps conditions)
    {
        // Utiliser la nouvelle classe SimulateurPlante pour calculer l'évolution des plantes
        SimulateurPlante simulateur = new SimulateurPlante();
        simulateur.SimulerEvolutionPlantes(parcelles, conditions);
    }
}