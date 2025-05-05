public class Pasteque:PlanteComestible {

    public Pasteque(Terrain terrain) : base(terrain) {
        Nom ="Pastèque";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Cratère";
        Espacement = 6;
        VitesseCroissance = 3;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 30};
        MaladiesPossibles = new List<string> {"mildiou", "oïdium"};
        ProbabilitesMaladies = new List<int> {15, 10};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}