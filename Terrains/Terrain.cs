public abstract class Terrain
{
    protected string Nom;
    protected double SurfaceTotale;
    protected string Type; // Plaine, Desert, Cratere, Jungle, Montagne, Marais, Foret, Prairie, Riviere
    protected List<Plante> Plantes;
    protected double QualiteSol;
    
    // Nouvelles propriétés
    protected double NiveauEau;
    protected string Luminosite; // Faible, Moyenne, Forte
    protected double Temperature;
    protected List<string> MaladiesPresentes;
    protected List<string> ParasitesPresents;
    protected double CapaciteMaxPlantes;
    protected bool EstProtege; // Protection contre les intempéries
    protected bool EstClos; // Protection contre les intrus

    public Terrain(string nom)
    {
        Nom = nom;
        SurfaceTotale = 9;
        Type = "Inconnu";
        Plantes = new List<Plante> {};
        QualiteSol = 0;
        
        // Initialisation des nouvelles propriétés
        NiveauEau = 50; // Niveau d'eau moyen au départ
        Luminosite = "Moyenne"; // Luminosité moyenne au départ
        Temperature = 20; // 20°C au départ
        MaladiesPresentes = new List<string>();
        ParasitesPresents = new List<string>();
        CapaciteMaxPlantes = Math.Floor(SurfaceTotale / 1.5); // Une plante tous les 1.5m²
        EstProtege = false;
        EstClos = false;
    }

    // Méthode pour mettre à jour les conditions du terrain
    public void MettreAJourConditions(double precipitation, double temperature, string saison)
    {
        // Mise à jour du niveau d'eau en fonction des précipitations
        NiveauEau = Math.Min(100, NiveauEau + (precipitation / 5));
        
        // Perte naturelle d'eau par évaporation
        NiveauEau = Math.Max(0, NiveauEau - 5);
        
        // Mise à jour de la température
        Temperature = temperature;
        
        // Ajustement de la luminosité en fonction de la saison
        if (saison == "Été")
        {
            Luminosite = "Forte";
        }
        else if (saison == "Hiver")
        {
            Luminosite = "Faible";
        }
        else
        {
            Luminosite = "Moyenne";
        }
        
        // Générer des maladies et parasites aléatoirement
        Random random = new Random();
        
        // Chance de développer une maladie (plus élevée par temps humide)
        if (NiveauEau > 70 && random.Next(100) < 20)
        {
            string[] maladiesPossibles = { "Mildiou", "Pourriture", "Rouille", "Oïdium", "Fusariose" };
            string nouvelleMaladie = maladiesPossibles[random.Next(maladiesPossibles.Length)];
            
            if (!MaladiesPresentes.Contains(nouvelleMaladie))
            {
                MaladiesPresentes.Add(nouvelleMaladie);
            }
        }
        
        // Chance de développer une infestation de parasites (plus élevée par temps chaud)
        if (Temperature > 25 && random.Next(100) < 15)
        {
            string[] parasitesPossibles = { "Pucerons", "Limaces", "Acariens", "Doryphores", "Aleurodes" };
            string nouveauParasite = parasitesPossibles[random.Next(parasitesPossibles.Length)];
            
            if (!ParasitesPresents.Contains(nouveauParasite))
            {
                ParasitesPresents.Add(nouveauParasite);
            }
        }
        
        // Effets protecteurs
        if (EstProtege)
        {
            // Un terrain protégé a moins de chances d'être affecté par les maladies
            if (MaladiesPresentes.Count > 0 && random.Next(100) < 30)
            {
                MaladiesPresentes.RemoveAt(0);
            }
        }
        
        if (EstClos)
        {
            // Un terrain clos a moins de chances d'être affecté par les parasites
            if (ParasitesPresents.Count > 0 && random.Next(100) < 30)
            {
                ParasitesPresents.RemoveAt(0);
            }
        }
    }
    
    // Méthode pour faire pousser toutes les plantes du terrain
    public void FairePousserPlantes()
    {
        // Créer l'objet conditions pour ce terrain
        ConditionsEnvironnementales conditions = new ConditionsEnvironnementales(
            NiveauEau,
            Temperature,
            Luminosite,
            Type
        );
        
        // Ajouter les maladies et parasites aux conditions
        foreach (string maladie in MaladiesPresentes)
        {
            conditions.AjouterMaladie(maladie);
        }
        
        foreach (string parasite in ParasitesPresents)
        {
            conditions.AjouterParasite(parasite);
        }
        
        // Faire pousser chaque plante avec ces conditions
        foreach (Plante plante in Plantes.ToList()) // Utilisation de ToList() pour éviter les erreurs de modification pendant l'itération
        {
            plante.Pousser(conditions);
            
            // Supprimer les plantes mortes après un certain temps
            if (plante.EstMorte())
            {
                // 20% de chance de supprimer une plante morte à chaque tour
                if (new Random().Next(100) < 20)
                {
                    Plantes.Remove(plante);
                }
            }
        }
    }
    
    // Méthode pour arroser le terrain
    public void Arroser()
    {
        NiveauEau = Math.Min(100, NiveauEau + 30);
        
        // Arroser toutes les plantes
        foreach (Plante plante in Plantes)
        {
            plante.Arroser();
        }
    }
    
    // Méthode pour traiter les maladies
    public void TraiterMaladies(string? maladie = null)
    {
        if (maladie != null)
        {
            // Traiter une maladie spécifique
            MaladiesPresentes.Remove(maladie);
            
            // Traiter cette maladie sur toutes les plantes
            foreach (Plante plante in Plantes)
            {
                plante.TraiterMaladie(maladie);
            }
        }
        else
        {
            // Traiter toutes les maladies
            MaladiesPresentes.Clear();
            
            // Traiter toutes les maladies sur toutes les plantes
            foreach (Plante plante in Plantes)
            {
                foreach (string m in MaladiesPresentes.ToList())
                {
                    plante.TraiterMaladie(m);
                }
            }
        }
    }
    
    // Méthode pour éliminer les parasites
    public void EliminerParasites(string? parasite = null)
    {
        if (parasite != null)
        {
            // Éliminer un parasite spécifique
            ParasitesPresents.Remove(parasite);
            
            // Éliminer ce parasite sur toutes les plantes
            foreach (Plante plante in Plantes)
            {
                plante.EliminerParasite(parasite);
            }
        }
        else
        {
            // Éliminer tous les parasites
            ParasitesPresents.Clear();
            
            // Éliminer tous les parasites sur toutes les plantes
            foreach (Plante plante in Plantes)
            {
                foreach (string p in ParasitesPresents.ToList())
                {
                    plante.EliminerParasite(p);
                }
            }
        }
    }
    
    // Méthode pour protéger le terrain contre les intempéries
    public void Proteger()
    {
        EstProtege = true;
    }
    
    // Méthode pour clore le terrain contre les intrus
    public void Clore()
    {
        EstClos = true;
    }
    
    // Méthode pour vérifier si on peut planter sur ce terrain
    public bool PeutPlanter(Plante plante)
    {
        // Vérifier si le terrain a atteint sa capacité maximale
        if (Plantes.Count >= CapaciteMaxPlantes)
        {
            return false;
        }
        
        // D'autres règles peuvent être ajoutées ici
        
        return true;
    }
    
    // Méthode pour récolter toutes les plantes et retourner le nombre de produits récoltés
    public int RecolterProduits()
    {
        int totalRecolte = 0;
        
        foreach (Plante plante in Plantes)
        {
            totalRecolte += plante.Recolter();
        }
        
        return totalRecolte;
    }
    
    // Méthode pour obtenir les conditions environnementales actuelles
    public ConditionsEnvironnementales GetConditionsActuelles()
    {
        return new ConditionsEnvironnementales(NiveauEau, Temperature, Luminosite, Type);
    }
    
    // Getters
    public string GetNom() { return Nom; }
    public string GetTerrainType() { return Type; }
    public List<Plante> GetPlantes() { return Plantes; }
    public double GetQualiteSol() { return QualiteSol; }
    public double GetNiveauEau() { return NiveauEau; }
    public string GetLuminosite() { return Luminosite; }
    public double GetTemperature() { return Temperature; }
    public bool GetEstProtege() { return EstProtege; }
    public bool GetEstClos() { return EstClos; }
    public List<string> GetMaladiesPresentes() { return MaladiesPresentes; }
    public List<string> GetParasitesPresents() { return ParasitesPresents; }

    public override string ToString()
    {
        string message = $"{Nom.ToUpper()} est de type {Type}.";
        message += $"\nSa surface totale est de {SurfaceTotale}m² et la qualité de son sol est {QualiteSol}.";
        message += $"\nNiveau d'eau actuel: {Math.Round(NiveauEau)}% | Luminosité: {Luminosite} | Température: {Math.Round(Temperature)}°C";
        
        // Afficher les protections
        message += "\nProtections: ";
        if (EstProtege) message += "Protection contre intempéries • ";
        if (EstClos) message += "Clôture contre intrus • ";
        if (!EstProtege && !EstClos) message += "Aucune • ";
        
        // Afficher les maladies présentes
        if (MaladiesPresentes.Count > 0)
        {
            message += "\nMaladies présentes: ";
            foreach (string maladie in MaladiesPresentes)
            {
                message += $"{maladie} • ";
            }
        }
        
        // Afficher les parasites présents
        if (ParasitesPresents.Count > 0)
        {
            message += "\nParasites présents: ";
            foreach (string parasite in ParasitesPresents)
            {
                message += $"{parasite} • ";
            }
        }
        
        message += $"\nLes plantes sur le terrain sont les suivantes :\n";
        foreach (Plante plante in Plantes)
            message += $"{plante.Nom} • ";
        
        return message;
    }

    public void AjouterPlante(Plante plante)
    {
        Plantes.Add(plante);
    }
}