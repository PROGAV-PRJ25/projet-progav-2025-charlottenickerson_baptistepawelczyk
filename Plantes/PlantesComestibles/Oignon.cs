public class Oignon:PlanteComestible {

    public Oignon(Terrain terrain) : base(terrain) {
        Nom ="Oignon";
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Prairie";
        Espacement = 4;
        VitesseCroissance = 3;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {15, 20};
        MaladiesPossibles = new List<string> {"mildiou", "rouille"};
        ProbabilitesMaladies = new List<int> {10, 5};
        EsperanceDeVie = 1;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}