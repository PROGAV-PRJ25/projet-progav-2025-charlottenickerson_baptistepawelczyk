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
        EtatPlante = "Inconnu";
        terrain.AjouterPlante(this);
    }

    // void Pousser(ConditionsEnvironnementales conditions);
    // bool EstMorte();

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
        return message;
    }
}