public class Jasmin:PlanteOrnementale {

    public Jasmin(Terrain terrain) : base(terrain) {
        Nom ="Jasmin";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Forêt";
        Espacement = 2;
        VitesseCroissance = 0.167;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {15, 30};
        MaladiesPossibles = new List<string> {"cochenilles", "oïdium"};
        ProbabilitesMaladies = new List<int> {10, 15};
        EsperanceDeVie = 1;
        Rendement = 15;
        EtatPlante ="Germination";
    }
}