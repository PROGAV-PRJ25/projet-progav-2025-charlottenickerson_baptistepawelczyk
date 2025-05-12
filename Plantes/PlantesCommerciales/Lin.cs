public class Lin:PlanteCommerciale {

    public Lin(Terrain terrain) : base(terrain) {
        Nom ="Lin";
        Type = "Annuelle";
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Prairie";
        Espacement = 2;
        VitesseCroissance = 0.167;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {10, 25};
        MaladiesPossibles = new List<string> {"fusariose", "oïdium"};
        ProbabilitesMaladies = new List<int> {15, 25};
        EsperanceDeVie = 0.25;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}