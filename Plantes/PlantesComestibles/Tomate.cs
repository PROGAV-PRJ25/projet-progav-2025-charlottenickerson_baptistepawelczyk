public class Tomate:PlanteComestible {

    public Tomate(Terrain terrain) : base(terrain) {
        Nom ="Tomate";
        MauvaisesHerbes = true;
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Cratère";
        Espacement = 5;
        VitesseCroissance = 2;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 30};
        MaladiesPossibles = new List<string> {"mildiou", "oïdium"};
        ProbabilitesMaladies = new List<int> {30, 15};
        EsperanceDeVie = 1;
        Rendement = 10;
        EtatPlante ="Germination";
    }
}