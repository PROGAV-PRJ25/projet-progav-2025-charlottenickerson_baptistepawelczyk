public class Estragon:PlanteAromatique {

    public Estragon(Terrain terrain) : base(terrain) {
        Nom ="Estragon";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Prairie";
        Espacement = 2;
        VitesseCroissance = 0.2;
        BesoinsEnEau = 1.5;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {15, 28};
        MaladiesPossibles = new List<string> {"oïdium"};
        ProbabilitesMaladies = new List<int> {20};
        EsperanceDeVie = 0.75;
        Rendement = 7;
        EtatPlante ="Germination";
    }
}