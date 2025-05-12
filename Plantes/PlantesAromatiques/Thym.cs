public class Thym:PlanteAromatique {

    public Thym(Terrain terrain) : base(terrain) {
        Nom ="Thym";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Désert";
        Espacement = 2;
        VitesseCroissance = 0.167;
        BesoinsEnEau = 0.5;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {15, 30};
        MaladiesPossibles = new List<string> {"oïdium"};
        ProbabilitesMaladies = new List<int> {20};
        EsperanceDeVie = 1.25;
        Rendement = 11;
        EtatPlante ="Germination";
    }
}