public abstract class Plante {
    public string Nom;
    protected string Type;
    protected bool EstComestible;
    protected bool MauvaisesHerbes;
    protected List<string> Saisons;
    protected string TerrainPrefere;
    protected double Espacement;
    protected double VitesseCroissance; // En mois
    protected double BesoinsEnEau; // En L/m²/jour
    protected string BesoinsEnLuminosite; // Faible, moyenne ou forte
    protected List<int> TemperaturesPreferees; // En °C
    protected List<string> MaladiesPossibles;
    protected List<int> ProbabilitesMaladies;
    protected double EsperanceDeVie; // En année
    protected int Rendement;
    protected string EtatPlante; // Germination, Croissance, Mûre, Morte
    
    // Nouvelles propriétés pour le suivi de la croissance et de la santé
    protected double TauxCroissance; // Pourcentage de croissance (0-100)
    protected double NiveauSante; // Pourcentage de santé (0-100)
    protected int AgeSemaines; // Âge en semaines
    protected List<string> MaladiesActuelles; // Maladies dont souffre la plante
    protected List<string> ParasitesActuels; // Parasites dont souffre la plante
    protected int NombreProduitsDisponibles; // Nombre de produits disponibles à récolter

    public Plante(Terrain terrain)
    {
        Nom = "Inconnue";
        Type = "Annuelle";
        MauvaisesHerbes = false;
        Saisons = new List<string> {};
        TerrainPrefere = "Inconnu";
        BesoinsEnLuminosite = "Luminosité inconnue";
        TemperaturesPreferees = new List<int> {0, 0};
        MaladiesPossibles = new List<string> {};
        ProbabilitesMaladies = new List<int> {};
        EtatPlante = "Germination";
        
        // Initialisation des nouvelles propriétés
        TauxCroissance = 0;
        NiveauSante = 100;
        AgeSemaines = 0;
        MaladiesActuelles = new List<string>();
        ParasitesActuels = new List<string>();
        NombreProduitsDisponibles = 0;
        
        // Ajouter la plante au terrain
        terrain.AjouterPlante(this);
    }
    
    // Méthode pour faire pousser la plante selon les conditions environnementales
    public virtual void Pousser(ConditionsEnvironnementales conditions)
    {
        // Vérifier si la plante est déjà morte
        if (EstMorte())
        {
            return;
        }
        
        // Augmenter l'âge de la plante
        AgeSemaines++;
        
        // Calculer la compatibilité avec les conditions
        double compatibilite = conditions.EvaluerCompatibilite(
            BesoinsEnEau, 
            TemperaturesPreferees, 
            BesoinsEnLuminosite, 
            TerrainPrefere
        );
        
        // Si les conditions sont insuffisantes (moins de 50%), la santé diminue
        if (compatibilite < 0.5)
        {
            NiveauSante -= (0.5 - compatibilite) * 20; // Perte de santé proportionnelle
            
            // Si la santé tombe à zéro ou moins, la plante meurt
            if (NiveauSante <= 0)
            {
                NiveauSante = 0;
                EtatPlante = "Morte";
            }
        }
        else
        {
            // Sinon, la plante se développe en fonction de la compatibilité
            // Plus la compatibilité est élevée, plus la croissance est rapide
            double croissanceHebdomadaire = (VitesseCroissance > 0) 
                ? (4.0 / (VitesseCroissance * 4)) * compatibilite // VitesseCroissance en mois, convertie en semaines
                : 0.1 * compatibilite; // Valeur par défaut si VitesseCroissance non spécifiée
                
            TauxCroissance += croissanceHebdomadaire * 10; // Multiplie par 10 pour obtenir un pourcentage
            
            // Limiter la croissance à 100%
            if (TauxCroissance > 100)
            {
                TauxCroissance = 100;
            }
            
            // Mettre à jour l'état de la plante en fonction de son taux de croissance
            if (TauxCroissance < 30)
            {
                EtatPlante = "Germination";
            }
            else if (TauxCroissance < 80)
            {
                EtatPlante = "Croissance";
            }
            else
            {
                EtatPlante = "Mûre";
                
                // Générer des produits si la plante est mûre
                if (NombreProduitsDisponibles < Rendement && new Random().Next(100) < 30) // 30% de chance par semaine
                {
                    NombreProduitsDisponibles++;
                }
            }
        }
        
        // Vérifier si la plante a atteint son espérance de vie (en semaines)
        double esperanceVieSemaines = EsperanceDeVie * 52; // Conversion d'années en semaines
        if (AgeSemaines >= esperanceVieSemaines)
        {
            EtatPlante = "Morte";
            NiveauSante = 0;
        }
        
        // Gérer les maladies et parasites
        GererMaladiesEtParasites(conditions);
    }
    
    // Méthode pour gérer les maladies et parasites
    private void GererMaladiesEtParasites(ConditionsEnvironnementales conditions)
    {
        Random random = new Random();
        
        // Risque de nouvelles maladies
        for (int i = 0; i < MaladiesPossibles.Count; i++)
        {
            if (!MaladiesActuelles.Contains(MaladiesPossibles[i]) && 
                random.Next(100) < ProbabilitesMaladies[i])
            {
                MaladiesActuelles.Add(MaladiesPossibles[i]);
            }
        }
        
        // Ajout des maladies de l'environnement
        foreach (string maladie in conditions.MaladiesActives)
        {
            if (!MaladiesActuelles.Contains(maladie) && random.Next(100) < 30) // 30% de chance de contamination
            {
                MaladiesActuelles.Add(maladie);
            }
        }
        
        // Ajout des parasites de l'environnement
        foreach (string parasite in conditions.ParasitesActifs)
        {
            if (!ParasitesActuels.Contains(parasite) && random.Next(100) < 40) // 40% de chance d'infestation
            {
                ParasitesActuels.Add(parasite);
            }
        }
        
        // Effet des maladies et parasites sur la santé
        if (MaladiesActuelles.Count > 0 || ParasitesActuels.Count > 0)
        {
            NiveauSante -= (MaladiesActuelles.Count * 5 + ParasitesActuels.Count * 3);
            
            // Si la santé tombe à zéro ou moins, la plante meurt
            if (NiveauSante <= 0)
            {
                NiveauSante = 0;
                EtatPlante = "Morte";
            }
        }
    }
    
    // Méthode pour vérifier si la plante est morte
    public bool EstMorte()
    {
        return EtatPlante == "Morte" || NiveauSante <= 0;
    }
    
    // Méthode pour récolter des produits
    public int Recolter()
    {
        int recolte = NombreProduitsDisponibles;
        NombreProduitsDisponibles = 0;
        return recolte;
    }
    
    // Méthode pour traiter une maladie
    public void TraiterMaladie(string maladie)
    {
        if (MaladiesActuelles.Contains(maladie))
        {
            MaladiesActuelles.Remove(maladie);
        }
    }
    
    // Méthode pour éliminer un parasite
    public void EliminerParasite(string parasite)
    {
        if (ParasitesActuels.Contains(parasite))
        {
            ParasitesActuels.Remove(parasite);
        }
    }
    
    // Méthode pour arroser la plante
    public void Arroser()
    {
        // Augmente légèrement la santé, sauf si la plante est morte
        if (!EstMorte())
        {
            NiveauSante = Math.Min(100, NiveauSante + 5);
        }
    }
    
    // Méthode pour obtenir le niveau de santé actuel
    public double GetNiveauSante()
    {
        return NiveauSante;
    }
    
    // Méthode pour obtenir le taux de croissance actuel
    public double GetTauxCroissance()
    {
        return TauxCroissance;
    }
    
    // Méthode pour obtenir l'état actuel de la plante
    public string GetEtatPlante()
    {
        return EtatPlante;
    }
    
    // Méthode pour obtenir le nombre de produits disponibles
    public int GetNombreProduitsDisponibles()
    {
        return NombreProduitsDisponibles;
    }
    
    // Méthode pour obtenir les maladies actuelles
    public List<string> GetMaladiesActuelles()
    {
        return new List<string>(MaladiesActuelles);
    }
    
    // Méthode pour obtenir les parasites actuels
    public List<string> GetParasitesActuels()
    {
        return new List<string>(ParasitesActuels);
    }
    
    // Méthode pour obtenir l'âge en semaines
    public int GetAgeSemaines()
    {
        return AgeSemaines;
    }
    
    public override string ToString()
    {
        string message = $"{Nom.ToUpper()} a pour saison(s) préférée(s) : ";
        foreach (string saison in Saisons)
            message += $"{saison} • ";
        message += $"\nType : {Type}";
        if (MauvaisesHerbes)
            message += " | Propage des mauvaises herbes";
        if (EstComestible)
            message += " | Est comestible";
        message += $"\nTerrain préféré : {TerrainPrefere} | Espacement entre deux plantes : {Espacement} cases";
        message += $"\nVitesse de croissance : ";
        if (VitesseCroissance<1)
            message += $"{Math.Round(VitesseCroissance*30)} jours ";
        else
            message += $"{VitesseCroissance} mois ";
        message += $"| Besoins en eau : {BesoinsEnEau} L/m²/jour";
        message += $"\nA besoin d'une {BesoinsEnLuminosite} | Températures préférées : {TemperaturesPreferees[0]}-{TemperaturesPreferees[1]}°C";
        message += $"\nMaladies possibles : ";
        int taille = MaladiesPossibles.Count;
        for (int i=0; i<taille; i++)
            message += $"{MaladiesPossibles[i]} (probabilité {ProbabilitesMaladies[i]}%) ";
        message += $"\nEspérance de vie : ";
        if (EsperanceDeVie<1)
            message += $"{EsperanceDeVie*12} mois ";
        else
            message += $"{EsperanceDeVie} an ";
        message += $"| Rendement : {Rendement} unité(s)";
        
        // Ajout des informations sur l'état actuel
        message += $"\n\nÉTAT ACTUEL:";
        message += $"\nÉtat: {EtatPlante} | Croissance: {Math.Round(TauxCroissance)}% | Santé: {Math.Round(NiveauSante)}%";
        message += $"\nÂge: {AgeSemaines} semaines";
        
        // Affichage des maladies actuelles
        if (MaladiesActuelles.Count > 0)
        {
            message += "\nMaladies actuelles: ";
            foreach (string maladie in MaladiesActuelles)
            {
                message += $"{maladie} • ";
            }
        }
        
        // Affichage des parasites actuels
        if (ParasitesActuels.Count > 0)
        {
            message += "\nParasites actuels: ";
            foreach (string parasite in ParasitesActuels)
            {
                message += $"{parasite} • ";
            }
        }
        
        // Affichage des produits disponibles
        if (NombreProduitsDisponibles > 0)
        {
            message += $"\nProduits disponibles à récolter: {NombreProduitsDisponibles}";
        }
        
        return message;
    }
}