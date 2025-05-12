public class Laurier:PlanteAromatique {

    public Laurier(Terrain terrain) : base(terrain) {
        Nom ="Laurier";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été", "Automne"};
        TerrainPrefere = "Forêt";
        Espacement = 4;
        VitesseCroissance = 0.267;
        BesoinsEnEau = 2.5;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {15, 25};
        MaladiesPossibles = new List<string> {"cochenilles"};
        ProbabilitesMaladies = new List<int> {30};
        EsperanceDeVie = 2.5;
        Rendement = 25;
        EtatPlante ="Germination";
    }
}