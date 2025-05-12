public class Coton:PlanteCommerciale {

    public Coton(Terrain terrain) : base(terrain) {
        Nom ="Coton";
        Type = "Annuelle";
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Prairie";
        Espacement = 3;
        VitesseCroissance = 0.233;
        BesoinsEnEau = 3.5;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 35};
        MaladiesPossibles = new List<string> {"verticilliose", "anthracnose"};
        ProbabilitesMaladies = new List<int> {20, 30};
        EsperanceDeVie = 0.25;
        Rendement = 5;
        EtatPlante ="Germination";
    }
}