public class Betterave:PlanteComestible {

    public Betterave(Terrain terrain) : base(terrain) {
        Nom ="Betterave";
        Saisons = new List<string> {"Printemps", "Automne"};
        TerrainPrefere = "Prairie";
        Espacement = 3;
        VitesseCroissance = 2;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {10, 20};
        MaladiesPossibles = new List<string> {"mildiou", "cercosporiose"};
        ProbabilitesMaladies = new List<int> {15, 10};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}