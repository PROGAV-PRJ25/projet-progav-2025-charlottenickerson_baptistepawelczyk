using System;
using System.Collections.Generic;

public class ModeIA
{
    // Propriétés
    private bool estActif;
    private readonly Random random;
    
    // Constructeur
    public ModeIA()
    {
        estActif = true;
        random = new Random();
    }
    
    // Activer ou désactiver le mode IA
    public void Activer(bool actif = true)
    {
        estActif = actif;
        Console.WriteLine(estActif ? "Mode IA activé." : "Mode IA désactivé.");
    }
    
    // Vérifier si le mode IA est actif
    public bool EstActif()
    {
        return estActif;
    }
    
    // Générer des recommandations pour un terrain
    public List<string> GenererRecommandations(Terrain terrain)
    {
        if (!estActif)
        {
            return new List<string>();
        }
        
        List<string> recommandations = new List<string>();
        
        // Vérifier le niveau d'eau
        double niveauEau = terrain.GetNiveauEau();
        if (niveauEau < 30)
        {
            recommandations.Add("Le terrain manque d'eau. Vous devriez arroser.");
        }
        else if (niveauEau > 80)
        {
            recommandations.Add("Le terrain est trop humide. Évitez d'arroser.");
        }
        
        // Vérifier la présence de maladies
        List<string> maladies = terrain.GetMaladiesPresentes();
        if (maladies.Count > 0)
        {
            recommandations.Add($"Attention : {maladies.Count} maladie(s) détectée(s). Un traitement est recommandé.");
        }
        
        // Vérifier la présence de parasites
        List<string> parasites = terrain.GetParasitesPresents();
        if (parasites.Count > 0)
        {
            recommandations.Add($"Attention : {parasites.Count} type(s) de parasite(s) détecté(s). Un traitement est recommandé.");
        }
        
        // Vérifier la température
        double temperature = terrain.GetTemperature();
        if (temperature < 5)
        {
            recommandations.Add("Température basse. Pensez à protéger vos plantes contre le gel.");
        }
        else if (temperature > 30)
        {
            recommandations.Add("Température élevée. Pensez à protéger vos plantes contre la chaleur.");
        }
        
        // Vérifier si le terrain est protégé
        if (!terrain.GetEstProtege())
        {
            recommandations.Add("Ce terrain n'est pas protégé contre les intempéries. Une protection serait utile.");
        }
        
        // Vérifier si le terrain est clos
        if (!terrain.GetEstClos())
        {
            recommandations.Add("Ce terrain n'est pas clos contre les intrus. Une clôture serait utile.");
        }
        
        // Recommandations pour les plantes
        List<Plante> plantes = terrain.GetPlantes();
        foreach (Plante plante in plantes)
        {
            // Vérifier l'état de la plante
            if (plante.GetEtatPlante() == "Morte")
            {
                recommandations.Add($"La plante {plante.Nom} est morte. Vous devriez la retirer.");
            }
            else if (plante.GetEtatPlante() == "Mûre")
            {
                recommandations.Add($"La plante {plante.Nom} est mûre. Vous pouvez la récolter.");
            }
            
            // Vérifier la santé de la plante
            double sante = plante.GetNiveauSante();
            if (sante < 50)
            {
                recommandations.Add($"La plante {plante.Nom} est en mauvaise santé ({sante}%). Vérifiez les conditions environnementales.");
            }
        }
        
        // Si aucune recommandation, ajouter un message positif
        if (recommandations.Count == 0)
        {
            recommandations.Add("Tout semble bien se passer sur ce terrain. Continuez comme ça !");
        }
        
        return recommandations;
    }
    
    // Générer des recommandations pour tous les terrains
    public Dictionary<string, List<string>> GenererRecommandationsPourTousTerrains(List<Terrain> terrains)
    {
        Dictionary<string, List<string>> recommandationsParTerrain = new Dictionary<string, List<string>>();
        
        foreach (Terrain terrain in terrains)
        {
            List<string> recommandations = GenererRecommandations(terrain);
            recommandationsParTerrain.Add(terrain.GetNom(), recommandations);
        }
        
        return recommandationsParTerrain;
    }
    
    // Afficher les recommandations pour tous les terrains
    public void AfficherRecommandations(List<Terrain> terrains)
    {
        if (!estActif)
        {
            return;
        }
        
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║              RECOMMANDATIONS DE L'IA                  ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
        
        Dictionary<string, List<string>> recommandationsParTerrain = GenererRecommandationsPourTousTerrains(terrains);
        
        foreach (var kvp in recommandationsParTerrain)
        {
            Console.WriteLine($"\n=== Terrain: {kvp.Key} ===");
            
            List<string> recommandations = kvp.Value;
            for (int i = 0; i < recommandations.Count; i++)
            {
                Console.WriteLine($"  {i+1}. {recommandations[i]}");
            }
        }
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Méthode pour suggérer l'action la plus urgente
    public string SuggererActionUrgente(List<Terrain> terrains)
    {
        if (!estActif || terrains.Count == 0)
        {
            return "";
        }
        
        List<string> actionsUrgentes = new List<string>();
        
        foreach (Terrain terrain in terrains)
        {
            // Vérifier les maladies
            if (terrain.GetMaladiesPresentes().Count > 0)
            {
                actionsUrgentes.Add($"Traiter les maladies sur le terrain {terrain.GetNom()}");
            }
            
            // Vérifier les parasites
            if (terrain.GetParasitesPresents().Count > 0)
            {
                actionsUrgentes.Add($"Éliminer les parasites sur le terrain {terrain.GetNom()}");
            }
            
            // Vérifier le niveau d'eau critique
            if (terrain.GetNiveauEau() < 20)
            {
                actionsUrgentes.Add($"Arroser de toute urgence le terrain {terrain.GetNom()}");
            }
            
            // Vérifier les plantes mûres
            List<Plante> plantes = terrain.GetPlantes();
            bool plantesMures = false;
            
            foreach (Plante plante in plantes)
            {
                if (plante.GetEtatPlante() == "Mûre")
                {
                    plantesMures = true;
                    break;
                }
            }
            
            if (plantesMures)
            {
                actionsUrgentes.Add($"Récolter les plantes mûres sur le terrain {terrain.GetNom()}");
            }
        }
        
        // S'il y a des actions urgentes, en choisir une au hasard
        if (actionsUrgentes.Count > 0)
        {
            return actionsUrgentes[random.Next(actionsUrgentes.Count)];
        }
        
        return "";
    }
}
