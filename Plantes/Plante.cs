public abstract class Plante {
    protected string ?Nom;
    protected string Type;
    protected bool EstComestible;
    protected bool MauvaisesHerbes;
    protected List<string> ?Saisons;
    protected string ?TerrainPrefere;
    protected double Espacement;
    protected double VitesseCroissance;
    protected double BesoinsEnEau;
    protected double BesoinsEnLuminosite;
    protected List<int> ?TemperaturesPreferees;
    protected List<string> ?MaladiesPossibles;
    protected List<int> ?ProbabilitesMaladies;
    protected int EsperanceDeVie;
    protected int Rendement;
    protected string ?EtatPlante; // Germination, Croissance, Mûre, Morte

    public Plante(Terrain terrain)
    {
        Type = "Annuelle";
        MauvaisesHerbes = false;
    }

    //void Pousser(ConditionsEnvironnementales conditions);
    //bool EstMorte();

    public override string ToString()
    {
        string message = $"{Nom.ToUpper()} a pour saison(s) préférée(s) : ";
        foreach (string saison in Saisons)
            message += $"{saison} ";
        message += $"\nTerrain préféré : {TerrainPrefere} | Espacement entre deux plantes : {Espacement}";
        message += $"\nVitesse de croissance : {VitesseCroissance} | Besoins en eau : {BesoinsEnEau} litres par mois";
        message += $"\nBesoins en luminosité : {BesoinsEnLuminosite} | Températures préférées : {TemperaturesPreferees[0]}-{TemperaturesPreferees[1]}°C";
        message += $"\nMaladies possibles : ";
        int taille = MaladiesPossibles.Count;
        for (int i=0; i<taille; i++)
            message += $"{MaladiesPossibles[i]} (probabilité {ProbabilitesMaladies[i]}%) ";
        message += $"\nEspérance de vie : {EsperanceDeVie} an | Rendement : {Rendement} plant";
        return message;
    }
}