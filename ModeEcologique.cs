using System;
using System.Collections.Generic;

public class ModeEcologique
{
    // Propriétés
    private bool estActif;
    private int bilanCarbone;
    private int impactEnvironnemental;
    private List<string> methodesEcologiquesUtilisees;
    private Dictionary<string, int> historiqueRotations;
    
    // Constructeur
    public ModeEcologique()
    {
        estActif = false;
        bilanCarbone = 0;
        impactEnvironnemental = 0;
        methodesEcologiquesUtilisees = new List<string>();
        historiqueRotations = new Dictionary<string, int>();
    }
    
    // Activer ou désactiver le mode écologique
    public void Activer(bool actif = true)
    {
        estActif = actif;
        Console.WriteLine(estActif ? "Mode écologique activé." : "Mode écologique désactivé.");
    }
    
    // Vérifier si le mode écologique est actif
    public bool EstActif()
    {
        return estActif;
    }
    
    // Ajouter une méthode écologique utilisée
    public void AjouterMethodeEcologique(string methode)
    {
        if (!methodesEcologiquesUtilisees.Contains(methode))
        {
            methodesEcologiquesUtilisees.Add(methode);
            
            // Amélioration du bilan carbone et de l'impact environnemental
            AmeliorerBilanCarbone(5);
            AmeliorerImpactEnvironnemental(5);
            
            Console.WriteLine($"Nouvelle méthode écologique adoptée: {methode}");
        }
    }
    
    // Enregistrer une rotation de culture
    public void EnregistrerRotation(string terrain, string plante)
    {
        string cle = $"{terrain}_{plante}";
        
        if (historiqueRotations.ContainsKey(cle))
        {
            historiqueRotations[cle]++;
            
            // Si une même plante est cultivée trop souvent sur le même terrain,
            // cela a un impact négatif sur l'environnement
            if (historiqueRotations[cle] > 2)
            {
                AugmenterImpactEnvironnemental(historiqueRotations[cle] * 2);
                Console.WriteLine($"Attention: {plante} est cultivée trop souvent sur {terrain}. Cela appauvrit le sol.");
            }
        }
        else
        {
            historiqueRotations.Add(cle, 1);
            
            // Une nouvelle rotation est bénéfique pour l'environnement
            AmeliorerImpactEnvironnemental(3);
        }
    }
    
    // Utiliser des animaux pour le désherbage
    public void DesherbageAnimal(string terrain, string animal)
    {
        if (!estActif)
        {
            return;
        }
        
        Console.WriteLine($"Des {animal}s sont utilisés pour désherber le terrain {terrain} de façon écologique.");
        
        // Amélioration du bilan carbone (pas d'utilisation de machines)
        AmeliorerBilanCarbone(10);
        
        // Amélioration de l'impact environnemental (pas d'herbicides)
        AmeliorerImpactEnvironnemental(15);
        
        // Ajouter aux méthodes écologiques utilisées
        AjouterMethodeEcologique($"Désherbage par {animal}s");
    }
    
    // Utiliser du compost
    public void UtiliserCompost(string terrain)
    {
        if (!estActif)
        {
            return;
        }
        
        Console.WriteLine($"Du compost est utilisé sur le terrain {terrain} pour enrichir le sol de façon naturelle.");
        
        // Amélioration du bilan carbone (pas d'engrais chimiques)
        AmeliorerBilanCarbone(8);
        
        // Amélioration de l'impact environnemental
        AmeliorerImpactEnvironnemental(12);
        
        // Ajouter aux méthodes écologiques utilisées
        AjouterMethodeEcologique("Utilisation de compost");
    }
    
    // Installer des récupérateurs d'eau de pluie
    public void InstallerRecuperateursEau()
    {
        if (!estActif)
        {
            return;
        }
        
        Console.WriteLine("Des récupérateurs d'eau de pluie sont installés pour économiser l'eau.");
        
        // Amélioration du bilan carbone
        AmeliorerBilanCarbone(15);
        
        // Amélioration de l'impact environnemental
        AmeliorerImpactEnvironnemental(20);
        
        // Ajouter aux méthodes écologiques utilisées
        AjouterMethodeEcologique("Récupération d'eau de pluie");
    }
    
    // Cultiver des plantes compagnes
    public void CultiverPlantesCompagnes(string terrain, string plante1, string plante2)
    {
        if (!estActif)
        {
            return;
        }
        
        Console.WriteLine($"Les plantes {plante1} et {plante2} sont cultivées ensemble sur le terrain {terrain} pour favoriser leur croissance mutuelle.");
        
        // Amélioration du bilan carbone
        AmeliorerBilanCarbone(5);
        
        // Amélioration de l'impact environnemental
        AmeliorerImpactEnvironnemental(10);
        
        // Ajouter aux méthodes écologiques utilisées
        AjouterMethodeEcologique("Culture de plantes compagnes");
    }
    
    // Améliorer le bilan carbone
    private void AmeliorerBilanCarbone(int valeur)
    {
        if (!estActif)
        {
            return;
        }
        
        bilanCarbone -= valeur;
        if (bilanCarbone < 0)
        {
            bilanCarbone = 0;
        }
    }
    
    // Détériorer le bilan carbone
    public void AugmenterBilanCarbone(int valeur)
    {
        if (!estActif)
        {
            return;
        }
        
        bilanCarbone += valeur;
    }
    
    // Améliorer l'impact environnemental
    private void AmeliorerImpactEnvironnemental(int valeur)
    {
        if (!estActif)
        {
            return;
        }
        
        impactEnvironnemental -= valeur;
        if (impactEnvironnemental < 0)
        {
            impactEnvironnemental = 0;
        }
    }
    
    // Détériorer l'impact environnemental
    public void AugmenterImpactEnvironnemental(int valeur)
    {
        if (!estActif)
        {
            return;
        }
        
        impactEnvironnemental += valeur;
    }
    
    // Obtenir le bilan carbone
    public int GetBilanCarbone()
    {
        return bilanCarbone;
    }
    
    // Obtenir l'impact environnemental
    public int GetImpactEnvironnemental()
    {
        return impactEnvironnemental;
    }
    
    // Afficher le bilan écologique
    public void AfficherBilanEcologique()
    {
        if (!estActif)
        {
            Console.WriteLine("Le mode écologique n'est pas activé.");
            return;
        }
        
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║                 BILAN ÉCOLOGIQUE                      ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
        
        // Afficher le bilan carbone
        Console.WriteLine($"\nBilan carbone: {bilanCarbone} unités");
        AfficherBarreProgression(bilanCarbone, 100, true);
        
        // Afficher l'impact environnemental
        Console.WriteLine($"\nImpact environnemental: {impactEnvironnemental} unités");
        AfficherBarreProgression(impactEnvironnemental, 100, true);
        
        // Afficher les méthodes écologiques utilisées
        Console.WriteLine("\nMéthodes écologiques utilisées:");
        if (methodesEcologiquesUtilisees.Count == 0)
        {
            Console.WriteLine("  Aucune méthode écologique n'a été utilisée.");
        }
        else
        {
            foreach (string methode in methodesEcologiquesUtilisees)
            {
                Console.WriteLine($"  - {methode}");
            }
        }
        
        // Afficher les recommandations
        Console.WriteLine("\nRecommandations écologiques:");
        AfficherRecommandationsEcologiques();
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Méthode pour afficher une barre de progression
    private void AfficherBarreProgression(int valeur, int max, bool inverser = false)
    {
        // Calculer le pourcentage
        int pourcentage = (int)((double)valeur / max * 100);
        if (pourcentage > 100) pourcentage = 100;
        
        // Si inverser est vrai, on considère que plus la valeur est basse, mieux c'est
        ConsoleColor couleur;
        if (inverser)
        {
            if (pourcentage < 30) couleur = ConsoleColor.Green;
            else if (pourcentage < 70) couleur = ConsoleColor.Yellow;
            else couleur = ConsoleColor.Red;
        }
        else
        {
            if (pourcentage > 70) couleur = ConsoleColor.Green;
            else if (pourcentage > 30) couleur = ConsoleColor.Yellow;
            else couleur = ConsoleColor.Red;
        }
        
        // Afficher la barre
        Console.Write("[");
        Console.ForegroundColor = couleur;
        
        int nbCaracteres = 50;
        int nbRemplis = (int)((double)pourcentage / 100 * nbCaracteres);
        
        Console.Write(new string('█', nbRemplis));
        Console.ResetColor();
        Console.Write(new string(' ', nbCaracteres - nbRemplis));
        Console.WriteLine("]");
    }
    
    // Afficher des recommandations écologiques
    private void AfficherRecommandationsEcologiques()
    {
        List<string> recommandations = new List<string>();
        
        // Recommandations basées sur le bilan carbone
        if (bilanCarbone > 70)
        {
            recommandations.Add("Utilisez des outils manuels plutôt que des machines à moteur.");
            recommandations.Add("Installez des récupérateurs d'eau de pluie pour économiser l'eau.");
        }
        
        // Recommandations basées sur l'impact environnemental
        if (impactEnvironnemental > 70)
        {
            recommandations.Add("Pratiquez la rotation des cultures pour maintenir la santé du sol.");
            recommandations.Add("Utilisez du compost au lieu d'engrais chimiques.");
            recommandations.Add("Plantez des espèces locales adaptées au climat.");
        }
        
        // Recommandations générales
        if (!methodesEcologiquesUtilisees.Contains("Désherbage par animaux"))
        {
            recommandations.Add("Utilisez des animaux comme les poules ou les canards pour désherber naturellement.");
        }
        
        if (!methodesEcologiquesUtilisees.Contains("Culture de plantes compagnes"))
        {
            recommandations.Add("Plantez des plantes compagnes qui se soutiennent mutuellement.");
        }
        
        if (!methodesEcologiquesUtilisees.Contains("Utilisation de compost"))
        {
            recommandations.Add("Créez votre propre compost avec les déchets organiques.");
        }
        
        // Afficher les recommandations
        if (recommandations.Count == 0)
        {
            Console.WriteLine("  Félicitations ! Vous appliquez déjà de nombreuses pratiques écologiques.");
        }
        else
        {
            foreach (string recommandation in recommandations)
            {
                Console.WriteLine($"  - {recommandation}");
            }
        }
    }
    
    // Méthode pour afficher le menu écologique
    public void AfficherMenu()
    {
        if (!estActif)
        {
            Console.WriteLine("Le mode écologique n'est pas activé.");
            return;
        }
        
        bool continuer = true;
        
        while (continuer)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   MODE ÉCOLOGIQUE                     ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            
            Console.WriteLine("\nActions disponibles:");
            Console.WriteLine("1. Utiliser des animaux pour le désherbage");
            Console.WriteLine("2. Utiliser du compost");
            Console.WriteLine("3. Installer des récupérateurs d'eau de pluie");
            Console.WriteLine("4. Cultiver des plantes compagnes");
            Console.WriteLine("5. Voir le bilan écologique");
            Console.WriteLine("6. Quitter le mode écologique");
            
            Console.Write("\nChoisissez une action (1-6): ");
            string? choix = Console.ReadLine() ?? "6";
            
            switch (choix)
            {
                case "1":
                    MenuDesherbageAnimal();
                    break;
                case "2":
                    MenuUtiliserCompost();
                    break;
                case "3":
                    InstallerRecuperateursEau();
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey(true);
                    break;
                case "4":
                    MenuCultiverPlantesCompagnes();
                    break;
                case "5":
                    AfficherBilanEcologique();
                    break;
                case "6":
                    continuer = false;
                    break;
                default:
                    Console.WriteLine("Choix non reconnu. Appuyez sur une touche pour continuer...");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
    
    // Menu pour utiliser des animaux pour le désherbage
    private void MenuDesherbageAnimal()
    {
        Console.Clear();
        Console.WriteLine("=== DÉSHERBAGE PAR ANIMAUX ===");
        
        Console.WriteLine("\nTerrain à désherber (0 pour annuler): ");
        if (!int.TryParse(Console.ReadLine(), out int choixTerrain) || choixTerrain == 0)
        {
            return;
        }
        
        Console.WriteLine("\nChoisir un animal:");
        Console.WriteLine("1. Poules");
        Console.WriteLine("2. Canards");
        Console.WriteLine("3. Oies");
        Console.WriteLine("4. Moutons nains");
        
        Console.Write("\nVotre choix (1-4): ");
        if (!int.TryParse(Console.ReadLine(), out int choixAnimal) || choixAnimal < 1 || choixAnimal > 4)
        {
            return;
        }
        
        string[] animaux = { "poule", "canard", "oie", "mouton nain" };
        DesherbageAnimal("Terrain " + choixTerrain, animaux[choixAnimal - 1]);
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Menu pour utiliser du compost
    private void MenuUtiliserCompost()
    {
        Console.Clear();
        Console.WriteLine("=== UTILISATION DE COMPOST ===");
        
        Console.WriteLine("\nTerrain à enrichir (0 pour annuler): ");
        if (!int.TryParse(Console.ReadLine(), out int choixTerrain) || choixTerrain == 0)
        {
            return;
        }
        
        UtiliserCompost("Terrain " + choixTerrain);
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
    
    // Menu pour cultiver des plantes compagnes
    private void MenuCultiverPlantesCompagnes()
    {
        Console.Clear();
        Console.WriteLine("=== CULTURE DE PLANTES COMPAGNES ===");
        
        Console.WriteLine("\nTerrain pour la culture (0 pour annuler): ");
        if (!int.TryParse(Console.ReadLine(), out int choixTerrain) || choixTerrain == 0)
        {
            return;
        }
        
        Console.WriteLine("\nChoisir la première plante:");
        Console.WriteLine("1. Tomate");
        Console.WriteLine("2. Basilic");
        Console.WriteLine("3. Carotte");
        Console.WriteLine("4. Oignon");
        
        Console.Write("\nVotre choix (1-4): ");
        if (!int.TryParse(Console.ReadLine(), out int choixPlante1) || choixPlante1 < 1 || choixPlante1 > 4)
        {
            return;
        }
        
        Console.WriteLine("\nChoisir la deuxième plante:");
        Console.WriteLine("1. Tomate");
        Console.WriteLine("2. Basilic");
        Console.WriteLine("3. Carotte");
        Console.WriteLine("4. Oignon");
        
        Console.Write("\nVotre choix (1-4): ");
        if (!int.TryParse(Console.ReadLine(), out int choixPlante2) || choixPlante2 < 1 || choixPlante2 > 4 || choixPlante2 == choixPlante1)
        {
            Console.WriteLine("Choix invalide ou identique à la première plante.");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        string[] plantes = { "Tomate", "Basilic", "Carotte", "Oignon" };
        CultiverPlantesCompagnes("Terrain " + choixTerrain, plantes[choixPlante1 - 1], plantes[choixPlante2 - 1]);
        
        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey(true);
    }
}
