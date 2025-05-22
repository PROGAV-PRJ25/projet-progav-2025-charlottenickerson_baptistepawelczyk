using System;
using System.Threading;

public class Webcam
{
    // Propriétés
    private bool EstActivee { get; set; }
    private string Nom { get; set; }
    private int QualiteImage { get; set; } // 1 à 10
    private bool VisionNocturne { get; set; }
    
    // Constructeur
    public Webcam(string nom = "Webcam jardin", int qualiteImage = 8, bool visionNocturne = true)
    {
        Nom = nom;
        QualiteImage = Math.Clamp(qualiteImage, 1, 10);
        VisionNocturne = visionNocturne;
        EstActivee = true;
    }
    
    // Méthode pour activer la webcam
    public void Activer()
    {
        EstActivee = true;
        Console.WriteLine($"La {Nom} est maintenant activée.");
    }
    
    // Méthode pour désactiver la webcam
    public void Desactiver()
    {
        EstActivee = false;
        Console.WriteLine($"La {Nom} est maintenant désactivée.");
    }
    
    // Méthode pour détecter un intrus
    public bool DetecterIntrus()
    {
        if (!EstActivee)
        {
            return false;
        }
        
        // Simulation de détection d'intrus
        Random random = new Random();
        
        // Plus la qualité d'image est élevée, plus la détection est fiable
        int chancesDetection = QualiteImage * 10; // 10% à 100% selon la qualité
        
        // La nuit, la détection est moins fiable sauf avec vision nocturne
        bool estNuit = DateTime.Now.Hour < 6 || DateTime.Now.Hour > 20;
        if (estNuit && !VisionNocturne)
        {
            chancesDetection /= 2;
        }
        
        return random.Next(100) < chancesDetection;
    }
    
    // Méthode pour détecter une intempérie
    public bool DetecterIntemperie()
    {
        if (!EstActivee)
        {
            return false;
        }
        
        // Simulation de détection d'intempérie
        Random random = new Random();
        
        // La détection d'intempérie est généralement plus fiable que celle d'intrus
        int chancesDetection = 50 + (QualiteImage * 5); // 55% à 100% selon la qualité
        
        return random.Next(100) < chancesDetection;
    }
    
    // Méthode pour envoyer une alerte
    public void EnvoyerAlerte(string type, string description)
    {
        if (!EstActivee)
        {
            return;
        }
        
        // Affichage de l'alerte avec animation
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        
        for (int i = 0; i < 3; i++)
        {
            Console.Clear();
            Console.WriteLine($"\n\n  ALERTE {type.ToUpper()} DÉTECTÉE PAR {Nom.ToUpper()}  ");
            Console.WriteLine($"\n  {description}  ");
            Thread.Sleep(500);
            
            Console.Clear();
            Thread.Sleep(300);
        }
        
        Console.Clear();
        Console.WriteLine($"\n\n  ALERTE {type.ToUpper()} DÉTECTÉE PAR {Nom.ToUpper()}  ");
        Console.WriteLine($"\n  {description}  ");
        Console.WriteLine("\n  Appuyez sur une touche pour voir l'image de la webcam...  ");
        
        Console.ResetColor();
        Console.ReadKey(true);
    }
    
    // Méthode pour capturer une image
    public void CapturerImage(string evenement)
    {
        if (!EstActivee)
        {
            Console.WriteLine("La webcam est désactivée, impossible de capturer une image.");
            return;
        }
        
        Console.Clear();
        Console.WriteLine($"Image capturée par {Nom} - {DateTime.Now}");
        Console.WriteLine($"Événement: {evenement}");
        Console.WriteLine($"Qualité d'image: {QualiteImage}/10");
        
        // Simulation d'affichage d'une image avec des caractères ASCII
        Console.WriteLine("\n+--------------------------------+");
        Console.WriteLine("|                                |");
        Console.WriteLine("|                                |");
        Console.WriteLine("|                                |");
        Console.WriteLine("|            [WEBCAM]            |");
        Console.WriteLine("|                                |");
        Console.WriteLine("|                                |");
        Console.WriteLine("|                                |");
        Console.WriteLine("+--------------------------------+");
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
}
