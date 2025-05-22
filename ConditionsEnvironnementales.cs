using System;
using System.Collections.Generic;

public class ConditionsEnvironnementales
{
    // Propriétés environnementales
    public double NiveauEau { get; private set; }
    public double Temperature { get; private set; }
    public string Luminosite { get; private set; }
    public string TypeTerrain { get; private set; }
    public List<string> MaladiesActives { get; private set; }
    public List<string> ParasitesActifs { get; private set; }
    
    // Constructeur
    public ConditionsEnvironnementales(double niveauEau, double temperature, string luminosite, string typeTerrain)
    {
        NiveauEau = niveauEau;
        Temperature = temperature;
        Luminosite = luminosite;
        TypeTerrain = typeTerrain;
        MaladiesActives = new List<string>();
        ParasitesActifs = new List<string>();
    }
    
    // Méthode pour mettre à jour les conditions
    public void MettreAJour(double niveauEau, double temperature, string luminosite)
    {
        NiveauEau = niveauEau;
        Temperature = temperature;
        Luminosite = luminosite;
    }
    
    // Méthode pour ajouter une maladie
    public void AjouterMaladie(string maladie)
    {
        if (!MaladiesActives.Contains(maladie))
        {
            MaladiesActives.Add(maladie);
        }
    }
    
    // Méthode pour ajouter un parasite
    public void AjouterParasite(string parasite)
    {
        if (!ParasitesActifs.Contains(parasite))
        {
            ParasitesActifs.Add(parasite);
        }
    }
    
    // Méthode pour supprimer une maladie
    public void SupprimerMaladie(string maladie)
    {
        MaladiesActives.Remove(maladie);
    }
    
    // Méthode pour supprimer un parasite
    public void SupprimerParasite(string parasite)
    {
        ParasitesActifs.Remove(parasite);
    }
    
    // Méthode pour évaluer la compatibilité avec les besoins d'une plante
    public double EvaluerCompatibilite(double besoinsEau, List<int> temperaturesPref, string luminositePref, string terrainPref)
    {
        // Évaluation de la compatibilité pour l'eau (0-1)
        double compatibiliteEau = Math.Max(0, 1 - Math.Abs(NiveauEau - besoinsEau) / besoinsEau);
        
        // Évaluation de la compatibilité pour la température (0-1)
        double compatibiliteTemp = 0;
        if (Temperature >= temperaturesPref[0] && Temperature <= temperaturesPref[1])
        {
            compatibiliteTemp = 1; // Température idéale
        }
        else
        {
            // Distance par rapport à la plage idéale
            double distMin = Math.Abs(Temperature - temperaturesPref[0]);
            double distMax = Math.Abs(Temperature - temperaturesPref[1]);
            double dist = Math.Min(distMin, distMax);
            
            // Plus la distance est grande, moins la compatibilité est bonne
            compatibiliteTemp = Math.Max(0, 1 - dist / 10);
        }
        
        // Évaluation de la compatibilité pour la luminosité (0-1)
        double compatibiliteLum = luminositePref == Luminosite ? 1 : 0.5;
        
        // Évaluation de la compatibilité pour le terrain (0-1)
        double compatibiliteTerrain = terrainPref == TypeTerrain ? 1 : 0.5;
        
        // Moyenne des compatibilités
        return (compatibiliteEau + compatibiliteTemp + compatibiliteLum + compatibiliteTerrain) / 4;
    }
}
