using System;

public class Temps
{
    // Numéro de la semaine en cours
    private int semaineActuelle;
    
    // Instance de la classe d'affichage
    private readonly Affichage affichage;
    
    // Instance de la classe saison
    private readonly Saison saison;
    
    // Instance de la classe température
    private readonly Temperature temperature;
    
    // Instance de la classe précipitations
    private readonly Precipitations precipitations;
    
    // Constructeur
    public Temps()
    {
        // Initialisation des propriétés
        semaineActuelle = 1;
        affichage = new Affichage();
        saison = new Saison();
        temperature = new Temperature();
        precipitations = new Precipitations();
        
        // Mise à jour de la saison initiale
        saison.MettreAJour(semaineActuelle);
        
        // Génération de la température initiale basée sur la saison
        temperature.GenererTemperature(saison.GetSaisonActuelle());
        
        // Génération des précipitations initiales basées sur la saison
        precipitations.GenererPrecipitations(saison.GetSaisonActuelle());
    }
    
    // Méthode pour passer à la semaine suivante
    public void PasserSemaine()
    {
        // Incrémente le compteur de semaines
        semaineActuelle++;
        
        // Mise à jour de la saison en fonction de la nouvelle semaine
        saison.MettreAJour(semaineActuelle);
        
        // Génération d'une nouvelle température basée sur la saison actuelle
        temperature.GenererTemperature(saison.GetSaisonActuelle());
        
        // Génération de nouvelles précipitations basées sur la saison actuelle
        precipitations.GenererPrecipitations(saison.GetSaisonActuelle());
        
        // Afficher une animation en fonction des conditions météorologiques
        if (temperature.EstEnGel())
        {
            // Animation de neige si gel
            Console.WriteLine("\nAlerte: Températures glaciales! Protégez vos plantes du gel.");
            System.Threading.Thread.Sleep(1000);
            affichage.AfficherAnimationMeteo("neige", 3000);
        }
        else if (temperature.EstEnCanicule())
        {
            // Animation de soleil intense si canicule
            Console.WriteLine("\nAlerte: Canicule! Pensez à arroser vos plantes fréquemment.");
            System.Threading.Thread.Sleep(1000);
            affichage.AfficherAnimationMeteo("soleil", 3000);
        }
        else if (precipitations.EstEnInondation())
        {
            // Animation de forte pluie si inondation
            Console.WriteLine("\nAlerte: Fortes précipitations! Risque d'inondation pour vos cultures.");
            System.Threading.Thread.Sleep(1000);
            affichage.AfficherAnimationMeteo("pluie", 3000);
        }
        else if (precipitations.EstEnSecheresseExtreme(saison.GetSaisonActuelle(), temperature) || 
                 precipitations.EstEnSecheresse(saison.GetSaisonActuelle()))
        {
            // Animation de vent si sécheresse
            Console.WriteLine("\nAlerte: Sécheresse! Arrosez vos plantes pour éviter qu'elles ne se dessèchent.");
            System.Threading.Thread.Sleep(1000);
            affichage.AfficherAnimationMeteo("vent", 3000);
        }
        else
        {
            // Déterminer une animation météo aléatoire pour les conditions normales
            Random random = new Random();
            int choixAnim = random.Next(100);
            
            if (choixAnim < 40) // 40% de chance de pluie légère
            {
                affichage.AfficherAnimationMeteo("pluie", 2000);
            }
            else if (choixAnim < 70) // 30% de chance de soleil
            {
                affichage.AfficherAnimationMeteo("soleil", 2000);
            }
            else if (choixAnim < 90) // 20% de chance de vent
            {
                affichage.AfficherAnimationMeteo("vent", 2000);
            }
            else // 10% de chance d'orage
            {
                Console.WriteLine("\nAlerte: Orages prévus!");
                System.Threading.Thread.Sleep(1000);
                affichage.AfficherAnimationMeteo("orage", 2000);
            }
        }
    }
    
    // Méthode pour obtenir le numéro de semaine actuelle
    public int GetSemaineActuelle()
    {
        return semaineActuelle;
    }
    
    // Méthode pour obtenir la saison actuelle
    public Saison.TypeSaison GetSaisonActuelle()
    {
        return saison.GetSaisonActuelle();
    }
    
    // Méthode pour afficher le tableau de bord avec les statistiques
    private int AfficherTableauDeBord(int posX, int posY, int largeurConsole)
    {
        // Largeur du tableau de bord (légèrement plus petit que la largeur de la console)
        int largeurTableau = Math.Min(60, largeurConsole - 4);
        
        // Note : ici on ignore le centrage horizontal, on fixe la position posX passée en paramètre
        
        int ligne = posY;
        
        // Ligne du haut du tableau
        Console.SetCursorPosition(posX, ligne++);
        Console.Write("┌" + new string('─', largeurTableau - 2) + "┐");
        
        // Ligne du titre "Tableau de bord"
        string titre = "TABLEAU DE BORD";
        int margeTitre = (largeurTableau - titre.Length - 2) / 2;
        Console.SetCursorPosition(posX, ligne++);
        Console.Write("│" + new string(' ', margeTitre) + titre + new string(' ', largeurTableau - 2 - titre.Length - margeTitre) + "│");
        
        // Ligne de séparation
        Console.SetCursorPosition(posX, ligne++);
        Console.Write("├" + new string('─', largeurTableau - 2) + "┤");
        
        // Ligne pour la semaine
        string infoSemaine = $"Semaine: {semaineActuelle}";
        int margeSemaine = (largeurTableau - infoSemaine.Length - 2) / 2;
        Console.SetCursorPosition(posX, ligne++);
        Console.Write("│" + new string(' ', margeSemaine) + infoSemaine + new string(' ', largeurTableau - 2 - infoSemaine.Length - margeSemaine) + "│");
        
        // Ligne pour la saison
        string infoSaison = $"Saison: {saison.GetNomSaison()}";
        int margeSaison = (largeurTableau - infoSaison.Length - 2) / 2;
        Console.SetCursorPosition(posX, ligne++);
        Console.Write("│" + new string(' ', margeSaison) + infoSaison + new string(' ', largeurTableau - 2 - infoSaison.Length - margeSaison) + "│");
        
        // Ligne pour la température avec couleur conditionnelle
        string tempText = temperature.GetTemperatureString();
        string infoTemp = $"Température: {tempText}";
        int margeTemp = (largeurTableau - infoTemp.Length - 2) / 2;
        
        Console.SetCursorPosition(posX, ligne);
        Console.Write("│" + new string(' ', margeTemp));
        
        // Texte avant la valeur de température
        Console.Write("Température: ");
        
        ConsoleColor couleurOriginale = Console.ForegroundColor;
        
        if (temperature.EstEnGel())
            Console.ForegroundColor = ConsoleColor.Blue;
        else if (temperature.EstEnCanicule())
            Console.ForegroundColor = ConsoleColor.Red;
        
        Console.Write(tempText);
        Console.ForegroundColor = couleurOriginale;
        
        Console.Write(new string(' ', largeurTableau - 2 - infoTemp.Length - margeTemp) + "│");
        ligne++;
        
        // Ligne pour les précipitations avec couleur conditionnelle
        string precipText = precipitations.GetPrecipitationsString(saison.GetSaisonActuelle(), temperature);
        string infoPrecip = $"Précipitations: {precipText}";
        int margePrecip = (largeurTableau - infoPrecip.Length - 2) / 2;
        
        Console.SetCursorPosition(posX, ligne);
        Console.Write("│" + new string(' ', margePrecip));
        
        Console.Write("Précipitations: ");
        
        Saison.TypeSaison saisonActuelle = saison.GetSaisonActuelle();
        
        if (precipitations.EstEnSecheresseExtreme(saisonActuelle, temperature) || 
            precipitations.EstEnSecheresse(saisonActuelle))
            Console.ForegroundColor = ConsoleColor.Red;
        else if (precipitations.EstEnInondation())
            Console.ForegroundColor = ConsoleColor.Blue;
        
        Console.Write(precipText);
        Console.ForegroundColor = couleurOriginale;
        
        Console.Write(new string(' ', largeurTableau - 2 - infoPrecip.Length - margePrecip) + "│");
        ligne++;
        
        // Ligne du bas
        Console.SetCursorPosition(posX, ligne++);
        Console.Write("└" + new string('─', largeurTableau - 2) + "┘");
        
        // Retourner la position Y actuelle du curseur après l'affichage du tableau de bord
        return ligne;
    }
    
    // Méthode pour afficher le jeu
    public void AfficherJeu()
    {
        Console.Clear();
        
        int largeurConsole = Console.WindowWidth;
        int hauteurConsole = Console.WindowHeight;
        
        if (largeurConsole < 80 || hauteurConsole < 30)
        {
            Console.WriteLine("Veuillez agrandir la fenêtre du terminal pour un meilleur affichage.");
            Console.WriteLine("Taille recommandée: 80x30 ou plus");
            Console.WriteLine("Taille actuelle: " + largeurConsole + "x" + hauteurConsole);
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        affichage.MettreAJourAffichagePlantes();
        affichage.AfficherPlateau();
        
        int positionApresTableau = AfficherTableauDeBord(Console.WindowWidth - 80, 3, largeurConsole);
        int posXPrevisions = Math.Min(largeurConsole - 50, 65);
        affichage.AfficherPrevisionsMeteo(Console.WindowWidth - 80, 14, temperature, precipitations, saison.GetSaisonActuelle());
        
        int espaceNecessaire = positionApresTableau + 15;
        if (espaceNecessaire > hauteurConsole - 5)
        {
            positionApresTableau = Math.Min(positionApresTableau, hauteurConsole - 20);
        }
        
        affichage.AfficherParcelleSelectionnee(positionApresTableau);
        
        // Positionner le curseur 5 lignes avant le bas pour afficher les commandes
        int ligneCommandes = hauteurConsole - 6;  // -1 pour ligne, -5 pour marge
        
        if (ligneCommandes < Console.CursorTop + 2)
        {
            ligneCommandes = Console.CursorTop + 2; // ne pas remonter le curseur par accident
        }
        
        Console.SetCursorPosition(0, ligneCommandes);
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("COMMANDES DISPONIBLES:");
        Console.ResetColor();
        Console.WriteLine("- Sélectionner une parcelle: entrez les coordonnées 'i,j'");
        Console.WriteLine("- Actions sur la parcelle sélectionnée:");
        Console.WriteLine("  1: Arroser   2: Fertiliser   3: Désherber   4: Planter   5: Récolter   6: Examiner");
        Console.WriteLine("- Passer à la semaine suivante: 'N' ou 'ENTER' ou 'next'");
        Console.WriteLine("- Quitter le jeu: 'Q'");
        
        int ligne = ligneCommandes + 7;
        if (ligne >= Console.WindowHeight)
            ligne = Console.WindowHeight - 1;
        Console.SetCursorPosition(0, ligne);

    }
    
    // Méthode principale de la boucle de jeu
    public void DemarrerJeu()
    {
        bool jeuEnCours = true;
        
        // Boucle principale du jeu
        while (jeuEnCours)
        {
            // Affichage de l'état actuel du jeu
            AfficherJeu();
            
            bool passerAuTourSuivant = false;
            
            // Attendre l'entrée de l'utilisateur jusqu'à ce qu'il décide de passer au tour suivant
            while (!passerAuTourSuivant && jeuEnCours)
            {
                Console.Write("Action (coordonnées i,j pour sélectionner une parcelle, action 1-6 pour la parcelle sélectionnée, N/ENTER pour tour suivant, Q pour quitter): ");
                string? input = Console.ReadLine();
                
                // Vérifier si l'utilisateur veut quitter le jeu
                if (input?.ToUpper() == "Q")
                {
                    jeuEnCours = false;
                }
                // Vérifier si l'utilisateur veut passer au tour suivant
                else if (string.IsNullOrWhiteSpace(input) || input.ToUpper() == "N" || input.ToLower() == "next")
                {
                    passerAuTourSuivant = true;
                }
                // Vérifier si l'utilisateur veut effectuer une action sur une parcelle sélectionnée
                else if (input == "1" || input == "2" || input == "3" || input == "4" || input == "5" || input == "6")
                {
                    // Traiter l'action sur la parcelle sélectionnée
                    affichage.TraiterActionParcelle(input);
                    
                    // Réafficher le jeu pour montrer les changements
                    AfficherJeu();
                }
                else
                {
                    // Gérer le clic sur une parcelle
                    bool passerTour = affichage.GererClicParcelle(input);
                    
                    if (passerTour)
                    {
                        passerAuTourSuivant = true;
                    }
                    else
                    {
                        // Réafficher le jeu pour montrer la parcelle sélectionnée
                        AfficherJeu();
                    }
                }
            }
            
            // Si on est toujours en jeu et qu'on veut passer au tour suivant
            if (jeuEnCours && passerAuTourSuivant)
            {
                // Passage à la semaine suivante
                PasserSemaine();
                
                // Faire croître toutes les plantes dans toutes les parcelles
                FaireCroitrePlantes();
            }
        }
    }
    
    // Méthode pour faire croître toutes les plantes
    private void FaireCroitrePlantes()
    {
        Random random = new Random();
        int largeur = affichage.GetLargeurGrille();
        int hauteur = affichage.GetHauteurGrille();
        
        for (int i = 0; i < hauteur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                // Récupérer la parcelle
                Parcelle parcelle = affichage.GetParcelle(i, j);
                
                // Faire croître la plante de cette parcelle si elle existe
                if (parcelle.EstPlantee)
                {
                    // Ajouter un élément aléatoire: probabilité de 75% de croissance
                    if (random.Next(100) < 75)
                    {
                        parcelle.FaireCroitre();
                    }
                }
                
                // Mise à jour de l'humidité (diminution naturelle)
                parcelle.Humidite = Math.Max(0, parcelle.Humidite - random.Next(5, 15));
                
                // Mise à jour de la fertilité (diminution lente)
                if (random.Next(100) < 20) // 20% de chance de diminuer
                {
                    parcelle.Fertilite = Math.Max(0, parcelle.Fertilite - random.Next(1, 5));
                }
            }
        }
    }
}