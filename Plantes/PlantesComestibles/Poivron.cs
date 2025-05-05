public class Poivron:PlanteComestible {

    public Poivron(Terrain terrain) : base(terrain) {
        Nom ="Poivron";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Cratère";
        Espacement = 4;
        VitesseCroissance = 3;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 30};
        MaladiesPossibles = new List<string> {"mildiou", "alternaria"};
        ProbabilitesMaladies = new List<int> {15, 10};
        EsperanceDeVie = 1;
        Rendement = 5;
        EtatPlante ="Germination";
    }
}