using System;

public class Precipitations
{
    // Générateur de nombres aléatoires
    private readonly Random random;
    
    // Précipitations minimales, maximales et moyennes par saison (en mm)
    private readonly (double Min, double Max, double MostCommon) precipAutomne = (0, 40, 22.5);
    private readonly (double Min, double Max, double MostCommon) precipHiver = (0, 25, 12.5);
    private readonly (double Min, double Max, double MostCommon) precipPrintemps = (0, 40, 19);
    private readonly (double Min, double Max, double MostCommon) precipEte = (0, 25, 1.5);
    
    // Précipitations actuelles en mm
    private double precipitationsActuelles;
    
    // Seuils pour les événements météorologiques
    public const double SEUIL_SECHERESSE = 5.0;
    public const double SEUIL_INONDATION = 35.0;

    // Constructeur
    public Precipitations()
    {
        random = new Random();
        // Initialiser avec une valeur par défaut jusqu'à la mise à jour
        precipitationsActuelles = 10.0;
    }
    
    // Méthode pour générer des précipitations basées sur une distribution normale (approximation de courbe en cloche)
    // pour une saison donnée
    public void GenererPrecipitations(Saison.TypeSaison saison)
    {
        // Sélection des paramètres de précipitations en fonction de la saison
        var (min, max, mostCommon) = saison switch
        {
            Saison.TypeSaison.Hiver => precipHiver,
            Saison.TypeSaison.Printemps => precipPrintemps,
            Saison.TypeSaison.Ete => precipEte,
            Saison.TypeSaison.Automne => precipAutomne,
            _ => precipAutomne // Par défaut, utiliser l'automne
        };
        
        // Utilisation de la méthode Box-Muller pour générer un nombre selon une distribution normale
        
        // Génération de deux nombres aléatoires uniformes entre 0 et 1
        double u1 = 1.0 - random.NextDouble();  // Éviter le zéro
        double u2 = 1.0 - random.NextDouble();
        
        // Transformation Box-Muller pour générer une variable aléatoire normale standard
        double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        
        // Ajustement de l'écart-type pour que la distribution ait une largeur appropriée
        double stdDev = (max - min) / 6.0;  // Les extrêmes sont à ~3 écarts-types
        
        // Transformation de la variable normale standard à notre échelle de précipitations
        // centrée sur la valeur la plus commune
        double precip = mostCommon + stdDev * z;
        
        // Limiter aux bornes de la saison et ne jamais descendre sous zéro
        precip = Math.Max(0, Math.Min(max, precip));
        
        // Arrondir à une décimale pour plus de clarté
        precipitationsActuelles = Math.Round(precip, 1);
    }
    
    // Méthode pour obtenir les précipitations actuelles
    public double GetPrecipitationsActuelles()
    {
        return precipitationsActuelles;
    }
    
    // Méthode pour vérifier s'il y a une sécheresse
    public bool EstEnSecheresse(Saison.TypeSaison saison)
    {
        // La sécheresse est définie uniquement pour l'été
        return saison == Saison.TypeSaison.Ete && precipitationsActuelles <= SEUIL_SECHERESSE;
    }
    
    // Méthode pour vérifier s'il y a une sécheresse extrême (canicule + sécheresse)
    public bool EstEnSecheresseExtreme(Saison.TypeSaison saison, Temperature temperature)
    {
        // La sécheresse extrême est la combinaison d'une sécheresse et d'une canicule
        return EstEnSecheresse(saison) && temperature.EstEnCanicule();
    }
    
    // Méthode pour vérifier s'il y a une inondation
    public bool EstEnInondation()
    {
        return precipitationsActuelles >= SEUIL_INONDATION;
    }
    
    // Méthode pour obtenir les précipitations actuelles sous forme de chaîne avec l'unité
    public string GetPrecipitationsString(Saison.TypeSaison saison, Temperature temperature)
    {
        string precipString = $"{precipitationsActuelles} mm";
        
        // Ajouter des indicateurs textuels selon les conditions
        if (EstEnSecheresseExtreme(saison, temperature))
        {
            precipString += " (Sécheresse extrême!)";
        }
        else if (EstEnSecheresse(saison))
        {
            precipString += " (Sécheresse!)";
        }
        else if (EstEnInondation())
        {
            precipString += " (Inondation!)";
        }
        
        return precipString;
    }
}