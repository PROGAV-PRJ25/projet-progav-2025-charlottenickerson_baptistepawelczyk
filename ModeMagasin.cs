using System;
using System.Collections.Generic;

public class ModeMagasin
{
    // Propriétés
    private int argent;
    private List<Produit> stockProduits;
    private List<Produit> stockSemis;
    private List<Terrain> terrains;
    
    // Constructeur
    public ModeMagasin(List<Terrain> terrains)
    {
        this.argent = 100; // Montant de départ
        this.stockProduits = new List<Produit>();
        this.stockSemis = new List<Produit>();
        this.terrains = terrains;
    }
    
    // Méthode pour ajouter un produit au stock
    public void AjouterProduit(string nom, int quantite, bool estSemis = false)
    {
        // Vérifier si le produit existe déjà dans le stock
        List<Produit> stock = estSemis ? stockSemis : stockProduits;
        Produit? produit = stock.Find(p => p.Nom == nom);
        
        if (produit != null)
        {
            // Le produit existe déjà, augmenter la quantité
            produit.Quantite += quantite;
        }
        else
        {
            // Le produit n'existe pas, le créer
            int prix = DeterminerPrixProduit(nom, estSemis);
            produit = new Produit(nom, prix, quantite, estSemis);
            stock.Add(produit);
        }
    }
    
    // Méthode pour déterminer le prix d'un produit
    private int DeterminerPrixProduit(string nom, bool estSemis)
    {
        // Détermination du prix en fonction du type de produit
        if (estSemis)
        {
            // Les semis sont généralement moins chers
            return new Random().Next(5, 20);
        }
        else
        {
            // Les produits récoltés ont une valeur plus élevée
            return new Random().Next(10, 50);
        }
    }
    
    // Méthode pour vendre un produit
    public void VendreProduit(string nom, int quantite)
    {
        Produit? produit = stockProduits.Find(p => p.Nom == nom);
        
        if (produit == null || produit.Quantite < quantite)
        {
            Console.WriteLine($"Impossible de vendre {quantite} {nom}(s): stock insuffisant.");
            return;
        }
        
        // Calculer le montant de la vente
        int montant = produit.Prix * quantite;
        
        // Mettre à jour le stock
        produit.Quantite -= quantite;
        
        // Supprimer le produit du stock s'il n'y en a plus
        if (produit.Quantite <= 0)
        {
            stockProduits.Remove(produit);
        }
        
        // Ajouter l'argent
        argent += montant;
        
        Console.WriteLine($"Vous avez vendu {quantite} {nom}(s) pour {montant} pièces.");
    }
    
    // Méthode pour transformer des produits en semis
    public void TransformerEnSemis(string nom, int quantite)
    {
        Produit? produit = stockProduits.Find(p => p.Nom == nom);
        
        if (produit == null || produit.Quantite < quantite)
        {
            Console.WriteLine($"Impossible de transformer {quantite} {nom}(s) en semis: stock insuffisant.");
            return;
        }
        
        // Mettre à jour le stock de produits
        produit.Quantite -= quantite;
        
        // Supprimer le produit du stock s'il n'y en a plus
        if (produit.Quantite <= 0)
        {
            stockProduits.Remove(produit);
        }
        
        // Ajouter les semis au stock (avec un ratio de conversion, par exemple 1:2)
        int nombreSemis = quantite * 2;
        AjouterProduit(nom + " (semis)", nombreSemis, true);
        
        Console.WriteLine($"Vous avez transformé {quantite} {nom}(s) en {nombreSemis} semis.");
    }
    
    // Méthode pour acheter un terrain
    public void AcheterTerrain(string type)
    {
        int prix = DeterminerPrixTerrain(type);
        
        if (argent < prix)
        {
            Console.WriteLine($"Vous n'avez pas assez d'argent pour acheter ce terrain ({prix} pièces).");
            return;
        }
        
        // Créer le nouveau terrain
        Terrain? nouveauTerrain = null;
        string nom = $"Terrain acheté ({terrains.Count + 1})";
        
        switch (type.ToLower())
        {
            case "plaine":
                nouveauTerrain = new Plaine(nom);
                break;
            case "prairie":
                nouveauTerrain = new Prairie(nom);
                break;
            case "desert":
                nouveauTerrain = new Desert(nom);
                break;
            case "jungle":
                nouveauTerrain = new Jungle(nom);
                break;
            case "foret":
                nouveauTerrain = new Foret(nom);
                break;
            case "marais":
                nouveauTerrain = new Marais(nom);
                break;
            case "montagne":
                nouveauTerrain = new Montagne(nom);
                break;
            case "riviere":
                nouveauTerrain = new Riviere(nom);
                break;
            case "cratere":
                nouveauTerrain = new Cratere(nom);
                break;
            default:
                Console.WriteLine($"Type de terrain inconnu: {type}");
                return;
        }
        
        // Déduire l'argent
        argent -= prix;
        
        // Ajouter le terrain à la liste
        terrains.Add(nouveauTerrain);
        
        Console.WriteLine($"Vous avez acheté un terrain de type {type} pour {prix} pièces.");
    }
    
    // Méthode pour déterminer le prix d'un terrain
    private int DeterminerPrixTerrain(string type)
    {
        // Prix de base
        int prix = 200;
        
        // Ajustement selon le type de terrain
        switch (type.ToLower())
        {
            case "plaine":
                prix = 200;
                break;
            case "prairie":
                prix = 200;
                break;
            case "desert":
                prix = 150;
                break;
            case "jungle":
                prix = 300;
                break;
            case "foret":
                prix = 250;
                break;
            case "marais":
                prix = 180;
                break;
            case "montagne":
                prix = 350;
                break;
            case "riviere":
                prix = 400;
                break;
            case "cratere":
                prix = 300;
                break;
        }
        
        return prix;
    }
    
    // Méthode pour acheter du matériel
    public void AcheterMateriel(string materiel)
    {
        int prix = DeterminerPrixMateriel(materiel);
        
        if (argent < prix)
        {
            Console.WriteLine($"Vous n'avez pas assez d'argent pour acheter ce matériel ({prix} pièces).");
            return;
        }
        
        // Déduire l'argent
        argent -= prix;
        
        // Appliquer les effets du matériel
        AppliquerEffetMateriel(materiel);
        
        Console.WriteLine($"Vous avez acheté {materiel} pour {prix} pièces.");
    }
    
    // Méthode pour déterminer le prix du matériel
    private int DeterminerPrixMateriel(string materiel)
    {
        switch (materiel.ToLower())
        {
            case "pelle":
                return 30;
            case "arrosoir":
                return 20;
            case "serre":
                return 150;
            case "barriere":
                return 80;
            case "voilage":
                return 50;
            case "epouvantail":
                return 40;
            default:
                return 50;
        }
    }
    
    // Méthode pour appliquer les effets du matériel acheté
    private void AppliquerEffetMateriel(string materiel)
    {
        switch (materiel.ToLower())
        {
            case "serre":
                // Protéger un terrain aléatoire ou le premier terrain
                if (terrains.Count > 0)
                {
                    terrains[0].Proteger();
                    Console.WriteLine($"La serre a été installée sur le terrain {terrains[0].GetNom()}.");
                }
                break;
            case "barriere":
                // Clore un terrain aléatoire ou le premier terrain
                if (terrains.Count > 0)
                {
                    terrains[0].Clore();
                    Console.WriteLine($"La barrière a été installée autour du terrain {terrains[0].GetNom()}.");
                }
                break;
            case "epouvantail":
                // Réduire les chances d'intrus sur tous les terrains
                Console.WriteLine("L'épouvantail a été installé pour éloigner les intrus.");
                break;
            default:
                Console.WriteLine($"Vous avez maintenant un(e) {materiel} dans votre inventaire.");
                break;
        }
    }
    
    // Méthode pour afficher le bilan financier
    public void AfficherBilan()
    {
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║                   BILAN FINANCIER                     ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
        
        Console.WriteLine($"\nSolde actuel: {argent} pièces");
        
        Console.WriteLine("\nInventaire des produits:");
        if (stockProduits.Count == 0)
        {
            Console.WriteLine("  Aucun produit en stock");
        }
        else
        {
            foreach (Produit produit in stockProduits)
            {
                Console.WriteLine($"  {produit.Nom}: {produit.Quantite} (valeur: {produit.Prix * produit.Quantite} pièces)");
            }
        }
        
        Console.WriteLine("\nInventaire des semis:");
        if (stockSemis.Count == 0)
        {
            Console.WriteLine("  Aucun semis en stock");
        }
        else
        {
            foreach (Produit semis in stockSemis)
            {
                Console.WriteLine($"  {semis.Nom}: {semis.Quantite} (valeur: {semis.Prix * semis.Quantite} pièces)");
            }
        }
        
        // Calculer la valeur totale
        int valeurTotale = argent;
        foreach (Produit produit in stockProduits)
        {
            valeurTotale += produit.Prix * produit.Quantite;
        }
        foreach (Produit semis in stockSemis)
        {
            valeurTotale += semis.Prix * semis.Quantite;
        }
        
        Console.WriteLine($"\nValeur totale: {valeurTotale} pièces");
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Méthode pour afficher le menu du magasin
    public void AfficherMenu()
    {
        bool continuer = true;
        
        while (continuer)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      MAGASIN                          ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            
            Console.WriteLine($"\nArgent disponible: {argent} pièces");
            
            Console.WriteLine("\nActions disponibles:");
            Console.WriteLine("1. Vendre des produits");
            Console.WriteLine("2. Transformer des produits en semis");
            Console.WriteLine("3. Acheter un terrain");
            Console.WriteLine("4. Acheter du matériel");
            Console.WriteLine("5. Voir le bilan financier");
            Console.WriteLine("6. Quitter le magasin");
            
            Console.Write("\nChoisissez une action (1-6): ");
            string? choix = Console.ReadLine() ?? "6";
            
            switch (choix)
            {
                case "1":
                    MenuVendreProduits();
                    break;
                case "2":
                    MenuTransformerEnSemis();
                    break;
                case "3":
                    MenuAcheterTerrain();
                    break;
                case "4":
                    MenuAcheterMateriel();
                    break;
                case "5":
                    AfficherBilan();
                    break;
                case "6":
                    continuer = false;
                    break;
                default:
                    Console.WriteLine("Choix non reconnu. Appuyez sur une touche pour continuer...");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
    
    // Menu pour vendre des produits
    private void MenuVendreProduits()
    {
        Console.Clear();
        Console.WriteLine("=== VENDRE DES PRODUITS ===");
        
        if (stockProduits.Count == 0)
        {
            Console.WriteLine("\nVous n'avez aucun produit à vendre.");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        Console.WriteLine("\nProduits disponibles:");
        for (int i = 0; i < stockProduits.Count; i++)
        {
            Console.WriteLine($"{i+1}. {stockProduits[i].Nom} (quantité: {stockProduits[i].Quantite}, prix: {stockProduits[i].Prix} pièces)");
        }
        
        Console.WriteLine("\nEntrez le numéro du produit à vendre (0 pour annuler): ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index == 0 || index > stockProduits.Count)
        {
            return;
        }
        
        Produit produit = stockProduits[index - 1];
        
        Console.WriteLine($"\nCombien de {produit.Nom} voulez-vous vendre? (max: {produit.Quantite}): ");
        if (!int.TryParse(Console.ReadLine(), out int quantite) || quantite <= 0 || quantite > produit.Quantite)
        {
            Console.WriteLine("Quantité invalide.");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        VendreProduit(produit.Nom, quantite);
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Menu pour transformer des produits en semis
    private void MenuTransformerEnSemis()
    {
        Console.Clear();
        Console.WriteLine("=== TRANSFORMER DES PRODUITS EN SEMIS ===");
        
        if (stockProduits.Count == 0)
        {
            Console.WriteLine("\nVous n'avez aucun produit à transformer.");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        Console.WriteLine("\nProduits disponibles:");
        for (int i = 0; i < stockProduits.Count; i++)
        {
            Console.WriteLine($"{i+1}. {stockProduits[i].Nom} (quantité: {stockProduits[i].Quantite})");
        }
        
        Console.WriteLine("\nEntrez le numéro du produit à transformer (0 pour annuler): ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index == 0 || index > stockProduits.Count)
        {
            return;
        }
        
        Produit produit = stockProduits[index - 1];
        
        Console.WriteLine($"\nCombien de {produit.Nom} voulez-vous transformer en semis? (max: {produit.Quantite}): ");
        if (!int.TryParse(Console.ReadLine(), out int quantite) || quantite <= 0 || quantite > produit.Quantite)
        {
            Console.WriteLine("Quantité invalide.");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        TransformerEnSemis(produit.Nom, quantite);
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Menu pour acheter un terrain
    private void MenuAcheterTerrain()
    {
        Console.Clear();
        Console.WriteLine("=== ACHETER UN TERRAIN ===");
        
        Console.WriteLine("\nTypes de terrains disponibles:");
        Console.WriteLine("1. Plaine (200 pièces)");
        Console.WriteLine("2. Prairie (200 pièces)");
        Console.WriteLine("3. Desert (150 pièces)");
        Console.WriteLine("4. Jungle (300 pièces)");
        Console.WriteLine("5. Foret (250 pièces)");
        Console.WriteLine("6. Marais (180 pièces)");
        Console.WriteLine("7. Montagne (350 pièces)");
        Console.WriteLine("8. Riviere (400 pièces)");
        Console.WriteLine("9. Cratere (300 pièces)");
        
        Console.WriteLine($"\nArgent disponible: {argent} pièces");
        
        Console.WriteLine("\nEntrez le numéro du terrain à acheter (0 pour annuler): ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index == 0 || index > 9)
        {
            return;
        }
        
        string[] types = { "Plaine", "Prairie", "Desert", "Jungle", "Foret", "Marais", "Montagne", "Riviere", "Cratere" };
        string type = types[index - 1];
        
        AcheterTerrain(type);
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Menu pour acheter du matériel
    private void MenuAcheterMateriel()
    {
        Console.Clear();
        Console.WriteLine("=== ACHETER DU MATÉRIEL ===");
        
        Console.WriteLine("\nMatériel disponible:");
        Console.WriteLine("1. Pelle (30 pièces)");
        Console.WriteLine("2. Arrosoir (20 pièces)");
        Console.WriteLine("3. Serre (150 pièces)");
        Console.WriteLine("4. Barrière (80 pièces)");
        Console.WriteLine("5. Voilage (50 pièces)");
        Console.WriteLine("6. Épouvantail (40 pièces)");
        
        Console.WriteLine($"\nArgent disponible: {argent} pièces");
        
        Console.WriteLine("\nEntrez le numéro du matériel à acheter (0 pour annuler): ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index == 0 || index > 6)
        {
            return;
        }
        
        string[] materiels = { "Pelle", "Arrosoir", "Serre", "Barriere", "Voilage", "Epouvantail" };
        string materiel = materiels[index - 1];
        
        AcheterMateriel(materiel);
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Getter pour l'argent
    public int GetArgent()
    {
        return argent;
    }
    
    // Méthode pour ajouter de l'argent
    public void AjouterArgent(int montant)
    {
        argent += montant;
    }
}

// Classe pour représenter un produit
public class Produit
{
    public string Nom { get; private set; }
    public int Prix { get; private set; }
    public int Quantite { get; set; }
    public bool EstSemis { get; private set; }
    
    public Produit(string nom, int prix, int quantite, bool estSemis)
    {
        Nom = nom;
        Prix = prix;
        Quantite = quantite;
        EstSemis = estSemis;
    }
}
