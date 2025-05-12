public class Cedre:PlanteCommerciale {

    public Cedre(Terrain terrain) : base(terrain) {
        Nom ="Cèdre";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été", "Automne", "Hiver"};
        TerrainPrefere = "Montagne";
        Espacement = 5;
        VitesseCroissance = 0.333;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {5, 25};
        MaladiesPossibles = new List<string> {"chancre bactérien", "pourriture des racines"};
        ProbabilitesMaladies = new List<int> {10, 20};
        EsperanceDeVie = 5;
        Rendement = 2;
        EtatPlante ="Germination";
    }
}