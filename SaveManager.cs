using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class SaveManager
{
    private const string SAVE_DIRECTORY = "saves";
    
    // Structure pour les données de sauvegarde
    public class GameSaveData
    {
        public string PaysNom { get; set; } = "";
        public int CompteurSemaines { get; set; }
        public int Argent { get; set; }
        public List<TerrainSaveData> Terrains { get; set; } = new List<TerrainSaveData>();
        public BilanEcologiqueSaveData BilanEcologique { get; set; } = new BilanEcologiqueSaveData();
        public DateTime DateSauvegarde { get; set; }
    }
    
    public class TerrainSaveData
    {
        public string Nom { get; set; } = "";
        public string Type { get; set; } = "";
        public double NiveauEau { get; set; }
        public double Temperature { get; set; }
        public string Luminosite { get; set; } = "";
        public bool EstProtege { get; set; }
        public bool EstClos { get; set; }
        public List<string> MaladiesPresentes { get; set; } = new List<string>();
        public List<string> ParasitesPresents { get; set; } = new List<string>();
        public List<PlanteSaveData> Plantes { get; set; } = new List<PlanteSaveData>();
    }
    
    public class PlanteSaveData
    {
        public string Nom { get; set; } = "";
        public string Type { get; set; } = "";
        public double TauxCroissance { get; set; }
        public double NiveauSante { get; set; }
        public int AgeSemaines { get; set; }
        public string EtatPlante { get; set; } = "";
        public List<string> MaladiesActuelles { get; set; } = new List<string>();
        public List<string> ParasitesActuels { get; set; } = new List<string>();
        public int NombreProduitsDisponibles { get; set; }
    }
    
    public class BilanEcologiqueSaveData
    {
        public int BilanCarbone { get; set; }
        public int ImpactEnvironnemental { get; set; }
        public List<string> MethodesEcologiquesUtilisees { get; set; } = new List<string>();
    }
    
    // Constructeur
    public SaveManager()
    {
        // Créer le répertoire de sauvegarde s'il n'existe pas
        if (!Directory.Exists(SAVE_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DIRECTORY);
        }
    }
    
    // Sauvegarder le jeu
    public void SauvegarderJeu(string nomSauvegarde, List<Terrain> terrains, Pays pays, int compteurSemaines, int argent, ModeEcologique modeEcologique)
    {
        try
        {
            // Créer l'objet de sauvegarde
            GameSaveData saveData = new GameSaveData
            {
                PaysNom = pays.Nom,
                CompteurSemaines = compteurSemaines,
                Argent = argent,
                Terrains = new List<TerrainSaveData>(),
                DateSauvegarde = DateTime.Now
            };
            
            // Sauvegarder les terrains
            foreach (Terrain terrain in terrains)
            {
                TerrainSaveData terrainData = new TerrainSaveData
                {
                    Nom = terrain.GetNom(),
                    Type = TypeTerrain.GetNomTypeTerrain(terrain),
                    NiveauEau = terrain.GetNiveauEau(),
                    Temperature = terrain.GetTemperature(),
                    Luminosite = terrain.GetLuminosite(),
                    EstProtege = terrain.GetEstProtege(),
                    EstClos = terrain.GetEstClos(),
                    MaladiesPresentes = terrain.GetMaladiesPresentes(),
                    ParasitesPresents = terrain.GetParasitesPresents(),
                    Plantes = new List<PlanteSaveData>()
                };
                
                // Sauvegarder les plantes de ce terrain
                foreach (Plante plante in terrain.GetPlantes())
                {
                    PlanteSaveData planteData = new PlanteSaveData
                    {
                        Nom = plante.Nom,
                        Type = plante.GetType().Name,
                        TauxCroissance = plante.GetTauxCroissance(),
                        NiveauSante = plante.GetNiveauSante(),
                        AgeSemaines = plante.GetAgeSemaines(),
                        EtatPlante = plante.GetEtatPlante(),
                        MaladiesActuelles = plante.GetMaladiesActuelles(),
                        ParasitesActuels = plante.GetParasitesActuels(),
                        NombreProduitsDisponibles = plante.GetNombreProduitsDisponibles()
                    };
                    
                    terrainData.Plantes.Add(planteData);
                }
                
                saveData.Terrains.Add(terrainData);
            }
            
            // Sauvegarder le bilan écologique si le mode est actif
            if (modeEcologique != null && modeEcologique.EstActif())
            {
                saveData.BilanEcologique = new BilanEcologiqueSaveData
                {
                    BilanCarbone = modeEcologique.GetBilanCarbone(),
                    ImpactEnvironnemental = modeEcologique.GetImpactEnvironnemental()
                    // Note: Les méthodes écologiques utilisées nécessiteraient d'être exposées par un getter dans ModeEcologique
                };
            }
            
            // Sérialiser les données
            string jsonString = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
            
            // Écrire dans un fichier
            string filePath = Path.Combine(SAVE_DIRECTORY, $"{nomSauvegarde}.json");
            File.WriteAllText(filePath, jsonString);
            
            Console.WriteLine($"Jeu sauvegardé avec succès sous le nom '{nomSauvegarde}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la sauvegarde: {ex.Message}");
        }
    }
    
    // Charger une sauvegarde
    public GameSaveData? ChargerSauvegarde(string nomSauvegarde)
    {
        try
        {
            string filePath = Path.Combine(SAVE_DIRECTORY, $"{nomSauvegarde}.json");
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Aucune sauvegarde trouvée avec le nom '{nomSauvegarde}'.");
                return null;
            }
            
            string jsonString = File.ReadAllText(filePath);
            GameSaveData? saveData = JsonSerializer.Deserialize<GameSaveData>(jsonString);
            
            if (saveData == null)
            {
                Console.WriteLine("Erreur lors du chargement de la sauvegarde: format invalide.");
                return null;
            }
            
            Console.WriteLine($"Sauvegarde '{nomSauvegarde}' chargée avec succès.");
            return saveData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du chargement de la sauvegarde: {ex.Message}");
            return null;
        }
    }
    
    // Lister toutes les sauvegardes disponibles
    public List<string> ListerSauvegardes()
    {
        List<string> sauvegardes = new List<string>();
        
        try
        {
            if (Directory.Exists(SAVE_DIRECTORY))
            {
                string[] files = Directory.GetFiles(SAVE_DIRECTORY, "*.json");
                
                foreach (string file in files)
                {
                    sauvegardes.Add(Path.GetFileNameWithoutExtension(file));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la liste des sauvegardes: {ex.Message}");
        }
        
        return sauvegardes;
    }
    
    // Supprimer une sauvegarde
    public bool SupprimerSauvegarde(string nomSauvegarde)
    {
        try
        {
            string filePath = Path.Combine(SAVE_DIRECTORY, $"{nomSauvegarde}.json");
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Aucune sauvegarde trouvée avec le nom '{nomSauvegarde}'.");
                return false;
            }
            
            File.Delete(filePath);
            Console.WriteLine($"Sauvegarde '{nomSauvegarde}' supprimée avec succès.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la suppression de la sauvegarde: {ex.Message}");
            return false;
        }
    }
    
    // Afficher le menu de sauvegarde/chargement
    public void AfficherMenuSauvegarde(List<Terrain> terrains, Pays pays, int compteurSemaines, int argent, ModeEcologique modeEcologique)
    {
        bool continuer = true;
        
        while (continuer)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║                SAUVEGARDE ET CHARGEMENT               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            
            Console.WriteLine("\nActions disponibles:");
            Console.WriteLine("1. Sauvegarder la partie");
            Console.WriteLine("2. Charger une partie");
            Console.WriteLine("3. Supprimer une sauvegarde");
            Console.WriteLine("4. Retour au jeu");
            
            Console.Write("\nChoisissez une action (1-4): ");
            string? choix = Console.ReadLine() ?? "4";
            
            switch (choix)
            {
                case "1":
                    MenuSauvegarderPartie(terrains, pays, compteurSemaines, argent, modeEcologique);
                    break;
                case "2":
                    MenuChargerPartie();
                    break;
                case "3":
                    MenuSupprimerSauvegarde();
                    break;
                case "4":
                    continuer = false;
                    break;
                default:
                    Console.WriteLine("Choix non reconnu. Appuyez sur une touche pour continuer...");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
    
    // Menu pour sauvegarder une partie
    private void MenuSauvegarderPartie(List<Terrain> terrains, Pays pays, int compteurSemaines, int argent, ModeEcologique modeEcologique)
    {
        Console.Clear();
        Console.WriteLine("=== SAUVEGARDER LA PARTIE ===");
        
        // Afficher les sauvegardes existantes
        List<string> sauvegardes = ListerSauvegardes();
        if (sauvegardes.Count > 0)
        {
            Console.WriteLine("\nSauvegardes existantes:");
            foreach (string sauvegarde in sauvegardes)
            {
                Console.WriteLine($"- {sauvegarde}");
            }
        }
        
        Console.Write("\nEntrez un nom pour la sauvegarde: ");
        string? nomSauvegarde = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(nomSauvegarde))
        {
            Console.WriteLine("Nom de sauvegarde invalide.");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        // Vérifier si une sauvegarde existe déjà avec ce nom
        if (sauvegardes.Contains(nomSauvegarde))
        {
            Console.Write($"Une sauvegarde nommée '{nomSauvegarde}' existe déjà. Voulez-vous l'écraser? (O/N): ");
            string? confirmation = Console.ReadLine()?.ToUpper();
            
            if (confirmation != "O")
            {
                Console.WriteLine("Sauvegarde annulée.");
                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey(true);
                return;
            }
        }
        
        // Sauvegarder la partie
        SauvegarderJeu(nomSauvegarde, terrains, pays, compteurSemaines, argent, modeEcologique);
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Menu pour charger une partie
    private void MenuChargerPartie()
    {
        Console.Clear();
        Console.WriteLine("=== CHARGER UNE PARTIE ===");
        
        // Afficher les sauvegardes disponibles
        List<string> sauvegardes = ListerSauvegardes();
        
        if (sauvegardes.Count == 0)
        {
            Console.WriteLine("\nAucune sauvegarde disponible.");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        Console.WriteLine("\nSauvegardes disponibles:");
        for (int i = 0; i < sauvegardes.Count; i++)
        {
            Console.WriteLine($"{i+1}. {sauvegardes[i]}");
        }
        
        Console.Write("\nChoisissez une sauvegarde à charger (0 pour annuler): ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index == 0 || index > sauvegardes.Count)
        {
            return;
        }
        
        string nomSauvegarde = sauvegardes[index - 1];
        GameSaveData? saveData = ChargerSauvegarde(nomSauvegarde);
        
        if (saveData != null)
        {
            // Afficher les informations de la sauvegarde
            Console.WriteLine("\nInformations sur la sauvegarde:");
            Console.WriteLine($"Pays: {saveData.PaysNom}");
            Console.WriteLine($"Semaine: {saveData.CompteurSemaines}");
            Console.WriteLine($"Argent: {saveData.Argent} pièces");
            Console.WriteLine($"Terrains: {saveData.Terrains.Count}");
            Console.WriteLine($"Date de sauvegarde: {saveData.DateSauvegarde}");
            
            Console.Write("\nVoulez-vous charger cette partie? (O/N): ");
            string? confirmation = Console.ReadLine()?.ToUpper();
            
            if (confirmation == "O")
            {
                // Logique pour charger effectivement la partie
                // (Cela sera fait dans la classe Program car il faut recréer tous les objets)
                Console.WriteLine("Chargement de la partie...");
            }
        }
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Menu pour supprimer une sauvegarde
    private void MenuSupprimerSauvegarde()
    {
        Console.Clear();
        Console.WriteLine("=== SUPPRIMER UNE SAUVEGARDE ===");
        
        // Afficher les sauvegardes disponibles
        List<string> sauvegardes = ListerSauvegardes();
        
        if (sauvegardes.Count == 0)
        {
            Console.WriteLine("\nAucune sauvegarde disponible.");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        Console.WriteLine("\nSauvegardes disponibles:");
        for (int i = 0; i < sauvegardes.Count; i++)
        {
            Console.WriteLine($"{i+1}. {sauvegardes[i]}");
        }
        
        Console.Write("\nChoisissez une sauvegarde à supprimer (0 pour annuler): ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index == 0 || index > sauvegardes.Count)
        {
            return;
        }
        
        string nomSauvegarde = sauvegardes[index - 1];
        
        Console.Write($"\nÊtes-vous sûr de vouloir supprimer la sauvegarde '{nomSauvegarde}'? (O/N): ");
        string? confirmation = Console.ReadLine()?.ToUpper();
        
        if (confirmation == "O")
        {
            SupprimerSauvegarde(nomSauvegarde);
        }
        else
        {
            Console.WriteLine("Suppression annulée.");
        }
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
}
