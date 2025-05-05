public class Courgette:PlanteComestible {

    public Courgette(Terrain terrain) : base(terrain) {
        Nom ="Courgette";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Cratère";
        Espacement = 6;
        VitesseCroissance = 2;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 30};
        MaladiesPossibles = new List<string> {"mildiou", "oïdium"};
        ProbabilitesMaladies = new List<int> {20, 10};
        EsperanceDeVie = 1;
        Rendement = 5;
        EtatPlante ="Germination";
    }
}