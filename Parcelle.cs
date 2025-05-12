using System;
using System.Collections.Generic;

public class Parcelle
{
    // Position de la parcelle sur la grille
    public int Row { get; private set; }
    public int Col { get; private set; }
    
    // Couleur de la parcelle
    public ConsoleColor Couleur { get; private set; }
    
    // Autres caractéristiques
    public string Type { get; set; } = "Normal";
    public int Fertilite { get; set; } = 100;
    public int Humidite { get; set; } = 50;
    
    // Plante sur la parcelle
    public Plante? PlanteParcelle { get; set; } = null;
    
    // Installations sur la parcelle
    public bool ASerre { get; set; } = false;
    public bool ABarriere { get; set; } = false;
    public bool APareSoleil { get; set; } = false;
    
    // Constructeur
    public Parcelle(int row, int col, ConsoleColor couleur)
    {
        Row = row;
        Col = col;
        Couleur = couleur;
    }
    
    // Méthode pour vérifier si une plante est sur la parcelle
    public bool AUnePlante()
    {
        return PlanteParcelle != null;
    }
    
    // Méthode pour obtenir l'emoji représentant l'état de la plante et les installations
    public string ObtenirEmojiPlante()
    {
        string resultat = "";
        
        // Déterminer l'emoji de la plante si présente
        if (AUnePlante())
        {
            if (PlanteParcelle.EstMorte())
            {
                resultat += "💀";
            }
            // Vérifier si la santé est faible (< 50%)
            else if (PlanteParcelle.Sante < 50)
            {
                // Utiliser l'emoji feuille morte pour les plantes en mauvaise santé
                resultat += "🍂";
            }
            else
            {
                // Obtenir l'étape actuelle du cycle de vie
                var etapeActuelle = CycleDeVie.GetEtapeActuelle(
                    PlanteParcelle.Nom, 
                    PlanteParcelle.Age, 
                    PlanteParcelle.Hauteur
                );
                
                // Utiliser l'emoji correspondant à l'étape
                resultat += etapeActuelle.Emoji;
            }
        }
        else
        {
            resultat += " "; // Espace vide par défaut
        }
        
        // Ajouter les installations (toutes, si elles sont présentes)
        if (ASerre)
            resultat += "🪟";
        
        if (ABarriere)
            resultat += "🧱";
        
        if (APareSoleil)
            resultat += "☂️";
        
        return resultat;
    }
    
    // Méthode pour afficher une vue détaillée de la parcelle
    public void AfficherVueDetaillee(int posX, int posY, int largeur = 10, int hauteur = 10)
    {
        try
        {
            // S'assurer que la vue s'adapte à la fenêtre de la console
            int maxX = Math.Min(posX + largeur, Console.WindowWidth - 1);
            int maxY = Math.Min(posY + hauteur + 7, Console.WindowHeight - 2); // +7 pour le tableau de bord
            
            // Ajuster les dimensions si nécessaire
            largeur = Math.Min(largeur, maxX - posX);
            hauteur = Math.Min(hauteur, maxY - posY - 7);
            
            if (largeur < 3 || hauteur < 3)
            {
                // Trop petit pour afficher de manière significative
                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                Console.WriteLine("Erreur: Pas assez d'espace pour afficher la parcelle.");
                return;
            }
            
            // Sauvegarder la position actuelle du curseur
            int originalX = Console.CursorLeft;
            int originalY = Console.CursorTop;
            
            Console.SetCursorPosition(posX, posY - 1);
            Console.WriteLine("PARCELLE SÉLECTIONNÉE:");
            
            // Afficher une vue plus grande de la parcelle
            for (int i = 0; i < hauteur; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                
                for (int j = 0; j < largeur; j++)
                {
                    if (i == 0 || i == hauteur - 1 || j == 0 || j == largeur - 1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.BackgroundColor = Couleur;
                        
                        // Afficher l'emoji de la plante au centre si présente
                        if (i == hauteur/2 && j == largeur/2 && AUnePlante())
                        {
                            Console.Write(ObtenirEmojiPlante());
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
            }
            
            // Réinitialiser la couleur d'arrière-plan
            Console.BackgroundColor = ConsoleColor.Black;
            
            // Afficher les informations de la parcelle directement sous la vue de la parcelle
            int tableauPosY = posY + hauteur;
            AfficherTableauDeBord(posX, tableauPosY, largeur);
            
            // Afficher les suggestions si c'est une plante
            if (AUnePlante() && PlanteParcelle.Sante < 100)
            {
                // Position pour les suggestions (juste sous le tableau de bord)
                int suggestionsPosY = tableauPosY + 8;
                AfficherSuggestions(posX, suggestionsPosY, largeur);
            }
            
            // Restaurer la position originale du curseur
            Console.SetCursorPosition(originalX, originalY);
        }
        catch (Exception ex)
        {
            // En cas d'erreur, l'afficher en bas de l'écran
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine($"Erreur d'affichage: {ex.Message}");
        }
    }
    
    // Méthode pour afficher les suggestions d'actions
    private void AfficherSuggestions(int posX, int posY, int largeur)
    {
        // Obtenir les suggestions
        var suggestions = ObtenirSuggestions();
        
        if (suggestions.Count > 0)
        {
            // Titre des suggestions
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SUGGESTIONS POUR AMÉLIORER SANTÉ:");
            Console.ResetColor();
            
            // Afficher chaque suggestion
            for (int i = 0; i < suggestions.Count; i++)
            {
                Console.SetCursorPosition(posX, posY + 1 + i);
                Console.WriteLine(suggestions[i]);
            }
        }
    }
    
    // Méthode pour afficher le tableau de bord de la parcelle
    private void AfficherTableauDeBord(int posX, int posY, int largeur)
    {
        // Utiliser un format d'affichage plus compact
        Console.SetCursorPosition(posX, posY);
        Console.Write("┌" + new string('─', Math.Max(0, largeur - 2)) + "┐");
        
        if (AUnePlante())
        {
            // Afficher les informations de la plante
            Console.SetCursorPosition(posX, posY + 1);
            Console.Write($"│ Plante: {PlanteParcelle.Nom.PadRight(Math.Max(0, largeur - 11))}│");
            
            Console.SetCursorPosition(posX, posY + 2);
            Console.Write($"│ Santé: {PlanteParcelle.Sante}%{new string(' ', Math.Max(0, largeur - 10 - PlanteParcelle.Sante.ToString().Length))}│");
            
            Console.SetCursorPosition(posX, posY + 3);
            Console.Write($"│ Hauteur: {PlanteParcelle.Hauteur}cm{new string(' ', Math.Max(0, largeur - 12 - PlanteParcelle.Hauteur.ToString().Length))}│");
            
            Console.SetCursorPosition(posX, posY + 4);
            Console.Write($"│ Hydratation: {PlanteParcelle.Hydratation}%{new string(' ', Math.Max(0, largeur - 16 - PlanteParcelle.Hydratation.ToString().Length))}│");
            
            Console.SetCursorPosition(posX, posY + 5);
            Console.Write($"│ État: {PlanteParcelle.EtatPlante.PadRight(Math.Max(0, largeur - 9))}│");
        }
        else
        {
            // Afficher les informations du terrain
            Console.SetCursorPosition(posX, posY + 1);
            Console.Write($"│ Type: {Type.PadRight(Math.Max(0, largeur - 9))}│");
            
            Console.SetCursorPosition(posX, posY + 2);
            Console.Write($"│ Fertilité: {Fertilite}%{new string(' ', Math.Max(0, largeur - 13 - Fertilite.ToString().Length))}│");
            
            Console.SetCursorPosition(posX, posY + 3);
            Console.Write($"│ Humidité: {Humidite}%{new string(' ', Math.Max(0, largeur - 12 - Humidite.ToString().Length))}│");
            
            Console.SetCursorPosition(posX, posY + 4);
            Console.Write($"│ Parcelle vide{new string(' ', Math.Max(0, largeur - 15))}│");
            
            Console.SetCursorPosition(posX, posY + 5);
            Console.Write($"│{new string(' ', Math.Max(0, largeur - 2))}│");
        }
        
        Console.SetCursorPosition(posX, posY + 6);
        Console.Write("└" + new string('─', Math.Max(0, largeur - 2)) + "┘");
        
        // Afficher les coordonnées réelles en dessous
        Console.SetCursorPosition(posX, posY + 7);
        Console.Write($"Coords: [{Row},{Col}]");
        
        // Afficher le menu d'actions
        AfficherMenuActions(posX, posY + 9, largeur);
    }
    
    // Méthode pour afficher le menu d'actions
    private void AfficherMenuActions(int posX, int posY, int largeur)
    {
        Console.SetCursorPosition(posX, posY);
        Console.WriteLine("ACTIONS DISPONIBLES:");
        
        if (AUnePlante())
        {
            // Menu pour une parcelle avec une plante
            Console.SetCursorPosition(posX, posY + 1);
            Console.WriteLine("1. Arroser");
            
            Console.SetCursorPosition(posX, posY + 2);
            Console.WriteLine("2. Désherber");
            
            Console.SetCursorPosition(posX, posY + 3);
            Console.WriteLine("3. Traiter");
            
            int optionIndex = 4;
            
            // Afficher l'option récolter si la plante est mûre
            if (PlanteParcelle.EstComestible && PlanteParcelle.EtatPlante == "Mûre")
            {
                Console.SetCursorPosition(posX, posY + optionIndex);
                Console.WriteLine($"{optionIndex++}. Récolter");
            }
            
            // Afficher les options d'installation ou de suppression pour la serre
            Console.SetCursorPosition(posX, posY + optionIndex);
            if (ASerre)
                Console.WriteLine($"{optionIndex++}. Retirer la serre");
            else
                Console.WriteLine($"{optionIndex++}. Installer une serre");
            
            // Afficher les options d'installation ou de suppression pour la barrière
            Console.SetCursorPosition(posX, posY + optionIndex);
            if (ABarriere)
                Console.WriteLine($"{optionIndex++}. Retirer la barrière");
            else
                Console.WriteLine($"{optionIndex++}. Installer une barrière");
            
            // Afficher les options d'installation ou de suppression pour le pare-soleil
            Console.SetCursorPosition(posX, posY + optionIndex);
            if (APareSoleil)
                Console.WriteLine($"{optionIndex++}. Retirer le pare-soleil");
            else
                Console.WriteLine($"{optionIndex++}. Installer un pare-soleil");
        }
        else
        {
            // Menu pour une parcelle vide
            Console.SetCursorPosition(posX, posY + 1);
            Console.WriteLine("1. Planter une graine");
            
            int optionIndex = 2;
            
            // Afficher les options d'installation ou de suppression pour la serre
            Console.SetCursorPosition(posX, posY + optionIndex);
            if (ASerre)
                Console.WriteLine($"{optionIndex++}. Retirer la serre");
            else
                Console.WriteLine($"{optionIndex++}. Installer une serre");
            
            // Afficher les options d'installation ou de suppression pour la barrière
            Console.SetCursorPosition(posX, posY + optionIndex);
            if (ABarriere)
                Console.WriteLine($"{optionIndex++}. Retirer la barrière");
            else
                Console.WriteLine($"{optionIndex++}. Installer une barrière");
            
            // Afficher les options d'installation ou de suppression pour le pare-soleil
            Console.SetCursorPosition(posX, posY + optionIndex);
            if (APareSoleil)
                Console.WriteLine($"{optionIndex++}. Retirer le pare-soleil");
            else
                Console.WriteLine($"{optionIndex++}. Installer un pare-soleil");
        }
        
        Console.SetCursorPosition(posX, posY + 8);
        Console.WriteLine("0. Retour au tableau principal");
    }
    
    // Méthode pour exécuter une action sur la parcelle
    public bool ExecuterAction(int choixAction)
    {
        if (AUnePlante())
        {
            switch (choixAction)
            {
                case 1: // Arroser
                    Arroser();
                    return true;
                
                case 2: // Désherber
                    Desherber();
                    return true;
                
                case 3: // Traiter
                    Traiter();
                    return true;
                
                case 4: // Option 4 varie selon l'état de la plante
                    if (PlanteParcelle.EstComestible && PlanteParcelle.EtatPlante == "Mûre")
                        return Recolter();
                    else if (ASerre)
                        return RetirerSerre();
                    else
                        return InstallerSerre();
                
                case 5: // Option 5 dépend des options précédentes
                    if (PlanteParcelle.EstComestible && PlanteParcelle.EtatPlante == "Mûre")
                    {
                        if (ASerre)
                            return RetirerSerre();
                        else
                            return InstallerSerre();
                    }
                    else if (ABarriere)
                        return RetirerBarriere();
                    else
                        return InstallerBarriere();
                
                case 6: // Option 6 dépend des options précédentes
                    if (PlanteParcelle.EstComestible && PlanteParcelle.EtatPlante == "Mûre")
                    {
                        if (ABarriere)
                            return RetirerBarriere();
                        else
                            return InstallerBarriere();
                    }
                    else if (APareSoleil)
                        return RetirerPareSoleil();
                    else
                        return InstallerPareSoleil();
                
                case 7: // Option 7 dépend des options précédentes
                    if (PlanteParcelle.EstComestible && PlanteParcelle.EtatPlante == "Mûre")
                    {
                        if (APareSoleil)
                            return RetirerPareSoleil();
                        else
                            return InstallerPareSoleil();
                    }
                    return false;
                
                default:
                    return false;
            }
        }
        else
        {
            // Menu pour une parcelle vide
            switch (choixAction)
            {
                case 1: // Planter une graine
                    return AfficherMenuPlantation();
                
                case 2: // Option pour la serre
                    if (ASerre)
                        return RetirerSerre();
                    else
                        return InstallerSerre();
                
                case 3: // Option pour la barrière
                    if (ABarriere)
                        return RetirerBarriere();
                    else
                        return InstallerBarriere();
                
                case 4: // Option pour le pare-soleil
                    if (APareSoleil)
                        return RetirerPareSoleil();
                    else
                        return InstallerPareSoleil();
                
                default:
                    return false;
            }
        }
    }
    
    // Méthodes d'action sur les plantes
    private void Arroser()
    {
        if (AUnePlante())
        {
            PlanteParcelle.Hydratation = Math.Min(100, PlanteParcelle.Hydratation + 30);
            Humidite = Math.Min(100, Humidite + 20);
            Console.WriteLine("La plante a été arrosée.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
    }
    
    private void Desherber()
    {
        if (AUnePlante())
        {
            PlanteParcelle.AEteDesherbee = true;
            PlanteParcelle.Sante = Math.Min(100, PlanteParcelle.Sante + 10);
            Console.WriteLine("Les mauvaises herbes ont été enlevées.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
    }
    
    private void Traiter()
    {
        if (AUnePlante())
        {
            PlanteParcelle.AEteTraitee = true;
            PlanteParcelle.Sante = Math.Min(100, PlanteParcelle.Sante + 20);
            Console.WriteLine("La plante a été traitée contre les maladies.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
    }
    
    private bool Recolter()
    {
        if (AUnePlante() && PlanteParcelle.EstComestible && PlanteParcelle.EtatPlante == "Mûre")
        {
            Console.WriteLine($"Vous avez récolté {PlanteParcelle.Rendement} {PlanteParcelle.Nom}(s)!");
            PlanteParcelle = null; // Enlever la plante après la récolte
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return true;
        }
        return false;
    }
    
    private bool InstallerSerre()
    {
        if (!ASerre)
        {
            ASerre = true;
            Console.WriteLine("Une serre a été installée sur la parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return true;
        }
        else
        {
            Console.WriteLine("Il y a déjà une serre sur cette parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return false;
        }
    }
    
    private bool RetirerSerre()
    {
        if (ASerre)
        {
            ASerre = false;
            Console.WriteLine("La serre a été retirée de la parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return true;
        }
        else
        {
            Console.WriteLine("Il n'y a pas de serre à retirer sur cette parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return false;
        }
    }
    
    private bool InstallerBarriere()
    {
        if (!ABarriere)
        {
            ABarriere = true;
            Console.WriteLine("Une barrière a été installée sur la parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return true;
        }
        else
        {
            Console.WriteLine("Il y a déjà une barrière sur cette parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return false;
        }
    }
    
    private bool RetirerBarriere()
    {
        if (ABarriere)
        {
            ABarriere = false;
            Console.WriteLine("La barrière a été retirée de la parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return true;
        }
        else
        {
            Console.WriteLine("Il n'y a pas de barrière à retirer sur cette parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return false;
        }
    }
    
    private bool InstallerPareSoleil()
    {
        if (!APareSoleil)
        {
            APareSoleil = true;
            Console.WriteLine("Un pare-soleil a été installé sur la parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return true;
        }
        else
        {
            Console.WriteLine("Il y a déjà un pare-soleil sur cette parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return false;
        }
    }
    
    private bool RetirerPareSoleil()
    {
        if (APareSoleil)
        {
            APareSoleil = false;
            Console.WriteLine("Le pare-soleil a été retiré de la parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return true;
        }
        else
        {
            Console.WriteLine("Il n'y a pas de pare-soleil à retirer sur cette parcelle.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return false;
        }
    }
    
    // Méthode pour afficher le menu de plantation
    private bool AfficherMenuPlantation()
    {
        Console.Clear();
        Console.WriteLine("MENU DE PLANTATION");
        Console.WriteLine("==================");
        Console.WriteLine("Choisissez une plante à planter:");
        
        // Cette liste devrait idéalement être générée dynamiquement à partir de toutes les plantes disponibles
        Console.WriteLine("1. Tomate");
        Console.WriteLine("2. Carotte");
        Console.WriteLine("3. Laitue");
        Console.WriteLine("4. Pomme de terre");
        Console.WriteLine("5. Haricots verts");
        Console.WriteLine("0. Retour");
        
        Console.Write("\nVotre choix: ");
        if (int.TryParse(Console.ReadLine(), out int choix))
        {
            switch (choix)
            {
                case 1:
                    PlanteParcelle = new Tomate(new Plaine());
                    Console.WriteLine("Tomate plantée avec succès!");
                    break;
                case 2:
                    PlanteParcelle = new Carotte(new Plaine());
                    Console.WriteLine("Carotte plantée avec succès!");
                    break;
                case 3:
                    PlanteParcelle = new Laitue(new Plaine());
                    Console.WriteLine("Laitue plantée avec succès!");
                    break;
                case 4:
                    PlanteParcelle = new Pomme_de_terre(new Plaine());
                    Console.WriteLine("Pomme de terre plantée avec succès!");
                    break;
                case 5:
                    PlanteParcelle = new Haricots_verts(new Plaine());
                    Console.WriteLine("Haricots verts plantés avec succès!");
                    break;
                case 0:
                    return false;
                default:
                    Console.WriteLine("Choix invalide.");
                    return false;
            }
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            return true;
        }
        return false;
    }

    // Méthode pour obtenir des suggestions d'actions pour améliorer la santé de la plante
    public List<string> ObtenirSuggestions()
    {
        var suggestions = new List<string>();
        
        if (!AUnePlante())
            return suggestions;
        
        // Vérifier l'hydratation
        if (PlanteParcelle.Hydratation < 40)
        {
            suggestions.Add("1. Arroser (hydratation faible)");
        }
        
        // Vérifier si désherbage nécessaire
        if (!PlanteParcelle.AEteDesherbee && PlanteParcelle.Sante < 80)
        {
            suggestions.Add("2. Désherber (améliore la santé)");
        }
        
        // Vérifier si traitement nécessaire
        if (!PlanteParcelle.AEteTraitee && PlanteParcelle.Sante < 70)
        {
            suggestions.Add("3. Traiter (protège contre les maladies)");
        }
        
        // Vérifier si une serre serait bénéfique
        // En hiver ou avec des températures extrêmes
        if (!ASerre && (Temps.GetSaisonActuelle().ToString() == "Hiver" || 
                        Temps.Temperature.EstEnGel() || 
                        Temps.Temperature.EstEnCanicule()))
        {
            int optionIndex = 4;
            if (PlanteParcelle.EstComestible && PlanteParcelle.EtatPlante == "Mûre")
                optionIndex = 5;
            
            suggestions.Add($"{optionIndex}. Installer une serre (protection climatique)");
        }
        
        // Vérifier si un pare-soleil serait utile en cas de canicule
        if (!APareSoleil && Temps.Temperature.EstEnCanicule())
        {
            int optionIndex = 6;
            if (PlanteParcelle.EstComestible && PlanteParcelle.EtatPlante == "Mûre")
                optionIndex = 7;
            else if (ASerre)
                optionIndex = 6;
            
            suggestions.Add($"{optionIndex}. Installer un pare-soleil (protection canicule)");
        }
        
        // Vérifier si une barrière serait utile pour les plantes fragiles
        if (!ABarriere && PlanteParcelle.Sante < 60)
        {
            int optionIndex = 5;
            if (PlanteParcelle.EstComestible && PlanteParcelle.EtatPlante == "Mûre")
                optionIndex = 6;
            else if (ASerre)
                optionIndex = 5;
            else if (APareSoleil)
                optionIndex = 5;
                
            suggestions.Add($"{optionIndex}. Installer une barrière (réduit les maladies)");
        }
        
        return suggestions;
    }
    
    // Référence à la classe Temps pour accéder aux conditions météo
    private static Temps? tempsReference;
    
    // Setter pour la référence à Temps (à appeler lors de l'initialisation)
    public static void SetTempsReference(Temps temps)
    {
        tempsReference = temps;
    }
    
    // Getter pour accéder aux informations de Temps
    private static Temps Temps
    {
        get
        {
            if (tempsReference == null)
                throw new InvalidOperationException("La référence à Temps n'a pas été initialisée.");
            return tempsReference;
        }
    }
}