public class Ciboulette:PlanteAromatique {

    public Ciboulette(Terrain terrain) : base(terrain) {
        Nom ="Ciboulette";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été", "Automne"};
        TerrainPrefere = "Prairie";
        Espacement = 1;
        VitesseCroissance = 0.133;
        BesoinsEnEau = 1.5;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {10, 25};
        MaladiesPossibles = new List<string> {"mildiou", "pourriture des racines"};
        ProbabilitesMaladies = new List<int> {40, 30};
        EsperanceDeVie = 0.75;
        Rendement = 7;
        EtatPlante ="Germination";
    }
}