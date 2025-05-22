using System;
using System.Collections.Generic;

public class Obstacle
{
    // Propriétés
    public string Type { get; private set; }
    public string Nom { get; private set; }
    public int PositionX { get; private set; }
    public int PositionY { get; private set; }
    public int Degats { get; private set; }
    public bool EstActif { get; private set; }
    public string Description { get; private set; }
    
    // Constructeur
    public Obstacle(string type, string nom, int degats, string description)
    {
        Type = type;
        Nom = nom;
        Degats = degats;
        Description = description;
        EstActif = true;
        
        // Position aléatoire
        Random random = new Random();
        PositionX = random.Next(20); // Ajustez selon la taille de votre grille
        PositionY = random.Next(10); // Ajustez selon la taille de votre grille
    }
    
    // Méthode pour déplacer l'obstacle (pour les intrus)
    public void Deplacer()
    {
        // Seulement si c'est un intrus et qu'il est actif
        if (Type == "Intrus" && EstActif)
        {
            Random random = new Random();
            int direction = random.Next(4);
            
            switch (direction)
            {
                case 0: // Haut
                    PositionY = Math.Max(0, PositionY - 1);
                    break;
                case 1: // Droite
                    PositionX = Math.Min(19, PositionX + 1); // Limité à 20 colonnes
                    break;
                case 2: // Bas
                    PositionY = Math.Min(9, PositionY + 1); // Limité à 10 lignes
                    break;
                case 3: // Gauche
                    PositionX = Math.Max(0, PositionX - 1);
                    break;
            }
        }
    }
    
    // Méthode pour appliquer l'effet de l'obstacle à un terrain
    public void AppliquerEffet(Terrain terrain)
    {
        if (!EstActif)
        {
            return;
        }
        
        // Effet différent selon le type d'obstacle
        switch (Type)
        {
            case "Intrus":
                // Un intrus peut endommager des plantes ou en manger
                List<Plante> plantes = terrain.GetPlantes();
                
                if (plantes.Count > 0 && terrain.GetEstClos() == false)
                {
                    Random random = new Random();
                    int index = random.Next(plantes.Count);
                    
                    // Simuler des dégâts sur une plante aléatoire
                    // Note: ceci simule l'effet, mais ne l'applique pas réellement aux plantes
                    // pour ne pas interférer avec le système existant
                    Console.WriteLine($"L'intrus {Nom} endommage une plante dans le terrain {terrain.GetNom()}!");
                }
                break;
                
            case "Intemperie":
                // Une intempérie affecte tout le terrain si non protégé
                if (!terrain.GetEstProtege())
                {
                    Console.WriteLine($"L'intempérie {Nom} frappe le terrain {terrain.GetNom()}!");
                    // Ici on pourrait ajouter des effets spécifiques selon le type d'intempérie
                }
                break;
        }
    }
    
    // Méthode pour désactiver l'obstacle
    public void Desactiver()
    {
        EstActif = false;
    }
    
    // Méthode pour afficher l'obstacle
    public string Afficher()
    {
        string symbole = "";
        
        switch (Type)
        {
            case "Intrus":
                switch (Nom)
                {
                    case "Rongeur": symbole = "🐁"; break;
                    case "Oiseau": symbole = "🐦"; break;
                    case "Lapin": symbole = "🐇"; break;
                    default: symbole = "?"; break;
                }
                break;
                
            case "Intemperie":
                switch (Nom)
                {
                    case "Grêle": symbole = "❄"; break;
                    case "Orage": symbole = "⚡"; break;
                    case "Tempête": symbole = "🌪"; break;
                    default: symbole = "☁"; break;
                }
                break;
                
            default:
                symbole = "✗";
                break;
        }
        
        return symbole;
    }
    
    // Méthode pour obtenir des informations détaillées sur l'obstacle
    public override string ToString()
    {
        return $"{Type}: {Nom}\nPosition: [{PositionX},{PositionY}]\nDégâts potentiels: {Degats}\nDescription: {Description}\nActif: {(EstActif ? "Oui" : "Non")}";
    }
}
