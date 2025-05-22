using System;
using System.Collections.Generic;

public class Parcelle
{
    // Position de la parcelle sur la grille
    public int Row { get; private set; }
    public int Col { get; private set; }
    
    // Couleur de la parcelle
    public ConsoleColor Couleur { get; set; }
    
    // Caractéristiques du terrain
    public string Type { get; set; } = "Normal";
    public int Fertilite { get; set; } = 100;
    public int Humidite { get; set; } = 50;
    public int Ensoleillement { get; set; } = 70;
    
    // État des plantes sur la parcelle
    public bool EstPlantee { get; set; } = false;
    public string NomPlante { get; set; } = "";
    public string EtatPlante { get; set; } = "Vide";
    public int SantePlante { get; set; } = 0;
    public int StadeCroissance { get; set; } = 0;
    
    // Maladies et parasites actifs sur cette parcelle
    public List<string> MaladiesActives { get; private set; }
    public List<string> ParasitesActifs { get; private set; }
    
    // Conditions environnementales spécifiques à la parcelle
    public ConditionsEnvironnementales Conditions { get; private set; }
    
    // Constructeur
    public Parcelle(int row, int col, ConsoleColor couleur)
    {
        Row = row;
        Col = col;
        Couleur = couleur;
        MaladiesActives = new List<string>();
        ParasitesActifs = new List<string>();
        
        // Initialisation des conditions environnementales par défaut
        double niveauEauInitial = 50;
        double temperatureInitiale = 20;
        string luminositeInitiale = "Moyenne";
        
        Conditions = new ConditionsEnvironnementales(niveauEauInitial, temperatureInitiale, luminositeInitiale, Type);
    }
    
    // Méthode pour planter une nouvelle plante
    public void Planter(string nomPlante)
    {
        EstPlantee = true;
        NomPlante = nomPlante;
        EtatPlante = "Graine";
        SantePlante = 100;
        StadeCroissance = 0;
    }
    
    // Méthode pour arroser la parcelle
    public void Arroser()
    {
        Humidite = Math.Min(100, Humidite + 30);
        Conditions.MettreAJour(Humidite, Conditions.Temperature, Conditions.Luminosite);
    }
    
    // Méthode pour fertiliser la parcelle
    public void Fertiliser()
    {
        Fertilite = Math.Min(100, Fertilite + 20);
    }
    
    // Méthode pour désherber la parcelle
    public void Desherber()
    {
        // Augmentation de la santé de la plante si elle existe
        if (EstPlantee && SantePlante < 100)
        {
            SantePlante = Math.Min(100, SantePlante + 15);
        }
    }
    
    // Méthode pour faire croître la plante
    public void FaireCroitre()
    {
        if (!EstPlantee) return;
        
        // Vérifier les conditions environnementales
        double compatibilite = EvaluerCompatibiliteEnvironnementale();
        
        if (compatibilite >= 0.5) // Si les conditions sont au moins à 50% favorables
        {
            // Progression du stade de croissance
            if (StadeCroissance < 5) // 5 stades: Graine, Pousse, Jeune, Mature, Floraison
            {
                StadeCroissance++;
                MettreAJourEtatPlante();
            }
            
            // Impact sur la santé basé sur la compatibilité
            if (compatibilite >= 0.8)
            {
                SantePlante = Math.Min(100, SantePlante + 5);
            }
        }
        else
        {
            // Conditions défavorables: diminution de la santé
            SantePlante = Math.Max(0, SantePlante - 10);
            
            // La plante meurt si sa santé tombe à zéro
            if (SantePlante == 0)
            {
                EtatPlante = "Morte";
            }
        }
    }
    
    // Méthode pour mettre à jour l'état de la plante en fonction de son stade de croissance
    private void MettreAJourEtatPlante()
    {
        switch (StadeCroissance)
        {
            case 0:
                EtatPlante = "Graine";
                break;
            case 1:
                EtatPlante = "Pousse";
                break;
            case 2:
                EtatPlante = "Jeune";
                break;
            case 3:
                EtatPlante = "Mature";
                break;
            case 4:
                EtatPlante = "Floraison";
                break;
            case 5:
                EtatPlante = "Fruit";
                break;
            default:
                EtatPlante = "Mature";
                break;
        }
    }
    
    // Méthode pour évaluer la compatibilité des conditions environnementales avec la plante
    private double EvaluerCompatibiliteEnvironnementale()
    {
        // Définir les besoins hypothétiques de la plante (à remplacer par de vrais besoins)
        double besoinsEau = 60;
        List<int> temperaturesPref = new List<int> { 15, 25 };
        string luminositePref = "Moyenne";
        string terrainPref = Type;
        
        // Utiliser la méthode de la classe ConditionsEnvironnementales
        return Conditions.EvaluerCompatibilite(besoinsEau, temperaturesPref, luminositePref, terrainPref);
    }
    
    // Méthode pour afficher une vue détaillée de la parcelle (utilisée par l'ancienne interface)
    public void AfficherVueDetaillee(int posX, int posY, int largeur = 10, int hauteur = 10)
    {
        try
        {
            // S'assurer que la vue tiendra dans la fenêtre de la console
            int maxX = Math.Min(posX + largeur, Console.WindowWidth - 1);
            int maxY = Math.Min(posY + hauteur + 7, Console.WindowHeight - 2);
            
            largeur = Math.Min(largeur, maxX - posX);
            hauteur = Math.Min(hauteur, maxY - posY - 7);
            
            if (largeur < 3 || hauteur < 3)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                Console.WriteLine("Erreur: Pas assez d'espace pour afficher la parcelle.");
                return;
            }
            
            int originalX = Console.CursorLeft;
            int originalY = Console.CursorTop;
            
            Console.SetCursorPosition(posX, posY - 1);
            Console.WriteLine("PARCELLE SÉLECTIONNÉE:");
            
            // Afficher une vue plus grande de la parcelle
            for (int i = 0; i < hauteur; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                
                for (int j = 0; j < largeur; j++)
                {
                    if (i == 0 || i == hauteur - 1 || j == 0 || j == largeur - 1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.BackgroundColor = Couleur;
                        
                        // Afficher un symbole représentant l'état de la plante si elle existe
                        if (EstPlantee && i == hauteur / 2 && j == largeur / 2)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            char symbole = '*';
                            
                            switch (EtatPlante)
                            {
                                case "Graine": symbole = '.'; break;
                                case "Pousse": symbole = ','; break;
                                case "Jeune": symbole = '╦'; break;
                                case "Mature": symbole = '╬'; break;
                                case "Floraison": symbole = '✿'; break;
                                case "Fruit": symbole = '❀'; break;
                                case "Morte": symbole = '✝'; break;
                                default: symbole = ' '; break;
                            }
                            
                            Console.Write(symbole);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
            }
            
            Console.BackgroundColor = ConsoleColor.Black;
            
            // Afficher les informations de la parcelle directement sous la vue
            AfficherTableauDeBordParcelle(posX, posY + hauteur, largeur);
            
            Console.SetCursorPosition(originalX, originalY);
        }
        catch (Exception ex)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine($"Erreur d'affichage: {ex.Message}");
        }
    }
    
    // Méthode pour afficher le tableau de bord de la parcelle
    private void AfficherTableauDeBordParcelle(int posX, int posY, int largeur)
    {
        // Utiliser un format d'affichage plus compact
        Console.SetCursorPosition(posX, posY);
        Console.Write("┌" + new string('─', Math.Max(0, largeur - 2)) + "┐");
        
        Console.SetCursorPosition(posX, posY + 1);
        Console.Write($"│ Type: {Type.PadRight(Math.Max(0, largeur - 9))}│");
        
        Console.SetCursorPosition(posX, posY + 2);
        Console.Write($"│ Fert: {Fertilite}%{new string(' ', Math.Max(0, largeur - 10 - Fertilite.ToString().Length))}│");
        
        Console.SetCursorPosition(posX, posY + 3);
        Console.Write($"│ Hum: {Humidite}%{new string(' ', Math.Max(0, largeur - 9 - Humidite.ToString().Length))}│");
        
        if (EstPlantee)
        {
            Console.SetCursorPosition(posX, posY + 4);
            Console.Write($"│ Plante: {NomPlante.PadRight(Math.Max(0, largeur - 11))}│");
            
            Console.SetCursorPosition(posX, posY + 5);
            Console.Write($"│ État: {EtatPlante.PadRight(Math.Max(0, largeur - 9))}│");
            
            Console.SetCursorPosition(posX, posY + 6);
            Console.Write($"│ Santé: {SantePlante}%{new string(' ', Math.Max(0, largeur - 11 - SantePlante.ToString().Length))}│");
            
            Console.SetCursorPosition(posX, posY + 7);
            Console.Write("└" + new string('─', Math.Max(0, largeur - 2)) + "┘");
        }
        else
        {
            Console.SetCursorPosition(posX, posY + 4);
            Console.Write("└" + new string('─', Math.Max(0, largeur - 2)) + "┘");
        }
        
        // Afficher les coordonnées réelles en dessous
        Console.SetCursorPosition(posX, posY + (EstPlantee ? 8 : 5));
        Console.Write($"Coords: [{Row},{Col}]");
    }
}