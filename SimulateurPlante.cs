using System;
using System.Collections.Generic;

/// <summary>
/// Classe responsable de simuler l'évolution des plantes à chaque tour
/// en calculant leur santé, hauteur, hydratation, etc. sans modifier les classes de plantes
/// </summary>
public class SimulateurPlante
{
    // Facteurs d'influence pour les différentes conditions
    private const double FACTEUR_SAISON_FAVORABLE = 1.2;
    private const double FACTEUR_SAISON_DEFAVORABLE = 0.8;
    private const double FACTEUR_TEMPERATURE_OPTIMALE = 1.1;
    private const double FACTEUR_TEMPERATURE_EXTREME = 0.7;
    private const double FACTEUR_HYDRATATION_OPTIMALE = 1.1;
    private const double FACTEUR_HYDRATATION_FAIBLE = 0.8;
    private const double FACTEUR_TERRAIN_FAVORABLE = 1.15;
    private const double FACTEUR_TERRAIN_DEFAVORABLE = 0.9;
    
    // Facteurs météo
    private const double FACTEUR_SECHERESSE = 0.85;      // -15% santé en sécheresse
    private const double FACTEUR_SECHERESSE_EXTREME = 0.7; // -30% santé en sécheresse extrême
    private const double FACTEUR_INONDATION = 0.8;       // -20% santé en inondation
    private const double BONUS_PRECIPITATION_OPTIMALE = 5.0; // +5 points d'hydratation avec précipitations optimales
    
    // Facteurs pour les installations
    private const double BONUS_SERRE_TEMPERATURE = 5.0; // +5°C
    private const double BONUS_SERRE_CROISSANCE = 1.2; // +20% croissance
    private const double BONUS_BARRIERE_PROTECTION = 0.5; // -50% risque maladies
    private const double BONUS_PARESOLEIL_HUMIDITE = 0.8; // -20% perte d'hydratation
    
    // Hauteur maximale par type de plante (en cm)
    private readonly Dictionary<string, int> hauteurMaximale = new Dictionary<string, int>
    {
        { "Tomate", 150 },
        { "Carotte", 30 },
        { "Laitue", 25 },
        { "Pomme_de_terre", 60 },
        { "Haricots_verts", 200 },
        { "Ananas", 100 },
        // Ajouter d'autres plantes selon besoin
    };
    
    // Plages de précipitations optimales par type de plante (en mm)
    private readonly Dictionary<string, (double Min, double Max)> precipitationsOptimales = new Dictionary<string, (double Min, double Max)>
    {
        { "Tomate", (10, 25) },
        { "Carotte", (5, 20) },
        { "Laitue", (15, 25) },
        { "Pomme_de_terre", (10, 20) },
        { "Haricots_verts", (15, 30) },
        { "Ananas", (20, 30) },
        // Ajouter d'autres plantes selon besoin
    };
    
    /// <summary>
    /// Simule l'évolution d'une plante sur une parcelle spécifique pendant un tour
    /// </summary>
    public void SimulerEvolution(Plante plante, Parcelle parcelle, Temps conditions)
    {
        // Si la plante est morte, ne rien faire
        if (plante.EstMorte())
            return;
        
        // Augmenter l'âge de la plante
        plante.Age++;
        
        // Calculer les ajustements en fonction des conditions
        CalculerHydratation(plante, parcelle, conditions);
        CalculerSante(plante, parcelle, conditions);
        CalculerHauteur(plante, parcelle, conditions);
        CalculerEtatPlante(plante);
        
        // Risque de maladies
        VerifierMaladies(plante, parcelle, conditions);
    }
    
    /// <summary>
    /// Calcule l'hydratation de la plante en fonction des conditions météorologiques
    /// </summary>
    private void CalculerHydratation(Plante plante, Parcelle parcelle, Temps conditions)
    {
        // Base de calcul: les besoins en eau et la température
        double perteHydratation = plante.BesoinsEnEau * conditions.Temperature.GetTemperatureActuelle() / 20.0;
        
        // Ajout des précipitations (bonus d'hydratation)
        double precipActuelles = conditions.Precipitations.GetPrecipitationsActuelles();
        
        // Bonus d'hydratation selon les précipitations
        int bonusHydratation = 0;
        
        // Vérifier si les précipitations sont dans la plage optimale pour cette plante
        (double min, double max) = precipitationsOptimales.ContainsKey(plante.Nom) 
            ? precipitationsOptimales[plante.Nom] 
            : (10, 25);  // Valeurs par défaut
        
        if (precipActuelles >= min && precipActuelles <= max)
        {
            // Précipitations optimales
            bonusHydratation = (int)BONUS_PRECIPITATION_OPTIMALE;
        }
        else if (precipActuelles > max)
        {
            // Trop de précipitations - moins efficace
            bonusHydratation = (int)(BONUS_PRECIPITATION_OPTIMALE * 0.7);
        }
        else if (precipActuelles > 0)
        {
            // Précipitations insuffisantes mais présentes
            bonusHydratation = (int)(BONUS_PRECIPITATION_OPTIMALE * (precipActuelles / min));
        }
        
        // Réduction du bonus en cas d'inondation
        if (conditions.Precipitations.EstEnInondation())
        {
            bonusHydratation = Math.Min(2, bonusHydratation); // Maximum +2 en inondation (excès d'eau)
        }
        
        // Modification selon les installations
        if (parcelle.APareSoleil)
        {
            perteHydratation *= BONUS_PARESOLEIL_HUMIDITE;
        }
        
        if (parcelle.ASerre)
        {
            // La serre garde mieux l'humidité en cas de faibles précipitations
            // mais génère plus d'évaporation en cas de forte chaleur
            if (conditions.Temperature.EstEnCanicule())
                perteHydratation *= 1.1; // +10% perte en canicule
            else
            {
                perteHydratation *= 0.9; // -10% perte normale
                
                // La serre protège des précipitations excessives
                if (precipActuelles > max)
                    bonusHydratation = (int)(BONUS_PRECIPITATION_OPTIMALE * 0.8); // Bonus modéré même avec trop de pluie
            }
        }
        
        // Ajustement selon l'humidité du sol
        int humiditeParcelleInfluence = Math.Max(0, parcelle.Humidite - 50) / 10;
        perteHydratation -= humiditeParcelleInfluence; // -1% par 10% au-dessus de 50%
        
        // Appliquer la perte d'hydratation et le bonus des précipitations
        int nouvelleHydratation = plante.Hydratation - (int)perteHydratation + bonusHydratation;
        plante.Hydratation = Math.Max(0, Math.Min(100, nouvelleHydratation)); // Borner entre 0 et 100
        
        // Ajuster l'humidité de la parcelle en fonction des précipitations
        parcelle.Humidite = Math.Min(100, parcelle.Humidite + (int)(precipActuelles / 5.0));
    }
    
    /// <summary>
    /// Calcule la santé de la plante en fonction des conditions
    /// </summary>
    private void CalculerSante(Plante plante, Parcelle parcelle, Temps conditions)
    {
        // Facteur de base pour la santé (légère diminution naturelle)
        double facteurSante = 0.99; // -1% par défaut
        
        // Facteur lié à l'hydratation
        if (plante.Hydratation < 30)
        {
            // Perte de santé significative si sous-hydraté
            facteurSante *= FACTEUR_HYDRATATION_FAIBLE;
            plante.Sante = Math.Max(0, plante.Sante - 5); // Perte directe si déshydraté
        }
        else if (plante.Hydratation > 70)
        {
            // Bonus de santé si bien hydraté
            facteurSante *= FACTEUR_HYDRATATION_OPTIMALE;
        }
        
        // Facteur lié à la température
        double temperature = conditions.Temperature.GetTemperatureActuelle();
        if (temperature < plante.TemperaturesPreferees[0] || temperature > plante.TemperaturesPreferees[1])
        {
            // En dehors de la plage de température optimale
            facteurSante *= FACTEUR_TEMPERATURE_EXTREME;
            
            // Perte supplémentaire si température très extrême
            if (temperature < plante.TemperaturesPreferees[0] - 10 || temperature > plante.TemperaturesPreferees[1] + 10)
            {
                plante.Sante = Math.Max(0, plante.Sante - 10);
            }
            
            // Effets gel et canicule
            if (conditions.Temperature.EstEnGel() && !parcelle.ASerre)
            {
                plante.Sante = Math.Max(0, plante.Sante - 15); // Dégâts de gel sans serre
            }
            else if (conditions.Temperature.EstEnCanicule() && !parcelle.APareSoleil)
            {
                plante.Sante = Math.Max(0, plante.Sante - 10); // Dégâts de canicule sans pare-soleil
            }
        }
        else if (temperature >= plante.TemperaturesPreferees[0] + 2 && temperature <= plante.TemperaturesPreferees[1] - 2)
        {
            // Température idéale (au coeur de la plage)
            facteurSante *= FACTEUR_TEMPERATURE_OPTIMALE;
        }
        
        // Facteur lié aux précipitations et conditions météorologiques extrêmes
        Saison.TypeSaison saisonActuelle = conditions.GetSaisonActuelle();
        
        if (conditions.Precipitations.EstEnSecheresseExtreme(saisonActuelle, conditions.Temperature))
        {
            facteurSante *= FACTEUR_SECHERESSE_EXTREME;
            plante.Sante = Math.Max(0, plante.Sante - 12); // Perte directe en sécheresse extrême
        }
        else if (conditions.Precipitations.EstEnSecheresse(saisonActuelle))
        {
            facteurSante *= FACTEUR_SECHERESSE;
            plante.Sante = Math.Max(0, plante.Sante - 7); // Perte directe en sécheresse
        }
        else if (conditions.Precipitations.EstEnInondation())
        {
            facteurSante *= FACTEUR_INONDATION;
            plante.Sante = Math.Max(0, plante.Sante - 5); // Perte directe en inondation
        }
        
        // Facteur lié à la saison
        string saisonActuelleStr = saisonActuelle.ToString();
        if (plante.Saisons.Contains(saisonActuelleStr))
        {
            // Saison favorable
            facteurSante *= FACTEUR_SAISON_FAVORABLE;
        }
        else
        {
            // Saison défavorable
            facteurSante *= FACTEUR_SAISON_DEFAVORABLE;
            
            // Perte supplémentaire en saison très défavorable (hiver pour beaucoup de plantes)
            if (saisonActuelleStr == "Hiver" && !plante.Saisons.Contains("Hiver") && !parcelle.ASerre)
            {
                plante.Sante = Math.Max(0, plante.Sante - 8); // Dégâts hivernaux sans serre
            }
        }
        
        // Facteur lié au terrain
        if (parcelle.Type == plante.TerrainPrefere)
        {
            // Terrain favorable
            facteurSante *= FACTEUR_TERRAIN_FAVORABLE;
        }
        else
        {
            // Terrain défavorable
            facteurSante *= FACTEUR_TERRAIN_DEFAVORABLE;
        }
        
        // Ajustements pour les installations
        if (parcelle.ASerre)
        {
            // La serre protège des conditions climatiques extrêmes
            facteurSante = Math.Max(facteurSante, 0.95); // Minimum -5% par tour avec serre
        }
        
        if (parcelle.APareSoleil && conditions.Temperature.EstEnCanicule())
        {
            // Le pare-soleil protège des canicules
            facteurSante *= 1.05; // +5% en canicule si pare-soleil
        }
        
        // Bonus si la plante a été désherbée/traitée ce tour
        if (plante.AEteDesherbee)
        {
            facteurSante *= 1.05; // +5% 
            plante.AEteDesherbee = false; // Réinitialiser pour le prochain tour
        }
        
        if (plante.AEteTraitee)
        {
            facteurSante *= 1.1; // +10%
            plante.AEteTraitee = false; // Réinitialiser pour le prochain tour
        }
        
        // Appliquer le facteur de santé
        plante.Sante = (int)(plante.Sante * facteurSante);
        plante.Sante = Math.Max(0, Math.Min(100, plante.Sante)); // Borner entre 0 et 100
    }
    
    /// <summary>
    /// Calcule la hauteur de la plante en fonction de son âge et des conditions
    /// </summary>
    private void CalculerHauteur(Plante plante, Parcelle parcelle, Temps conditions)
    {
        // Ne pas augmenter la hauteur si la plante est à maturité ou morte
        if (plante.EtatPlante == "Mûre" || plante.EtatPlante == "Morte")
            return;
        
        // Hauteur maximum pour cette plante (par défaut 100cm si non spécifiée)
        int maxHauteur = 100;
        if (hauteurMaximale.ContainsKey(plante.Nom))
        {
            maxHauteur = hauteurMaximale[plante.Nom];
        }
        
        // Coefficient de croissance de base selon la vitesse de croissance
        double coeffCroissance = 5.0 * (1.0 / plante.VitesseCroissance);
        
        // Facteurs influençant la croissance
        double facteurCroissance = 1.0;
        
        // Facteur hydratation
        if (plante.Hydratation > 70)
            facteurCroissance *= 1.1; // +10% si bien hydraté
        else if (plante.Hydratation < 30)
            facteurCroissance *= 0.7; // -30% si sous-hydraté
        
        // Facteur saison
        string saisonActuelle = conditions.GetSaisonActuelle().ToString();
        if (plante.Saisons.Contains(saisonActuelle))
            facteurCroissance *= 1.2; // +20% en saison favorable
        else if (saisonActuelle == "Hiver" && !plante.Saisons.Contains("Hiver"))
            facteurCroissance *= 0.3; // -70% en hiver pour les plantes qui n'aiment pas l'hiver
        
        // Facteur température
        double temperature = conditions.Temperature.GetTemperatureActuelle();
        if (temperature >= plante.TemperaturesPreferees[0] && temperature <= plante.TemperaturesPreferees[1])
            facteurCroissance *= 1.1; // +10% en température optimale
        else if (temperature < plante.TemperaturesPreferees[0] - 5 || temperature > plante.TemperaturesPreferees[1] + 5)
            facteurCroissance *= 0.6; // -40% si température très inadaptée
        
        // Facteur précipitations
        double precipActuelles = conditions.Precipitations.GetPrecipitationsActuelles();
        (double minPrec, double maxPrec) = precipitationsOptimales.ContainsKey(plante.Nom) 
            ? precipitationsOptimales[plante.Nom] 
            : (10, 25);
            
        if (precipActuelles >= minPrec && precipActuelles <= maxPrec)
            facteurCroissance *= 1.1; // +10% avec précipitations optimales
        else if (conditions.Precipitations.EstEnSecheresse(conditions.GetSaisonActuelle()) || conditions.Precipitations.EstEnInondation())
            facteurCroissance *= 0.7; // -30% en conditions extrêmes
        
        // Facteur santé
        facteurCroissance *= (plante.Sante / 100.0); // Réduit proportionnellement à la santé
        
        // Effet de la serre
        if (parcelle.ASerre)
            facteurCroissance *= BONUS_SERRE_CROISSANCE; // +20% avec serre
        
        // Calculer la croissance pour ce tour
        int croissance = (int)(coeffCroissance * facteurCroissance);
        
        // Appliquer la croissance avec un maximum
        plante.Hauteur = Math.Min(maxHauteur, plante.Hauteur + croissance);
    }
    
    /// <summary>
    /// Détermine l'état de la plante en fonction de son âge, hauteur et cycle de vie spécifique
    /// </summary>
    private void CalculerEtatPlante(Plante plante)
    {
        // Si déjà morte, ne rien changer
        if (plante.EstMorte())
            return;
        
        // Obtenir l'étape actuelle du cycle de vie en fonction de l'âge et de la hauteur
        var etapeActuelle = CycleDeVie.GetEtapeActuelle(plante.Nom, plante.Age, plante.Hauteur);
        
        // Mettre à jour l'état de la plante
        plante.EtatPlante = etapeActuelle.Nom;
        
        // Vérifier si la récolte est possible
        if (etapeActuelle.PeutEtreRecoltee)
        {
            // Rendre la plante récoltable si elle est comestible
            if (plante.EstComestible)
            {
                plante.EtatPlante = "Mûre";
            }
        }
        
        // Mort naturelle si l'espérance de vie est dépassée
        if (plante.Age > plante.EsperanceDeVie * 30) // Conversion en jours
        {
            plante.Sante = 0;
            plante.EtatPlante = "Morte";
        }
        
        // Mort si la santé est à 0
        if (plante.Sante <= 0)
        {
            plante.EtatPlante = "Morte";
        }
    }
    
    /// <summary>
    /// Vérifie si la plante contracte des maladies en fonction de probabilités et conditions
    /// </summary>
    private void VerifierMaladies(Plante plante, Parcelle parcelle, Temps conditions)
    {
        // Pas de maladies si déjà morte
        if (plante.EstMorte())
            return;
            
        // Si la plante a été traitée, pas de risque de maladie ce tour
        if (plante.AEteTraitee)
            return;
            
        // Liste des maladies possibles pour cette plante
        List<string> maladiesPossibles = plante.MaladiesPossibles;
        List<int> probabilitesMaladies = plante.ProbabilitesMaladies;
        
        // Si pas de maladies définies, sortir
        if (maladiesPossibles == null || maladiesPossibles.Count == 0)
            return;
            
        // Facteur de risque de base
        double facteurRisque = 1.0;
        
        // Facteurs augmentant le risque
        if (plante.Hydratation > 90)
            facteurRisque *= 1.2; // Risque accru si trop humide (pourriture)
        
        if (plante.Hydratation < 20)
            facteurRisque *= 1.1; // Risque accru si trop sec (stress)
            
        if (plante.Sante < 50)
            facteurRisque *= 1.5; // Plantes faibles plus susceptibles
        
        // Réduction du risque avec barrière
        if (parcelle.ABarriere)
            facteurRisque *= BONUS_BARRIERE_PROTECTION; // -50% risque
            
        // Pour chaque maladie possible
        Random random = new Random();
        for (int i = 0; i < maladiesPossibles.Count; i++)
        {
            // Calculer la probabilité ajustée
            double probaAjustee = probabilitesMaladies[i] * facteurRisque;
            
            // Tirer un nombre aléatoire
            if (random.Next(100) < probaAjustee)
            {
                // La plante contracte la maladie
                plante.Sante -= random.Next(10, 30); // -10 à -30% de santé
                
                // Ne pas descendre sous 0
                plante.Sante = Math.Max(0, plante.Sante);
                
                // Sortir après la première maladie contractée
                break;
            }
        }
    }
    
    /// <summary>
    /// Méthode principale appelée par la classe Affichage pour simuler l'évolution de toutes les plantes
    /// </summary>
    public void SimulerEvolutionPlantes(Parcelle[,] parcelles, Temps conditions)
    {
        int rows = parcelles.GetLength(0);
        int cols = parcelles.GetLength(1);
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (parcelles[i, j].AUnePlante())
                {
                    SimulerEvolution(parcelles[i, j].PlanteParcelle, parcelles[i, j], conditions);
                }
            }
        }
    }
}