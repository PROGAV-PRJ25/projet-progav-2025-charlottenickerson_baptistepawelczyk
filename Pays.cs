using System;
using System.Collections.Generic;

public class Pays
{
    // Propriétés
    public string Nom { get; private set; }
    public string Description { get; private set; }
    public List<string> PlantesDispo { get; private set; }
    public string ClimatGeneral { get; private set; }
    public double ModificateurTemperature { get; private set; }
    public double ModificateurPrecipitations { get; private set; }
    
    // Constructeur
    public Pays(string nom, string description, List<string> plantesDispo, string climatGeneral, 
                double modificateurTemperature, double modificateurPrecipitations)
    {
        Nom = nom;
        Description = description;
        PlantesDispo = plantesDispo;
        ClimatGeneral = climatGeneral;
        ModificateurTemperature = modificateurTemperature;
        ModificateurPrecipitations = modificateurPrecipitations;
    }
    
    // Méthode pour créer une liste de pays disponibles
    public static List<Pays> ObtenirPaysDispo()
    {
        List<Pays> pays = new List<Pays>();
        
        // France
        pays.Add(new Pays(
            "France",
            "Un pays au climat tempéré avec une grande variété de terrains et de cultures.",
            new List<string> { "Tomate", "Carotte", "Pomme de terre", "Laitue", "Haricots verts", "Poireau", "Thym", "Romarin", "Lavande" },
            "Tempéré",
            0, // Modificateur neutre pour la température
            0  // Modificateur neutre pour les précipitations
        ));
        
        // Égypte
        pays.Add(new Pays(
            "Égypte",
            "Un pays au climat désertique, chaud et sec, où l'irrigation est essentielle.",
            new List<string> { "Pastèque", "Concombre", "Tomate", "Menthe", "Coton", "Datte", "Melon" },
            "Désertique",
            5,    // Plus chaud
            -20   // Beaucoup moins de précipitations
        ));
        
        // Japon
        pays.Add(new Pays(
            "Japon",
            "Un archipel au climat varié, favorable à la culture de riz et de plantes ornementales.",
            new List<string> { "Riz", "Soja", "Thé", "Bambou", "Lotus bleu", "Cycas du Japon", "Jasmin" },
            "Insulaire humide",
            0,    // Température moyenne
            15    // Plus de précipitations
        ));
        
        // Pays imaginaire: Verdania
        pays.Add(new Pays(
            "Verdania",
            "Un pays imaginaire à la végétation luxuriante et au climat particulièrement favorable à l'agriculture.",
            new List<string> { "Tomate magique", "Carotte géante", "Plante lumineuse", "Fraise éternelle", "Baies colorées", "Herbes médicinales" },
            "Idéal pour la culture",
            2,    // Légèrement plus chaud
            10    // Plus de précipitations
        ));
        
        // Pays imaginaire: Aridium
        pays.Add(new Pays(
            "Aridium",
            "Un pays imaginaire aride et rocailleux où l'eau est rare et les plantes résistantes.",
            new List<string> { "Cactus épineux", "Succulente dorée", "Herbe des sables", "Fleur du désert", "Tomate désertique", "Palmier résistant" },
            "Aride et sec",
            8,     // Beaucoup plus chaud
            -30    // Extrêmement peu de précipitations
        ));
        
        return pays;
    }
    
    // Méthode pour afficher les informations du pays
    public override string ToString()
    {
        string info = $"Pays: {Nom}\n";
        info += $"Description: {Description}\n";
        info += $"Climat général: {ClimatGeneral}\n";
        info += "Plantes disponibles: ";
        
        foreach (string plante in PlantesDispo)
        {
            info += plante + ", ";
        }
        
        info = info.TrimEnd(',', ' ');
        
        // Afficher les modificateurs de météo
        info += $"\nParticularités climatiques: ";
        
        if (ModificateurTemperature > 0)
        {
            info += $"Température plus élevée (+{ModificateurTemperature}°C), ";
        }
        else if (ModificateurTemperature < 0)
        {
            info += $"Température plus basse ({ModificateurTemperature}°C), ";
        }
        else
        {
            info += "Température moyenne, ";
        }
        
        if (ModificateurPrecipitations > 0)
        {
            info += $"Précipitations plus abondantes (+{ModificateurPrecipitations}%)";
        }
        else if (ModificateurPrecipitations < 0)
        {
            info += $"Précipitations plus rares ({ModificateurPrecipitations}%)";
        }
        else
        {
            info += "Précipitations normales";
        }
        
        return info;
    }
}
