public class Avocat:PlanteComestible {

    public Avocat(Terrain terrain) : base(terrain) {
        Nom ="Avocat";
        Type = "Vivace";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Cratère";
        Espacement = 8;
        VitesseCroissance = 36;
        BesoinsEnEau = 5;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 30};
        MaladiesPossibles = new List<string> {"mildiou", "phytophthora"};
        ProbabilitesMaladies = new List<int> {20, 10};
        EsperanceDeVie = 20;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}