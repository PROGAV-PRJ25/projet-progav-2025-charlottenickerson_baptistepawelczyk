using System;
using System.Collections.Generic;

public class Affichage 
{
    // Dictionnaire de symboles pour les différents types de terrain
    private readonly Dictionary<string, char> terrainSymbols = new Dictionary<string, char>
    {
        { "Normal", '·' },
        { "Sable", '▒' },
        { "Terre", '▓' },
        { "Argile", '█' },
        { "Marais", '≈' },
        { "Prairie", '♣' },
        { "Foret", '♠' },
        { "Montagne", '▲' },
        { "Desert", '◊' },
        { "Riviere", '∼' },
        { "Jungle", '♦' },
        { "Cratere", '◙' }
    };
    
    // Dictionnaire des couleurs associées aux types de terrain
    private readonly Dictionary<string, ConsoleColor> terrainColors = new Dictionary<string, ConsoleColor>
    {
        { "Normal", ConsoleColor.DarkGreen },
        { "Sable", ConsoleColor.Yellow },
        { "Terre", ConsoleColor.DarkYellow },
        { "Argile", ConsoleColor.Red },
        { "Marais", ConsoleColor.DarkCyan },
        { "Prairie", ConsoleColor.Green },
        { "Foret", ConsoleColor.DarkGreen },
        { "Montagne", ConsoleColor.Gray },
        { "Desert", ConsoleColor.Yellow },
        { "Riviere", ConsoleColor.Blue },
        { "Jungle", ConsoleColor.Green },
        { "Cratere", ConsoleColor.DarkGray }
    };
    
    // Symboles pour représenter les états des plantes
    private readonly Dictionary<string, char> planteSymbols = new Dictionary<string, char>
    {
        { "Vide", ' ' },
        { "Graine", '.' },
        { "Pousse", ',' },
        { "Jeune", '╦' },
        { "Mature", '╬' },
        { "Floraison", '✿' },
        { "Fruit", '❀' },
        { "Malade", '✗' },
        { "Morte", '✝' }
    };
    
    // Grille de parcelles
    private Parcelle[,] parcelles;
    
    // Dimensions du plateau
    private readonly int largeur = 80; // Augmenté pour plus d'espace
    private readonly int hauteur = 25; // Augmenté pour plus de parcelles
    
    // Taille de chaque cellule - utiliser une largeur paire pour une meilleure symétrie
    private readonly int cellWidth = 6; // Largeur fixée à 6 pour garantir une symétrie parfaite
    private readonly int cellHeight = 3;
    
    // Parcelle actuellement sélectionnée
    private Parcelle? parcelleSelectionnee = null;
    
    // Position pour l'affichage détaillé d'une parcelle
    private int vueDetailleePosX = 5;
    private int vueDetailleePosY = 30; // Adjusted to be dynamic based on plateau height
    
    // Position pour l'affichage de la météo
    private int meteoPosX = 50; // Adjusted to avoid overlap
    private int meteoPosY = 2; // Adjusted to be near the top
    
    // Titre du jeu
    private readonly string titreJeu = "SIMULATEUR DE POTAGER";
    
    // Constructeur qui initialise les parcelles une seule fois
    public Affichage()
    {
        // Initialisation de la grille de parcelles
        int cellRows = hauteur / cellHeight;
        int cellCols = largeur / cellWidth;
        parcelles = new Parcelle[cellRows, cellCols];
        
        // Création des parcelles avec différents types de terrain
        Random random = new Random();
        string[] typesTerrains = { "Normal", "Sable", "Terre", "Argile", "Prairie", "Foret" };
        
        for (int i = 0; i < cellRows; i++) {
            for (int j = 0; j < cellCols; j++) {
                string typeTerrain = typesTerrains[random.Next(typesTerrains.Length)];
                ConsoleColor couleur = terrainColors[typeTerrain];
                parcelles[i, j] = new Parcelle(i, j, couleur);
                parcelles[i, j].Type = typeTerrain;
                
                // Initialisation aléatoire de la fertilité et humidité
                parcelles[i, j].Fertilite = random.Next(60, 100);
                parcelles[i, j].Humidite = random.Next(30, 80);
            }
        }
    }
    
    public void AfficherPlateau() {
        // Effacer la console avant d'afficher le plateau
        Console.Clear();
        
        // Afficher le titre du jeu centré en haut
        Console.ForegroundColor = ConsoleColor.Cyan;
        int positionTitre = (largeur - titreJeu.Length) / 2;
        Console.SetCursorPosition(positionTitre, 0);
        Console.WriteLine(titreJeu);
        Console.ResetColor();

        // Afficher la météo en haut à droite
        AfficherMeteo(meteoPosX, meteoPosY);
        
        // Marge supérieure
        Console.WriteLine();
        
        // Définir la position de départ pour le plateau
        int startX = 2;
        int startY = 2;
        
        // Nombre de cellules
        int cellRows = parcelles.GetLength(0);
        int cellCols = parcelles.GetLength(1);
        
        // Largeur totale du plateau
        int totalWidth = cellCols * cellWidth + 1;
        
        // Dessiner la bordure supérieure
        Console.SetCursorPosition(startX, startY);
        Console.Write("╔");
        for (int j = 0; j < cellCols; j++) {
            Console.Write(new string('═', cellWidth - 1));
            if (j < cellCols - 1) Console.Write("╦");
        }
        Console.WriteLine("╗");
        
        // Dessiner les cellules
        for (int i = 0; i < cellRows; i++) {
            // Dessiner les lignes de contenu des cellules
            for (int ligne = 0; ligne < cellHeight - 1; ligne++) {
                Console.SetCursorPosition(startX, startY + 1 + i * cellHeight + ligne);
                Console.Write("║");
                
                for (int j = 0; j < cellCols; j++) {
                    // Contenu de la cellule
                    ConsoleColor bgColor = parcelles[i, j].Couleur;
                    string typeTerrain = parcelles[i, j].Type;
                    char terrainSymbol = terrainSymbols.ContainsKey(typeTerrain) ? terrainSymbols[typeTerrain] : ' ';
                    
                    // Différentes lignes pour ajouter des détails visuels
                    if (ligne == 0) {
                        // Ligne supérieure: afficher les coordonnées
                        Console.BackgroundColor = bgColor;
                        // Format pour assurer que les coordonnées sont affichées avec une largeur fixe et centrées
                        string coordsDisplay = $"{i},{j}";
                        int padding = cellWidth - 1 - coordsDisplay.Length;
                        int leftPad = padding / 2;
                        int rightPad = padding - leftPad;
                        Console.Write(new string(' ', leftPad) + coordsDisplay + new string(' ', rightPad));
                        Console.BackgroundColor = ConsoleColor.Black;
                    } else if (ligne == 1) {
                        // Ligne centrale: afficher le symbole de la plante si elle existe, sinon le symbole du terrain
                        Console.BackgroundColor = bgColor;
                        
                        if (parcelles[i, j].EstPlantee) {
                            // Récupérer le symbole correspondant à l'état de la plante
                            char planteSymbol = ' ';
                            string etatPlante = parcelles[i, j].EtatPlante;
                            
                            if (planteSymbols.ContainsKey(etatPlante)) {
                                planteSymbol = planteSymbols[etatPlante];
                            }
                            
                            // Changer la couleur du texte selon la santé de la plante
                            if (parcelles[i, j].SantePlante < 30) {
                                Console.ForegroundColor = ConsoleColor.Red;
                            } else if (parcelles[i, j].SantePlante < 60) {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            } else {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            
                            // S'assurer que la largeur est constante et parfaitement centrée
                            string display = planteSymbol.ToString();
                            int padding = cellWidth - 1 - display.Length;
                            int leftPad = padding / 2;
                            int rightPad = padding - leftPad;
                            Console.Write(new string(' ', leftPad) + display + new string(' ', rightPad));
                            Console.ForegroundColor = ConsoleColor.White;
                        } else {
                            // S'assurer que la largeur est constante
                            string display = $" {terrainSymbol} ";
                            Console.Write(display.PadLeft((cellWidth - display.Length) / 2 + display.Length).PadRight(cellWidth - 1));
                        }
                        
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    
                    // S'assurer que chaque colonne a exactement la même largeur
                    if (j < cellCols - 1) {
                        Console.Write("║");
                    }
                }
                Console.WriteLine("║");
            }
            
            // Dessiner la bordure entre les lignes de cellules
            if (i < cellRows - 1) {
                Console.SetCursorPosition(startX, startY + (i + 1) * cellHeight);
                Console.Write("╠");
                for (int j = 0; j < cellCols; j++) {
                    Console.Write(new string('═', cellWidth - 1));
                    if (j < cellCols - 1) Console.Write("╬");
                }
                Console.WriteLine("╣");
            }
        }
        
        // Dessiner la bordure inférieure
        Console.SetCursorPosition(startX, startY + cellRows * cellHeight);
        Console.Write("╚");
        for (int j = 0; j < cellCols; j++) {
            Console.Write(new string('═', cellWidth - 1));
            if (j < cellCols - 1) Console.Write("╩");
        }
        Console.WriteLine("╝");
        
        // Afficher la légende des terrains
        AfficherLegende(startX, startY + cellRows * cellHeight + 2);
        
        // Afficher les instructions pour la sélection des parcelles
        Console.SetCursorPosition(startX, startY + cellRows * cellHeight + terrainColors.Count + 4);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Sélectionnez une parcelle (entrez les coordonnées i,j séparées par une virgule) ou 'next' pour continuer");
        Console.ResetColor();
    }
    
    // Méthode pour afficher la légende des types de terrain
    private void AfficherLegende(int posX, int posY)
    {
        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("LÉGENDE DES TERRAINS:");
        Console.ResetColor();
        
        int ligne = 1;
        foreach (var terrain in terrainSymbols)
        {
            if (terrainColors.ContainsKey(terrain.Key))
            {
                Console.SetCursorPosition(posX, posY + ligne);
                Console.ForegroundColor = terrainColors[terrain.Key];
                Console.Write($"{terrain.Value} ");
                Console.ResetColor();
                Console.Write($"- {terrain.Key}");
                ligne++;
            }
        }
    }
    
    // Méthode pour afficher des statistiques supplémentaires
    private void AfficherStatistiques(int posX, int posY)
    {
        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("STATISTIQUES DU JARDIN:");
        Console.ResetColor();
        
        // Comptage des types de terrain
        Dictionary<string, int> compteurTerrains = new Dictionary<string, int>();
        int totalParcelles = 0;
        
        for (int i = 0; i < parcelles.GetLength(0); i++)
        {
            for (int j = 0; j < parcelles.GetLength(1); j++)
            {
                string type = parcelles[i, j].Type;
                if (!compteurTerrains.ContainsKey(type))
                {
                    compteurTerrains[type] = 0;
                }
                compteurTerrains[type]++;
                totalParcelles++;
            }
        }
        
        int ligne = 1;
        foreach (var compteur in compteurTerrains)
        {
            Console.SetCursorPosition(posX, posY + ligne);
            Console.ForegroundColor = terrainColors[compteur.Key];
            float pourcentage = (float)compteur.Value / totalParcelles * 100;
            Console.Write($"{compteur.Key}: {compteur.Value} parcelles ({pourcentage:F1}%)");
            Console.ResetColor();
            ligne++;
        }
    }
    
    // Méthode pour afficher la parcelle sélectionnée avec des détails améliorés
    public void AfficherParcelleSelectionnee(int tableauDeBordPosY)
    {
        if (parcelleSelectionnee != null)
        {
            // Calculer la position pour la vue détaillée
            int cellRows = parcelles.GetLength(0);
            int plateauHeight = cellRows * cellHeight; // Hauteur du contenu du plateau
            int legendHeight = terrainSymbols.Count + 2; // Hauteur approximative de la légende
            int instructionsHeight = 3; // Hauteur approximative des instructions
            
            // La vue détaillée sera affichée sous le plateau, la légende et les instructions
            // startY (2) + plateauHeight + 2 (bordures) + 1 (espace) + legendHeight + 1 (espace) + instructionsHeight + 1 (espace)
            vueDetailleePosY = 2 + plateauHeight + 2 + 1 + legendHeight + 1 + instructionsHeight + 1;
            
            // Centrer horizontalement
            int largeurParcelleDetails = 45; // Largeur pour le cadre des détails
            vueDetailleePosX = (largeur - largeurParcelleDetails) / 2;
            
            // Hauteur approximative de la boîte de détails de la parcelle
            int hauteurParcelleDetails = 10; // Ajusté car AfficherVisuelParcelle est retiré
            
            // Effacer la zone où les détails de la parcelle seront affichés
            EffacerZone(vueDetailleePosX - 2, vueDetailleePosY - 2, largeurParcelleDetails + 4, hauteurParcelleDetails + 4); 

            // Afficher un cadre spécial pour indiquer la sélection
            Console.SetCursorPosition(vueDetailleePosX - 2, vueDetailleePosY - 1); // Ajusté pour le titre
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════ DÉTAILS DE LA PARCELLE SÉLECTIONNÉE ═══════╗");
            
            // Afficher les informations de base
            Console.SetCursorPosition(vueDetailleePosX, vueDetailleePosY +1); // Ajusté pour être dans le cadre
            Console.WriteLine($"Position: [{parcelleSelectionnee.Row}, {parcelleSelectionnee.Col}]");
            
            Console.SetCursorPosition(vueDetailleePosX, vueDetailleePosY + 2);
            Console.ForegroundColor = terrainColors[parcelleSelectionnee.Type];
            Console.Write($"Terrain: {parcelleSelectionnee.Type} ");
            Console.Write(terrainSymbols[parcelleSelectionnee.Type]);
            Console.ResetColor();
            
            // Afficher les barres de progression pour fertilité et humidité
            AfficherBarreProgression(vueDetailleePosX, vueDetailleePosY + 4, "Fertilité", parcelleSelectionnee.Fertilite, 100, ConsoleColor.Green);
            AfficherBarreProgression(vueDetailleePosX, vueDetailleePosY + 6, "Humidité", parcelleSelectionnee.Humidite, 100, ConsoleColor.Blue);
            
            // Fermer le cadre des détails
            Console.SetCursorPosition(vueDetailleePosX - 2, vueDetailleePosY + 10); // Position après les barres de progression
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.ResetColor();
            
            // Ajouter un espace entre les détails et les actions
            int actionsPosY = vueDetailleePosY + 12; // 2 lignes après la fermeture du cadre des détails
            
            // Afficher une nouvelle section pour les actions disponibles
            Console.SetCursorPosition(vueDetailleePosX - 2, actionsPosY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════ ACTIONS DISPONIBLES ════════════╗");
            
            // Afficher les actions possibles dans un nouveau cadre séparé
            AfficherActionsDisponibles(vueDetailleePosX, actionsPosY + 2);
            
            // Fermer le cadre des actions
            Console.SetCursorPosition(vueDetailleePosX - 2, actionsPosY + 6);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
    
    // Méthode pour afficher une barre de progression
    private void AfficherBarreProgression(int posX, int posY, string label, int valeur, int max, ConsoleColor couleur)
    {
        int longueurBarre = 30;
        int remplissage = (int)Math.Ceiling((double)valeur / max * longueurBarre);
        
        Console.SetCursorPosition(posX, posY);
        Console.Write($"{label}: {valeur}% ");
        
        Console.SetCursorPosition(posX, posY + 1);
        Console.Write("[");
        Console.ForegroundColor = couleur;
        
        // Ajout de caractères de remplissage proportionnels à la valeur
        Console.Write(new string('█', remplissage));
        
        Console.ResetColor();
        Console.Write(new string(' ', longueurBarre - remplissage));
        Console.Write("]");
    }
    
    // Méthode pour afficher une représentation visuelle de la parcelle (maintenue si besoin ailleurs, mais non appelée ici)
    private void AfficherVisuelParcelle(int posX, int posY, int largeurVisuel, int hauteurVisuel)
    {
        if (parcelleSelectionnee == null) return;
        
        string typeTerrain = parcelleSelectionnee.Type;
        ConsoleColor couleurTerrain = terrainColors[typeTerrain];
        char symboleTerrain = terrainSymbols[typeTerrain];
        
        // Dessiner le contour
        Console.SetCursorPosition(posX, posY);
        Console.Write("┌" + new string('─', largeurVisuel - 2) + "┐");
        
        // Dessiner le contenu
        for (int i = 1; i < hauteurVisuel - 1; i++)
        {
            Console.SetCursorPosition(posX, posY + i);
            Console.Write("│");
            
            // Remplir avec le symbole du terrain
            Console.BackgroundColor = couleurTerrain;
            for (int j = 0; j < largeurVisuel - 2; j++)
            {
                if (j % 2 == 0 && i % 2 == 0)
                {
                    Console.Write(symboleTerrain);
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            
            Console.Write("│");
        }
        
        // Dessiner le bas
        Console.SetCursorPosition(posX, posY + hauteurVisuel - 1);
        Console.Write("└" + new string('─', largeurVisuel - 2) + "┘");
    }
    
    // Méthode pour afficher les actions disponibles pour une parcelle
    private void AfficherActionsDisponibles(int posX, int posY)
    {
        Console.SetCursorPosition(posX, posY);
        Console.WriteLine("1. Arroser    2. Fertiliser    3. Désherber");
        
        Console.SetCursorPosition(posX, posY + 1);
        Console.WriteLine("4. Planter    5. Récolter      6. Examiner");
    }
    
    // Méthode pour afficher la météo
    public void AfficherMeteo(int posX, int posY)
    {
        // Effacer la zone de la météo avant de la redessiner
        EffacerZone(posX -1, posY -1, 30, 7); // largeur et hauteur approx de la box météo

        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════ MÉTÉO ════════╗");
        
        Console.SetCursorPosition(posX, posY + 1);
        Console.WriteLine("║                        ║"); // Placeholder for weather icon or details
        
        Console.SetCursorPosition(posX, posY + 2);
        Console.WriteLine("║ Temp: 22°C             ║"); // Example temperature
        
        Console.SetCursorPosition(posX, posY + 3);
        Console.WriteLine("║ Pluie: 5mm             ║"); // Example precipitation
        
        Console.SetCursorPosition(posX, posY + 4);
        Console.WriteLine("║ Vent: 15km/h           ║"); // Example wind
        
        Console.SetCursorPosition(posX, posY + 5);
        Console.WriteLine("╚════════════════════════╝");
        Console.ResetColor();
    }

    // Méthode pour effacer une zone de la console
    private void EffacerZone(int posX, int posY, int largeur, int hauteur)
    {
        for (int i = 0; i < hauteur; i++)
        {
            Console.SetCursorPosition(posX, posY + i);
            Console.Write(new string(' ', largeur));
        }
    }

    // Méthode pour gérer les clics sur les parcelles
    public bool GererClicParcelle(string input) {
        // Vérifier si l'entrée est vide ou "next" pour passer au tour suivant
        if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "next") {
            parcelleSelectionnee = null; // Désélectionner la parcelle actuelle
            return true; // Indiquer qu'on veut passer au tour suivant
        }
        
        // Essayer de parser les coordonnées i,j
        string[] coords = input.Split(',');
        if (coords.Length != 2) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Format invalide. Utilisez le format 'i,j' ou 'next'.");
            Console.ResetColor();
            return false; // Format invalide, ne pas passer au tour suivant
        }
        
        if (!int.TryParse(coords[0].Trim(), out int i) || !int.TryParse(coords[1].Trim(), out int j)) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Coordonnées non numériques. Utilisez des nombres.");
            Console.ResetColor();
            return false; // Coordonnées non numériques, ne pas passer au tour suivant
        }
        
        // Vérifier si les coordonnées sont dans les limites
        int cellRows = parcelles.GetLength(0);
        int cellCols = parcelles.GetLength(1);
        
        if (i < 0 || i >= cellRows || j < 0 || j >= cellCols) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Coordonnées hors limites. Les valeurs doivent être entre [0,0] et [{cellRows-1},{cellCols-1}].");
            Console.ResetColor();
            return false; // Coordonnées hors limites, ne pas passer au tour suivant
        }
        
        // Sélectionner la parcelle
        parcelleSelectionnee = parcelles[i, j];
        
        // Feedback visuel
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Parcelle [{i},{j}] sélectionnée. Type: {parcelleSelectionnee.Type}");
        Console.ResetColor();
        
        return false; // Ne pas passer au tour suivant, permettre d'autres sélections
    }
    
    // Méthode pour traiter les actions du joueur sur une parcelle sélectionnée
    public void TraiterActionParcelle(string action)
    {
        if (parcelleSelectionnee == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Aucune parcelle sélectionnée. Veuillez d'abord sélectionner une parcelle.");
            Console.ResetColor();
            return;
        }
        
        switch (action)
        {
            case "1": // Arroser
                parcelleSelectionnee.Arroser();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"La parcelle [{parcelleSelectionnee.Row},{parcelleSelectionnee.Col}] a été arrosée.");
                Console.ResetColor();
                break;
                
            case "2": // Fertiliser
                parcelleSelectionnee.Fertiliser();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"La parcelle [{parcelleSelectionnee.Row},{parcelleSelectionnee.Col}] a été fertilisée.");
                Console.ResetColor();
                break;
                
            case "3": // Désherber
                parcelleSelectionnee.Desherber();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"La parcelle [{parcelleSelectionnee.Row},{parcelleSelectionnee.Col}] a été désherbée.");
                Console.ResetColor();
                break;
                
            case "4": // Planter
                // Liste de plantes disponibles à planter (pourrait être récupérée depuis une autre source)
                List<string> plantesDisponibles = new List<string>
                {
                    "Tomate", "Carotte", "Laitue", "Poivron", "Aubergine", "Courgette",
                    "Menthe", "Persil", "Basilic", "Rosier", "Bambou", "Coton"
                };
                
                // Afficher la liste des plantes disponibles
                Console.WriteLine("\nPlantes disponibles:");
                for (int i = 0; i < plantesDisponibles.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {plantesDisponibles[i]}");
                }
                
                // Demander à l'utilisateur de choisir une plante
                Console.Write("\nChoisissez une plante à planter (1-" + plantesDisponibles.Count + "): ");
                if (int.TryParse(Console.ReadLine(), out int choixPlante) && choixPlante >= 1 && choixPlante <= plantesDisponibles.Count)
                {
                    string planteName = plantesDisponibles[choixPlante - 1];
                    parcelleSelectionnee.Planter(planteName);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Une {planteName} a été plantée dans la parcelle [{parcelleSelectionnee.Row},{parcelleSelectionnee.Col}].");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Choix invalide. Aucune plante n'a été plantée.");
                    Console.ResetColor();
                }
                break;
                
            case "5": // Récolter
                if (parcelleSelectionnee.EstPlantee && parcelleSelectionnee.EtatPlante == "Fruit")
                {
                    string nomPlante = parcelleSelectionnee.NomPlante;
                    // Réinitialiser la parcelle après récolte
                    parcelleSelectionnee.EstPlantee = false;
                    parcelleSelectionnee.NomPlante = "";
                    parcelleSelectionnee.EtatPlante = "Vide";
                    parcelleSelectionnee.SantePlante = 0;
                    parcelleSelectionnee.StadeCroissance = 0;
                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"La {nomPlante} a été récoltée de la parcelle [{parcelleSelectionnee.Row},{parcelleSelectionnee.Col}].");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Rien à récolter sur cette parcelle.");
                    Console.ResetColor();
                }
                break;
                
            case "6": // Examiner
                // Afficher des informations détaillées sur la parcelle
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"=== EXAMEN DÉTAILLÉ DE LA PARCELLE [{parcelleSelectionnee.Row},{parcelleSelectionnee.Col}] ===");
                Console.ResetColor();
                
                Console.WriteLine($"\nType de terrain: {parcelleSelectionnee.Type}");
                Console.WriteLine($"Fertilité: {parcelleSelectionnee.Fertilite}%");
                Console.WriteLine($"Humidité: {parcelleSelectionnee.Humidite}%");
                Console.WriteLine($"Ensoleillement: {parcelleSelectionnee.Ensoleillement}%");
                
                if (parcelleSelectionnee.EstPlantee)
                {
                    Console.WriteLine($"\nPlante: {parcelleSelectionnee.NomPlante}");
                    Console.WriteLine($"État: {parcelleSelectionnee.EtatPlante}");
                    Console.WriteLine($"Santé: {parcelleSelectionnee.SantePlante}%");
                    Console.WriteLine($"Stade de croissance: {parcelleSelectionnee.StadeCroissance}/5");
                    
                    // Afficher des conseils spécifiques basés sur l'état de la plante
                    if (parcelleSelectionnee.SantePlante < 50)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nConseil: Cette plante a besoin d'attention! Pensez à l'arroser et la fertiliser.");
                        Console.ResetColor();
                    }
                    else if (parcelleSelectionnee.Humidite < 30)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nConseil: Le sol est sec. Un arrosage serait bénéfique pour la plante.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.WriteLine("\nAucune plante sur cette parcelle.");
                    
                    // Afficher des recommandations basées sur le type de terrain
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPlantes recommandées pour ce type de terrain:");
                    
                    switch (parcelleSelectionnee.Type)
                    {
                        case "Sable":
                            Console.WriteLine("- Cactus");
                            Console.WriteLine("- Aloès");
                            Console.WriteLine("- Romarin");
                            break;
                        case "Terre":
                            Console.WriteLine("- Tomate");
                            Console.WriteLine("- Carotte");
                            Console.WriteLine("- Salade");
                            break;
                        case "Argile":
                            Console.WriteLine("- Chou");
                            Console.WriteLine("- Poivron");
                            Console.WriteLine("- Betterave");
                            break;
                        case "Prairie":
                            Console.WriteLine("- Blé");
                            Console.WriteLine("- Maïs");
                            Console.WriteLine("- Tournesol");
                            break;
                        default:
                            Console.WriteLine("- Tomate");
                            Console.WriteLine("- Carotte");
                            Console.WriteLine("- Salade");
                            break;
                    }
                    Console.ResetColor();
                }
                
                Console.WriteLine("\nAppuyez sur une touche pour revenir au jeu...");
                Console.ReadKey(true);
                break;
                
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Action non reconnue.");
                Console.ResetColor();
                break;
        }
    }

    // Méthode pour récupérer une parcelle par ses coordonnées
    public Parcelle GetParcelle(int row, int col)
    {
        return parcelles[row, col];
    }
    
    // Méthode pour récupérer la largeur de la grille (nombre de colonnes)
    public int GetLargeurGrille()
    {
        return parcelles.GetLength(1);
    }
    
    // Méthode pour récupérer la hauteur de la grille (nombre de lignes)
    public int GetHauteurGrille()
    {
        return parcelles.GetLength(0);
    }
    
    // Méthode pour mettre à jour l'affichage des plantes dans la grille
    public void MettreAJourAffichagePlantes()
    {
        // Récupération des dimensions de la grille
        int cellRows = parcelles.GetLength(0);
        int cellCols = parcelles.GetLength(1);
        
        // Parcourir toutes les parcelles
        for (int i = 0; i < cellRows; i++)
        {
            for (int j = 0; j < cellCols; j++)
            {
                // Si la parcelle contient une plante
                if (parcelles[i, j].EstPlantee)
                {
                    // Mettre à jour l'affichage en fonction de l'état de la plante
                    string etatPlante = parcelles[i, j].EtatPlante;
                    
                    // Changer la couleur de la parcelle en fonction de la santé de la plante
                    if (parcelles[i, j].SantePlante < 30)
                    {
                        // Plante en mauvaise santé: rouge
                        parcelles[i, j].Couleur = ConsoleColor.DarkRed;
                    }
                    else if (parcelles[i, j].SantePlante < 60)
                    {
                        // Plante en santé moyenne: jaune
                        parcelles[i, j].Couleur = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        // Plante en bonne santé: vert
                        parcelles[i, j].Couleur = ConsoleColor.Green;
                    }
                }
            }
        }
    }

    // Méthode pour afficher les prévisions météo
    public void AfficherPrevisionsMeteo(int posX, int posY, Temperature temperature, Precipitations precipitations, Saison.TypeSaison saison)
    {
        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("PRÉVISIONS MÉTÉO POUR LA SEMAINE PROCHAINE:");
        Console.ResetColor();
        
        // Générer des prévisions météo aléatoires mais réalistes
        Random random = new Random();
        
        // Générer la température prévue en fonction de la saison
        double tempBase = 0;
        string previsionGenerale = "";
        
        switch (saison)
        {
            case Saison.TypeSaison.Printemps:
                tempBase = 15;
                previsionGenerale = "Temps variable, averses possibles";
                break;
            case Saison.TypeSaison.Ete:
                tempBase = 25;
                previsionGenerale = "Ensoleillé, chaleur persistante";
                break;
            case Saison.TypeSaison.Automne:
                tempBase = 10;
                previsionGenerale = "Frais, possibilité de pluie";
                break;
            case Saison.TypeSaison.Hiver:
                tempBase = 0;
                previsionGenerale = "Froid, risque de gel";
                break;
        }
        
        // Afficher la prévision générale
        Console.SetCursorPosition(posX, posY + 1);
        Console.WriteLine($"Tendance générale: {previsionGenerale}");
        
        // Afficher les prévisions sur 3 jours
        for (int jour = 1; jour <= 3; jour++)
        {
            // Calculer la température prévue avec une variation aléatoire
            double tempPrevue = tempBase + random.Next(-5, 6);
            
            // Générer des précipitations prévues
            int precipPrevues = random.Next(0, 101);
            string typePrecip = "";
            
            if (tempPrevue < 0 && precipPrevues > 30)
            {
                typePrecip = "Neige";
            }
            else if (precipPrevues < 20)
            {
                typePrecip = "Sec";
            }
            else if (precipPrevues < 50)
            {
                typePrecip = "Quelques gouttes";
            }
            else if (precipPrevues < 70)
            {
                typePrecip = "Pluie modérée";
            }
            else
            {
                typePrecip = "Fortes pluies";
            }
            
            // Définir les couleurs en fonction des conditions
            ConsoleColor couleurTemp = ConsoleColor.White;
            if (tempPrevue < 5) couleurTemp = ConsoleColor.Blue;
            else if (tempPrevue > 30) couleurTemp = ConsoleColor.Red;
            
            ConsoleColor couleurPrecip = ConsoleColor.White;
            if (typePrecip == "Sec") couleurPrecip = ConsoleColor.Yellow;
            else if (typePrecip == "Fortes pluies") couleurPrecip = ConsoleColor.Blue;
            
            // Afficher les prévisions pour ce jour
            Console.SetCursorPosition(posX, posY + 2 + jour);
            Console.Write($"Jour +{jour}: ");
            
            Console.ForegroundColor = couleurTemp;
            Console.Write($"{tempPrevue:F1}°C, ");
            
            Console.ForegroundColor = couleurPrecip;
            Console.Write(typePrecip);
            
            Console.ResetColor();
        }
    }

    // Méthode pour afficher une animation météo sur le plateau
    public void AfficherAnimationMeteo(string typeAnimation, int duree = 2000)
    {
        int cellRows = parcelles.GetLength(0);
        int cellCols = parcelles.GetLength(1);
        
        // Sauvegarder la position actuelle du curseur
        int cursorLeft = Console.CursorLeft;
        int cursorTop = Console.CursorTop;
        
        // Définir les caractères d'animation selon le type
        char[] animChars;
        ConsoleColor couleur;
        
        switch (typeAnimation.ToLower())
        {
            case "pluie":
                animChars = new char[] { '|', '╱', '─', '╲' };
                couleur = ConsoleColor.Blue;
                break;
            case "neige":
                animChars = new char[] { '*', '❄', '·', '❅' };
                couleur = ConsoleColor.White;
                break;
            case "soleil":
                animChars = new char[] { '☀', '۞', '☼', '۝' };
                couleur = ConsoleColor.Yellow;
                break;
            case "orage":
                animChars = new char[] { '⚡', '☇', '↯', '☈' };
                couleur = ConsoleColor.DarkYellow;
                break;
            case "vent":
                animChars = new char[] { '~', '≈', '≋', '≈' };
                couleur = ConsoleColor.Cyan;
                break;
            case "grele":
                animChars = new char[] { '•', '◦', '°', '○' };
                couleur = ConsoleColor.DarkBlue;
                break;
            default:
                animChars = new char[] { '.', 'o', 'O', 'o' };
                couleur = ConsoleColor.White;
                break;
        }
        
        // Variables pour le timing
        int frameDelay = 100; // Délai entre les frames en millisecondes
        int totalFrames = duree / frameDelay;
        
        // Afficher l'animation
        Random random = new Random();
        Console.ForegroundColor = couleur;
        
        for (int frame = 0; frame < totalFrames; frame++)
        {
            // Pour chaque frame, mettre à jour quelques positions aléatoires sur le plateau
            for (int i = 0; i < 10; i++) // 10 mises à jour par frame
            {
                int row = random.Next(cellRows);
                int col = random.Next(cellCols);
                
                // Calculer la position d'affichage
                int posX = 2 + col * cellWidth + random.Next(cellWidth);
                int posY = 2 + row * cellHeight + random.Next(cellHeight);
                
                // S'assurer que nous sommes dans les limites de l'écran
                if (posX < Console.WindowWidth && posY < Console.WindowHeight)
                {
                    Console.SetCursorPosition(posX, posY);
                    Console.Write(animChars[frame % animChars.Length]);
                }
            }
            
            // Pause entre les frames
            System.Threading.Thread.Sleep(frameDelay);
        }
        
        // Restaurer la couleur et la position du curseur
        Console.ResetColor();
        Console.SetCursorPosition(cursorLeft, cursorTop);
        
        // Réafficher le plateau pour effacer l'animation
        AfficherPlateau();
    }

    // Méthode pour afficher une alerte d'urgence avec animation
    public void AfficherAlerteUrgence(string typeUrgence)
    {
        // Effacer la console
        Console.Clear();
        
        // Afficher une alerte animée
        Console.ForegroundColor = ConsoleColor.Red;
        
        // Cadre clignotant
        for (int i = 0; i < 5; i++)
        {
            Console.Clear();
            Console.WriteLine("\n\n");
            Console.WriteLine("  ╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("  ║                        ALERTE URGENCE                          ║");
            Console.WriteLine("  ╚════════════════════════════════════════════════════════════════╝");
            Console.WriteLine("\n");
            Console.WriteLine($"  {typeUrgence}");
            Console.WriteLine("\n");
            Console.WriteLine("  Préparez-vous à intervenir rapidement pour protéger votre jardin!");
            Console.WriteLine("\n");
            Console.WriteLine("  Le mode urgence va s'activer dans quelques instants...");
            
            System.Threading.Thread.Sleep(300);
            Console.Clear();
            System.Threading.Thread.Sleep(200);
        }
        
        // Afficher l'information finale
        Console.WriteLine("\n\n");
        Console.WriteLine("  ╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("  ║                        ALERTE URGENCE                          ║");
        Console.WriteLine("  ╚════════════════════════════════════════════════════════════════╝");
        Console.WriteLine("\n");
        Console.WriteLine($"  {typeUrgence}");
        Console.WriteLine("\n");
        Console.WriteLine("  Préparez-vous à intervenir rapidement pour protéger votre jardin!");
        Console.WriteLine("\n");
        Console.WriteLine("  Appuyez sur une touche pour commencer le mode urgence...");
        Console.ResetColor();
        
        // Attendre que l'utilisateur appuie sur une touche
        Console.ReadKey(true);
    }
    
    // Méthode pour afficher une mini-jeu pour interagir avec une urgence (comme un rongeur)
    public void AfficherMiniJeuIntrus(string typeIntrus)
    {
        Console.Clear();
        
        // Configuration du mini-jeu
        int largeurJeu = 60;
        int hauteurJeu = 20;
        int posXIntrus = 5;
        int posYIntrus = 5;
        int posXJoueur = 30;
        int posYJoueur = 15;
        char symbolIntrus = 'R'; // R pour rongeur
        char symbolJoueur = 'J'; // J pour jardinier
        ConsoleColor couleurIntrus = ConsoleColor.Red;
        ConsoleColor couleurJoueur = ConsoleColor.Green;
        bool intrusCapture = false;
        int points = 0;
        int tempsRestant = 30; // 30 secondes
        DateTime debutJeu = DateTime.Now;
        
        // Instructions
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("MODE URGENCE: CAPTURE D'INTRUS");
        Console.WriteLine($"Un {typeIntrus} est en train d'envahir votre jardin!");
        Console.WriteLine("Utilisez les flèches directionnelles pour déplacer le jardinier (J).");
        Console.WriteLine("Approchez-vous de l'intrus (R) pour le capturer.");
        Console.WriteLine("Vous avez 30 secondes pour capturer l'intrus avant qu'il ne détruise vos cultures!");
        Console.WriteLine("Appuyez sur une touche pour commencer...");
        Console.ResetColor();
        Console.ReadKey(true);
        
        // Boucle principale du mini-jeu
        while (!intrusCapture && tempsRestant > 0)
        {
            // Effacer l'écran
            Console.Clear();
            
            // Calculer le temps restant
            tempsRestant = 30 - (int)(DateTime.Now - debutJeu).TotalSeconds;
            
            // Afficher le cadre du jeu
            Console.WriteLine("╔" + new string('═', largeurJeu) + "╗");
            for (int i = 0; i < hauteurJeu; i++)
            {
                Console.Write("║");
                for (int j = 0; j < largeurJeu; j++)
                {
                    if (i == posYIntrus && j == posXIntrus)
                    {
                        Console.ForegroundColor = couleurIntrus;
                        Console.Write(symbolIntrus);
                        Console.ResetColor();
                    }
                    else if (i == posYJoueur && j == posXJoueur)
                    {
                        Console.ForegroundColor = couleurJoueur;
                        Console.Write(symbolJoueur);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("║");
            }
            Console.WriteLine("╚" + new string('═', largeurJeu) + "╝");
            
            // Afficher les informations du jeu
            Console.WriteLine($"Points: {points} | Temps restant: {tempsRestant} secondes");
            
            // Vérifier si le joueur a capturé l'intrus
            if (Math.Abs(posXJoueur - posXIntrus) <= 1 && Math.Abs(posYJoueur - posYIntrus) <= 1)
            {
                points += 10;
                
                // Déplacer l'intrus à une nouvelle position aléatoire
                Random random = new Random();
                posXIntrus = random.Next(1, largeurJeu - 1);
                posYIntrus = random.Next(1, hauteurJeu - 1);
                
                // Si le joueur a obtenu 50 points, il gagne
                if (points >= 50)
                {
                    intrusCapture = true;
                }
            }
            
            // Déplacer l'intrus de manière aléatoire
            if (!intrusCapture && tempsRestant % 3 == 0) // Déplacement toutes les 3 secondes
            {
                Random random = new Random();
                int direction = random.Next(4);
                
                switch (direction)
                {
                    case 0: // Haut
                        if (posYIntrus > 1) posYIntrus--;
                        break;
                    case 1: // Bas
                        if (posYIntrus < hauteurJeu - 2) posYIntrus++;
                        break;
                    case 2: // Gauche
                        if (posXIntrus > 1) posXIntrus--;
                        break;
                    case 3: // Droite
                        if (posXIntrus < largeurJeu - 2) posXIntrus++;
                        break;
                }
            }
            
            // Lire l'entrée utilisateur
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (posYJoueur > 1) posYJoueur--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (posYJoueur < hauteurJeu - 2) posYJoueur++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (posXJoueur > 1) posXJoueur--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (posXJoueur < largeurJeu - 2) posXJoueur++;
                        break;
                    case ConsoleKey.Escape:
                        // Sortir du jeu
                        tempsRestant = 0;
                        break;
                }
            }
            
            // Petite pause pour ne pas surcharger le CPU
            System.Threading.Thread.Sleep(50);
        }
        
        // Afficher le résultat du mini-jeu
        Console.Clear();
        Console.ForegroundColor = intrusCapture ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine("\n\n");
        Console.WriteLine("  ╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine(intrusCapture ? 
                         "  ║                      INTRUS CAPTURÉ !                           ║" : 
                         "  ║                    TEMPS ÉCOULÉ !                               ║");
        Console.WriteLine("  ╚════════════════════════════════════════════════════════════════╝");
        Console.WriteLine("\n");
        Console.WriteLine(intrusCapture ? 
                         $"  Bravo! Vous avez capturé le {typeIntrus} avant qu'il ne cause trop de dégâts." : 
                         $"  Le {typeIntrus} a réussi à s'échapper et a causé des dégâts à vos cultures.");
        Console.WriteLine($"\n  Points: {points}");
        Console.WriteLine("\n  Appuyez sur une touche pour continuer...");
        Console.ResetColor();
        
        Console.ReadKey(true);
    }
}