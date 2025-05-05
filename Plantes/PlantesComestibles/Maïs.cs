public class Mais:PlanteComestible {

    public Mais(Terrain terrain) : base(terrain) {
        Nom ="Maïs";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Plaine";
        Espacement = 6;
        VitesseCroissance = 3;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 30};
        MaladiesPossibles = new List<string> {"mildiou", "pourriture du maïs"};
        ProbabilitesMaladies = new List<int> {20, 10};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}