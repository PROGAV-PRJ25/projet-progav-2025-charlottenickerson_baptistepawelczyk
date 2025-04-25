using System;

public class Temperature
{
    // Générateur de nombres aléatoires
    private readonly Random random;
    
    // Températures minimales, maximales et moyennes par saison (en degrés Celsius)
    // Ajustement des limites pour permettre des températures plus extrêmes
    private readonly (int Min, int Max, int MostCommon) tempHiver = (-5, 11, 8);
    private readonly (int Min, int Max, int MostCommon) tempPrintemps = (11, 25, 16);
    private readonly (int Min, int Max, int MostCommon) tempEte = (16, 42, 25);
    private readonly (int Min, int Max, int MostCommon) tempAutomne = (6, 16, 12);
    
    // Température actuelle en degrés Celsius
    private int temperatureActuelle;

    // Constructeur
    public Temperature()
    {
        random = new Random();
        // Initialiser avec une valeur par défaut jusqu'à la mise à jour
        temperatureActuelle = 12;
    }
    
    // Méthode pour générer une température basée sur une distribution normale (approximation de courbe en cloche)
    // pour une saison donnée, avec probabilités ajustées pour les extrêmes
    public void GenererTemperature(Saison.TypeSaison saison)
    {
        // Sélection des paramètres de température en fonction de la saison
        var (min, max, mostCommon) = saison switch
        {
            Saison.TypeSaison.Hiver => tempHiver,
            Saison.TypeSaison.Printemps => tempPrintemps,
            Saison.TypeSaison.Ete => tempEte,
            Saison.TypeSaison.Automne => tempAutomne,
            _ => tempAutomne // Par défaut, utiliser la saison actuelle (automne)
        };
        
        // Augmenter la probabilité de températures extrêmes en hiver et en été
        if (saison == Saison.TypeSaison.Hiver || saison == Saison.TypeSaison.Ete)
        {
            // 1 chance sur 14 (~1 par saison) de forcer une température extrême
            if (random.Next(14) == 0)
            {
                if (saison == Saison.TypeSaison.Hiver)
                {
                    // Force une température sous zéro en hiver
                    temperatureActuelle = random.Next(min, 0);
                    return;
                }
                else // Été
                {
                    // Force une température de canicule en été
                    temperatureActuelle = random.Next(30, max + 1);
                    return;
                }
            }
        }
        
        // Pour les cas non-extrêmes, utilisation de la méthode Box-Muller
        
        // Génération de deux nombres aléatoires uniformes entre 0 et 1
        double u1 = 1.0 - random.NextDouble();  // Éviter le zéro
        double u2 = 1.0 - random.NextDouble();
        
        // Transformation Box-Muller pour générer une variable aléatoire normale standard
        double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        
        // Ajustement de l'écart-type pour que la distribution ait une largeur appropriée
        double stdDev = (max - min) / 6.0;  // Les extrêmes sont à ~3 écarts-types
        
        // Transformation de la variable normale standard à notre échelle de température
        // centrée sur la valeur la plus commune
        double temp = mostCommon + stdDev * z;
        
        // Limiter aux bornes de la saison
        temp = Math.Max(min, Math.Min(max, temp));
        
        // Conversion en entier
        temperatureActuelle = (int)Math.Round(temp);
    }
    
    // Méthode pour obtenir la température actuelle
    public int GetTemperatureActuelle()
    {
        return temperatureActuelle;
    }
    
    // Méthode pour vérifier s'il y a gel
    public bool EstEnGel()
    {
        return temperatureActuelle <= 0;
    }
    
    // Méthode pour vérifier s'il y a canicule
    public bool EstEnCanicule()
    {
        return temperatureActuelle >= 30;
    }
    
    // Méthode pour obtenir la température actuelle sous forme de chaîne avec l'unité
    public string GetTemperatureString()
    {
        string tempString = $"{temperatureActuelle} °C";
        
        // Ajouter des indicateurs textuels pour gel et canicule
        if (EstEnGel())
        {
            tempString += " (Gel!)";
        }
        else if (EstEnCanicule())
        {
            tempString += " (Canicule!)";
        }
        
        return tempString;
    }
}