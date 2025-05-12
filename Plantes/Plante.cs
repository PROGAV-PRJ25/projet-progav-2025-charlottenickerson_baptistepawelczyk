public abstract class Plante {
    // Propriétés de base
    public string Nom { get; protected set; }
    protected string Type;
    public bool EstComestible { get; protected set; }
    protected bool MauvaisesHerbes;
    public List<string> Saisons { get; protected set; } // Rendu public avec setter protégé
    public string TerrainPrefere { get; protected set; } // Rendu public avec setter protégé
    protected double Espacement;
    public double VitesseCroissance { get; protected set; } // Rendu public avec setter protégé
    public double BesoinsEnEau { get; protected set; } // Rendu public avec setter protégé
    protected string BesoinsEnLuminosite;
    public List<int> TemperaturesPreferees { get; protected set; } // Rendu public avec setter protégé
    public List<string> MaladiesPossibles { get; protected set; } // Rendu public avec setter protégé
    public List<int> ProbabilitesMaladies { get; protected set; } // Rendu public avec setter protégé
    public int EsperanceDeVie { get; protected set; } // Rendu public avec setter protégé
    public int Rendement { get; protected set; }
    public string EtatPlante { get; set; } // Rendu entièrement public pour le simulateur
    
    // Propriétés pour la simulation et l'affichage
    public int Sante { get; set; } = 100;
    public int Hauteur { get; set; } = 0;
    public int Hydratation { get; set; } = 70;
    public bool AEteDesherbee { get; set; } = false;
    public bool AEteTraitee { get; set; } = false;
    public int Age { get; set; } = 0; // En jours

    public Plante(Terrain terrain)
    {
        Nom = "Inconnue";
        Type = "Annuelle";
        EstComestible = false;
        MauvaisesHerbes = false;
        Saisons = new List<string> {};
        TerrainPrefere = "Inconnu";
        BesoinsEnLuminosite = "Luminosité inconnue";
        TemperaturesPreferees = new List<int> {0, 0};
        MaladiesPossibles = new List<string> {};
        ProbabilitesMaladies = new List<int> {};
        EtatPlante = "Germination";
    }

    // Méthode pour vérifier si la plante est morte
    public bool EstMorte()
    {
        return Sante <= 0 || EtatPlante == "Morte";
    }
    
    // Méthode qui simule la croissance d'une plante (à appeler à chaque tour)
    public virtual void Pousser(Temps conditions)
    {
        // Si la plante est morte, ne rien faire
        if (EstMorte())
            return;
            
        // Augmenter l'âge de la plante
        Age++;
        
        // Réduire l'hydratation en fonction des conditions
        Hydratation = Math.Max(0, Hydratation - (int)(BesoinsEnEau * conditions.Temperature.GetTemperatureActuelle() / 20.0));
        
        // Réduire la santé si hydratation trop basse
        if (Hydratation < 30)
        {
            Sante = Math.Max(0, Sante - 5);
        }
        
        // Augmenter la hauteur en fonction de l'âge si la plante n'est pas mature
        if (EtatPlante != "Mûre" && EtatPlante != "Morte")
        {
            Hauteur += (int)(5 * (1.0 / VitesseCroissance));
        }
        
        // Vérifier si la plante change d'état en fonction de l'âge
        if (Age > 7 && EtatPlante == "Germination")
        {
            EtatPlante = "Croissance";
        }
        else if (Age > 14 && EtatPlante == "Croissance")
        {
            EtatPlante = "Mûre";
        }
        
        // Vérifier si la plante meurt de vieillesse
        if (Age > EsperanceDeVie * 30) // Conversion approximative en jours
        {
            Sante = 0;
            EtatPlante = "Morte";
        }
        
        // Vérifier si la plante est morte à cause d'une santé à 0
        if (Sante <= 0)
        {
            EtatPlante = "Morte";
        }
    }

    public override string ToString()
    {
        string message = $"{Nom.ToUpper()} a pour saison(s) préférée(s) : ";
        foreach (string saison in Saisons)
            message += $"{saison} • ";
        message += $"\nType : {Type}";
        if (MauvaisesHerbes)
            message += " | Propage des mauvaises herbes";
        if (EstComestible)
            message += " | Est comestible";
        message += $"\nTerrain préféré : {TerrainPrefere} | Espacement entre deux plantes : {Espacement} cases";
        message += $"\nVitesse de croissance : {VitesseCroissance} mois | Besoins en eau : {BesoinsEnEau} L/m²/jour";
        message += $"\nA besoin d'une {BesoinsEnLuminosite} | Températures préférées : {TemperaturesPreferees[0]}-{TemperaturesPreferees[1]}°C";
        message += $"\nMaladies possibles : ";
        int taille = MaladiesPossibles.Count;
        for (int i=0; i<taille; i++)
            message += $"{MaladiesPossibles[i]} (probabilité {ProbabilitesMaladies[i]}%) ";
        message += $"\nEspérance de vie : {EsperanceDeVie} an | Rendement : {Rendement} unité(s)";
        message += $"\nÉtat actuel : {EtatPlante} | Santé : {Sante}% | Hydratation : {Hydratation}%";
        return message;
    }
}