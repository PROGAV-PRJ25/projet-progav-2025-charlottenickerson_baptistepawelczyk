public class Ble:PlanteComestible {

    public Ble(Terrain terrain) : base(terrain) {
        Nom ="Ble";
        MauvaisesHerbes = true;
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Plaine";
        Espacement = 6;
        VitesseCroissance = 3;
        BesoinsEnEau = 1;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {15, 25};
        MaladiesPossibles = new List<string> {"rouille", "oïdium"};
        ProbabilitesMaladies = new List<int> {20, 15};
        EsperanceDeVie = 1;
        Rendement = 30;
        EtatPlante ="Germination";
    }
}