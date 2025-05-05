public class Radis:PlanteComestible {

    public Radis(Terrain terrain) : base(terrain) {
        Nom ="Radis";
        Saisons = new List<string> {"Printemps", "Automne"};
        TerrainPrefere = "Prairie";
        Espacement = 2;
        VitesseCroissance = 1;
        BesoinsEnEau = 1;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {10, 15};
        MaladiesPossibles = new List<string> {"mildiou", "altises"};
        ProbabilitesMaladies = new List<int> {10, 5};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}