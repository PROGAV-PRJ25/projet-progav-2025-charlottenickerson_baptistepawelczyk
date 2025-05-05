public class Aubergine:PlanteComestible {

    public Aubergine(Terrain terrain) : base(terrain) {
        Nom ="Aubergine";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Cratère";
        Espacement = 4;
        VitesseCroissance = 3;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 30};
        MaladiesPossibles = new List<string> {"mildiou", "verticilliose"};
        ProbabilitesMaladies = new List<int> {20, 15};
        EsperanceDeVie = 1;
        Rendement = 6;
        EtatPlante ="Germination";
    }
}