public class Pois:PlanteComestible {

    public Pois(Terrain terrain) : base(terrain) {
        Nom ="Pois";
        Saisons = new List<string> {"Printemps"};
        TerrainPrefere = "Forêt";
        Espacement = 4;
        VitesseCroissance = 2;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {10, 20};
        MaladiesPossibles = new List<string> {"rouille", "oïdium"};
        ProbabilitesMaladies = new List<int> {20, 10};
        EsperanceDeVie = 1;
        Rendement = 5;
        EtatPlante ="Germination";
    }
}