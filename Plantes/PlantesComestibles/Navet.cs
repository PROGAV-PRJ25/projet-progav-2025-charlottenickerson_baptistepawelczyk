public class Navet:PlanteComestible {

    public Navet(Terrain terrain) : base(terrain) {
        Nom ="Navet";
        Saisons = new List<string> {"Automne", "Hiver"};
        TerrainPrefere = "Prairie";
        Espacement = 3;
        VitesseCroissance = 2;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {10, 20};
        MaladiesPossibles = new List<string> {"mildiou", "altises"};
        ProbabilitesMaladies = new List<int> {10, 15};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}