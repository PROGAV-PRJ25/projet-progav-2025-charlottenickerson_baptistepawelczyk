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
    private int AfficherTableauDeBord(int largeurConsole)
    {
        // Largeur du tableau de bord (légèrement plus petit que la largeur de la console)
        int largeurTableau = Math.Min(60, largeurConsole - 4);
        
        // Calcul de la marge pour centrer le tableau
        int marge = (largeurConsole - largeurTableau) / 2;
        
        // Ligne du haut du tableau
        Console.WriteLine();
        Console.Write(new string(' ', marge));
        Console.Write("┌" + new string('─', largeurTableau - 2) + "┐");
        Console.WriteLine();
        
        // Ligne du titre "Tableau de bord"
        string titre = "TABLEAU DE BORD";
        int margeTitre = (largeurTableau - titre.Length - 2) / 2;
        Console.Write(new string(' ', marge));
        Console.Write("│" + new string(' ', margeTitre) + titre + new string(' ', largeurTableau - 2 - titre.Length - margeTitre) + "│");
        Console.WriteLine();
        
        // Ligne de séparation
        Console.Write(new string(' ', marge));
        Console.Write("├" + new string('─', largeurTableau - 2) + "┤");
        Console.WriteLine();
        
        // Ligne pour la semaine
        string infoSemaine = $"Semaine: {semaineActuelle}";
        int margeSemaine = (largeurTableau - infoSemaine.Length - 2) / 2;
        Console.Write(new string(' ', marge));
        Console.Write("│" + new string(' ', margeSemaine) + infoSemaine + new string(' ', largeurTableau - 2 - infoSemaine.Length - margeSemaine) + "│");
        Console.WriteLine();
        
        // Ligne pour la saison
        string infoSaison = $"Saison: {saison.GetNomSaison()}";
        int margeSaison = (largeurTableau - infoSaison.Length - 2) / 2;
        Console.Write(new string(' ', marge));
        Console.Write("│" + new string(' ', margeSaison) + infoSaison + new string(' ', largeurTableau - 2 - infoSaison.Length - margeSaison) + "│");
        Console.WriteLine();
        
        // Ligne pour la température avec couleur conditionnelle
        string tempText = temperature.GetTemperatureString();
        string infoTemp = $"Température: {tempText}";
        int margeTemp = (largeurTableau - infoTemp.Length - 2) / 2;
        
        // Affichage de la ligne de température complète avec centrage
        Console.Write(new string(' ', marge));
        Console.Write("│" + new string(' ', margeTemp));
        
        // Texte avant la valeur de température
        Console.Write("Température: ");
        
        // Appliquer les couleurs en fonction de la température
        ConsoleColor couleurOriginale = Console.ForegroundColor;
        
        if (temperature.EstEnGel())
        {
            // Bleu pour le gel
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        else if (temperature.EstEnCanicule())
        {
            // Rouge pour la canicule
            Console.ForegroundColor = ConsoleColor.Red;
        }
        
        // Écrire l'information de température avec la couleur appropriée
        Console.Write(tempText);
        
        // Restaurer la couleur d'origine
        Console.ForegroundColor = couleurOriginale;
        
        // Compléter la ligne
        Console.Write(new string(' ', largeurTableau - 2 - infoTemp.Length - margeTemp) + "│");
        Console.WriteLine();
        
        // Ligne pour les précipitations avec couleur conditionnelle
        string precipText = precipitations.GetPrecipitationsString(saison.GetSaisonActuelle(), temperature);
        string infoPrecip = $"Précipitations: {precipText}";
        int margePrecip = (largeurTableau - infoPrecip.Length - 2) / 2;
        
        // Affichage de la ligne de précipitations complète avec centrage
        Console.Write(new string(' ', marge));
        Console.Write("│" + new string(' ', margePrecip));
        
        // Texte avant la valeur de précipitations
        Console.Write("Précipitations: ");
        
        // Appliquer les couleurs en fonction des précipitations
        Saison.TypeSaison saisonActuelle = saison.GetSaisonActuelle();
        
        if (precipitations.EstEnSecheresseExtreme(saisonActuelle, temperature) || 
            precipitations.EstEnSecheresse(saisonActuelle))
        {
            // Rouge pour la sécheresse
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (precipitations.EstEnInondation())
        {
            // Bleu pour l'inondation
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        
        // Écrire l'information de précipitations avec la couleur appropriée
        Console.Write(precipText);
        
        // Restaurer la couleur d'origine
        Console.ForegroundColor = couleurOriginale;
        
        // Compléter la ligne
        Console.Write(new string(' ', largeurTableau - 2 - infoPrecip.Length - margePrecip) + "│");
        Console.WriteLine();
        
        // Ligne du bas
        Console.Write(new string(' ', marge));
        Console.Write("└" + new string('─', largeurTableau - 2) + "┘");
        Console.WriteLine();
        
        // Retourner la position Y actuelle du curseur après l'affichage du tableau de bord
        return Console.CursorTop;
    }
    
    // Méthode pour afficher le jeu
    public void AfficherJeu()
    {
        // Effacement de la console
        Console.Clear();
        
        // Récupération de la largeur de la console
        int largeurConsole = Console.WindowWidth;
        int hauteurConsole = Console.WindowHeight;
        
        // S'assurer que la console est assez grande
        if (largeurConsole < 80 || hauteurConsole < 30)
        {
            Console.WriteLine("Veuillez agrandir la fenêtre du terminal pour un meilleur affichage.");
            Console.WriteLine("Taille recommandée: 80x30 ou plus");
            Console.WriteLine("Taille actuelle: " + largeurConsole + "x" + hauteurConsole);
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            return;
        }
        
        // Mettre à jour l'affichage des plantes dans la grille
        affichage.MettreAJourAffichagePlantes();
        
        // Affichage du plateau de jeu
        affichage.AfficherPlateau();
        
        // Affichage du tableau de bord avec les statistiques et récupération de la position finale
        int positionApresTableau = AfficherTableauDeBord(largeurConsole);
        
        // Afficher les prévisions météo à côté du tableau de bord
        int posXPrevisions = Math.Min(largeurConsole - 50, 65);
        affichage.AfficherPrevisionsMeteo(posXPrevisions, positionApresTableau - 10, temperature, precipitations, saison.GetSaisonActuelle());
        
        // Prévoir de l'espace pour les instructions et la saisie utilisateur
        int espaceNecessaire = positionApresTableau + 15; // 15 lignes pour la vue de parcelle
        
        // Réduire la taille de la vue de parcelle si nécessaire
        if (espaceNecessaire > hauteurConsole - 5)
        {
            positionApresTableau = Math.Min(positionApresTableau, hauteurConsole - 20);
        }
        
        // Affichage de la parcelle sélectionnée en dessous du tableau de bord
        affichage.AfficherParcelleSelectionnee(positionApresTableau);
        
        // Positionner le curseur après tous les éléments affichés
        int cursorPos = Math.Min(Console.CursorTop + 2, hauteurConsole - 5);
        Console.SetCursorPosition(0, cursorPos);
        
        // Affichage des instructions pour le joueur
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nCOMMANDES DISPONIBLES:");
        Console.ResetColor();
        Console.WriteLine("- Sélectionner une parcelle: entrez les coordonnées 'i,j'");
        Console.WriteLine("- Actions sur la parcelle sélectionnée:");
        Console.WriteLine("  1: Arroser   2: Fertiliser   3: Désherber   4: Planter   5: Récolter   6: Examiner");
        Console.WriteLine("- Passer à la semaine suivante: 'N' ou 'ENTER' ou 'next'");
        Console.WriteLine("- Quitter le jeu: 'Q'");
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