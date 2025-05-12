using System;
using System.Collections.Generic;

/// <summary>
/// Classe qui définit les étapes de croissance pour chaque type de plante
/// </summary>
public class CycleDeVie
{
    // Structure pour définir une étape de croissance
    public struct EtapeCroissance
    {
        public string Nom { get; set; }          // Nom de l'étape (ex: "Germination")
        public int DureeMinimale { get; set; }   // Durée minimale en jours
        public int HauteurMin { get; set; }      // Hauteur minimale en cm
        public int HauteurMax { get; set; }      // Hauteur maximale en cm
        public string Emoji { get; set; }        // Emoji représentant cette étape
        public bool PeutEtreRecoltee { get; set; } // Si la plante peut être récoltée à cette étape

        public EtapeCroissance(string nom, int duree, int hauteurMin, int hauteurMax, string emoji, bool peutEtreRecoltee = false)
        {
            Nom = nom;
            DureeMinimale = duree;
            HauteurMin = hauteurMin;
            HauteurMax = hauteurMax;
            Emoji = emoji;
            PeutEtreRecoltee = peutEtreRecoltee;
        }
    }

    // Dictionnaire contenant les cycles de vie pour chaque type de plante
    private static Dictionary<string, List<EtapeCroissance>> cyclesDeVie = new Dictionary<string, List<EtapeCroissance>>();

    // Initialisation des cycles de vie
    static CycleDeVie()
    {
        // Initialiser les cycles de vie pour chaque plante
        InitialiserCycleTomate();
        InitialiserCycleCarotte();
        InitialiserCycleLaitue();
        InitialiserCyclePommeDeTerre();
        InitialiserCycleHaricot();
        InitialiserCycleAnanas();
        InitialiserCycleAil();
        InitialiserCycleAubergine();
        InitialiserCycleBetterave();
        InitialiserCycleRadis();
        
        // Cycle par défaut pour les plantes non spécifiées
        InitialiserCycleParDefaut();
    }
    
    // Méthode pour obtenir le cycle de vie d'une plante
    public static List<EtapeCroissance> GetCycleDeVie(string nomPlante)
    {
        if (cyclesDeVie.ContainsKey(nomPlante))
        {
            return cyclesDeVie[nomPlante];
        }
        return cyclesDeVie["Default"];
    }
    
    // Méthode pour déterminer l'étape actuelle d'une plante
    public static EtapeCroissance GetEtapeActuelle(string nomPlante, int age, int hauteur)
    {
        var cycle = GetCycleDeVie(nomPlante);
        
        // Parcourir les étapes dans l'ordre inverse pour trouver la première correspondant aux critères
        for (int i = cycle.Count - 1; i >= 0; i--)
        {
            if (age >= cycle[i].DureeMinimale && hauteur >= cycle[i].HauteurMin)
            {
                return cycle[i];
            }
        }
        
        // Si aucune étape ne correspond, retourner la première étape
        return cycle[0];
    }
    
    // Initialisation des cycles spécifiques
    private static void InitialiserCycleTomate()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 5, "🌱"),
            new EtapeCroissance("Plantule", 7, 5, 20, "🌿"),
            new EtapeCroissance("Croissance", 14, 20, 50, "🌱"),
            new EtapeCroissance("Floraison", 28, 50, 100, "🌸"),
            new EtapeCroissance("Fruit vert", 42, 100, 130, "🍏"),
            new EtapeCroissance("Mûre", 56, 130, 150, "🍅", true)
        };
        
        cyclesDeVie["Tomate"] = cycle;
    }
    
    private static void InitialiserCycleCarotte()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 3, "🌱"),
            new EtapeCroissance("Plantule", 7, 3, 8, "🌿"),
            new EtapeCroissance("Feuillage", 14, 8, 15, "🌿"),
            new EtapeCroissance("Formation", 28, 15, 25, "🌿"),
            new EtapeCroissance("Mûre", 50, 25, 30, "🥕", true)
        };
        
        cyclesDeVie["Carotte"] = cycle;
    }
    
    private static void InitialiserCycleLaitue()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 2, "🌱"),
            new EtapeCroissance("Plantule", 5, 2, 8, "🌿"),
            new EtapeCroissance("Formation", 14, 8, 15, "🌿"),
            new EtapeCroissance("Développement", 21, 15, 20, "🥬"),
            new EtapeCroissance("Mûre", 30, 20, 25, "🥗", true)
        };
        
        cyclesDeVie["Laitue"] = cycle;
    }
    
    private static void InitialiserCyclePommeDeTerre()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 5, "🌱"),
            new EtapeCroissance("Plantule", 10, 5, 15, "🌿"),
            new EtapeCroissance("Croissance", 20, 15, 30, "🌿"),
            new EtapeCroissance("Floraison", 40, 30, 45, "🌸"),
            new EtapeCroissance("Formation", 60, 45, 55, "🌿"),
            new EtapeCroissance("Mûre", 80, 55, 60, "🥔", true)
        };
        
        cyclesDeVie["Pomme_de_terre"] = cycle;
    }
    
    private static void InitialiserCycleHaricot()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 10, "🌱"),
            new EtapeCroissance("Plantule", 7, 10, 40, "🌿"),
            new EtapeCroissance("Croissance", 14, 40, 100, "🌱"),
            new EtapeCroissance("Floraison", 21, 100, 150, "🌸"),
            new EtapeCroissance("Formation", 35, 150, 180, "🌿"),
            new EtapeCroissance("Mûre", 45, 180, 200, "🫛", true)
        };
        
        cyclesDeVie["Haricots_verts"] = cycle;
    }
    
    private static void InitialiserCycleAnanas()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 10, "🌱"),
            new EtapeCroissance("Plantule", 30, 10, 25, "🌿"),
            new EtapeCroissance("Croissance", 90, 25, 50, "🌱"),
            new EtapeCroissance("Formation", 180, 50, 75, "🌿"),
            new EtapeCroissance("Développement", 270, 75, 90, "🍍"),
            new EtapeCroissance("Mûre", 365, 90, 100, "🍍", true)
        };
        
        cyclesDeVie["Ananas"] = cycle;
    }
    
    private static void InitialiserCycleAil()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 5, "🌱"),
            new EtapeCroissance("Plantule", 14, 5, 15, "🌿"),
            new EtapeCroissance("Croissance", 45, 15, 25, "🌱"),
            new EtapeCroissance("Développement", 90, 25, 35, "🌿"),
            new EtapeCroissance("Mûre", 150, 35, 40, "🧄", true)
        };
        
        cyclesDeVie["Ail"] = cycle;
    }
    
    private static void InitialiserCycleAubergine()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 5, "🌱"),
            new EtapeCroissance("Plantule", 10, 5, 20, "🌿"),
            new EtapeCroissance("Croissance", 25, 20, 40, "🌱"),
            new EtapeCroissance("Floraison", 45, 40, 60, "🌸"),
            new EtapeCroissance("Formation", 60, 60, 80, "🌿"),
            new EtapeCroissance("Mûre", 80, 80, 90, "🍆", true)
        };
        
        cyclesDeVie["Aubergine"] = cycle;
    }
    
    private static void InitialiserCycleBetterave()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 3, "🌱"),
            new EtapeCroissance("Plantule", 7, 3, 10, "🌿"),
            new EtapeCroissance("Croissance", 21, 10, 20, "🌱"),
            new EtapeCroissance("Formation", 40, 20, 30, "🌿"),
            new EtapeCroissance("Mûre", 60, 30, 40, "🫒", true) // Pas d'emoji betterave, utilisation approximative
        };
        
        cyclesDeVie["Betterave"] = cycle;
    }
    
    private static void InitialiserCycleRadis()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 2, "🌱"),
            new EtapeCroissance("Plantule", 3, 2, 5, "🌿"),
            new EtapeCroissance("Croissance", 7, 5, 10, "🌱"),
            new EtapeCroissance("Formation", 14, 10, 15, "🌿"),
            new EtapeCroissance("Mûre", 21, 15, 20, "🫒", true) // Pas d'emoji radis, utilisation approximative
        };
        
        cyclesDeVie["Radis"] = cycle;
    }
    
    private static void InitialiserCycleParDefaut()
    {
        var cycle = new List<EtapeCroissance>
        {
            new EtapeCroissance("Germination", 0, 0, 5, "🌱"),
            new EtapeCroissance("Croissance", 14, 5, 50, "🌿"),
            new EtapeCroissance("Mûre", 28, 50, 100, "🌱", true)
        };
        
        cyclesDeVie["Default"] = cycle;
    }
}