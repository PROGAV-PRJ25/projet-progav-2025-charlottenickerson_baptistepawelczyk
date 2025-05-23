using System;
using System.Collections.Generic;

namespace projstartover
{
    class Program
    {
        // Liste des terrains du jeu
        static List<Terrain> terrains = new List<Terrain>();
        
        // Mode urgence du jeu
        static ModeUrgence modeUrgence = null!;
        
        // Instances des classes pour les modes de jeu bonus
        static ModeMagasin modeMagasin = null!;
        static ModeIA modeIA = null!;
        static ModeEcologique modeEcologique = null!;
        static SaveManager saveManager = null!;
        
        // Compteur de semaines
        static int compteurSemaines = 0;
        
        // Chance de déclenchement d'une urgence (en pourcentage)
        static int chanceUrgence = 15; // 15% de chance par semaine
        
        // Pays choisi par le joueur
        static Pays paysChoisi = null!;
        
        static void Main(string[] args)
        {
            // Afficher l'écran de bienvenue
            AfficherEcranBienvenue();
            
            // Choisir un pays
            ChoisirPays();
            
            // Initialisation du jeu avec le pays choisi
            InitialiserJeu();
            
            // Démarrage de la boucle de jeu
            Temps temps = new Temps();
            temps.DemarrerJeu();
            
            // Boucle principale du jeu
            bool jeuEnCours = true;
            while (jeuEnCours)
            {
                // Vérifier s'il y a une urgence active
                if (modeUrgence.EstActif())
                {
                    // Gérer le mode urgence
                    modeUrgence.GererUrgence();
                }
                else
                {
                    // Mode classique: passer à la semaine suivante
                    compteurSemaines++;
                    
                    // Mettre à jour les conditions environnementales pour chaque terrain
                    MettreAJourTerrains(temps);
                    
                    // Faire pousser les plantes sur chaque terrain
                    FairePousserPlantes();
                    
                    // Afficher l'état du jeu
                    AfficherEtatJeu(temps);
                    
                    // Offrir des actions au joueur
                    GererActionsJoueur();
                    
                    // Vérifier si une urgence doit être déclenchée
                    VerifierDeclenchementUrgence();
                }
            }
        }
        
        // Méthode pour choisir un pays
        static void ChoisirPays()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║              CHOISISSEZ VOTRE PAYS                    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            
            List<Pays> paysDispo = Pays.ObtenirPaysDispo();
            
            Console.WriteLine("\nChoisissez un pays pour votre potager:");
            
            for (int i = 0; i < paysDispo.Count; i++)
            {
                Console.WriteLine($"\n{i+1}. {paysDispo[i].Nom}");
                // Afficher l'ASCII art pour le pays
                PaysAsciiArt.AfficherAsciiArt(paysDispo[i].Nom, ConsoleColor.Yellow); 
                Console.WriteLine(paysDispo[i].Description); // Afficher la description sous l'art
            }
            
            Console.WriteLine("\nVotre choix (1-" + paysDispo.Count + "):");
            int choix = LireEntier(1, paysDispo.Count);
            
            paysChoisi = paysDispo[choix - 1];
            
            // Afficher les détails du pays choisi
            Console.Clear();
            Console.WriteLine("Vous avez choisi:");
            Console.WriteLine(paysChoisi);
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }        // Méthode pour initialiser le jeu
        static void InitialiserJeu()
        {
            // Création des terrains en fonction du pays choisi
            if (paysChoisi.Nom == "Égypte" || paysChoisi.Nom == "Aridium")
            {
                // Pays désertiques
                Desert desert = new Desert("Terrain principal");
                terrains.Add(desert);
                
                Plaine plaine = new Plaine("Terrain secondaire");
                terrains.Add(plaine);
            }
            else if (paysChoisi.Nom == "Japon")
            {
                // Pays insulaire
                Riviere riviere = new Riviere("Terrain principal");
                terrains.Add(riviere);
                
                Montagne montagne = new Montagne("Terrain secondaire");
                terrains.Add(montagne);
            }
            else if (paysChoisi.Nom == "Verdania")
            {
                // Pays imaginaire luxuriant
                Jungle jungle = new Jungle("Terrain principal");
                terrains.Add(jungle);
                
                Prairie prairie = new Prairie("Terrain secondaire");
                terrains.Add(prairie);
            }
            else
            {
                // Pays par défaut (France)
                Plaine plaine = new Plaine("Terrain principal");
                terrains.Add(plaine);
                
                Prairie prairie = new Prairie("Terrain secondaire");
                terrains.Add(prairie);
            }
            
            // Création de quelques plantes adaptées au pays
            // Selon le premier terrain créé
            Terrain terrainPrincipal = terrains[0];
            
            if (paysChoisi.PlantesDispo.Contains("Tomate"))
            {
                new Tomate(terrainPrincipal);
            }
            
            if (paysChoisi.PlantesDispo.Contains("Carotte"))
            {
                new Carotte(terrainPrincipal);
            }
            
            if (paysChoisi.PlantesDispo.Contains("Coton"))
            {
                new Coton(terrainPrincipal);
            }
            
            // Initialisation du mode urgence
            modeUrgence = new ModeUrgence(terrains);
            
            // Initialisation des modes bonus
            modeMagasin = new ModeMagasin(terrains);
            modeIA = new ModeIA();
            modeEcologique = new ModeEcologique();
            saveManager = new SaveManager();
            
            // Activer le mode IA par défaut
            modeIA.Activer(true);
        }
        
        // Méthode pour afficher l'écran de bienvenue
        static void AfficherEcranBienvenue()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║               SIMULATEUR DE POTAGER                   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\nCONSEIL VIVEMENT RECOMMANDÉ : JOUEZ AVEC LA FENÊTRE DU TERMINAL EN PLEIN ÉCRAN DANS VISUAL STUDIO CODE");
            Console.WriteLine("\nBienvenue dans votre simulateur de potager!");
            Console.WriteLine("Vous allez pouvoir cultiver différentes plantes et gérer votre jardin.");
            Console.WriteLine("\nCaractéristiques:");
            Console.WriteLine("- Mode classique: cultivez et faites pousser vos plantes semaine après semaine");
            Console.WriteLine("- Mode urgence: protégez votre jardin contre les intempéries et les intrus");
            Console.WriteLine("\nAppuyez sur une touche pour commencer...");
            Console.ReadKey(true);
        }
          // Méthode pour mettre à jour les terrains avec les conditions météorologiques
        static void MettreAJourTerrains(Temps temps)
        {
            if (paysChoisi == null)
            {
                return; // Protection contre un appel prématuré
            }
            
            // Récupérer les informations météorologiques
            Saison.TypeSaison saisonActuelle = temps.GetSaisonActuelle();
            
            // Appliquer les modificateurs du pays choisi
            double precipitation = new Random().Next(5, 50);
            // Ajuster les précipitations selon le pays (en pourcentage)
            precipitation = Math.Max(0, precipitation * (1 + paysChoisi.ModificateurPrecipitations / 100));
            
            double temperature = 15 + new Random().Next(-10, 20);
            // Ajuster la température selon le pays (en degrés)
            temperature += paysChoisi.ModificateurTemperature;
            
            // Conversion de la saison en string
            string nomSaison = "";
            switch (saisonActuelle)
            {
                case Saison.TypeSaison.Printemps: nomSaison = "Printemps"; break;
                case Saison.TypeSaison.Ete: nomSaison = "Été"; break;
                case Saison.TypeSaison.Automne: nomSaison = "Automne"; break;
                case Saison.TypeSaison.Hiver: nomSaison = "Hiver"; break;
            }
            
            // Mettre à jour chaque terrain
            foreach (Terrain terrain in terrains)
            {
                terrain.MettreAJourConditions(precipitation, temperature, nomSaison);
            }
        }
        
        // Méthode pour faire pousser les plantes sur tous les terrains
        static void FairePousserPlantes()
        {
            foreach (Terrain terrain in terrains)
            {
                terrain.FairePousserPlantes();
            }
        }
          // Méthode pour afficher l'état du jeu
        static void AfficherEtatJeu(Temps temps)
        {
            Console.Clear();
            
            // Afficher l'en-tête
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║               SIMULATEUR DE POTAGER                   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            
            // Afficher les informations temporelles
            Console.WriteLine($"Semaine: {compteurSemaines} | Saison: {temps.GetSaisonActuelle()} | Pays: {paysChoisi.Nom}");
            Console.WriteLine($"Argent: {modeMagasin.GetArgent()} pièces");
            Console.WriteLine(new string('-', 50));
            
            // Afficher les terrains
            for (int i = 0; i < terrains.Count; i++)
            {
                Console.WriteLine($"Terrain {i+1}: {terrains[i].GetNom()} ({TypeTerrain.GetNomTypeTerrain(terrains[i])})");
                Console.WriteLine($"Niveau d'eau: {Math.Round(terrains[i].GetNiveauEau())}% | Température: {Math.Round(terrains[i].GetTemperature())}°C");
                
                // Afficher les protections
                string protections = "";
                if (terrains[i].GetEstProtege()) protections += "Protégé contre les intempéries | ";
                if (terrains[i].GetEstClos()) protections += "Clos contre les intrus | ";
                if (protections != "") Console.WriteLine(protections.TrimEnd(' ', '|'));
                
                // Afficher les maladies et parasites
                List<string> maladies = terrains[i].GetMaladiesPresentes();
                if (maladies.Count > 0)
                {
                    Console.WriteLine($"Maladies: {string.Join(", ", maladies)}");
                }
                
                List<string> parasites = terrains[i].GetParasitesPresents();
                if (parasites.Count > 0)
                {
                    Console.WriteLine($"Parasites: {string.Join(", ", parasites)}");
                }
                
                // Afficher les plantes de ce terrain
                List<Plante> plantes = terrains[i].GetPlantes();
                if (plantes.Count > 0)
                {
                    Console.WriteLine("Plantes:");
                    for (int j = 0; j < plantes.Count; j++)
                    {
                        Console.WriteLine($"  {j+1}. {plantes[j].Nom} - État: {plantes[j].GetEtatPlante()} | Santé: {Math.Round(plantes[j].GetNiveauSante())}%");
                        
                        // Afficher les produits disponibles si la plante est mûre
                        if (plantes[j].GetEtatPlante() == "Mûre" && plantes[j].GetNombreProduitsDisponibles() > 0)
                        {
                            Console.WriteLine($"     Produits disponibles: {plantes[j].GetNombreProduitsDisponibles()}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Aucune plante sur ce terrain.");
                }
                
                Console.WriteLine(new string('-', 50));
            }
            
            // Afficher une recommandation d'action urgente si le mode IA est actif
            if (modeIA.EstActif())
            {
                string actionUrgente = modeIA.SuggererActionUrgente(terrains);
                if (!string.IsNullOrEmpty(actionUrgente))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nRecommandation IA: {actionUrgente}");
                    Console.ResetColor();
                }
            }
            
            // Afficher le statut du mode écologique s'il est actif
            if (modeEcologique.EstActif())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nMode écologique: Actif | Bilan carbone: {modeEcologique.GetBilanCarbone()} | Impact: {modeEcologique.GetImpactEnvironnemental()}");
                Console.ResetColor();
            }
        }// Méthode pour gérer les actions du joueur
        static void GererActionsJoueur()
        {
            Console.WriteLine("\nActions disponibles:");
            Console.WriteLine("1. Consulter une plante en détail");
            Console.WriteLine("2. Arroser un terrain");
            Console.WriteLine("3. Traiter les maladies sur un terrain");
            Console.WriteLine("4. Éliminer les parasites sur un terrain");
            Console.WriteLine("5. Protéger un terrain contre les intempéries");
            Console.WriteLine("6. Clore un terrain contre les intrus");
            Console.WriteLine("7. Récolter les produits");
            Console.WriteLine("8. Accéder au magasin");
            Console.WriteLine("9. Consulter les recommandations de l'IA");
            Console.WriteLine("10. Gérer l'écologie");
            Console.WriteLine("11. Sauvegarder/Charger la partie");
            Console.WriteLine("12. Passer à la semaine suivante");
            
            Console.Write("\nChoisissez une action (1-12): ");
            string? choix = Console.ReadLine() ?? "12";
            
            switch (choix)
            {
                case "1":
                    ConsulterPlante();
                    break;
                case "2":
                    ArroserTerrain();
                    break;
                case "3":
                    TraiterMaladies();
                    break;
                case "4":
                    EliminerParasites();
                    break;
                case "5":
                    ProtegerTerrain();
                    break;
                case "6":
                    CloreTerrain();
                    break;
                case "7":
                    RecolterProduits();
                    break;
                case "8":
                    AccederAuMagasin();
                    break;
                case "9":
                    ConsulterIA();
                    break;
                case "10":
                    GererEcologie();
                    break;
                case "11":
                    GererSauvegarde();
                    break;
                case "12":
                    // Passer à la semaine suivante (rien à faire, la boucle principale s'en charge)
                    break;
                default:
                    Console.WriteLine("Choix non reconnu. Appuyez sur une touche pour continuer...");
                    Console.ReadKey(true);
                    break;
            }
        }
        
        // Méthode pour consulter une plante en détail
        static void ConsulterPlante()
        {
            Console.WriteLine("\nChoisissez un terrain (1-" + terrains.Count + "):");
            int indexTerrain = LireEntier(1, terrains.Count) - 1;
            
            List<Plante> plantes = terrains[indexTerrain].GetPlantes();
            
            if (plantes.Count == 0)
            {
                Console.WriteLine("Aucune plante sur ce terrain. Appuyez sur une touche pour continuer...");
                Console.ReadKey(true);
                return;
            }
            
            Console.WriteLine("\nChoisissez une plante (1-" + plantes.Count + "):");
            int indexPlante = LireEntier(1, plantes.Count) - 1;
            
            Console.Clear();
            Console.WriteLine(plantes[indexPlante]);
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }
        
        // Méthode pour arroser un terrain
        static void ArroserTerrain()
        {
            Console.WriteLine("\nChoisissez un terrain à arroser (1-" + terrains.Count + "):");
            int indexTerrain = LireEntier(1, terrains.Count) - 1;
            
            terrains[indexTerrain].Arroser();
            
            Console.WriteLine($"Le terrain {terrains[indexTerrain].GetNom()} a été arrosé.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }
        
        // Méthode pour traiter les maladies sur un terrain
        static void TraiterMaladies()
        {
            Console.WriteLine("\nChoisissez un terrain à traiter (1-" + terrains.Count + "):");
            int indexTerrain = LireEntier(1, terrains.Count) - 1;
            
            List<string> maladies = terrains[indexTerrain].GetMaladiesPresentes();
            
            if (maladies.Count == 0)
            {
                Console.WriteLine("Aucune maladie sur ce terrain. Appuyez sur une touche pour continuer...");
                Console.ReadKey(true);
                return;
            }
            
            Console.WriteLine("\nMaladies présentes:");
            for (int i = 0; i < maladies.Count; i++)
            {
                Console.WriteLine($"{i+1}. {maladies[i]}");
            }
            Console.WriteLine($"{maladies.Count+1}. Traiter toutes les maladies");
            
            Console.WriteLine("\nChoisissez une maladie à traiter (1-" + (maladies.Count+1) + "):");
            int choix = LireEntier(1, maladies.Count+1);
            
            if (choix <= maladies.Count)
            {
                terrains[indexTerrain].TraiterMaladies(maladies[choix-1]);
                Console.WriteLine($"La maladie {maladies[choix-1]} a été traitée.");
            }
            else
            {
                terrains[indexTerrain].TraiterMaladies();
                Console.WriteLine("Toutes les maladies ont été traitées.");
            }
            
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }
        
        // Méthode pour éliminer les parasites sur un terrain
        static void EliminerParasites()
        {
            Console.WriteLine("\nChoisissez un terrain à traiter (1-" + terrains.Count + "):");
            int indexTerrain = LireEntier(1, terrains.Count) - 1;
            
            List<string> parasites = terrains[indexTerrain].GetParasitesPresents();
            
            if (parasites.Count == 0)
            {
                Console.WriteLine("Aucun parasite sur ce terrain. Appuyez sur une touche pour continuer...");
                Console.ReadKey(true);
                return;
            }
            
            Console.WriteLine("\nParasites présents:");
            for (int i = 0; i < parasites.Count; i++)
            {
                Console.WriteLine($"{i+1}. {parasites[i]}");
            }
            Console.WriteLine($"{parasites.Count+1}. Éliminer tous les parasites");
            
            Console.WriteLine("\nChoisissez un parasite à éliminer (1-" + (parasites.Count+1) + "):");
            int choix = LireEntier(1, parasites.Count+1);
            
            if (choix <= parasites.Count)
            {
                terrains[indexTerrain].EliminerParasites(parasites[choix-1]);
                Console.WriteLine($"Le parasite {parasites[choix-1]} a été éliminé.");
            }
            else
            {
                terrains[indexTerrain].EliminerParasites();
                Console.WriteLine("Tous les parasites ont été éliminés.");
            }
            
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }
        
        // Méthode pour protéger un terrain contre les intempéries
        static void ProtegerTerrain()
        {
            Console.WriteLine("\nChoisissez un terrain à protéger (1-" + terrains.Count + "):");
            int indexTerrain = LireEntier(1, terrains.Count) - 1;
            
            terrains[indexTerrain].Proteger();
            
            Console.WriteLine($"Le terrain {terrains[indexTerrain].GetNom()} a été protégé contre les intempéries.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }
        
        // Méthode pour clore un terrain contre les intrus
        static void CloreTerrain()
        {
            Console.WriteLine("\nChoisissez un terrain à clore (1-" + terrains.Count + "):");
            int indexTerrain = LireEntier(1, terrains.Count) - 1;
            
            terrains[indexTerrain].Clore();
            
            Console.WriteLine($"Le terrain {terrains[indexTerrain].GetNom()} a été clos contre les intrus.");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }
        
        // Méthode pour récolter les produits
        static void RecolterProduits()
        {
            Console.WriteLine("\nChoisissez un terrain pour récolter (1-" + terrains.Count + "):");
            int indexTerrain = LireEntier(1, terrains.Count) - 1;
            
            int recolte = terrains[indexTerrain].RecolterProduits();
            
            if (recolte > 0)
            {
                Console.WriteLine($"Vous avez récolté {recolte} produits sur le terrain {terrains[indexTerrain].GetNom()}.");
            }
            else
            {
                Console.WriteLine("Aucun produit à récolter sur ce terrain.");
            }
            
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }
        
        // Méthode pour accéder au magasin
        static void AccederAuMagasin()
        {
            modeMagasin.AfficherMenu();
        }
        
        // Méthode pour consulter les recommandations de l'IA
        static void ConsulterIA()
        {
            modeIA.AfficherRecommandations(terrains);
        }
        
        // Méthode pour gérer l'écologie
        static void GererEcologie()
        {
            if (!modeEcologique.EstActif())
            {
                Console.WriteLine("\nLe mode écologique n'est pas actif. Voulez-vous l'activer? (O/N): ");
                string? reponse = Console.ReadLine()?.ToUpper() ?? "N";
                
                if (reponse == "O")
                {
                    modeEcologique.Activer(true);
                }
            }
            
            if (modeEcologique.EstActif())
            {
                modeEcologique.AfficherMenu();
            }
            else
            {
                Console.WriteLine("\nMode écologique non activé. Appuyez sur une touche pour continuer...");
                Console.ReadKey(true);
            }
        }
        
        // Méthode pour gérer les sauvegardes
        static void GererSauvegarde()
        {
            saveManager.AfficherMenuSauvegarde(terrains, paysChoisi, compteurSemaines, modeMagasin.GetArgent(), modeEcologique);
        }
        
        // Méthode pour vérifier si une urgence doit être déclenchée
        static void VerifierDeclenchementUrgence()
        {
            Random random = new Random();
            
            // Vérifier si une urgence doit être déclenchée
            if (random.Next(100) < chanceUrgence)
            {
                modeUrgence.DeclencherUrgence();
            }
        }
        
        // Méthode utilitaire pour lire un entier dans une plage donnée
        static int LireEntier(int min, int max)
        {
            int valeur;
            bool estValide;
            
            do
            {
                estValide = int.TryParse(Console.ReadLine(), out valeur);
                estValide = estValide && valeur >= min && valeur <= max;
                
                if (!estValide)
                {
                    Console.Write($"Veuillez entrer un nombre entre {min} et {max}: ");
                }
            } while (!estValide);
              return valeur;
        }
    }
}

