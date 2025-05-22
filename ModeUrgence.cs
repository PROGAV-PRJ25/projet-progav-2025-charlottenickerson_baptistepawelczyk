using System;
using System.Collections.Generic;
using System.Threading;

public class ModeUrgence
{
    // Propriétés
    private Obstacle? obstacle;
    private List<Terrain> terrains;
    private bool estActif;
    private DateTime heureDebut;
    private int dureeSecondes;
    private Webcam webcam;
    
    // Constructeur
    public ModeUrgence(List<Terrain> terrains)
    {
        this.terrains = terrains;
        estActif = false;
        dureeSecondes = 30; // Durée par défaut d'une urgence: 30 secondes
        webcam = new Webcam();
    }
    
    // Méthode pour déclencher une urgence
    public void DeclencherUrgence()
    {
        // Si une urgence est déjà active, ne pas en déclencher une nouvelle
        if (estActif)
        {
            return;
        }
        
        Random random = new Random();
        
        // Choisir aléatoirement entre intrus ou intempérie
        string type = random.Next(2) == 0 ? "Intrus" : "Intemperie";
        
        // Créer l'obstacle selon le type
        if (type == "Intrus")
        {
            string[] intrus = { "Rongeur", "Oiseau", "Lapin" };
            string[] descriptions = { 
                "Un petit rongeur affamé qui cherche à manger vos plantes.",
                "Un oiseau curieux qui picote vos semis.",
                "Un lapin gourmand qui aime vos légumes."
            };
            
            int index = random.Next(intrus.Length);
            obstacle = new Obstacle(
                "Intrus", 
                intrus[index], 
                random.Next(10, 30), // Dégâts entre 10 et 30
                descriptions[index]
            );
        }
        else
        {
            string[] intemperies = { "Grêle", "Orage", "Tempête" };
            string[] descriptions = { 
                "Des grêlons qui peuvent endommager vos plantes fragiles.",
                "Un orage violent avec de fortes pluies qui risquent d'inonder vos cultures.",
                "Une tempête avec des vents violents qui peuvent arracher vos plantes."
            };
            
            int index = random.Next(intemperies.Length);
            obstacle = new Obstacle(
                "Intemperie", 
                intemperies[index], 
                random.Next(20, 50), // Dégâts entre 20 et 50
                descriptions[index]
            );
        }
        
        // Activer le mode urgence
        estActif = true;
        heureDebut = DateTime.Now;
        
        // Afficher l'alerte
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("ALERTE! ALERTE! ALERTE!");
        if (obstacle != null)
        {
            Console.WriteLine($"Une urgence a été détectée: {obstacle.Type} - {obstacle.Nom}");
            Console.WriteLine(obstacle.Description);
        }
        Console.ResetColor();
        Console.WriteLine("\nAppuyez sur une touche pour réagir à cette urgence...");
        Console.ReadKey(true);
    }
    
    // Méthode pour gérer le mode urgence
    public void GererUrgence()
    {
        if (!estActif || obstacle == null)
        {
            return;
        }
        
        // Vérifier si le temps est écoulé
        TimeSpan tempsPasse = DateTime.Now - heureDebut;
        if (tempsPasse.TotalSeconds > dureeSecondes)
        {
            TerminerUrgence();
            return;
        }
        
        // Effacer l'écran
        Console.Clear();
        
        // Afficher le titre du mode urgence
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        if (obstacle != null)
        {
            Console.WriteLine($"MODE URGENCE: {obstacle.Type} - {obstacle.Nom}");
        }
        else
        {
            Console.WriteLine("MODE URGENCE");
        }
        Console.WriteLine($"Temps restant: {Math.Max(0, dureeSecondes - (int)tempsPasse.TotalSeconds)} secondes");
        Console.ResetColor();
        Console.WriteLine();
        
        // Afficher le plateau de jeu simplifié avec l'obstacle
        AfficherPlateauUrgence();
        
        // Afficher les options d'actions d'urgence
        AfficherMenuActionsUrgence();
        
        // Attendre l'action du joueur
        string? action = Console.ReadLine();
        
        // Traiter l'action du joueur
        TraiterActionUrgence(action);
        
        // Si c'est un intrus, le déplacer
        if (obstacle != null && obstacle.Type == "Intrus")
        {
            obstacle.Deplacer();
        }
    }
    
    // Méthode pour afficher le plateau en mode urgence
    private void AfficherPlateauUrgence()
    {
        // Dimensions du plateau
        int largeur = 20;
        int hauteur = 10;
        
        // Créer un tableau représentant le plateau
        char[,] plateau = new char[hauteur, largeur];
        
        // Initialiser le plateau avec des espaces
        for (int i = 0; i < hauteur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                plateau[i, j] = ' ';
            }
        }
        
        // Placer l'obstacle sur le plateau
        if (obstacle != null)
        {
            plateau[obstacle.PositionY, obstacle.PositionX] = 'X';
        }
        
        // Afficher le plateau
        Console.WriteLine("+" + new string('-', largeur) + "+");
        for (int i = 0; i < hauteur; i++)
        {
            Console.Write("|");
            for (int j = 0; j < largeur; j++)
            {
                if (plateau[i, j] == 'X' && obstacle != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(obstacle.Afficher());
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(plateau[i, j]);
                }
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("+" + new string('-', largeur) + "+");
        Console.WriteLine();
    }
    
    // Méthode pour afficher le menu d'actions d'urgence
    private void AfficherMenuActionsUrgence()
    {
        if (obstacle == null)
        {
            Console.WriteLine("Aucune urgence active.");
            return;
        }
        
        Console.WriteLine("Actions d'urgence disponibles:");
        
        if (obstacle.Type == "Intrus")
        {
            Console.WriteLine("1 - Faire du bruit");
            Console.WriteLine("2 - Installer un épouvantail");
            Console.WriteLine("3 - Fermer les clôtures");
        }
        else if (obstacle.Type == "Intemperie")
        {
            Console.WriteLine("1 - Déployer une bâche");
            Console.WriteLine("2 - Fermer les serres");
            Console.WriteLine("3 - Créer des tranchées de drainage");
        }
        
        Console.WriteLine("Q - Quitter le mode urgence (l'obstacle continuera à faire des dégâts)");
        Console.Write("\nVotre action: ");
    }
    
    // Méthode pour traiter l'action d'urgence choisie par le joueur
    private void TraiterActionUrgence(string? action)
    {
        if (string.IsNullOrEmpty(action) || obstacle == null)
        {
            return;
        }
        
        action = action.ToUpper();
        
        if (action == "Q")
        {
            TerminerUrgence();
            return;
        }
        
        if (obstacle.Type == "Intrus")
        {
            switch (action)
            {
                case "1": // Faire du bruit
                    Console.WriteLine("Vous faites du bruit pour effrayer l'intrus!");
                    Thread.Sleep(1000);
                    
                    // 50% de chance que l'intrus s'enfuie
                    if (new Random().Next(100) < 50)
                    {
                        Console.WriteLine($"Le {obstacle.Nom} s'enfuit!");
                        obstacle.Desactiver();
                        Thread.Sleep(2000);
                        TerminerUrgence();
                    }
                    else
                    {
                        Console.WriteLine($"Le {obstacle.Nom} continue à rôder...");
                        Thread.Sleep(2000);
                    }
                    break;
                    
                case "2": // Installer un épouvantail
                    Console.WriteLine("Vous installez un épouvantail...");
                    Thread.Sleep(1000);
                    
                    // 70% de chance que l'intrus s'enfuie
                    if (new Random().Next(100) < 70)
                    {
                        Console.WriteLine($"Le {obstacle.Nom} s'enfuit!");
                        obstacle.Desactiver();
                        Thread.Sleep(2000);
                        TerminerUrgence();
                    }
                    else
                    {
                        Console.WriteLine($"Le {obstacle.Nom} n'est pas impressionné...");
                        Thread.Sleep(2000);
                    }
                    break;
                    
                case "3": // Fermer les clôtures
                    Console.WriteLine("Vous fermez toutes les clôtures...");
                    Thread.Sleep(1000);
                    
                    // 90% de chance que l'intrus soit bloqué
                    if (new Random().Next(100) < 90)
                    {
                        Console.WriteLine($"Le {obstacle.Nom} ne peut plus atteindre vos plantes!");
                        
                        // Protéger tous les terrains
                        foreach (Terrain terrain in terrains)
                        {
                            terrain.Clore();
                        }
                        
                        obstacle.Desactiver();
                        Thread.Sleep(2000);
                        TerminerUrgence();
                    }
                    else
                    {
                        Console.WriteLine($"Le {obstacle.Nom} a trouvé un passage...");
                        Thread.Sleep(2000);
                    }
                    break;
                    
                default:
                    Console.WriteLine("Action non reconnue!");
                    Thread.Sleep(1000);
                    break;
            }
        }
        else if (obstacle.Type == "Intemperie")
        {
            switch (action)
            {
                case "1": // Déployer une bâche
                    Console.WriteLine("Vous déployez des bâches de protection...");
                    Thread.Sleep(1000);
                    
                    // 60% de chance que les plantes soient protégées
                    if (new Random().Next(100) < 60)
                    {
                        Console.WriteLine("Les bâches protègent efficacement vos plantes!");
                        
                        // Protéger un terrain aléatoire
                        if (terrains.Count > 0)
                        {
                            int index = new Random().Next(terrains.Count);
                            terrains[index].Proteger();
                        }
                        
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.WriteLine("Les bâches ne suffisent pas face à cette intempérie...");
                        Thread.Sleep(2000);
                    }
                    break;
                    
                case "2": // Fermer les serres
                    Console.WriteLine("Vous fermez toutes les serres...");
                    Thread.Sleep(1000);
                    
                    // 80% de chance que les plantes soient protégées
                    if (new Random().Next(100) < 80)
                    {
                        Console.WriteLine("Les serres protègent efficacement vos plantes!");
                        
                        // Protéger tous les terrains
                        foreach (Terrain terrain in terrains)
                        {
                            terrain.Proteger();
                        }
                        
                        Thread.Sleep(2000);
                        TerminerUrgence();
                    }
                    else
                    {
                        Console.WriteLine("Certaines serres sont endommagées par l'intempérie...");
                        Thread.Sleep(2000);
                    }
                    break;
                    
                case "3": // Créer des tranchées de drainage
                    Console.WriteLine("Vous creusez des tranchées de drainage...");
                    Thread.Sleep(1000);
                    
                    // 70% de chance que les plantes soient protégées
                    if (new Random().Next(100) < 70)
                    {
                        Console.WriteLine("Les tranchées évacuent l'excès d'eau efficacement!");
                        
                        // Protéger tous les terrains
                        foreach (Terrain terrain in terrains)
                        {
                            terrain.Proteger();
                        }
                        
                        Thread.Sleep(2000);
                        TerminerUrgence();
                    }
                    else
                    {
                        Console.WriteLine("Les tranchées se remplissent trop vite...");
                        Thread.Sleep(2000);
                    }
                    break;
                    
                default:
                    Console.WriteLine("Action non reconnue!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
    
    // Méthode pour terminer le mode urgence
    private void TerminerUrgence()
    {
        estActif = false;
        obstacle = null; // Using null! would be better with nullable reference types enabled
        
        Console.WriteLine("\nFin du mode urgence!");
        Thread.Sleep(2000);
    }
    
    // Getter pour savoir si le mode urgence est actif
    public bool EstActif()
    {
        return estActif;
    }
}
