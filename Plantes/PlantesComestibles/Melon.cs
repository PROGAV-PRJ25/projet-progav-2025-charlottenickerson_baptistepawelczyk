public class Melon:PlanteComestible {

    public Melon(Terrain terrain) : base(terrain) {
        Nom ="Melon";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Cratère";
        Espacement = 5;
        VitesseCroissance = 3;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 30};
        MaladiesPossibles = new List<string> {"mildiou", "fusariose"};
        ProbabilitesMaladies = new List<int> {20, 15};
        EsperanceDeVie = 1;
        Rendement = 2;
        EtatPlante ="Germination";
    }
}